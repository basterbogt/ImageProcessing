using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing.Filtering
{
    public partial class Object
    {
        public int xOnOriginalImage { get; private set; }
        public int yOnOriginalImage { get; private set; }

        public Image image;


        public Object(Image image)
        {
            Trim(image);
            CalculateValues();
        }

        private void CalculateValues()
        {

            CalculateArea();
            CalculatePerimeter();
            CalculateCompactness();
            CalculateLongestChordLength();
            CalculateLongestChordOrientation();
            CalculateLongestPerpendicularChord();
            CalculateEccentricity();
            CalculateMinimalBoundingBoxArea();
            CalculateRectangularity();
            CalculateElongation();
            CalculateElongation2();
            CalculateCurvature();
            CalculateBendingEnergy();
        }


        /// <summary>
        /// Trim image:
        /// Use the information while trimming to determine the x and y values on original canvas
        /// </summary>
        /// <param name="image"></param>
        private void Trim(Image image)
        {
            int amountOfEmptyRowsTop = CalculateEmptyRowsFromTop(image);
            int amountOfEmptyRowsBot = CalculateEmptyRowsFromBottom(image);
            int amountOfEmptyRowsLeft = CalculateEmptyRowsFromLeft(image);
            int amountOfEmptyRowsRight = CalculateEmptyRowsFromRight(image);
            
            //Create new image:
            int newImageHeight = image.Size.Height - amountOfEmptyRowsTop - amountOfEmptyRowsBot;
            int newImageWidth = image.Size.Width - amountOfEmptyRowsLeft - amountOfEmptyRowsRight;
            int[,] newImage = new int[newImageWidth, newImageHeight];
            for (int y = 0; y < newImageHeight; y++)
            {
                for (int x = 0; x < newImageWidth; x++)
                {
                    newImage[x, y] = image.GetPixelColor(x + amountOfEmptyRowsLeft, y + amountOfEmptyRowsTop);
                }
            }
            Image image2 = new Image(newImage, new System.Drawing.Size(newImageWidth, newImageHeight));

            //Set local variables:
            this.image = image2;
            this.xOnOriginalImage = amountOfEmptyRowsLeft;
            this.yOnOriginalImage = amountOfEmptyRowsTop;
        }


        private int CalculateEmptyRowsFromTop(Image image)
        {
            //CalculateEmptyRowsFromTop:
            for (int y = 0; y < image.Size.Height; y++)
            {
                for (int x = 0; x < image.Size.Width; x++)
                {
                    if (image.GetPixelColor(x, y) != Image.White)
                    {
                        return y;
                    }
                }
            }
            return image.Size.Height;
        }
        private int CalculateEmptyRowsFromBottom(Image image)
        {
            //CalculateEmptyRowsFromBottom:
            for (int y = image.Size.Height; y > 0; y--)
            {
                for (int x = 0; x < image.Size.Width; x++)
                {
                    if (image.GetPixelColor(x, y - 1 /* -1 to get right index */) != Image.White)
                    {
                        return image.Size.Height - y;
                    }
                }
            }
            return 0;
        }
        private int CalculateEmptyRowsFromLeft(Image image)
        {
            //CalculateEmptyRowsFromLeft:
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    if (image.GetPixelColor(x, y) != Image.White)
                    {
                        return x;
                    }
                }
            }
            return image.Size.Width;
        }
        private int CalculateEmptyRowsFromRight(Image image)
        {
            //CalculateEmptyRowsFromRight:
            for (int x = image.Size.Width; x > 0; x--)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    if (image.GetPixelColor(x - 1 /* -1 to get right index */, y) != Image.White)
                    {
                        return image.Size.Width - x;
                    }
                }
            }
            return 0;
        }
    }
}
