using NNetwork.KerasApplication.Networks;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNetwork.Domain.Models
{
    public class NetworkContainer
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Network Network { get; set; }
        public string PlotImage { get; set; }
        public string ModelConfiguration { get; set; }
        public byte[] ModelWeights { get; set; }
    }
}
