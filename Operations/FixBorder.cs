using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessing.Operations
{
    /// <summary>
    /// Used to redraw the outer border, in case this gets removed (eg erosion)
    /// Requires a black/white images.
    /// </summary>
    public class FixBorder
    {
        public static void Fix(Image image, int layers)
        {
            int[,] result = image.GetPixels();

            for(int i = layers -1; i >= 0; i--) { 
                for(int x = i; x < image.Size.Width - i; x++)
                {
                    result[x, i] = CalculateNewPixelValue(x, i, i, image);
                    result[x, image.Size.Height -1 - i] = CalculateNewPixelValue(x, image.Size.Height - 1 - i, i, image);
                }
                for (int y = i; y < image.Size.Height - i; y++)
                {
                    result[i, y] = CalculateNewPixelValue(i, y, i, image);
                    result[image.Size.Width - 1 - i, y] = CalculateNewPixelValue(image.Size.Width - 1 - i, y, i, image);
                }
                image.SetPixels(result);
            }
        }

        private static int CalculateNewPixelValue(int x, int y, int layer, Image image)
        {
            int[] values = new int[8];
            List<Point> neighbours = FindNeighbours(image, layer, new Point(x, y));
            int counter = 0;
            foreach(Point p in neighbours)
            {
                if(image.GetPixelColor(p.X, p.Y) == Image.Black) counter += 1;
            }
            return (counter >= neighbours.Count/2) ? Image.Black : Image.White;

        }

        /// <summary>
        /// Find nearby pixels.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="layer"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private static List<Point> FindNeighbours(Image image, int layer, Point p)
        {
            List<Point> neighbours = new List<Point>();

            //West
            if (!(p.X - 1 < layer))
            {
                neighbours.Add(new Point(p.X - 1, p.Y));
            }

            //NorthWest

            if (!(p.X - 1 < layer) && !(p.Y - 1 < layer))
            {
                neighbours.Add(new Point(p.X - 1, p.Y - 1));
            }

            //North
            if (!(p.Y - 1 < layer))
            {
                neighbours.Add(new Point(p.X, p.Y - 1));
            }

            //NorthEast
            if (!(p.Y - 1 < layer) && !(p.X + 1 >= image.Size.Width - layer))
            {
                neighbours.Add(new Point(p.X + 1, p.Y - 1));
            }

            //East
            if (!(p.X + 1 >= image.Size.Width - layer))
            {
                neighbours.Add(new Point(p.X + 1, p.Y));
            }

            //SouthEast
            if (!(p.Y + 1 >= image.Size.Height - layer) && !(p.X + 1 >= image.Size.Width - layer))
            {
                neighbours.Add(new Point(p.X + 1, p.Y + 1));
            }

            //South
            if (!(p.Y + 1 >= image.Size.Height - layer))
            {
                neighbours.Add(new Point(p.X, p.Y + 1));
            }
            //SouthWest
            if (!(p.Y + 1 >= image.Size.Height - layer) && !(p.X - 1 < layer))
            {
                neighbours.Add(new Point(p.X - 1, p.Y + 1));
            }

            return neighbours;
        }

        private struct Value{
            public int value;
            public int count;
            public Value(int value)
            {
                this.value = value;
                count = 0;
            }
            public void Add()
            {
                count++;
            }

        }

    }
}
