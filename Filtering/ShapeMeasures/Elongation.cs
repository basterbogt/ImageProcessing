namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Calculates the Elongation of the object. (Aspect ratio of the longest and shortest side)
    /// </summary>
    public class Elongation
    {
        public static double Calculate(Chord longestChord, Chord longestPerpendicularChord)
        {
            return (longestChord.Length > longestPerpendicularChord.Length) ? (longestChord.Length / longestPerpendicularChord.Length) : (longestChord.Length / longestPerpendicularChord.Length);
        }
    }
}
