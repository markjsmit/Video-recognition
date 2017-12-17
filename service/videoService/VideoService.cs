using Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoService
{
    public class VideoService
    {
        GraphicsService.GraphicsService GraphicsService = new GraphicsService.GraphicsService();

        public Video GetVideo(string filename)
        {
            return new Video(filename, GetMotionDetector(), GraphicsService.RelevantGetAreaFinder(), GraphicsService.GetHeatmapBuilder());
        }


        public void DetectMotion(Frame input)
        {
            input.Video.MotionDetector.Detect(input);
        }

        public MotionDetector GetMotionDetector()
        {
            return new MotionDetector();
        }

        public VideoFrames ProcessVideo(Video video)
        {
            VideoFrames frames = new VideoFrames();
            frames.OriginalFps = video.Framerate;
            for (int i = 0; i < video.FrameCount; i++)
            {
                Frame frame = video.GetFrame(i);
                DetectMotion(frame);
                GraphicsService.TakeRelevantArea(frame);
                GraphicsService.EnlargePixels(frame);
                GraphicsService.BuildHeatmap(frame);
                Debug.WriteLine(i+"/"+video.FrameCount);
                frames.Frames.Add((ProcessedFrame)frame);
            }
            return frames;
        }



    }
}
