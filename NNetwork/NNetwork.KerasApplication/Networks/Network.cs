using Keras;
using Keras.Callbacks;
using Keras.Layers;
using Keras.Models;
using Keras.Optimizers;
using NNetwork.KerasApplication.Arguments;
using NNetwork.KerasApplication.Enums;
using Numpy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NNetwork.KerasApplication.Networks
{
    public class Network
    {
        private BaseLayer[] Layers { get; set; }
        public  BaseModel NetworkModel { get; set; }

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
                var inputs = layer.Transitions.Select(id => Layers[id]).ToList();
                BaseLayer createdLayer = null;
                if (layer.LayerType == LayerType.Concatenate)
                {
                    createdLayer = new Concatenate(inputs.ToArray());
                }
                else
                {
                    createdLayer = LayerCreator.CreateLayer(layer.LayerType, layer.Parameters);
                    if (inputs.Count() == 1)
                    {
                        createdLayer = createdLayer.Set(inputs.ToArray());
                    }
                    else if (inputs.Count() > 1)
                    {
                        createdLayer = createdLayer.Set(new Concatenate(inputs.ToArray()));
                    }
                }

                Layers[index] = createdLayer;
                index++;
            }

            NetworkModel = new Keras.Models.Model(new Input[] { (Input)Layers[0] }, new BaseLayer[] { Layers[^1] });
            return true;
        }

        public bool InitializeNetwork(string jsonModel, byte[] weightFileByteArray)
        {
            var loaded_model = Model.ModelFromJson(jsonModel);

            var weightFileName = Guid.NewGuid().ToString() + ".h5";
            File.WriteAllBytes(weightFileName, weightFileByteArray);
            loaded_model.LoadWeight(weightFileName);
            File.Delete(weightFileName);

            NetworkModel = loaded_model;
            return true;
        }

        public void Compile(
            StringOrInstance optimizer = null,
            string loss = null,
            string[] metrics = null)
        {
            NetworkModel.Compile(
                optimizer: (StringOrInstance)optimizer ?? new RMSprop(lr: 0.0001f, decay: 1e-6f),
                loss : loss ?? "categorical_crossentropy",
                metrics : metrics ?? new string[] { "accuracy" }
                );

        }

        public (string, byte[]) Save()
        {
            string jsonModel = NetworkModel.ToJson();
            var weightFileName = Guid.NewGuid().ToString() + ".h5";
            NetworkModel.SaveWeight(weightFileName);
            var weightFileByteArray = File.ReadAllBytes(weightFileName);
            File.Delete(weightFileName);
            return (jsonModel, weightFileByteArray);
        }

        public History Fit(NDarray x_train,
            NDarray y_train,
            int batch_size,
            int epochs,
            NDarray[] validation_data,
            bool shuffle)
        {
            return NetworkModel.Fit(
                x_train,
                y_train,
                batch_size: batch_size,
                epochs: epochs,
                verbose: 1,
                validation_data: validation_data,
                shuffle: shuffle
                );
        }

        public byte[] GetPlot()
        {
            var fileName = Guid.NewGuid().ToString() + ".png";
            Keras.Utils.Util.PlotModel(
                NetworkModel,
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

        public NDarray Predict(NDarray x)
        {
            return NetworkModel.Predict(x);
        }
    }
}
