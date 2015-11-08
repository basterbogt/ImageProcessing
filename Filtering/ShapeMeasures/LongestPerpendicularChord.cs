using System;
using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Calculates the longest chord perpendicular to the longest chord
    /// </summary>
    public class LongestPerpendicularChord
    {
        public static Chord Calculate(Image image, Chord longestChord)
        {

            List<Point> coordinates = ObjectOuterPixels.OuterPoints(image);

            Chord lpc;
            double margin = 0;
            do
            {
                lpc = CalculateLongestPerpendicularChord(coordinates, longestChord, margin); //Tries to calculate with the current margin (starts without a margin)
                margin += 0.01d; //update margin
            } while (lpc.Length <= 0 && margin < 1); //while we didnt find a perpendicular chord, and the margin is still smaller then 1:

            //ObjectOuterPixels.SaveOuterPixelsAsImageWithChords(longestChord, lpc, coordinates, image.Size);
            return lpc;

        }

        private static Chord CalculateLongestPerpendicularChord(List<Point> coordinates, Chord longestChord, double margin = 0)
        {
            double length = 0;
            double rotation = 0;
            Point StartingPoint = new Point();
            Point EndingPoint = new Point();
            foreach (Point p1 in coordinates)
            {
                foreach (Point p2 in coordinates)
                {
                    if (p1.Equals(p2)) continue;
                    //if (p2.X > p1.X) continue;
                    //if (p2.Y > p1.Y) continue;

                    double slope = (-1.0d / ((p2.Y - p1.Y) / ((double)(p2.X - p1.X))));
                    double slopeTarget = (longestChord.Rotation);
                    if (!(slope >= slopeTarget * (1.0d - margin) && slope <= slopeTarget * (1.0d + margin))) continue;
                    

                    double lengthCurrentChord = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
                    if (lengthCurrentChord > length)
                    {
                        length = lengthCurrentChord;
                        rotation = (p2.Y - p1.Y) / ((double)(p2.X - p1.X));
                        StartingPoint = p1;
                        EndingPoint = p2;

                    }
                }
            }

            return new Chord(length, rotation, StartingPoint, EndingPoint);
        }
    }
}
