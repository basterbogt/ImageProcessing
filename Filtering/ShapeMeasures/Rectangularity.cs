namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Calculate the Rectangularity of an object
    /// </summary>
    public class Rectangularity
    {
        public static double Calculate(double Area, double MinimalBoundingBoxArea)
        {
            return Area / MinimalBoundingBoxArea;
        }
    }
}
