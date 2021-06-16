using NNetwork.Domain.Models;
using NNetwork.KerasApplication.Networks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NNetwork.Domain.Services
{
    public interface INetworkService
    {

        Task<IEnumerable<NetworkTitleInfo>> GetAll();

        Task<NetworkAllInfo> GetNetworkAllInfo(Guid networkId);

        Task<NetworkCreationResult> CreateNetworkAsync(NetworkInitializationModel init);

        Task<PredictionResult>  PredictImage(Guid networkId, string path);
    }
}