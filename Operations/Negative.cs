using System.Drawing;

namespace ImageProcessing.Operations
{
    public class Negative : Operation
    {
        public Negative()
        {

        }

        public override void Apply(Image image)
        {
            //for (int x = 0; x < Image.Size.Width; x++)
            //{
            //    for (int y = 0; y < Image.Size.Height; y++)
            //    {
            //        Color pixelColor = Image.GetPixelColor(x, y);                // Get the pixel color at coordinate (x,y)
            //        Color updatedColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B); // Negative image
            //        Image.SetPixelColor(x, y, updatedColor);                              // Set the new pixel color at coordinate (x,y)
            //    }
            //}
        }
    }
}
