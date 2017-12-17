using AForge.Neuro;
using AForge.Neuro.Learning;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class NeuralNetwork
    {
        ActivationNetwork Network;
        string Filename;
        BackPropagationLearning teacher;

        public NeuralNetwork(string file)
        {
            Filename = file;
            if (File.Exists(file))
            {
                Network = (ActivationNetwork)ActivationNetwork.Load(file);
            }
            else
            {
                Network = new ActivationNetwork(new BipolarSigmoidFunction(0.5), 31, 10,1);
                Network.Randomize();
                Save();
                
            }
          
            teacher = new BackPropagationLearning(Network);
            teacher.LearningRate = 0.1;
         
        }

        public void Teach(double[] input, double output)
        {
            teacher.Run(input, new double[] { output });
        }

        public double Run(double[] input)
        {
            var computed = Network.Compute(input);
            return computed[0];
        }

       

        public void Save() {
            Network.Save(Filename);
        }

    }
}
