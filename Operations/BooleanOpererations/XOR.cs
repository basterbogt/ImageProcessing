﻿namespace ImageProcessing.Operations.BooleanOpererations
{
    /// <summary>
    /// Given a black/white image, this class returns a new image that only contains the black pixels that are unique in each image
    /// </summary>
    public class XOR
    {
        /// <summary>
        /// Applies XOR, returning a new image that only contains black pixels that are unique in each image
        /// </summary>
        /// <param name="Image">target image 1</param>
        /// <param name="Image2">target image 2</param>
        /// <returns>new image that contains the black pixels that are unique in each image</returns>
        public static Image Apply(Image Image, Image Image2)
        {
            Image newImg = new Image(Image.GetPixels(), Image.Size);
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    bool img1 = (Image.GetPixelColor(x, y) == Image.Black);//if image one has a black pixel on this position
                    bool img2 = (Image2.GetPixelColor(x, y) == Image.Black);//if image two has a black pixel on this position
                    int result = (img1 ^ img2) ? Image.Black : Image.White; //if only one of the two booleans is true, draw black. Else draw white.
                    newImg.SetPixelColor(x, y, result);
                }
            }
            return newImg;
        }
    }
}
