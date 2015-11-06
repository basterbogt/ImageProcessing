namespace ImageProcessing.Operations
{
    class Addition
    {
        public Addition()
        {

        }

        public static Image Apply(Image Image, Image Image2)
        {
            Image newImg = new Image(Image.GetPixels(), Image.Size);
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    int additive_value = (Image.GetPixelColor(x, y)+ Image2.GetPixelColor(x, y));
                    //if (additive_value > Image.White) additive_value = Image.White;
                    additive_value = (int)((additive_value) / 2.0f);//This line will re-balance the values from [ 0 , 510 ], to [ 0 , 255 ]
                    newImg.SetPixelColor(x, y, additive_value);
                }
            }
            return newImg;
        }
    }
}
