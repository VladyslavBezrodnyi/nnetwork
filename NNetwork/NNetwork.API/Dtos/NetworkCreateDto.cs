using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNetwork.API.Dtos
{
    public class NetworkCreateDto: NetworkBaseDto
    {
        public LayerDto[] Layers { get; set; }
    }
}
