using NNetwork.Infrastructure.Entities.Mongo;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNetwork.Infrastructure.Entities.SqlServer
{
    public class LabeledImage
    {
        public long LabelId { get; set; }
        public Label Label { get; set; }
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}
