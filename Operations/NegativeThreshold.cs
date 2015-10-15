namespace ImageProcessing.Operations
{
    public class NegativeThreshold: Operation
    {
        public NegativeThreshold()
        {

        }
        public override void Apply(Image Image)
        {
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    int pixelColor = Image.GetPixelColor(x, y);                         // Get the pixel color at coordinate (x,y)
                    int threshold = 186;
                    int updatedColor = (pixelColor > threshold) ? 255 : 0;          // black or white
                    Image.SetPixelColor(x, y, updatedColor);                              // Set the new pixel color at coordinate (x,y)
                }
            }
        }
    }
}
