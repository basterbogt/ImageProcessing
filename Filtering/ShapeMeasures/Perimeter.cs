using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    public class Perimeter
    {

        private static int Visited = 1;
        /// <summary>
        /// Calculates the perimeter, meaning the distance around the object
        /// </summary>
        public Perimeter()
        {

        }
        
        public static float Calculate(Image image)
        {

            float perimeter = 0;
            Point p = Toolbox.FindFirstPixel(image);
            if (image.GetPixelColor(p.X, p.Y) == Image.White) return 0;//In case we found an empty starting point

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

                List<Point> neighbours = FindNeighbours(image, point); //find black neighbours
                if (neighbours.Count >= 4) continue;//If this pixel is surrounded by black pixels, ignore it
                foreach (Point n in neighbours)
                {
                    if (!HasVisited(ref visited, n))
                    {
                        toVisit.Push(n);
                    }
                }
                perimeter += (4 - neighbours.Count); //4 possible neighbours, but the list contains the black ones, so this will add the white ones.
            }
            return perimeter;

        }

        private static List<Point> FindNeighbours(Image image, Point p)
        {
            List<Point> neighbours = new List<Point>();

            //West
            if (!(p.X - 1 < 0))
            {
                if (image.GetPixelColor(p.X - 1, p.Y) == Image.Black) neighbours.Add(new Point(p.X - 1, p.Y));
            }

            //North
            if (!(p.Y - 1 < 0))
            {
                if (image.GetPixelColor(p.X, p.Y - 1) == Image.Black) neighbours.Add(new Point(p.X, p.Y - 1));
            }

            //East
            if (!(p.X + 1 >= image.Size.Width))
            {
                if (image.GetPixelColor(p.X + 1, p.Y) == Image.Black) neighbours.Add(new Point(p.X + 1, p.Y));
            }

            //South
            if (!(p.Y + 1 >= image.Size.Height))
            {
                if (image.GetPixelColor(p.X, p.Y + 1) == Image.Black) neighbours.Add(new Point(p.X, p.Y + 1));
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






    }
}
