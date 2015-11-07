using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Operations.BooleanOpererations
{
    public class XOR
    {
        public static Image Apply(Image Image, Image Image2)
        {
            Image newImg = new Image(Image.GetPixels(), Image.Size);
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    bool img1 = (Image.GetPixelColor(x, y) == Image.Black);
                    bool img2 = (Image2.GetPixelColor(x, y) == Image.Black);
                    int result = (img1 ^ img2) ? Image.Black : Image.White;
                    newImg.SetPixelColor(x, y, result);
                }
            }
            return newImg;
        }
    }
}
