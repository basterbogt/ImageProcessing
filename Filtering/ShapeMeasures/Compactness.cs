using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    class Compactness
    {
      

        public static double Calculate(int area, double perimeter)
        {
            return Math.Pow(perimeter, 2) / (4 * Math.PI * area);

        }
    }
}
