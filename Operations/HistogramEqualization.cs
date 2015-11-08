using System;

namespace ImageProcessing.Operations
{
    public class HistogramEqualization : Operation
    {
        /* 

            "For image enhancement purposes, it is often useful to apply a remapping g(x) that
            makes the histogram “flat”, i.e., remaps the grey values so that each value occurs an
            equal number of times in the image. This is called histogram equalization. The idea behind
            it is that the available image contrast is used optimally." 
            
                - Digital and medical Imgae Processing by Twan maintz, update 11/2005, page 59
                
        */

        /// <summary>
        /// Apply the histogram equalization
        /// </summary>
        /// <param name="image"></param>
        public override void Apply(Image image)
        {

            //h(v) = default pixel distribution
            int[] h = new int[Image.TotalGrayValues];
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    int pixelColor = image.GetPixelColor(x, y);
                    h[pixelColor] = h[pixelColor] + 1; //Add one to this bin
                }
            }

            
            //c(v) = added pixel distribution
            int[] c = new int[Image.TotalGrayValues];
            int counter = 0;
            for (int i = 0; i < Image.TotalGrayValues; i++)
            {
                //We are checking the amount of pixels of the current gray value, and add those to the total counter.
                counter += h[i];
                //We set the new value to the c(v) list
                c[i] = counter;
            }

            //We calculate the desired pixesl per bin:
            float totalPixels = image.Size.Width * image.Size.Height;
            float idealPixelsPerBin = totalPixels / ((float)Image.TotalGrayValues);

            //g(v) = the new mapping for the image's pixels. So this array holds the new positions.
            int[] g = new int[Image.TotalGrayValues];
            for (int i = 0; i < Image.TotalGrayValues; i++)
            {
                int result = (int)(c[i] / idealPixelsPerBin + 0.5f) - 1; //these two magic numbers are part of the formula
                g[i] = Math.Max(0, result); //Make all negative numbers zero
            }

            //Remap image:
            int[,] resultAfterRemap = new int[image.Size.Width, image.Size.Height];
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    int originalPixel = image.GetPixelColor(x, y); //Get pixel at coordinate
                    int remapPixel = g[originalPixel]; //Find in remap list, which pixel this value should get
                    resultAfterRemap[x,y] = remapPixel; //Place this new value pixel in the temporary list
                }
            }
            image.SetPixels(resultAfterRemap);//Overwrite image with new values
        }
    }
}
