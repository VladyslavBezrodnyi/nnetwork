using NNetwork.Domain.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace NNetwork.Domain.Services
{
    public interface INetworkStore
    {
        ConcurrentDictionary<string, NetworkContainer> Networks { get; set; }
    }
}
