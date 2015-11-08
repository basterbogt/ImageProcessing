namespace ImageProcessing.Operations
{
    /// <summary>
    /// A class that will subtract the values of the second image from the first image
    /// </summary>
    public class Subtraction
    {
        public Subtraction()
        {

        }

        /// <summary>
        /// Subtract the values of the second image from the first image. Normalise the image afterwards.
        /// </summary>
        public static Image Apply(Image Image, Image Image2)
        {
            Image newImg = new Image(Image.GetPixels(), Image.Size); //Todo: Shouln't we just make a new pixel array here, instead of copying the first image?
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    int additive_value = (Image.GetPixelColor(x, y)- Image2.GetPixelColor(x, y));
                    //if (additive_value < 0) additive_value = Image.Black;
                    int highestValue = Image.White;
                    additive_value = (int)((additive_value + highestValue) / 2.0f);//This line will re-balance the values from [ -225, 255 ], to [ 0 , 255 ]
                    newImg.SetPixelColor(x, y, additive_value);
                }
            }
            return newImg;
        }

        /// <summary>
        /// Subtract the values from the second image from the first image. This method is used for black/white images.
        /// So all the black values from the second image will be subtracted from the first one.
        /// Read: Black = 1, White = 0; (This doesn't correspond with the actual values)
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="Image2"></param>
        /// <returns></returns>
        public static Image ApplyBlackWhite(Image Image, Image Image2)
        {
            Image newImg = new Image(Image.GetPixels(), Image.Size);
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    if (Image2.GetPixelColor(x, y) == Image.Black) newImg.SetPixelColor(x, y, Image.White);
                }
            }
            return newImg;
        }
    }
}
