using System;
using System.Collections.Generic;
using System.Text;

namespace NNetwork.Infrastructure.Entities.SqlServer
{
    public class Network
    {
        public long NetworkId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public IEnumerable<LabelSet> LabelSets { get; set; }
        public IEnumerable<NetworkVersion> NetworkVersions { get; set; }
    }
}
