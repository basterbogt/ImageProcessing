using ImageProcessing.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Filtering.ShapeMeasures
{
    public class SkeletonByThinning
    {
        public static int Intensity = 1;

        public static Image GenerateSkeleton(Image image)
        {
            Image skeleton = new Image(image.GetPixels(), image.Size);

            DoubleStructure2D L1 = new DoubleStructure2D(
                                    new Structure2D(0, 0, 0, 
                                                    0, 1, 0, 
                                                    1, 1, 1), 
                                    new Structure2D(1, 1, 1,
                                                    0, 0, 0, 
                                                    0, 0, 0), skeleton.Size);

            DoubleStructure2D L2 = new DoubleStructure2D(
                                    new Structure2D(0, 0, 0,
                                                    1, 1, 0,
                                                    1, 1, 0),
                                    new Structure2D(0, 1, 0,
                                                    0, 0, 1,
                                                    0, 0, 0), skeleton.Size);

            DoubleStructure2D L3 = new DoubleStructure2D(
                                    new Structure2D(1, 0, 0,
                                                    1, 1, 0,
                                                    1, 0, 0),
                                    new Structure2D(0, 0, 1,
                                                    0, 0, 1,
                                                    0, 0, 1), skeleton.Size);

            DoubleStructure2D L4 = new DoubleStructure2D(
                                    new Structure2D(1, 1, 0,
                                                    1, 1, 0,
                                                    0, 0, 0),
                                    new Structure2D(0, 0, 0,
                                                    0, 0, 1,
                                                    0, 1, 0), skeleton.Size);

            DoubleStructure2D L5 = new DoubleStructure2D(
                                    new Structure2D(1, 1, 1,
                                                    0, 1, 0,
                                                    0, 0, 0),
                                    new Structure2D(0, 0, 0,
                                                    0, 0, 0,
                                                    1, 1, 1), skeleton.Size);

            DoubleStructure2D L6 = new DoubleStructure2D(
                                    new Structure2D(0, 1, 1,
                                                    0, 1, 1,
                                                    0, 0, 0),
                                    new Structure2D(0, 0, 0,
                                                    1, 0, 0,
                                                    0, 1, 0), skeleton.Size);

            DoubleStructure2D L7 = new DoubleStructure2D(
                                    new Structure2D(0, 0, 1,
                                                    0, 1, 1,
                                                    0, 0, 1),
                                    new Structure2D(1, 0, 0,
                                                    1, 0, 0,
                                                    1, 0, 0), skeleton.Size);

            DoubleStructure2D L8 = new DoubleStructure2D(
                                    new Structure2D(0, 0, 0,
                                                    0, 1, 1,
                                                    0, 1, 1),
                                    new Structure2D(0, 1, 0,
                                                    1, 0, 0,
                                                    0, 0, 0), skeleton.Size);

            int[,] currentImage;
            while (true)
            {
                currentImage = (int[,])skeleton.GetPixels().Clone();
                ApplyDoubleStructure(image, L1);
                ApplyDoubleStructure(image, L2);
                ApplyDoubleStructure(image, L3);
                ApplyDoubleStructure(image, L4);
                ApplyDoubleStructure(image, L5);
                ApplyDoubleStructure(image, L6);
                ApplyDoubleStructure(image, L7);
                ApplyDoubleStructure(image, L8);
                if (Toolbox.EqualPixels(currentImage, skeleton.GetPixels(), skeleton.Size)) break;
            }





            return skeleton;
        }


        public static void ApplyDoubleStructure(Image image, DoubleStructure2D structure)
        {

            int[,] currentPixels = image.GetPixels();
            int[,] newPixels = new int[image.Size.Width, image.Size.Height];

            //initialise image
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {

                    newPixels[x, y] = Image.White;//Default White value
                }
            }

            //structure information
            int structureWidth = structure.StructureSize.Width;
            int structureHeight = structure.StructureSize.Height;
            int middelPixelIndexWidth = structureWidth / 2;
            int middelPixelIndexHeight = structureHeight / 2;

            //Loop through the image
            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    
                    //Determin structure's position, based on current pixel
                    int structureStartPositionX = x - middelPixelIndexWidth;
                    int structureStartPositionY = y - middelPixelIndexHeight;

                    //variable to store the new values that are being calculated
                    bool pixelEnabled = true;

                    //checks if the structure isn't out of bounce
                    if ((structureStartPositionX < 0 || structureStartPositionY < 0) ||
                     (structureStartPositionX + structureWidth > image.Size.Width || structureStartPositionY + structureHeight > image.Size.Height))
                    {
                        newPixels[x, y] = Image.White;
                        continue;
                    }

                    //Loop through the structure
                    for (int k = 0; k < structureWidth; k++)
                    {
                        for (int l = 0; l < structureHeight; l++)
                        {
                            int pixelColor = image.GetPixelColor(structureStartPositionX + k, structureStartPositionY + l);

                            bool ForeGroundValue = structure.Foreground.GetValue(k, l);
                            bool BackGroundValue = structure.Background.GetValue(k, l);

                            if (ForeGroundValue && pixelColor == Image.Black) continue;
                            if (BackGroundValue && pixelColor == Image.White) continue;

                            pixelEnabled = false;
                            break;

                        }
                    }
                    newPixels[x, y] = (pixelEnabled) ? Image.Black : Image.White; //sets new value

                }
            }
            image.SetPixels(newPixels);
        }
    }
}
