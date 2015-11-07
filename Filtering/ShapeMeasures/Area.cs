using System;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    public class Area
    {
        /// <summary>
        /// Filter to calculate the area of the given object.
        /// </summary>
        public Area()
        {

        }

        public static int Calculate(Image image)
        {
            int counter = 0;
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    if (image.GetPixelColor(x, y) == Image.Black) counter++;
                }
            }
            return counter;
        }
    }
}
