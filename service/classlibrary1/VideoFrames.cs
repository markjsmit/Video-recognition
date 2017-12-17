using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class VideoFrames
    {
        public float OriginalFps { get; set; }
        public IList<ProcessedFrame> Frames { get; set; }
        public VideoFrames() {
            Frames = new List<ProcessedFrame>();
        }
    }
}
