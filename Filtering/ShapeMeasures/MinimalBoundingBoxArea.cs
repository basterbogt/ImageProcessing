namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Calculate the minimal Bounding box area
    /// </summary>
    public class MinimalBoundingBoxArea
    {
        public static double Calculate(Chord longestChord, Chord longestPerpendicularChord)
        {
            return longestChord.Length * longestPerpendicularChord.Length;
        }
    }
}
