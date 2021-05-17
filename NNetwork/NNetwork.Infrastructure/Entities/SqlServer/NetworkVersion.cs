using NNetwork.Infrastructure.Entities.Mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNetwork.Infrastructure.Entities.SqlServer
{
    public class NetworkVersion
    {
        public long NetworkVersionId { get; set; }
        public long NetworkId { get; set; }
        public string Description { get; set; }
        public long Version { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid NetworkConfigurationId { get; set; }
        public NetworkConfiguration NetworkConfiguration { get; set; }
        public Network Network { get; set; }
    }
}
