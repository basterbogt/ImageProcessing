namespace ImageProcessing.Operations
{
    /// <summary>
    /// Create the negative of the given image
    /// </summary>
    public class Negative : Operation
    {
        public Negative()
        {

        }

        /// <summary>
        /// Turns the image into its negative
        /// </summary>
        /// <param name="image">Image that gets the operation applied to</param>
        public override void Apply(Image image)
        {
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    image.SetPixelColor(x, y, (Image.TotalGrayValues - 1) - (image.GetPixelColor(x, y)));     //Inverse by subtracting the current pixel value of the max gray value
                }
            }
        }
    }
}
