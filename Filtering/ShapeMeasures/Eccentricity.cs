namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Calculate the eccentricity, which is the length of the longest chord / length of the longestPerpendicularChord
    /// </summary>
    public class Eccentricity
    {
        public static double Calculate(Chord longestChord, Chord longestPerpendicularChord)
        {
            return longestPerpendicularChord.Length / longestChord.Length;
        }
    }
}
