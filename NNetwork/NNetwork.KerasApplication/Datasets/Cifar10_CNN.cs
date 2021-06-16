using Keras.Datasets;
using System;
using System.Collections.Generic;
using System.Text;
using Numpy;
using K = Keras.Backend;
using Keras;
using Keras.Models;
using Keras.Layers;
using Keras.Utils;
using Keras.Optimizers;
using Keras.PreProcessing.Image;
using System.IO;
using NNetwork.KerasApplication.Networks;

namespace NNetwork.KerasApplication.Datasets
{
    public class Cifar10_CNN
    {
        public static string[] Labels { get; } = new string[] { "airplane", "automobile", "bird", "cat", "deer", "dog", "frog", "horse", "ship", "truck" };

        public static void Run(Network network)
        {
            int batch_size = 128;
            int num_classes = 10;
            int epochs = 100;

            var ((x_train, y_train), (x_test, y_test)) = Cifar10.LoadData();

            Console.WriteLine("x_train shape: " + x_train.shape);
            Console.WriteLine(x_train.shape[0] + " train samples");
            Console.WriteLine(x_test.shape[0] + " test samples");

            y_train = Util.ToCategorical(y_train, num_classes);
            y_test = Util.ToCategorical(y_test, num_classes);

            network.Compile(loss: "categorical_crossentropy",
                optimizer: new RMSprop(lr: 0.0001f, decay: 1e-6f), 
                metrics: new string[] { "accuracy" });

            x_train = x_train.astype(np.float32);
            x_test = x_test.astype(np.float32);
            x_train /= 255;
            x_test /= 255;

            network.Fit(x_train, y_train,
              batch_size: batch_size,
              epochs: epochs,
              validation_data: new NDarray[] { x_test, y_test },
              shuffle: true);

            var score = network.NetworkModel.Evaluate(x_test, y_test, verbose: 0);
            Console.WriteLine("Test loss:" + score[0]);
            Console.WriteLine("Test accuracy:" + score[1]);
        }

        public static (double[], int) Predict(Network network, string path)
        {
            string imagePath = Path.GetFullPath(path);

            if (File.Exists(imagePath))
            {
                var img = ImageUtil.LoadImg(path, target_size: new Shape(32, 32));
                NDarray x = ImageUtil.ImageToArray(img);
                x = x.astype(np.float32);
                x /= 255;
                x = x.reshape(1, x.shape[0], x.shape[1], x.shape[2]);
                var y = network.NetworkModel.Predict(x);
                int index = y.argmax().asscalar<int>();
                return (y[0].GetData<double>(), index);
            }
            else
            {
                throw (new Exception("No Image found at: " + imagePath));
            }
        }
    }
}
