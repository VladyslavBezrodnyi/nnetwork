using NNetwork.Domain.Enums;
using System.Collections.Generic;

namespace NNetwork.API.Dtos
{
    public class LayerDto
    {
        public LayerType LayerType { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public List<int> Transitions { get; set; }
    }
}
