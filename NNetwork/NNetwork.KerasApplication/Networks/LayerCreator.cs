using Keras;
using Keras.Layers;
using NNetwork.KerasApplication.Arguments;
using NNetwork.KerasApplication.Enums;
using System;

namespace NNetwork.KerasApplication.Networks
{
    public class LayerCreator
    {
        public static BaseLayer CreateLayer(LayerType type, Parameters parameters)
        {
            return type switch
            {
                LayerType.Activation => new Activation(act: "relu"),
                LayerType.Conv2D => new Conv2D(
                    filters: (int)parameters.Filters,
                    kernel_size: new Tuple<int, int>(parameters.KernelSize[0], parameters.KernelSize[1])
                    ),
                LayerType.Dense => new Dense(
                    units: (int)parameters.Units
                    ),
                LayerType.Dropout => new Dropout(
                    rate: (double)parameters.Rate
                    ),
                LayerType.Flatten => new Flatten(),
                LayerType.Input => new Input(
                    shape: new Shape((int[])parameters.Shape)
                    ),
                LayerType.MaxPooling2D => new MaxPooling2D(
                    pool_size: new Tuple<int, int>(parameters.PoolSize[0], parameters.PoolSize[1])
                    ),
                LayerType.UpSampling2D => new UpSampling2D(
                    size: new Tuple<int, int>(parameters.Size[0], parameters.Size[1])
                    ),
                LayerType.LeakyReLU => new LeakyReLU(
                    alpha: (float)parameters.Alpha
                    ),
                LayerType.PReLU => new PReLU(),
                LayerType.ReLU => new ReLU(),
                LayerType.ELU => new ELU(
                    alpha: (float)(parameters.Alpha ?? 1)
                    ),
                LayerType.Softmax => new Softmax(
                    axis: (int)(parameters.Axis ?? -1)
                    ),
                _ => null,
            };
        }
    }
}
