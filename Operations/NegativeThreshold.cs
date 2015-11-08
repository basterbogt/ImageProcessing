namespace ImageProcessing.Operations
{
    /// <summary>
    /// Thresholding the current image, changing each pixel to either white or black
    /// </summary>
    public class NegativeThreshold: Operation
    {
        public NegativeThreshold()
        {

        }

        /// <summary>
        /// Apply this operation to the image
        /// </summary>
        /// <param name="image">Image that gets the operation applied to</param>
        public override void Apply(Image image)
        {
            int totalGrayValue = 0;//Start counter
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    totalGrayValue += image.GetPixelColor(x, y);
                }
            }

            int AverageGrayValue = (totalGrayValue > 0) ? totalGrayValue / (image.Size.Width * image.Size.Height) : 0;

            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    int pixelColor = image.GetPixelColor(x, y);                         // Get the pixel color at coordinate (x,y)
                    //AverageGrayValue = 210;
                    int updatedColor = (pixelColor > AverageGrayValue) ? Image.White : Image.Black;          // black or white
                    image.SetPixelColor(x, y, updatedColor);                              // Set the new pixel color at coordinate (x,y)
                }
            }
        }
    }
}
