using System;
using System.Collections.Generic;
using System.Text;

namespace NNetwork.Infrastructure.Entities.Mongo
{
    public class NetworkConfiguration
    {
        public Guid ConfigurationId { get; set; }
        public string ModelConfiguration { get; set; }
        public string ModelWeights { get; set; }
    }
}
