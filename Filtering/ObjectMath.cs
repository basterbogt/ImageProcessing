﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing.Filtering
{
    public partial class Object
    {

        public int Area { get; private set; }
        public float Perimeter { get; private set; }
        public float Compactness { get; private set; }
        public float LongestChordLength { get; private set; }
        public float LongestChordOrientation { get; private set; }
        public float LongestPerpendicularChord { get; private set; }
        public float Eccentricity { get; private set; }
        public float MinimalBoundingBoxArea { get; private set; }
        public float Rectangularity { get; private set; }
        public float Elongation { get; private set; }
        public float Elongation2 { get; private set; }
        public float Curvature { get; private set; }
        public float BendingEnergy { get; private set; }
        
        
        public void CalculateArea()
        {
            int counter = 0;
            for(int x = 0; x < this.image.Size.Height; x++)
            {
                for (int y = 0; y < this.image.Size.Width; y++)
                {
                    if (this.image.GetPixelColor(x, y) == Image.Black) counter++;
                }
            }
            Area = counter;
        }

        public void CalculatePerimeter()
        {
            int startingPixelX;
            int startingpixelY;
            bool done = false;
            for (int x = 0; x < this.image.Size.Height; x++)
            {
                for (int y = 0; y < this.image.Size.Width; y++)
                {
                    if (image.GetPixelColor(x, y) == Image.Black)
                    {
                        startingPixelX = x;
                        startingpixelY = y;
                        done = true;
                        break;
                    }
                }
                if (done) break;//fuggly way to exit for loop :3

                //todo: route bepalen
            }




        }

        public void CalculateCompactness()
        {


        }
        public void CalculateLongestChordLength()
        {


        }
        public void CalculateLongestChordOrientation()
        {
            throw new NotImplementedException();
        }
        public void CalculateLongestPerpendicularChord()
        {
            throw new NotImplementedException();
        }


        public void CalculateEccentricity()
        {
            throw new NotImplementedException();
        }
        public void CalculateMinimalBoundingBoxArea()
        {
            throw new NotImplementedException();
        }
        public void CalculateRectangularity()
        {
            throw new NotImplementedException();
        }

        public void CalculateElongation()
        {
            throw new NotImplementedException();
        }
        public void CalculateElongation2()
        {
            throw new NotImplementedException();
        }
        public void CalculateCurvature()
        {
            throw new NotImplementedException();
        }
        public void CalculateBendingEnergy()
        {
            throw new NotImplementedException();

        }


    }
}