namespace ImageProcessing.Operations
{
    class Maxval
    {
        public Maxval()
        {

        }

        public static Image Apply(Image Image, Image Image2)
        {
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    if (Image.GetPixelColor(x, y) < Image2.GetPixelColor(x, y))
                    {
                        Image.SetPixelColor(x,y,Image2.GetPixelColor(x, y));
                    }
                }
            }
            return Image;
        }
    }
}
