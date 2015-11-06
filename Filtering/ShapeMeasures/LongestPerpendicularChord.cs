using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    class LongestPerpendicularChord
    {
        public static Chord Calculate(Image image, Chord longestChord)
        {
            double length = 0;
            double rotation = 0;
            Point StartingPoint = new Point();
            Point EndingPoint = new Point();

            List<Point> coordinates = new List<Point>();

            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    if (image.GetPixelColor(x, y) == Image.Black)
                        coordinates.Add(new Point(x, y));
                }
            }

            foreach (Point p1 in coordinates)
            {
                foreach (Point p2 in coordinates)
                {
                    if (p1.Equals(p2)) continue;
                    //if (p2.X > p1.X) continue;
                    //if (p2.Y > p1.Y) continue;

                    double slope = (p2.Y - p1.Y) / ((double)(p2.X - p1.X));
                    if (-1 / slope != longestChord.Rotation) continue;

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
