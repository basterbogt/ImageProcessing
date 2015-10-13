using System;

namespace ImageProcessing
{
    public class Toolbox
    {
        public static bool IsPerferctSquare(int number)
        {
            return (Math.Sqrt(number) % 1 == 0);
        }
    }
}
