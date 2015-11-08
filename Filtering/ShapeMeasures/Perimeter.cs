using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Calculate the Perimeter (the total length of the object boundary)
    /// </summary>
    public class Perimeter
    {

        private static int Visited = 1;
       
        public Perimeter()
        {

        }

        /// <summary>
        /// Calculate the Perimeter (the total length of the object boundary)
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static float Calculate(Image image)
        {

            float perimeter = 0;//Variable that holds our current length
            Point p = Toolbox.FindFirstPixel(image); //find the first pixel so we can loop through the edge/pixels of our object
            if (image.GetPixelColor(p.X, p.Y) == Image.White) return 0;//In case we found an empty starting point

            int[,] visited = new int[image.Size.Width, image.Size.Height];//array that represents visited pixels

            Stack<Point> toVisit = new Stack<Point>(); //stack with points that we still have to visit
            toVisit.Push(p);

            //While we still have to visit pixels:
            //Loop through those pixels. If they are near the edge, add a value to our perimeter and check if it has neighbours that we still have to visit. 
            while (toVisit.Count > 0)
            {
                Point point = toVisit.Pop();

                if (HasVisited(ref visited, point)) //did we already visit this pixel?
                {
                    continue;
                }

                Visit(ref visited, point); //mark this pixel as visited

                List<Point> neighbours = FindNeighbours(image, point); //find black neighbours
                if (neighbours.Count >= 4) continue;//If this pixel is surrounded by black pixels, ignore it
                foreach (Point n in neighbours)
                {
                    if (!HasVisited(ref visited, n))
                    {
                        toVisit.Push(n); //push neighbours only if we havent visit them yet
                    }
                }
                perimeter += (4 - neighbours.Count); //4 possible neighbours, but the list contains the black ones, so this will add the white ones.
            }
            return perimeter;

        }

        /// <summary>
        /// Private method to find neighbours of a given pixel
        /// </summary>
        /// <param name="image">target image</param>
        /// <param name="p">given pixel</param>
        /// <returns>list with neighbouring pixels</returns>
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
