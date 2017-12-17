using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GraphicsService
{
    class PixelEnlarger
    {

        int pixelSize=0;

        internal void Enlarge(Domain.Frame frame)
        {
            
            UnmanagedImage original=frame.Img;
            pixelSize = original.Width / 100;
            if (pixelSize == 0)
            {
                pixelSize = 1;
            }
            var width= original.Width / pixelSize;
            var height= original.Height / pixelSize;

            bool[,] whitepos=new bool[width,height];
            FindWhitePixels(whitepos, original);

            UnmanagedImage img = UnmanagedImage.FromManagedImage(new System.Drawing.Bitmap(width, height));
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    img.SetPixel(new AForge.IntPoint(x, y), whitepos[x, y] ? Color.White : Color.Black);
                }
            }

            frame.Img = UnmanagedImage.FromManagedImage(new Bitmap(img.ToManagedImage(),100,100));
        }


        private void FindWhitePixels(bool[,] whitepos, UnmanagedImage original)
        {
            var width= original.Width / pixelSize;
            var height= original.Height / pixelSize;
            for (var blockX = 0; blockX < width; blockX++) {
                for (var blockY = 0; blockY < height; blockY++)
                {
                    var startX=blockX*pixelSize;
                    var startY=blockY*pixelSize;
                    var endX=startX+pixelSize;
                    var endY=startY+pixelSize;
                    whitepos[blockX,blockY]=false;

                    for(var x= startX; x<endX; x++)
                    { var found=false;
                        for (var y = startY; y < endY; y++)
                        {
                            if (original.GetPixel(x, y).R != 0)
                            {
                                whitepos[blockX, blockY] = true;
                                found =true;
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




    }
}
