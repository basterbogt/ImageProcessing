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
    }
}
