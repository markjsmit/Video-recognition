using AForge.Imaging;
using Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsService
{
    class HeatmapBuilder:IHeatmapBuilder
    {

        private IList<Frame> PreviousFrames = new List<Frame>();

        public void BuildHeatmap(Frame frame)
        {

            PreviousFrames.Add(frame);
            var whitepos = new bool[frame.Img.Width, frame.Img.Height];
            FindWhitePixels(whitepos, frame.Img);
            frame.FoundWhitePosHeatmap = whitepos;

           DrawMaps(frame.Img);
     
            if (PreviousFrames.Count >40)
            {
                PreviousFrames.RemoveAt(0);
            }

        }

        private void DrawMaps(UnmanagedImage unmanagedImage)
        {
            for (var x = 0; x < unmanagedImage.Width; x++)
            {
                for (var y = 0; y < unmanagedImage.Height; y++)
                {
                    var color = Color.FromArgb(CalculateDecay(x, y), CalculateOccurance(x, y), CalculateFirstOccurance(x,y));
                    unmanagedImage.SetPixel(new AForge.IntPoint(x, y), color) ;
                }
            }
        }

        private int CalculateOccurance(int x, int y)
        {

            return (int)(((Double)PreviousFrames.Where(i => i.FoundWhitePosHeatmap[x, y]).Count()/PreviousFrames.Count())*255);
        }

        private int CalculateFirstOccurance(int x, int y){
            double value=0;
            double influence = 255 / PreviousFrames.Count();
            var i=0;
            foreach (var frame in PreviousFrames)
            {
                if(frame.FoundWhitePosHeatmap[x, y])
                {
                    value = i;
                    break;
                }
                i++;
            }
            if (value > 0)
            {
                value = 255 - (value * influence);
            }

            return (int)value;
        }


        private int CalculateDecay(int x, int y)
        {
            double value = 0;
            double influence = 255 / PreviousFrames.Count();
            foreach (var frame in PreviousFrames)
            {
                if (frame.FoundWhitePosHeatmap[x, y])
                {
                    value = 255;
                }
                else
                {
                    if (value > 0)
                    {
                        value -= influence;
                    }
                }
              
            }

            if (value <= 0)
            {
                value = 0;
            }
            return (int)value;
        }





        private void FindWhitePixels(bool[,] whitepos, UnmanagedImage original)
        {
            for (var x = 0; x < original.Width; x++)
            {
                for (var y = 0; y < original.Height; y++)
                {
                    if (original.GetPixel(x, y).R > 0)
                    {
                        whitepos[x, y] = true;
                    }
                }
            }
        }




    }
}
