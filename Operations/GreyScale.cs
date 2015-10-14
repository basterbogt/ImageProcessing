using System;
using System.Drawing;

namespace ImageProcessing.Operations
{
    public class GreyScale//: Operation
    {
        public GreyScale()
        {

        }

        //public override void Apply(Image Image)
        //{
        //    for (int x = 0; x < Image.Size.Width; x++)
        //    {
        //        for (int y = 0; y < Image.Size.Height; y++)
        //        {
        //            Image.SetPixelColor(x, y, ColorToGreyColor(Image.GetPixelColor(x, y)));
        //        }
        //    }
        //}

        public static int ColorToGrey(Color color)
        {
            return (int)((color.R * 0.299) + (color.G * 0.587) + (color.B * 0.114)); // greyscale //https://en.wikipedia.org/wiki/Grayscale -> reason behind constants used
        }

        public static Color ColorToGreyColor(Color color)
        {
            int grayScale = ColorToGrey(color);
            return Color.FromArgb(color.A, grayScale, grayScale, grayScale);
        }

        public static Color CreateColorFromGrayValue(int value)
        {
            if (value < 0 || value > 255) throw new Exception("Out of range");
            return Color.FromArgb(Convert.ToInt32(value), Convert.ToInt32(value), Convert.ToInt32(value)); ;
        }
    }
}
