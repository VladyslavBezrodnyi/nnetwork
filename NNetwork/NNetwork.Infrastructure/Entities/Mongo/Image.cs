using System;
using System.Collections.Generic;
using System.Text;

namespace NNetwork.Infrastructure.Entities.Mongo
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public byte[] ImagePayload { get; set; }
    }
}
