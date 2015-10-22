using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageProcessing.Operations
{
    public class Reconstruction
    {
        public Reconstruction()
        {

        }

        public static Image Apply(Image original, Image toBeReconstructedImage)
        {

            if (original.Size.Width != toBeReconstructedImage.Size.Width || original.Size.Height != toBeReconstructedImage.Size.Height) throw new Exception("Images dont match in size..");

            int[,] currentPixelsImg1 = original.GetPixels();
            int[,] currentPixelsImg2 = toBeReconstructedImage.GetPixels();

            int[,] newPixels = new int[original.Size.Width, original.Size.Height];

            //initialise image
            for (int x = 0; x < original.Size.Width; x++)
            {
                for (int y = 0; y < original.Size.Height; y++)
                {
                    newPixels[x, y] = Image.White;
                }
            }
                    
            //Loop through the image
            for (int x = 0; x < toBeReconstructedImage.Size.Width; x++)
            {
                for (int y = 0; y < toBeReconstructedImage.Size.Height; y++)
                {
                    //if there is a pixel marked on the 'toBeReconstructedImage', reconstruct that object based on the original image's pixels:
                    if (currentPixelsImg2[x,y] == Image.Black)
                        ColorNeightbours(x, y, original.Size, ref currentPixelsImg1, ref newPixels);

                }
            }

            return new Image(newPixels, original.Size);
            
        }

        public static void ColorNeightbours(int X, int Y, Size size, ref int[,] original, ref int[,] result)
        {
            Stack<Coordinate> list = new Stack<Coordinate>(); //Using a new stack, combined with an iterative way to loop through the nearby pixels, to prevent stackoverflow error
            list.Push(new Coordinate(X, Y));

            while(list.Count > 0)
            {
                Coordinate currentCoordinate = list.Pop();
                int x = currentCoordinate.x;
                int y = currentCoordinate.y;

                if (!LegitPixel(x, y, size, ref original, ref result)) continue;

                result[x, y] = Image.Black; //Color the current pixel

                //Loop through neighbours. Ignoring diagonal neighbours. They will be visited indirectly...
                if (LegitPixel(x - 1, y, size, ref original, ref result))
                    list.Push(new Coordinate(x - 1, y));
                if (LegitPixel(x, y -1, size, ref original, ref result))
                    list.Push(new Coordinate(x , y - 1));
                if (LegitPixel(x +1, y, size, ref original, ref result))
                    list.Push(new Coordinate(x + 1, y));
                if (LegitPixel(x, y+ 1, size, ref original, ref result))
                    list.Push(new Coordinate(x , y +1));
                
            }
        }

        
        private static bool LegitPixel(int x, int y, Size size, ref int[,] original, ref int[,] result)
        {
            if (x < 0 || x >= size.Width || y < 0 || y >= size.Height) return false;    //Out of bounce
            if (original[x, y] == Image.White) return false;        //If original is white then dont fill it in and quit looking at neighbours
            if (result[x, y] == Image.Black) return false;          //If this pixel has already been filled in and been looped through, dont continue and cause stack overflows!
            return true;
        }


    }
    public struct Coordinate
    {
        public int x, y;

        public Coordinate(int p1, int p2)
        {
            x = p1;
            y = p2;
        }
    }
}
