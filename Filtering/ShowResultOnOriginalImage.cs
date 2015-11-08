using System.Collections.Generic;

namespace ImageProcessing.Filtering
{
    /// <summary>
    /// Code used to show the detected cups on the original image
    /// </summary>
    public class ShowResultOnOriginalImage
    {
        private List<Item> list;
        private System.Drawing.Bitmap original;

        private int Margin = 10;
        private System.Drawing.Color color = System.Drawing.Color.Red;

        /// <summary>
        /// Create an image based on the original image and the found objects(cups)
        /// </summary>
        /// <param name="original">Original image</param>
        /// <param name="list">Found objects (cups)</param>
        public ShowResultOnOriginalImage(System.Drawing.Bitmap original, List<Item> list)
        {
            this.original = (System.Drawing.Bitmap)original.Clone();
            this.list = list;
        }
        
        public System.Drawing.Bitmap ConstructNewImage()
        {

            //Loop through each object and paint it on the canvas...
            foreach (Item obj in list)
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
