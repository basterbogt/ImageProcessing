using ImageProcessing.Kernels;
using ImageProcessing.Structures;
using System.Drawing;

namespace ImageProcessing.Operations
{
    public abstract class Operation
    {
        public enum Operations { Smoothing, Negative, NegativeThreshold, Opening, Erosion, Reconstruction };

        public Operation()
        {

        }

        public abstract void Apply(Image Image);

        public void ApplyKernel(Image Image, Kernel kernel)
        {
            int[,] currentPixels = Image.GetPixels();
            int[,] newPixels = new int[Image.Size.Width, Image.Size.Height];

            //kernel information
            int kernelWidth = kernel.kernelSize.Width;
            int kernelHeight = kernel.kernelSize.Height;
            int middelPixelIndexWidth = kernelWidth / 2;
            int middelPixelIndexHeight = kernelHeight / 2;

            //Loop through the image
            for (int x = 0; x < Image.Size.Width; x++)
            {
                for (int y = 0; y < Image.Size.Height; y++)
                {
                    newPixels[x, y] = currentPixels[x, y];//current quickfix untill the proper kernel-out-of-bounce-code is implemented. This line adds an initial value of the current pixel, based on the original image.

                    //Determin kernel's position, based on current pixel
                    int kernelStartPositionX = x - middelPixelIndexWidth;
                    int kernelStartPositionY = y - middelPixelIndexHeight;

                    //variable to store the new values that are being calculated
                    double ColourValue = 0;

                    //checks if the kernel isn't out of bounce
                    if (kernelStartPositionX < 0 || kernelStartPositionY < 0) continue;
                    if (kernelStartPositionX + kernel.kernelSize.Width > Image.Size.Width || kernelStartPositionY + kernel.kernelSize.Height > Image.Size.Height) continue;
                    //todo: change code so it will do something other then just 'not'changing the pixel, when the kernel is ou tof bounce

                    //Loop through the kernel
                    for (int k = 0; k < kernelWidth; k++)
                    {
                        for (int l = 0; l < kernelHeight; l++)
                        {
                            //Get the current kernel's position's color-value
                            int pixelColor = Image.GetPixelColor(kernelStartPositionX + k, kernelStartPositionY + l);
                            ColourValue += (pixelColor * (kernel.GetValue(l, k) * kernel.multiplier)); //calculates the value that has to be added, based on the kernel value and multiplier
                        }
                    }

                    newPixels[x, y] = (int)(ColourValue); //sets new value
                    
                }
            }

            Image.SetPixels(newPixels);


        }






    }
}
