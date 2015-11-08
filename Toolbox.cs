using System;
using System.Drawing;

namespace ImageProcessing
{
    public class Toolbox
    {
        /// <summary>
        /// Code to check if the number has a full integer as square root. 
        /// Eg: Math.Sqrt(9) = 3. 3 is a full integer, and this method will return true.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPerferctSquare(int number)
        {
            return (Math.Sqrt(number) % 1 == 0);
        }

        /// <summary>
        /// Checks if the pixel arrays are identical. The arrays must have the same length
        /// </summary>
        /// <param name="f1">First array</param>
        /// <param name="f2">Second array</param>
        /// <param name="size">Size of the images</param>
        /// <returns></returns>
        public static bool EqualPixels(int[,] f1, int[,] f2, Size size)
        {
            if (f1.Length != f2.Length) return false;

            for(int x = 0; x < size.Width; x++)
            {
                for(int y = 0; y < size.Height; y++)
                {
                    if (f1[x, y] != f2[x, y]) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Code to find the first black pixel in a black/white image.
        /// </summary>
        /// <param name="image">The image in which we are looking for the first black pixel</param>
        /// <returns></returns>
        public static Point FindFirstPixel(Image image)
        {
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    if (image.GetPixelColor(x, y) == Image.Black)
                    {
                        return new Point(x, y);
                    }
                }
            }
            return Point.Empty;
        }
    }
}
