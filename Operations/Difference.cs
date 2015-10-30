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
            //if (img1.Size.Width != img2.Size.Width || img1.Size.Height != img2.Size.Height) throw new Exception("Images dont match in size..");
            
            //int[,] currentPixelsImg1 = img1.GetPixels();
            //int[,] currentPixelsImg2 = img2.GetPixels();
            //int[,] newPixels = new int[img1.Size.Width, img1.Size.Height];

            for (int x = 0; x < img1.Size.Width; x++)
            {
                for (int y = 0; y < img1.Size.Height; y++)
                {
                    if (img1.GetPixelColor(x, y) == img2.GetPixelColor(x, y))
                    {
                        img1.SetPixelColor(x, y, 0);
                    }
                    else
                    {
                        img1.SetPixelColor(x, y, 255);
                    }
                }
            }
            return img1;

        }
    }
}
