using AForge;
using AForge.Vision.Motion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;

namespace VideoService
{
    class MotionProcessor : IMotionProcessing
    {

        public void ProcessFrame(AForge.Imaging.UnmanagedImage videoFrame, AForge.Imaging.UnmanagedImage motionFrame)
        {
  


            for (var x = 0; x < motionFrame.Width; x++)
            {
                for (var y = 0; y < motionFrame.Height; y++)
                {

                    if (motionFrame.GetPixel(x,y).R==0)
                    {
                        videoFrame.SetPixel(new IntPoint(x, y), Color.Black);
                    }else
                    {
                        videoFrame.SetPixel(new IntPoint(x, y), Color.White);
                    }


                }
            }

        }




        public void Reset()
        {

        }
    }
}
