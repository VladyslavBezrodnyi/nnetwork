using NNetwork.KerasApplication.Layers;
using System;
using System.Collections.Generic;
using System.Text;

namespace NNetwork.KerasApplication.Networks
{
    public class NetworkInitializationModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Layer[] Layers { get; set; }
    }
}
