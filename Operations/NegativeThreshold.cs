namespace ImageProcessing.Operations
{
    public class NegativeThreshold: Operation
    {
        public NegativeThreshold()
        {

        }
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
<<<<<<< HEAD
=======

            int AverageGrayValue = (totalGrayValue > 0) ? totalGrayValue / (Image.Size.Width * Image.Size.Height) : 0;
>>>>>>> refs/remotes/origin/Koen-Edges

            int AverageGrayValue = (totalGrayValue > 0) ? totalGrayValue / (image.Size.Width * image.Size.Height) : 0;

            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    int pixelColor = image.GetPixelColor(x, y);                         // Get the pixel color at coordinate (x,y)
                    //int threshold = 186;
                    int updatedColor = (pixelColor > AverageGrayValue) ? Image.White : Image.Black;          // black or white
                    image.SetPixelColor(x, y, updatedColor);                              // Set the new pixel color at coordinate (x,y)
                }
            }
        }
    }
}
