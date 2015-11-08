using ImageProcessing.Structures;

namespace ImageProcessing.Operations
{
    /// <summary>
    /// Erosion: Removes the border of all objects in an image
    /// </summary>
    public class Erosion: Operation
    {
        public Erosion()
        {

        }

        /// <summary>
        /// Apply default Erosion
        /// </summary>
        /// <param name="image">Target Image</param>
        public override void Apply(Image image)
        {

            Structure structure = new Structure2D();
            structure.SetValues(1, 1, 1, 1, 1, 1, 1, 1, 1);

            ApplyErosion(image, structure);

        }

        /// <summary>
        /// Apply erosion with a given structure
        /// </summary>
        /// <param name="image">target image</param>
        /// <param name="structure">given structure</param>
        public void ApplyErosion(Image image, Structure structure)
        {
            int[,] currentPixels = image.GetPixels();
            int[,] newPixels = new int[image.Size.Width, image.Size.Height];

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
                            //Get the current structure's position's color-value
                            int pixelColor = image.GetPixelColor(structureStartPositionX + k, structureStartPositionY + l);
                            bool structureValue = structure.GetValue(k, l);

                            if (structureValue)
                            {
                                if(pixelColor == Image.White)
                                {
                                    pixelEnabled = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (pixelColor == Image.Black)
                                {
                                    pixelEnabled = false;
                                    break;
                                }
                            }

                        }
                    }
                    
                    newPixels[x, y] = (pixelEnabled) ? Image.Black : Image.White; //sets new value

                }
            }

            image.SetPixels(newPixels);

        }
    }
}
