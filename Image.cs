﻿using ImageProcessing.Operations;
using System;
using System.Drawing;
using System.IO;

namespace ImageProcessing
{
    /// <summary>
    /// Our own Image class, that represents a grayvalued images of full integers
    /// </summary>
    public class Image
    {
        public static int TotalGrayValues = 256;
        public static int White = 255;
        public static int Black = 0;
        public static int Gray = 128;

        private int[,] pixelArray;

        public Size Size { get; set; }

        public Image(Bitmap InputImage)
        {
            pixelArray = new int[InputImage.Size.Width, InputImage.Size.Height]; // Create array to speed-up operations (Bitmap functions are very slow)
            Size = new Size(InputImage.Size.Width, InputImage.Size.Height);

            // Copy input Bitmap to array            
            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    pixelArray[x, y] = GreyScale.ColorToGrey(InputImage.GetPixel(x, y));                // Set pixel color in array at (x,y)
                }
            }
        }

        public Image(int[,] pixelArray, Size size)
        {
            this.pixelArray = (int[,])pixelArray.Clone();
            this.Size = size;
        }

        public void SetPixels(int[,] image)
        {
            this.pixelArray = (int[,])image.Clone();
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

        /// <summary>
        /// Apply an operation on this image.
        /// </summary>
        /// <param name="operation">The requisted opereration</param>
        public void Apply(Operation.Operations operation)
        {
            switch (operation)
            {
                case Operation.Operations.Smoothing:
                    new Smoothing().Apply(this);
                    break;
                case Operation.Operations.Negative:
                    new Negative().Apply(this);
                    break;
                case Operation.Operations.NegativeThreshold:
                    new NegativeThreshold().Apply(this);
                    break;
                case Operation.Operations.Opening:
                    new Opening().Apply(this);
                    break;
                case Operation.Operations.Erosion:
                    new Erosion().Apply(this);
                    break;
                case Operation.Operations.Closing:
                    new Closing().Apply(this);
                    break;
                case Operation.Operations.Dilation:
                    new Dilation().Apply(this);
                    break;
                case Operation.Operations.Edges:
                    new Edges().Apply(this);
                    break;
                case Operation.Operations.Gaussian:
                    new Gaussian().Apply(this);
                    break;
                case Operation.Operations.HistogramEqualization:
                    new HistogramEqualization().Apply(this);
                    break;
                default:
                    throw new System.Exception("This operation doesn't exist!");
            }

        }

        /// <summary>
        /// Normalise the current Image.
        /// </summary>
        /// <returns>Returns a bool wether or not the normalization has succeeded</returns>
        public bool Normalise()
        {

            int min = int.MaxValue;
            int max = int.MinValue;

            //Find max and min values from the image:          
            for (int x = 0; x < Size.Width; x++)
            {
                for (int y = 0; y < Size.Height; y++)
                {
                    int pixelValue = pixelArray[x, y];
                    min = Math.Min(min, pixelValue);
                    max = Math.Max(max, pixelValue);
                }
            }
            int newMin = Math.Abs(min);
            int newMax = newMin + max;

            //If there is a situation where the min + max is greater then an int can store:
            if (newMax >= int.MaxValue) return false;

            float divide = ((float)newMax) / ((float)(Image.TotalGrayValues - 1));

            //normalise each pixelValue:        
            for (int x = 0; x < Size.Width; x++)
            {
                for (int y = 0; y < Size.Height; y++)
                {
                    pixelArray[x, y] = (int)((pixelArray[x, y] + newMin) / divide);
                }
            }


            return true;
        }

        /// <summary>
        /// Save the image to a file.
        /// </summary>
        /// <param name="name">Name of the file</param>
        /// <param name="defaultFolder">Optional: Destination folder</param>
        public void Save(string name, string defaultFolder = null)
        {
            SaveFullPath(GetFileName(name, defaultFolder));
        }
        /// <summary>
        /// Save this image to a file
        /// </summary>
        /// <param name="name">Full path and name</param>
        public void SaveFullPath(string name)
        {
            Bitmap b = new Bitmap(Size.Width, Size.Height);
            for (int x = 0; x < Size.Width; x++)
            {
                for (int y = 0; y < Size.Height; y++)
                {
                    b.SetPixel(x, y, GreyScale.CreateColorFromGrayValue(GetPixelColor(x, y)));
                }
            }
            b.Save(name, System.Drawing.Imaging.ImageFormat.Png);
        }

        /// <summary>
        /// A method to easily generate a filename, based on the current timestamp and the input name
        /// </summary>
        /// <param name="name">Name of the file</param>
        /// <param name="defaultFolder">Optional: Folder destination</param>
        /// <returns>A string that contains the path and file name (with a timestamp on it)</returns>
        public static string GetFileName(string name, string defaultFolder = null)
        {

            if (defaultFolder == null) defaultFolder = Program.ImageDirectory;
            Directory.CreateDirectory(defaultFolder);
            DateTime dateValue = new DateTime(DateTime.Now.Ticks);
            String time = dateValue.ToString("dd-MM-yyyy HH.mm.ss.fff");
            return defaultFolder + "\\" + name + " - " + time + ".png";
        }
        
    }
}
