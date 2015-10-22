﻿namespace ImageProcessing.Operations
{
    class Average
    {
        public Average()
        {

        }

        public static Image Apply(Image Image, Image Image2)
        {
            int additive_value = 0;
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    additive_value = (Image.GetPixelColor(x, y)+ Image2.GetPixelColor(x, y))/2;
                    if (additive_value > 255) additive_value = 255;
                    if (additive_value < 0) additive_value = 0;
                    Image.SetPixelColor(x, y, additive_value);
                }
            }
            return Image;
        }
    }
}