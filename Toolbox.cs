using System;
using System.Drawing;

namespace ImageProcessing
{
    public class Toolbox
    {
        public static bool IsPerferctSquare(int number)
        {
            return (Math.Sqrt(number) % 1 == 0);
        }


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

        public static Point FindFirstPixel(Image image)
        {
            Point point = new Point();
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    int pixelColor = image.GetPixelColor(x, y);
                    if (pixelColor == Image.Black)
                    {
                        point.X = x;
                        point.Y = y;
                        return point;
                    }
                }
            }
            return point;
        }
    }
}
