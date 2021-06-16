using System;
using System.Collections.Generic;
using System.Text;

namespace NNetwork.Domain.Models
{
    public class NetworkAllInfo : NetworkTitleInfo
    {
        public string PlotImage { get; set; }
        public PredictionResult PredictionResult { get; set; }
    }
}
