using AForge.Neuro;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkService
{
    public class NeuralNetworkService
    {
        public NeuralNetwork GetNetwork(string file)
        {
            return new NeuralNetwork(file);
        }

        public void SaveNeuralNetwork(NeuralNetwork network)
        {
            network.Save();
        }




    }
}
