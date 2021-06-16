using System;
using System.Collections.Generic;
using System.Text;

namespace NNetwork.Domain.Models
{
    public class PredictionResult
    {
        public double[] Probabilities { get; set; }
        public int Index { get; set; }
    }
}
