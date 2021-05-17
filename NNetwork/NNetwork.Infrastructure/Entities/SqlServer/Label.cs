using System;
using System.Collections.Generic;
using System.Text;

namespace NNetwork.Infrastructure.Entities.SqlServer
{
    public class Label
    {
        public long LabelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<LabelSet> LabelSets { get; set; }
        public IEnumerable<LabeledImage> LabeledImages { get; set; }
    }
}
