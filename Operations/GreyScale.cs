using System;
using System.Drawing;

namespace ImageProcessing.Operations
{
    /// <summary>
    /// Class used to calculate grayvalues and revert grayvalues to color objects
    /// </summary>
    public class GreyScale//: Operation
    {
        public GreyScale()
        {

        }
        
        /// <summary>
        /// Return a gray int value based on a color object
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static int ColorToGrey(Color color)
        {
            return (int)((color.R * 0.299) + (color.G * 0.587) + (color.B * 0.114)); // greyscale //https://en.wikipedia.org/wiki/Grayscale -> reason behind constants used
        }

        /// <summary>
        /// returns a gray colour object based on coloured colour object
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ColorToGreyColor(Color color)
        {
            int grayScale = ColorToGrey(color);
            return Color.FromArgb(color.A, grayScale, grayScale, grayScale);
        }
        
        /// <summary>
        /// Create a gray colour object based on an grayvalue int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Color CreateColorFromGrayValue(int value)
        {
            if (value < 0) value = 0;
            if (value > 255) value = 255;
            //if (value < 0 || value > 255) throw new Exception("Out of range");
            return Color.FromArgb(Convert.ToInt32(value), Convert.ToInt32(value), Convert.ToInt32(value)); ;
        }
    }
}
