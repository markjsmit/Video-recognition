using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class Trick
    {
        [NonSerialized]
        public NeuralNetwork Network;

       public string TrickName { get; set; }
       public string NetworkFile { get; set; }

       public Trick(string trickName, string networkFile)
       {
           TrickName = trickName;
           NetworkFile = networkFile;
       }

       public void InitNetowrk() {
           Network = new NeuralNetwork(NetworkFile);
       }

    }
}
