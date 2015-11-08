namespace ImageProcessing.Operations
{
    /// <summary>
    /// Class that uses two images, to return a new image that contains white pixels for each difference in the two supplied images,
    /// and a black pixel for each pixel that is the same
    /// </summary>
    public class Difference
    {
        public Difference()
        {

        }

        /// <summary>
        /// Calculate Difference and return a new image that represents the differences
        /// </summary>
        /// <param name="img1">first image</param>
        /// <param name="img2">second image</param>
        /// <returns>a new images that represents the difference between the input images</returns>
        public static Image Apply(Image img1, Image img2)
        {
            Image newImg = new Image(img1.GetPixels(), img1.Size);

            for (int x = 0; x < newImg.Size.Width; x++)
            {
                for (int y = 0; y < newImg.Size.Height; y++)
                {
                    if (img1.GetPixelColor(x, y) == img2.GetPixelColor(x, y)) //If pixels are the same
                    {
                        newImg.SetPixelColor(x, y, Image.Black); //set a black pixel
                    }
                    else
                    {
                        newImg.SetPixelColor(x, y, Image.White); //else, set a white pixel
                    }
                }
            }
            return newImg;

        }
    }
}
