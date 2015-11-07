using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Filtering
{
    public class ShowResultOnOriginalImage
    {
        private List<Object> list;
        private System.Drawing.Bitmap original;

        private int Margin = 10;
        private System.Drawing.Color color = System.Drawing.Color.Red;

        public ShowResultOnOriginalImage(System.Drawing.Bitmap original, List<Object> list)
        {
            this.original = (System.Drawing.Bitmap)original.Clone();
            this.list = list;
        }
        
        public System.Drawing.Bitmap ConstructNewImage()
        {

            //Loop through each object and paint it on the canvas...

            foreach (Object obj in list)
            {
                int x = obj.xOnOriginalImage - Margin;
                int y = obj.yOnOriginalImage - Margin;
                int width = obj.image.Size.Width + 2 * Margin;
                int height = obj.image.Size.Height + 2 * Margin;

                for(int i = 0; i < width; i++)
                {
                    if (x + i > 0 && x + i < original.Size.Width && y  > 0 && y < original.Size.Height)
                        original.SetPixel(x + i, y, color);
                    if (x + i > 0 && x + i < original.Size.Width && y + height > 0 && y + height < original.Size.Height)
                        original.SetPixel(x + i, y+height, color);
                }
                for (int i = 0; i < height; i++)
                {
                    if(x > 0 && x < original.Size.Width && y + i > 0 && y + i < original.Size.Height)
                        original.SetPixel(x, y + i, color);
                    if (x + width > 0 && x + width < original.Size.Width && y + i  > 0 && y + i  < original.Size.Height)
                        original.SetPixel(x + width, y + i, color);
                }
            }

            return original;
        }
    }
}
