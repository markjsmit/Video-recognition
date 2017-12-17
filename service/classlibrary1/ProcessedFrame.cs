using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class ProcessedFrame
    {
        public int Width { get; set; }
        public byte[] Data { get; set; }

        public static explicit operator ProcessedFrame(Frame frame)
        {
            var p = new ProcessedFrame()
            {
                Data = frame.ToArray(),
                Width = frame.Width
            };

            return p;
        }


        public double[] ToDoubleArray()
        {
            var aantal = Data.Where(x => (int)x != 0).Count();
            return Data.Select(x =>(double)x/255).ToArray();
        }


        public double[] SimpleDoubleArray()
        {
            var doubleList = new List<double>();
    

            double avg = 0;
            var i=0;
            foreach (var value in Data)
            {
                avg += value;
                i++;
                if (i == 1000)
                {
                    doubleList.Add(avg);
                    avg = 0;
                    i = 0;
                }
            
            }

            var aantal = Data.Where(x => (int)x != 0).Count();
            doubleList.Add(aantal);
            var newList = new List<double>();
            var max = doubleList.Max();
            if (max > 0)
            {
                foreach (var value in doubleList)
                {
                    newList.Add(value / max);
                }
                return newList.ToArray();
            }
            return doubleList.ToArray();


          
        }

    }
}
