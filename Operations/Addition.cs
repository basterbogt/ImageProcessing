namespace ImageProcessing.Operations
{
    /// <summary>
    /// Addition: Merging two images together
    /// </summary>
    public class Addition
    {
        public Addition()
        {

        }

        /// <summary>
        /// Merge two images together
        /// </summary>
        /// <param name="Image">Input image 1</param>
        /// <param name="Image2">Input image 2</param>
        /// <returns>New image, which contains the two input images merged</returns>
        public static Image Apply(Image Image, Image Image2)
        {
            Image newImg = new Image(Image.GetPixels(), Image.Size);
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    int additive_value = (Image.GetPixelColor(x, y)+ Image2.GetPixelColor(x, y));
                    additive_value = (int)((additive_value) / 2.0f);//This line will re-balance the values from [ 0 , 510 ], to [ 0 , 255 ]
                    newImg.SetPixelColor(x, y, additive_value);
                }
            }
            return newImg;
        }
    }
}
