using NNetwork.Domain.Models;
using NNetwork.Domain.Services;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NNetwork.Application.Services
{
    public class NetworkStore : INetworkStore
    {
        public ConcurrentDictionary<string, NetworkContainer> Networks { get; set; } = new ConcurrentDictionary<string, NetworkContainer>();
    }
}
