using AForge.Imaging;
using AForge.Imaging.Filters;
using Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsService
{
    public class RelevantAreaFinder:IRelefanceChecker
    {

         int pixelSize =0;

        public void CutToRelefantArea(Frame frame)
        {

            UnmanagedImage original = frame.Img;
            pixelSize = (frame.Img.Width ) / 30;

            var width = original.Width / pixelSize;
            var height = original.Height / pixelSize;

            bool[,] whitepos = new bool[width, height];
            FindWhitePixels(whitepos, original);

            frame.FoundWhitePosStage1=whitepos;
            PreviousFrames.Add(frame);

            int startX = original.Width;
            int startY = original.Height;
            int endX = 0;
            int endY = 0;

            var needed = PreviousFrames.Count / 5 + 1;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (PreviousFrames.Where(z => z.FoundWhitePosStage1[x, y]).Count() > needed)
                        {
                            UpdatePositions(ref startX, ref startY,ref endX, ref endY, x,y);   
                        } 
        
                }
            }


            if (startX < endX && startY<endY)
            {

                startX -= width/10;
                if (startX < 0)
                    startX = 0;

                startY -= height/10;
                if (startY < 0)
                    startY = 0;

                endX += width/10;
                if (endX > width)
                    endX = width;


                endY += height/10;
                if (endY > height)
                    endY = height;


                if (endX-startX  > endY- startY  )
                {
                    endY += ((endX- startX  ) - (endY-startY  ));
                }
                else if (endX - startX   <endY-startY  )
                {
                    endX += ((endY-startY  ) - (endX-startX  ));
                }



                Crop filter = new Crop(new Rectangle(startX * pixelSize, startY * pixelSize, (endX - startX) * pixelSize, (endY - startY) * pixelSize));
                frame.Img=filter.Apply(original);
                original.Dispose();
                
            }

            if (PreviousFrames.Count > 120)
            {
                PreviousFrames.RemoveAt(0);
            }
         
        }


        private void UpdatePositions(ref int startX, ref int startY, ref int endX, ref int endY, int x, int y)
        {
            if (x < startX)
            {
                startX = x;
            }

            if (y < startY)
            {
                startY = y;
            }
            if(x>endX)
            {
                endX = x;
            }
            if (y > endY)
            {
                endY = y;
            }
        }

        private void FindWhitePixels(bool[,] whitepos, UnmanagedImage original)
        {
            var width = original.Width / pixelSize;
            var height = original.Height / pixelSize;
            for (var blockX = 0; blockX < width; blockX++)
            {
                for (var blockY = 0; blockY < height; blockY++)
                {
                    var startX = blockX * pixelSize;
                    var startY = blockY * pixelSize;
                    var endX = startX + pixelSize;
                    var endY = startY + pixelSize;
                    whitepos[blockX, blockY] = false;

                    for (var x = startX; x < endX; x++)
                    {
                        var found = false;
                        for (var y = startY; y < endY; y++)
                        {
                            if (original.GetPixel(x, y).R != 0)
                            {
                                whitepos[blockX, blockY] = true;
                                found = true;
                                break;
                            }
                        }
                        if (found)
                        {
                            break;
                        }

                    }

                }
            }
        }


        private IList<Frame> PreviousFrames = new List<Frame>();
    }
}
