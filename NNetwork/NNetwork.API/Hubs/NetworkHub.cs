using Microsoft.AspNetCore.SignalR;
using NNetwork.Domain.Services;
using System.Threading.Tasks;

namespace NNetwork.API.Hubs
{
    public class NetworkHub: Hub
    {
        private readonly INetworkService _networkService;

        public NetworkHub(INetworkService networkService)
        {
            _networkService = networkService;
        }
    }
}