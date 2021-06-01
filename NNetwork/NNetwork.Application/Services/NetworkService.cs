using NNetwork.Domain.Repositories;
using NNetwork.Domain.Services;

namespace NNetwork.Application.Services
{
    public class NetworkService : INetworkService
    {
        private readonly INetworkRepository _networkRepository;

        public NetworkService(INetworkRepository networkRepository)
        {
            _networkRepository = networkRepository;
        }



    }
}