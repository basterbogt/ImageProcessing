using System;
using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Code to calculate the longestChord of an item
    /// </summary>
    public class LongestChord
    {
        public static Chord Calculate(Image image)
        {
            List<Point> coordinates = ObjectOuterPixels.OuterPoints(image); //Get all the outer pixels of the object

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

                    double lengthCurrentChord = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)); //Calculate the distance between two points (pythagoras)
                    if(lengthCurrentChord > length) //if we find a new, longer, chord, update the current variables:
                    {
                        length = lengthCurrentChord;
                        rotation = (p2.Y - p1.Y) / ((double)(p2.X- p1.X));
                        StartingPoint = p1;
                        EndingPoint = p2;
                    }
                }
            }

            return new Chord(length, rotation, StartingPoint, EndingPoint);//return the longest. We all know, size matters...
            
        }


    }
}
