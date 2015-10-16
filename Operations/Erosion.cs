using ImageProcessing.Structures;

namespace ImageProcessing.Operations
{
    public class Erosion: Operation
    {
        public Erosion()
        {

        }


        public override void Apply(Image Image)
        {

            Structure structure = new Structure2D();
            structure.SetValues(1, 1, 1, 1, 1, 1, 1, 1, 1);

            ApplyErosion(Image, structure);

        }


        public void ApplyErosion(Image Image, Structure structure)
        {
            int[,] currentPixels = Image.GetPixels();
            int[,] newPixels = new int[Image.Size.Width, Image.Size.Height];

            //structure information
            int structureWidth = structure.StructureSize.Width;
            int structureHeight = structure.StructureSize.Height;
            int middelPixelIndexWidth = structureWidth / 2;
            int middelPixelIndexHeight = structureHeight / 2;

            //Loop through the image
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {

                    //Determin structure's position, based on current pixel
                    int structureStartPositionX = x - middelPixelIndexWidth;
                    int structureStartPositionY = y - middelPixelIndexHeight;

                    //variable to store the new values that are being calculated
                    bool pixelEnabled = true;

                    //checks if the structure isn't out of bounce
                    if (structureStartPositionX < 0 || structureStartPositionY < 0) continue;
                    if (structureStartPositionX + structureWidth > Image.Size.Width || structureStartPositionY + structureHeight > Image.Size.Height) continue;
                    //todo: change code so it will do something other then just 'not'changing the pixel, when the structure is ou tof bounce

                    //Loop through the structure
                    for (int k = 0; k < structureWidth; k++)
                    {
                        for (int l = 0; l < structureHeight; l++)
                        {
                            //Get the current structure's position's color-value
                            int pixelColor = Image.GetPixelColor(structureStartPositionX + k, structureStartPositionY + l);
                            bool structureValue = structure.GetValue(l, k);

                            if (structureValue)
                            {
                                if(pixelColor > 128)
                                {
                                    pixelEnabled = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (pixelColor < 128)
                                {
                                    pixelEnabled = false;
                                    break;
                                }
                            }

                        }
                    }
                    
                    newPixels[x, y] = (pixelEnabled) ? 0 : 255; //sets new value

                }
            }

            Image.SetPixels(newPixels);

        }
    }
}
