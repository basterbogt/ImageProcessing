using System;
using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Calculating the amount of openings in an object, first by calculating the skeleton and then calculating the amount of openings.
    /// Requires one object per image. If there are more then one objects in the same image, only one will be calculated, and it will be kinda random which one..
    /// </summary>
    public class CountOpenings
    {
        //private static int Unvisited = 0;
        private static int Visited = 1;
        public static int Calculate(Image image)
        {
            Image skeleton = SkeletonByThinning.GenerateSkeleton(image);
            int Openings = Count(skeleton);
            //int random = new Random().Next(100, 999);
            //image.Save("Image with " + Openings + " openings - " + random);
            //skeleton.Save("Image with " + Openings + " openings - " + random + " - skeleton");
            return Openings;
        }


        private static int Count(Image image)
        {
            int circleCounter = 0;
            Point p = Toolbox.FindFirstPixel(image);
            int[,] visited = new int[image.Size.Width,image.Size.Height];
            Stack<Point> toVisit = new Stack<Point>();
            toVisit.Push(p);

            while(toVisit.Count > 0)
            {
                Point point = toVisit.Pop();

                if (HasVisited(ref visited, point))
                {
                    //Circle detected. Add one 
                    circleCounter++;
                }

                Visit(ref visited, point);

                List<Point> neighbours = FindNeighbours(image, point);
                foreach(Point n in neighbours)
                {
                    if (!HasVisited(ref visited, n))
                    {
                        toVisit.Push(n);
                    }
                }
            }
            return circleCounter;
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
