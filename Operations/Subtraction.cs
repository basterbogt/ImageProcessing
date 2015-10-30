namespace ImageProcessing.Operations
{
    class Subtraction
    {
        public Subtraction()
        {

        }

        public static Image Apply(Image Image, Image Image2)
        {
            Image newImg = new Image(Image.GetPixels(), Image.Size);
            int additive_value = Image.Black;
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    additive_value = (Image.GetPixelColor(x, y)- Image2.GetPixelColor(x, y));
                    if (additive_value < 0) additive_value = Image.Black;
                    newImg.SetPixelColor(x, y, additive_value);
                }
            }
            return newImg;
        }
    }
}
