using Keras;
using Keras.Layers;
using Keras.Models;
using Keras.Optimizers;
using NNetwork.KerasApplication.Arguments;
using NNetwork.KerasApplication.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NNetwork.KerasApplication.Networks
{
    public class Network
    {
        private BaseLayer[] Layers { get; set; }
        private Model _model { get; set; }

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
                var inputs = layer.Transitions.Select(id => Layers[id]);
                BaseLayer createdLayer = null;
                if(layer.LayerType == LayerType.Concatenate)
                {
                    createdLayer = new Concatenate(inputs.ToArray());
                } else {
                    createdLayer = CreateLayer(layer.LayerType, layer.Parameters);
                    createdLayer = createdLayer.Set(inputs.ToArray());
                }
                
                Layers[index] = createdLayer;
                index++;
            }

            _model = new Model(Layers[0], Layers[^1]);
            return true;
        }

        public bool InitializeNetwork(string jsonModel, byte[] weightFileByteArray)
        {
            var loaded_model = Model.ModelFromJson(jsonModel);

            var weightFileName = Guid.NewGuid().ToString() + ".h5";
            File.WriteAllBytes(weightFileName, weightFileByteArray);
            loaded_model.LoadWeight(weightFileName);
            File.Delete(weightFileName);

            _model = (Model)loaded_model;
            return true;
        }

        public void Compile(
            string optimizer,
            string loss,
            string[]  metrics)
        {
            _model.Compile(
                (StringOrInstance)optimizer ?? new Adadelta(), 
                loss ?? "categorical_crossentropy", 
                metrics ?? new string[] { "accuracy" }
                );

        }

        public (string, byte[]) Save()
        {
            string jsonModel = _model.ToJson();
            var weightFileName = Guid.NewGuid().ToString() + ".h5";
            _model.SaveWeight(weightFileName);
            var weightFileByteArray = File.ReadAllBytes(weightFileName);
            File.Delete(weightFileName);
            return (jsonModel, weightFileByteArray);
        }

        public void Fit()
        {
            //_model.Fit(x, y, batch_size: 2, epochs: 1000, verbose: 1);
        }

        public byte[] GetPlot()
        {
            var fileName = Guid.NewGuid().ToString() + ".png";
            Keras.Utils.Util.PlotModel(
                _model,
                to_file: fileName,
                show_shapes: true,
                show_layer_names: true,
                rankdir: "TB",
                expand_nested: true,
                dpi: 96
                );
            var fileByteArray = File.ReadAllBytes(fileName);
            File.Delete(fileName);
            return fileByteArray;
        }

        public void Predict()
        {
            //_model.Predict();
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
