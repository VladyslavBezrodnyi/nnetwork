using NNetwork.Domain.Enums;
using System.Collections.Generic;

namespace NNetwork.API.Dtos
{
    public class LayerDto
    {
        public long Id { get; set; }
        public LayerType LayerType { get; set; }
        public ParametersDto Parameters { get; set; }
        public List<int> Transitions { get; set; }
    }
}
