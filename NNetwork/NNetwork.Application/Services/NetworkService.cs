using NNetwork.Domain.Models;
using NNetwork.Domain.Repositories;
using NNetwork.Domain.Services;
using NNetwork.KerasApplication.Datasets;
using NNetwork.KerasApplication.Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNetwork.Application.Services
{
    public class NetworkService: INetworkService
    {
        private readonly INetworkRepository _networkRepository;
        private readonly INetworkStore _networkStore;


        public NetworkService(INetworkRepository networkRepository,
            INetworkStore networkStore)
        {
            _networkRepository = networkRepository;
            _networkStore = networkStore;
        }

        public async Task<NetworkCreationResult> CreateNetworkAsync(NetworkInitializationModel init)
        {
            var network = new Network();
            network.InitializeNetwork(init);
            var plotImageByteArray = network.GetPlot();
            var plotImageBase64 = Convert.ToBase64String(plotImageByteArray);
            Task.Run(() =>
            {
                var networkContainer = new NetworkContainer()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = init.Title,
                    Description = init.Description,
                    Network = network,
                    PlotImage = Convert.ToBase64String(network.GetPlot())
                };
                //Cifar10_CNN.Run(network);
                _networkStore.Networks.TryAdd(networkContainer.Id, networkContainer);
            });
            var result = new NetworkCreationResult()
            {
                PlotImage = plotImageBase64
            };
            return result;
        }

        public async Task<IEnumerable<NetworkTitleInfo>> GetAll()
        {
            var networks = _networkStore.Networks.Select(keyPair =>
            {
                return new NetworkTitleInfo()
                {
                    Id = keyPair.Key,
                    Title = keyPair.Value.Title,
                    Description = keyPair.Value.Description
                };
            }).ToList();
            return networks;
        }

        public async Task<NetworkAllInfo> GetNetworkAllInfo(Guid networkId)
        {
            var networkContainer = _networkStore.Networks[networkId.ToString()];
            var network = new NetworkAllInfo()
            {
                Id = networkId.ToString(),
                Title = networkContainer.Title,
                Description = networkContainer.Description,
                PlotImage = networkContainer.PlotImage
            };
            return network;
        }

        public async Task<PredictionResult> PredictImage(Guid networkId, string path)
        {
            var network = _networkStore.Networks[networkId.ToString()];
            network.Network.InitializeNetwork(network.ModelConfiguration, network.ModelWeights);
            var (probabilities, index) = Cifar10_CNN.Predict(network.Network, path);
            var predictResult = new PredictionResult()
            {
                Probabilities = probabilities,
                Index = index
            };
            return predictResult;
        }
    }
}