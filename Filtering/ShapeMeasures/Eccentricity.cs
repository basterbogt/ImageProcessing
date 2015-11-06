using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    class Eccentricity
    {
        public static double Calculate(Chord longestChord, Chord longestPerpendicularChord)
        {
            return longestPerpendicularChord.Length / longestChord.Length;
        }
    }
}
