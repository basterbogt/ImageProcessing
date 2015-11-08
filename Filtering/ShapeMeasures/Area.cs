namespace ImageProcessing.Filtering.ShapeMeasures
{
    /// <summary>
    /// Calculate the Area of an item/object
    /// </summary>
    public class Area
    {
        public Area()
        {

        }

        /// <summary>
        /// Calculate the Area of an item/object
        ///  - loop through each pixel and count if its a black pixel
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
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
