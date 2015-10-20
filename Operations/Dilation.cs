using ImageProcessing.Structures;

namespace ImageProcessing.Operations
{
    public class Dilation: Operation
    {
        public Dilation()
        {

        }

        public override void Apply(Image Image)
        {

            Structure structure = new Structure2D();
            structure.SetValues(1, 1, 1, 1, 1, 1, 1, 1, 1);

            ApplyDilation(Image, structure);
        }


        public void ApplyDilation(Image Image, Structure structure)
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
                    newPixels[x, y] = Image.White;//Default White value

                    if (Image.GetPixelColor(x, y) > Image.Gray) continue;

                    //Determin structure's position, based on current pixel
                    int structureStartPositionX = x - middelPixelIndexWidth;
                    int structureStartPositionY = y - middelPixelIndexHeight;
                    
                    //Loop through the structure
                    for (int k = 0; k < structureWidth; k++)
                    {
                        for (int l = 0; l < structureHeight; l++)
                        {
                            bool structureValue = structure.GetValue(k, l);
                            if (!structureValue) continue;

                            int posX = structureStartPositionX + k;
                            int posY = structureStartPositionY + l;
                            
                            if (posX >= 0 && posX < Image.Size.Width && posY >= 0 && posY < Image.Size.Height) //check for out of bounce
                                 newPixels[posX, posY] = 0;


                        }
                    }
                    
                }
            }

            Image.SetPixels(newPixels);

        }
    }
}
