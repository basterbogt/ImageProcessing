using System.Collections.Generic;
using System.Drawing;

namespace ImageProcessing.Filtering
{
    /// <summary>
    /// Recoloring of the current selected objects
    /// </summary>
    public class Coloring
    {
        private List<Object> list;
        public Coloring(List<Object> list)
        {
            this.list = list;
        }


        public Image ConstructNewImage(Size size)
        {
            int[,] image = new int[size.Width, size.Height];

            //initialise image
            for (int x = 0; x < size.Width; x++)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    image[x, y] = Image.White;
                }
            }

            //Loop through each object and paint it on the canvas...
            int totalCount = list.Count;
            int countSteps = Image.TotalGrayValues / (totalCount + 1); //256 total grayvalues
            int currentStep = countSteps;
            foreach (Object obj in list)
            {
                int[,] objImage = obj.image.GetPixels();
                for (int x = 0; x < obj.image.Size.Width; x++)
                {
                    for (int y = 0; y < obj.image.Size.Height; y++)
                    {
                        if (objImage[x, y] == Image.Black)
                        {
                            image[x + obj.xOnOriginalImage, y + obj.yOnOriginalImage] = currentStep;
                        }
                    }
                }
                currentStep += countSteps;
            }

            return new Image(image, size);
        }

    }
}
