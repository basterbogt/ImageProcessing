using System.Collections.Generic;

namespace ImageProcessing.Filtering
{
    /// <summary>
    /// Code to collect all the different objects from the black/white image and put them in an array.
    /// </summary>
    public class ObjectDetection
    {
        private Image originalImage;
        public List<Object> objects { get; private set; }

        private bool IgnoreObjectsNearBorder;

        public ObjectDetection(Image image, bool ignoreObjectsNearBorder = true)
        {
            this.originalImage = image;
            this.IgnoreObjectsNearBorder = ignoreObjectsNearBorder;
            objects = new List<Object>();
        }

        public void Apply()
        {
            Image copy = new Image(originalImage.GetPixels(), originalImage.Size);
            
            int[,] copyPixels = copy.GetPixels();
            
            //Loop through the image
            for (int x = 0; x < copy.Size.Width; x++)
            {
                for (int y = 0; y < copy.Size.Height; y++)
                {
                    //if there is a pixel marked on the 'toBeReconstructedImage', reconstruct that object based on the original image's pixels:
                    if (copyPixels[x, y] == Image.Black)
                        ExtractObjectFromImage(x, y, ref copyPixels);

                }
            }
        }

        /// <summary>
        /// Extract the object from the original image to a new object...
        /// </summary>
        /// <param name="x">starting point with extracting the image</param>
        /// <param name="y"></param>
        /// <param name="image"></param>
        private void ExtractObjectFromImage(int X, int Y, ref int[,] image)
        {
            int[,] newPixels = new int[originalImage.Size.Width, originalImage.Size.Height];

            //initialise image
            for (int x = 0; x < originalImage.Size.Width; x++)
            {
                for (int y = 0; y < originalImage.Size.Height; y++)
                {
                    newPixels[x, y] = Image.White;
                }
            }

            Stack<Coordinate> list = new Stack<Coordinate>(); //Using a new stack, combined with an iterative way to loop through the nearby pixels, to prevent stackoverflow error
            list.Push(new Coordinate(X, Y));

            while (list.Count > 0)
            {
                Coordinate currentCoordinate = list.Pop();
                int x = currentCoordinate.x;
                int y = currentCoordinate.y;

                if (!LegitPixel(x, y)) continue;

                newPixels[x, y] = Image.Black;
                image[x, y] = Image.White;
                originalImage.SetPixelColor(x, y, Image.White);//Color the current pixel

                //Loop through neighbours. Ignoring diagonal neighbours. They will be visited indirectly...
                if (LegitPixel(x - 1, y))
                    list.Push(new Coordinate(x - 1, y));
                if (LegitPixel(x, y - 1))
                    list.Push(new Coordinate(x, y - 1));
                if (LegitPixel(x + 1, y))
                    list.Push(new Coordinate(x + 1, y));
                if (LegitPixel(x, y + 1))
                    list.Push(new Coordinate(x, y + 1));

            }

            //Ignore Objects that touch the border
            if(!(IgnoreObjectsNearBorder && ObjectNearBorder(newPixels)))
            {
                objects.Add(new Object(new Image(newPixels, originalImage.Size)));//Add object to object array
            }

        }
        private bool LegitPixel(int x, int y)
        {
            if (x < 0 || x >= originalImage.Size.Width || y < 0 || y >= originalImage.Size.Height) return false;    //Out of bounce
            if (originalImage.GetPixelColor(x, y) == Image.White) return false;        //If original is white then dont fill it in and quit looking at neighbours
            return true;
        }

        private bool ObjectNearBorder(int[,] pixels)
        {
            bool result = false;
            
            //Check first x row
            for(int x = 0; x < originalImage.Size.Width; x++)
            {
                if (pixels[x, 0] == Image.Black) result = true;
            }
            //Check last x row
            for (int x = 0; x < originalImage.Size.Width; x++)
            {
                if (pixels[x, originalImage.Size.Height -1] == Image.Black) result = true;
            }
            //check first y row
            for (int y = 0; y < originalImage.Size.Height; y++)
            {
                if (pixels[0, y] == Image.Black) result = true;
            }
            //check last y row
            for (int y = 0; y < originalImage.Size.Height; y++)
            {
                if (pixels[originalImage.Size.Width - 1, y] == Image.Black) result = true;
            }
            return result;
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
}
