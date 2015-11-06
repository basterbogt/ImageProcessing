using System;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Calculate the roundness of the given object
    /// </summary>
    public class Roundness
    {
        public static double Calculate(double compactness)
        {
            return 1 / compactness;
        }
    }
}
