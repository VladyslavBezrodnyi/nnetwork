﻿using System;
using System.Text.Json.Serialization;

namespace NNetwork.API.Dtos
{
    public class ParametersDto
    {
        [JsonPropertyName("filters")]
        public int? Filters { get; set; }

        [JsonPropertyName("kernel_size")]
        public int[] KernelSize { get; set; }

        [JsonPropertyName("units")]
        public int? Units { get; set; }

        [JsonPropertyName("rate")]
        public double? Rate { get; set; }

        [JsonPropertyName("shape")]
        public int[] Shape { get; set; }

        [JsonPropertyName("pool_size")]
        public int[] PoolSize { get; set; }
        
        [JsonPropertyName("size")]
        public int[] Size { get; set; }

        [JsonPropertyName("alpha")]
        public float? Alpha { get; set; }

        [JsonPropertyName("axis")]
        public int? Axis { get; set; }
    }
}
