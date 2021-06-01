using System;
using System.IO;
using Keras;
using Keras.Layers;
using Keras.Models;
using Numpy;
using Python.Runtime;

namespace NNetwork.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var pythonPath = @"P:\Python\Python\Python38";

            //Environment.SetEnvironmentVariable("PATH", $@"{pythonPath};" + Environment.GetEnvironmentVariable("PATH"));
            //Environment.SetEnvironmentVariable("PYTHONHOME", pythonPath);
            //Environment.SetEnvironmentVariable("PYTHONPATH ", $@"{pythonPath}\Lib");

            //using (Py.GIL())
            //{
            //    dynamic np = Py.Import("numpy");
            //    //Console.WriteLine(np.cos(np.pi * 2));
            //}

            //NDarray y = np.array(new float[] { 0, 1, 1, 0 });

            ////////Build sequential model
            //var model = new Sequential();
            //model.Add(new Dense(32, activation: "relu", input_shape: new Shape(2)));
            //model.Add(new Dense(64, activation: "relu"));
            //model.Add(new Dense(1, activation: "sigmoid"));

            //////Compile and train
            //model.Compile(optimizer: "sgd", loss: "binary_crossentropy", metrics: new string[] { "accuracy" });
            //model.Summary(100);
            ////Keras.Utils.Util.PlotModel(model,
            ////    to_file: "model.png", 
            ////    show_shapes: true,
            ////    show_layer_names: true,
            ////    rankdir: "TB",
            ////    expand_nested: false,
            ////    dpi:96
            ////    );


            ////Load train data
            //NDarray x = np.array(new float[,] { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 } });
            //model.Fit(x, y, batch_size: 2, epochs: 1000, verbose: 1);

            ////Save model and weights
            //string json = model.ToJson();
            //File.WriteAllText("model.json", json);
            //model.SaveWeight("model.h5");

            //Load model and weight
            var loaded_model = Model.ModelFromJson(File.ReadAllText("model.json"));
            loaded_model.LoadWeight("model.h5");
            Keras.Utils.Util.PlotModel(loaded_model,
                to_file: "model.png",
                show_shapes: true,
                show_layer_names: true,
                rankdir: "TB",
                expand_nested: true,
                dpi: 96
                );
            var t = File.ReadAllBytes("model.png");
            //Console.WriteLine("Hello World!");
        }
    }
}
