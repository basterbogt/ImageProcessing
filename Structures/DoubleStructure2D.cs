using System.Drawing;

namespace ImageProcessing.Structures
{
    /// <summary>
    /// A wrapper that contains two structures, used for several calculations. Eg: Skeleton by thinning
    /// </summary>
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
