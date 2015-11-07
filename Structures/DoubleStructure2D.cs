using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Structures
{
    public class DoubleStructure2D
    {
        public Size StructureSize { set; get; }
        public Structure2D Foreground { get; private set; }
        public Structure2D Background { get; private set; }

        public DoubleStructure2D(Structure2D foreground, Structure2D background)
        {
            Foreground = foreground;
            Background = background;
            StructureSize = foreground.StructureSize;
        }
    }
}
