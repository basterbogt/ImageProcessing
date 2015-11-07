using ImageProcessing.Operations;
using System;
using System.Drawing;
using System.IO;

namespace ImageProcessing
{
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
                case Operation.Operations.Inverse:
                    new Inverse().Apply(this);
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

        public void Save(string name, string defaultFolder = null)
        {
            if (defaultFolder == null) defaultFolder = Program.ImageDirectory;
            Directory.CreateDirectory(defaultFolder);
            SaveFullPath(defaultFolder + "\\" + name + " - " + DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond + ".png");
        }
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
        
    }
}
