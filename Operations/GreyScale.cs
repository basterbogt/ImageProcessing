using System.Drawing;

namespace ImageProcessing.Operations
{
    public class GreyScale : Operation
    {
        public GreyScale()
        {

        }

        public override void Apply(Image Image)
        {
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    Color pixelColor = Image.GetPixelColor(x, y);                         // Get the pixel color at coordinate (x,y)
                    int grayScale = (int)((pixelColor.R * 0.3) + (pixelColor.G * 0.59) + (pixelColor.B * 0.11)); // greyscale //https://en.wikipedia.org/wiki/Grayscale -> reason behind constants used
                    Color updatedColor = Color.FromArgb(pixelColor.A, grayScale, grayScale, grayScale);          // greyscale
                    Image.SetPixelColor(x, y, updatedColor);                             // Set the new pixel color at coordinate (x,y)
                }
            }
        }
    }
}
