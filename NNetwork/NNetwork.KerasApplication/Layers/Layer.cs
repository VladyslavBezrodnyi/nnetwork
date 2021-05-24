using NNetwork.KerasApplication.Enums;
using System.Collections.Generic;

namespace NNetwork.KerasApplication.Layers
{
    public class Layer
    {
        public long Id { get; set; }
        public LayerType LayerType { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public List<int> Transitions { get; set; }
    }
}
