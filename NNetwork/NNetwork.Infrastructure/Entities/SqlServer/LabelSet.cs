using System;
using System.Collections.Generic;
using System.Text;

namespace NNetwork.Infrastructure.Entities.SqlServer
{
    public class LabelSet
    {
        public long LabelId { get; set; }
        public Label Label { get; set; }
        public long NetworkId { get; set; }
        public Network Network { get; set; }
    }
}
