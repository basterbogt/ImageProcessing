﻿using ImageProcessing.Operations;
using System.Drawing;

namespace ImageProcessing
{
    public class Image
    {
        public enum Operations { /*GreyScale,*/ Smoothing, Negative, NegativeThreshold, Opening };

        private int[,] pixelArray;

        public Size Size { get; set; }

        public Image(Bitmap InputImage)
        {
            pixelArray = new int[InputImage.Size.Width, InputImage.Size.Height]; // Create array to speed-up operations (Bitmap functions are very slow)
            Size = InputImage.Size;

            // Copy input Bitmap to array            
            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    pixelArray[x, y] = GreyScale.ColorToGrey(InputImage.GetPixel(x, y));                // Set pixel color in array at (x,y)
                }
            }
        }

        public void SetPixels(int[,] image)
        {
            this.pixelArray = image;
        }
        public int[,] GetPixels()
        {
            return pixelArray;
        }

        public int GetPixelColor(int x, int y) {
            return pixelArray[x, y];
        }
        public void SetPixelColor(int x, int y, int color)
        {
            pixelArray[x, y] = color;
        }

        public void Apply(Operations operation)
        {
            switch (operation)
            {
                //case Operations.GreyScale:
                //    new GreyScale().Apply(this);
                //    break;
                case Operations.Smoothing:
                    new Smoothing().Apply(this);
                    break;
                case Operations.Negative:
                    new Negative().Apply(this);
                    break;
                case Operations.NegativeThreshold:
                    new NegativeThreshold().Apply(this);
                    break;
                case Operations.Opening:
                    new Opening().Apply(this);
                    break;
                default:
                    throw new System.Exception("This operation doesn't exist!");
            }

        }
    }
}
