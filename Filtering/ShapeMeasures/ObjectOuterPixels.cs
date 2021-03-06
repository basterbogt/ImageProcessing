﻿using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Calculate the outer pixels of an object/item
    /// </summary>
    public class ObjectOuterPixels
    {
        private static int Visited = 1;
        public ObjectOuterPixels()
        {

        }

        /// <summary>
        /// Returns a list of pixels that represent the border of an item
        /// </summary>
        /// <param name="image">target item</param>
        /// <returns>list of points that represent the border of the item</returns>
        public static List<Point> OuterPoints(Image image)
        {
            List<Point> coordinates = new List<Point>();
            Point p = Toolbox.FindFirstPixel(image);
            if (image.GetPixelColor(p.X, p.Y) == Image.White) return coordinates;//In case we found an empty starting point
            
            int[,] visited = new int[image.Size.Width, image.Size.Height];
            Stack<Point> toVisit = new Stack<Point>();
            toVisit.Push(p);

            while (toVisit.Count > 0)
            {
                Point point = toVisit.Pop();

                if (HasVisited(ref visited, point))
                {
                    continue;
                }

                Visit(ref visited, point);
                coordinates.Add(point);
                //ObjectOuterPixels.SaveOuterPixelsAsImage(coordinates, image.Size);//uncomment this to view each step :D

                List<Point> neighbours = FindNeighbours(image, point); //find black neighbours
                if (neighbours.Count >= 8) continue;
                foreach (Point n in neighbours)
                {
                    if (!HasVisited(ref visited, n))
                    {
                        toVisit.Push(n);
                    }
                }
            }
            //ObjectOuterPixels.SaveOuterPixelsAsImage(coordinates, image.Size);//uncomment this to view result

            return coordinates;
        }


        private static List<Point> FindNeighbours(Image image, Point p)
        {
            List<Point> neighbours = new List<Point>();

            //West
            if (!(p.X - 1 < 0))
            {
                if (image.GetPixelColor(p.X - 1, p.Y) == Image.Black) neighbours.Add(new Point(p.X - 1, p.Y));
            }

            //NorthWest

            if (!(p.X - 1 < 0) && !(p.Y - 1 < 0))
            {
                if (image.GetPixelColor(p.X - 1, p.Y - 1) == Image.Black) neighbours.Add(new Point(p.X - 1, p.Y - 1));
            }

            //North
            if (!(p.Y - 1 < 0))
            {
                if (image.GetPixelColor(p.X, p.Y - 1) == Image.Black) neighbours.Add(new Point(p.X, p.Y - 1));
            }

            //NorthEast
            if (!(p.Y - 1 < 0) && !(p.X + 1 >= image.Size.Width))
            {
                if (image.GetPixelColor(p.X +1, p.Y - 1) == Image.Black) neighbours.Add(new Point(p.X +1, p.Y - 1));
            }

            //East
            if (!(p.X + 1 >= image.Size.Width))
            {
                if (image.GetPixelColor(p.X + 1, p.Y) == Image.Black) neighbours.Add(new Point(p.X + 1, p.Y));
            }

            //SouthEast
            if (!(p.Y + 1 >= image.Size.Height) && !(p.X + 1 >= image.Size.Width))
            {
                if (image.GetPixelColor(p.X + 1, p.Y + 1) == Image.Black) neighbours.Add(new Point(p.X +1, p.Y + 1));
            }

            //South
            if (!(p.Y + 1 >= image.Size.Height))
            {
                if (image.GetPixelColor(p.X, p.Y + 1) == Image.Black) neighbours.Add(new Point(p.X, p.Y + 1));
            }
            //SouthWest
            if (!(p.Y + 1 >= image.Size.Height) && !(p.X - 1 < 0))
            {
                if (image.GetPixelColor(p.X -1, p.Y + 1) == Image.Black) neighbours.Add(new Point(p.X -1, p.Y + 1));
            }

            return neighbours;
        }


        private static bool HasVisited(ref int[,] visitedList, Point p)
        {
            return (visitedList[p.X, p.Y] == Visited);
        }

        private static void Visit(ref int[,] visitedList, Point p)
        {
            visitedList[p.X, p.Y] = Visited;
        }

        /// <summary>
        /// Save the outerpixelpoints to an image (useful for debugging)
        /// </summary>
        /// <param name="points"></param>
        /// <param name="size"></param>
        public static void SaveOuterPixelsAsImage(List<Point> points, Size size)
        {
            int[,] visited = new int[size.Width, size.Height];

            foreach(Point p in points)
            {
                visited[p.X, p.Y] = Image.White;
            }

            Image image = new Image(visited, size);
            image.Save("OuterPixel");

        }

        /// <summary>
        /// Save the with chords (useful for debugging)
        /// </summary>
        /// <param name="longestChord"></param>
        /// <param name="lpc"></param>
        /// <param name="coordinates"></param>
        /// <param name="size"></param>
        public static void SaveOuterPixelsAsImageWithChords(Chord longestChord, Chord lpc, List<Point> coordinates, Size size)
        {
            
            Bitmap b = new Bitmap(size.Width, size.Height);
            foreach(Point p in coordinates)
            {
                b.SetPixel(p.X, p.Y, Color.Black);
            }
            Pen red = new Pen(Color.Red, 1);
            using (var graphics = Graphics.FromImage(b))
            {
                graphics.DrawLine(red, longestChord.StartingPoint, longestChord.EndingPoint);
                graphics.DrawLine(red, lpc.StartingPoint, lpc.EndingPoint);
                b.Save(Image.GetFileName("Chords through object"), System.Drawing.Imaging.ImageFormat.Png);
                //Image image = new Image(b);
                //image.Save("Chords through object");
            }
            //Doesnt work quite yet...not sure why.

        }

    }
}
