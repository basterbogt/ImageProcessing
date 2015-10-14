using System.Drawing;

namespace ImageProcessing.Operations
{
    public class NegativeThreshold: Operation
    {
        public NegativeThreshold()
        {

        }
        public override void Apply(Image Image)
        {
            //for (int x = 0; x < Image.Size.Width; x++)
            //{
            //    for (int y = 0; y < Image.Size.Height; y++)
            //    {
            //        Color pixelColor = Image.GetPixelColor(x, y);                         // Get the pixel color at coordinate (x,y)
            //        int threshold = -16777216 / 4; //02/09/2015 16:26 Bas ter Bogt: Currently an educated guessed value, not sure yet how to properly determine this value...
            //        int test = pixelColor.ToArgb();
            //        Color updatedColor = (pixelColor.ToArgb() < threshold) ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0);          // black or white
            //        Image.SetPixelColor(x, y, updatedColor);                              // Set the new pixel color at coordinate (x,y)
            //    }
            //}
        }
    }
}
