namespace ImageProcessing.Operations
{
    class Minval
    {
        public Minval()
        {

        }

        public static Image Apply(Image Image, Image Image2)
        {
            Image newImg = new Image(Image.GetPixels(), Image.Size);
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    if (Image.GetPixelColor(x, y) > Image2.GetPixelColor(x, y))
                    {
                        newImg.SetPixelColor(x,y,Image2.GetPixelColor(x, y));
                    }
                }
            }
            return newImg;
        }
    }
}
