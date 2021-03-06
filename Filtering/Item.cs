﻿using ImageProcessing.Filtering.ShapeMeasures;
using System;

namespace ImageProcessing.Filtering
{
    /// <summary>
    /// Our own object class, that represents an object in our image
    /// </summary>
    public class Item
    {
        public Image image;
        public int xOnOriginalImage { get; private set; }
        public int yOnOriginalImage { get; private set; }

        #region ShapeMeasures
        public int Area { get; private set; }
        public double Perimeter { get; private set; }
        public double Compactness { get; private set; }
        public double Roundness { get; private set; }
        public Chord LongestChord { get; private set; }
        public Chord LongestPerpendicularChord { get; private set; }
        public double Eccentricity { get; private set; }
        public double MinimalBoundingBoxArea { get; private set; }
        public double Rectangularity { get; private set; }
        public double Elongation { get; private set; }
        public double Curvature { get; private set; }
        public double BendingEnergy { get; private set; }
        public int Openings { get; private set; }
        #endregion

        public Item(Image image)
        {
            Trim(image);
            CalculateValues();
        }

        /// <summary>
        /// Do the individual object calculation:
        /// </summary>
        private void CalculateValues()
        {
            Area = ShapeMeasures.Area.Calculate(image);
            Perimeter = ShapeMeasures.Perimeter.Calculate(image);
            Compactness = ShapeMeasures.Compactness.Calculate(Area, Perimeter);
            Roundness = ShapeMeasures.Roundness.Calculate(Compactness);

        
            LongestChord = ShapeMeasures.LongestChord.Calculate(image);
            LongestPerpendicularChord = ShapeMeasures.LongestPerpendicularChord.Calculate(image, LongestChord);
            Eccentricity = ShapeMeasures.Eccentricity.Calculate(LongestChord, LongestPerpendicularChord);
            MinimalBoundingBoxArea = ShapeMeasures.MinimalBoundingBoxArea.Calculate(LongestChord, LongestPerpendicularChord);
            Rectangularity = ShapeMeasures.Rectangularity.Calculate(Area, MinimalBoundingBoxArea);
            Elongation = ShapeMeasures.Elongation.Calculate(LongestChord, LongestPerpendicularChord);
            
            Openings = NumberOfHoles.Calculate(image);


        }


        /// <summary>
        /// Trim image:
        /// Use the information while trimming to determine the x and y values on original canvas
        /// </summary>
        /// <param name="image"></param>
        private void Trim(Image image)
        {
            int amountOfEmptyRowsTop = Math.Max(CalculateEmptyRowsFromTop(image) - 1, 0); //Removing one, so we always keep 1 empty space around the images.
            int amountOfEmptyRowsBot = Math.Max(CalculateEmptyRowsFromBottom(image) - 1, 0);
            int amountOfEmptyRowsLeft = Math.Max(CalculateEmptyRowsFromLeft(image) - 1, 0);
            int amountOfEmptyRowsRight = Math.Max(CalculateEmptyRowsFromRight(image) - 1, 0);

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
