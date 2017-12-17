using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public interface IHeatmapBuilder
    {
        void BuildHeatmap(Frame frame);
    }
}
