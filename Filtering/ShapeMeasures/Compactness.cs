using System;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Calculate the compactness of an item/object. A perfect circle has the value of 1
    /// </summary>
    public class Compactness
    {
        public static double Calculate(int area, double perimeter)
        {
            return Math.Pow(perimeter, 2) / (4 * Math.PI * area);
        }
    }
}
