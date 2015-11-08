using ImageProcessing.Structures;

namespace ImageProcessing.Operations
{
    /// <summary>
    /// Dilation: Adds a border around all objects in an image
    /// </summary>
    public class Dilation: Operation
    {
        public Dilation()
        {

        }

        /// <summary>
        /// Apply default dilation
        /// </summary>
        /// <param name="image">target image</param>
        public override void Apply(Image image)
        {

            Structure structure = new Structure2D();
            structure.SetValues(1, 1, 1, 1, 1, 1, 1, 1, 1);

            ApplyDilation(image, structure);
        }

        /// <summary>
        /// Apply specific dialation with a structure
        /// </summary>
        /// <param name="image">target image</param>
        /// <param name="structure">used structure</param>
        public void ApplyDilation(Image image, Structure structure)
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

                    if (image.GetPixelColor(x, y) == Image.White) continue;

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
                            
                            if (posX >= 0 && posX < image.Size.Width && posY >= 0 && posY < image.Size.Height) //check for out of bounce
                                 newPixels[posX, posY] = Image.Black;
                            
                        }
                    }
                    
                }
            }

            image.SetPixels(newPixels);

        }
    }
}
