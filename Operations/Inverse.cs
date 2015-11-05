using System;

namespace ImageProcessing.Operations
{
    class Inverse : Operation
    {
        public Inverse()
        {

        }

        public override void Apply(Image image)
        {
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    image.SetPixelColor(x, y, Image.White - image.GetPixelColor(x, y));
                }
            }
        }
    }
}
