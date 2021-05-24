using Keras;
using Keras.Layers;
using Keras.Models;
using NNetwork.KerasApplication.Enums;
using System;
using System.Collections.Generic;

namespace NNetwork.KerasApplication.Networks
{
    public class Network
    {
        private BaseLayer[] Layers { get; set; }

        public bool InitializeNetwork(NetworkInitializationModel initializer)
        {
            if (initializer?.Layers == null)
            {
                return false;
            }
            Layers = new BaseLayer[initializer.Layers.Length];
            int index = 0;
            foreach (var layer in initializer.Layers)
            {
                var createdLayer = CreateLayer(layer.LayerType, layer.Parameters);
                
                Layers[index] = createdLayer;
                index++;
            }

            foreach (var layer in initializer.Layers)
            {
                if (layer.Transitions == null)
                {
                    continue;
                }
                foreach (var transition in layer.Transitions)
                {

                }
            }

            return true;
        }

        public Model InitializeModel(NetworkInitializationModel initializer)
        {
            if (Layers == null || Layers.Length == 0)
            {
                return null;
            }
            var model = new Model(Layers[0], Layers[^1]);
            return model;
        }

        private static BaseLayer CreateLayer(LayerType type, Dictionary<string, object> parameters)
        {
            return type switch
            {
                LayerType.Activation => new Activation(act: (string)parameters["act"]),
                LayerType.Conv2D => new Conv2D(
                    filters: (int)parameters["filters"],
                    kernel_size: (Tuple<int, int>)parameters["kernel_size"]
                    ),
                LayerType.Dense => new Dense(
                    units: (int)parameters["units"]
                    ),
                LayerType.Dropout => new Dropout(
                    rate: (double)parameters["rate"]
                    ),
                LayerType.Flatten => new Flatten(),
                LayerType.Input => new Input(
                    shape: ((int, int, int))parameters["shape"]
                    ),
                LayerType.MaxPooling2D => new MaxPooling2D(
                    pool_size: (Tuple<int, int>)parameters["pool_size"]
                    ),
                LayerType.UpSampling2D => new UpSampling2D(
                    size: (Tuple<int, int>)parameters["size"]
                    ),
                LayerType.Concatenate => new Concatenate(
                    inputs: (BaseLayer[])parameters["inputs"]
                    ),
                LayerType.LeakyReLU => new LeakyReLU(
                    alpha: (float)parameters["alpha"]
                    ),
                LayerType.PReLU => new PReLU(),
                LayerType.ReLU => new ReLU(),
                LayerType.ELU => new ELU(
                    alpha: (float)parameters["alpha"]
                    ),
                LayerType.Softmax => new Softmax(
                    axis: (int)parameters["axis"]
                    ),
                _ => null,
            };
        }
    }
}
