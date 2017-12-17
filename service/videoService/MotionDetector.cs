using AForge.Vision.Motion;
using Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoService
{
    
   public class MotionDetector:Domain.IMotionDetector
    {

       AForge.Vision.Motion.MotionDetector detector = new AForge.Vision.Motion.MotionDetector(
        new TwoFramesDifferenceDetector(true),
        new MotionProcessor());


        public void Detect(Frame frame)
        {
            
           detector.ProcessFrame(frame.Img);

        }
    }
}
