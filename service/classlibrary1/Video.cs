using AForge.Imaging;
using AForge.Video.VFW;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Video
    {
        public float Framerate { get; set; }
        public int FrameCount { get; set; }
        private string FilePath { get; set; }
        private int FramesRead;
        private AVIReader Reader = new AVIReader();
        private string filename;

        public IMotionDetector MotionDetector { get; set; }
        public IRelefanceChecker RelefanceChecker { get; set; }
        public IHeatmapBuilder HeatmapBuilder { get; set; }

        public Video()
        {

        }

        public Video(string videoPath, IMotionDetector motionDetector, IRelefanceChecker relefanceChecker, IHeatmapBuilder heatmapBuilder)
        {
            FilePath = videoPath;
            Reader.Open(videoPath);
            Framerate = Reader.FrameRate;
            FrameCount = Reader.Length;
            MotionDetector = motionDetector;
            RelefanceChecker = relefanceChecker;
            HeatmapBuilder = heatmapBuilder;
        }



        public Frame GetFrame(int number)
        {
            UnmanagedImage result = null;
            if (number < FrameCount)
            {
                Reader.Position = number;
                result = UnmanagedImage.FromManagedImage(new Bitmap(Reader.GetNextFrame(), 960, 540));
                FramesRead++;
            }
            if (FramesRead % 24 == 0)
            {
                Reader.Close();
                Reader = new AVIReader();
                Reader.Open(FilePath);
            }

            return new Frame(result, this);
        }
    }
}
