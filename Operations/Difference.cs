using System;

namespace ImageProcessing.Operations
{
    public class Difference
    {
        public Difference()
        {

        }

        public static Image Apply(Image img1, Image img2)
        {
            Image newImg = new Image(img1.GetPixels(), img1.Size);

            for (int x = 0; x < newImg.Size.Width; x++)
            {
                for (int y = 0; y < newImg.Size.Height; y++)
                {
                    if (img1.GetPixelColor(x, y) == img2.GetPixelColor(x, y))
                    {
                        newImg.SetPixelColor(x, y, Image.Black);
                    }
                    else
                    {
                        newImg.SetPixelColor(x, y, Image.White);
                    }
                }
            }
            return newImg;

        }
    }
}
