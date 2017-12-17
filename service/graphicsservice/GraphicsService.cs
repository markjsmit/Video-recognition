using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace GraphicsService
{
    public class GraphicsService
    {
        PixelEnlarger Enlarger = new PixelEnlarger();
      

        public void EnlargePixels(Frame frame)
        {
            Enlarger.Enlarge(frame);
        }

        public void BuildHeatmap(Frame frame)
        {
            frame.Video.HeatmapBuilder.BuildHeatmap(frame);
        }

        public void TakeRelevantArea(Frame frame)
        {
           frame.Video.RelefanceChecker.CutToRelefantArea(frame);
        }

        public RelevantAreaFinder RelevantGetAreaFinder()
        {
            return new RelevantAreaFinder();
        }

        public IHeatmapBuilder GetHeatmapBuilder()
        {
            return new HeatmapBuilder();
        }
    }
}
