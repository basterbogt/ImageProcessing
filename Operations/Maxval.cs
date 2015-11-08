namespace ImageProcessing.Operations
{
    /// <summary>
    /// Creates a new image based on the max values of the two given images
    /// </summary>
    class Maxval
    {
        public Maxval()
        {

        }

        /// <summary>
        /// Creates a new image based on the max values of the two given images
        /// </summary>
        /// <param name="Image">first image</param>
        /// <param name="Image2">second image</param>
        /// <returns>image with highest values of two input images</returns>
        public static Image Apply(Image Image, Image Image2)
        {
            Image newImg = new Image(Image.GetPixels(), Image.Size);

            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    if (Image.GetPixelColor(x, y) < Image2.GetPixelColor(x, y))
                    {
                        newImg.SetPixelColor(x,y,Image2.GetPixelColor(x, y));
                    }
                }
            }
            return newImg;
        }
    }
}
