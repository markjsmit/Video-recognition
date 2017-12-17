using AForge;
using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class Frame
    {
        public Frame(UnmanagedImage img, Video video)
        {
            Img = img;
            Original = Img.Clone();
            Video = video;
        }
        public Frame(Bitmap img, Video video)
        {
            Video= video;
            Img = UnmanagedImage.FromManagedImage(img);
            Original = UnmanagedImage.FromManagedImage(img);
        }

 

        public Bitmap GetBitmap()
        {
            return Img.ToManagedImage();
        }

        public byte[] ToArray() {
            byte[] output = new byte[Img.Width*Img.Height*3];
            for (var x = 0; x < Img.Width; x++)
            {
                for (var y = 0; y < Img.Height; y++)
                {
                    var pos=x+y*Img.Width;
                    var offsetPer=Img.Width*Img.Height;
                    output[pos+offsetPer*0]=Img.GetPixel(x,y).R;
                    output[pos + offsetPer * 1] = Img.GetPixel(x, y).G;
                    output[pos + offsetPer * 2] = Img.GetPixel(x, y).B;
                }
            }
            return output;
        }


        public UnmanagedImage Img { get; set; }
        public UnmanagedImage Original { get; set; }

        [NonSerialized]
        public Video Video;



        public bool[,] FoundWhitePosStage1 { get; set; }
        public bool[,] FoundWhitePosHeatmap { get; set; }

        public int Height { get { return Img.Width; } }

        public int Width { get { return Img.Height; } }
    }
}
