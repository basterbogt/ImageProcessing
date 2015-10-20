﻿using ImageProcessing.Kernels;

namespace ImageProcessing.Operations
{
    class Edges : Operation
    {
        public Edges()
        {
        }

        public override void Apply(Image Image)
        {
            //TODO: keep start image in value
            Image temp_img = Image;

            Kernel kernel = new Kernel2D(1.0f);

            //edge 1
            kernel.SetKernelValues(-1, -2, -1,
                                    0,  0,  0,
                                    1,  2,  1);
            ApplyKernel(Image, kernel);

            //edge 2
            //kernel.SetKernelValues(-1, 0, 1, 
            //                       -2, 0, 2, 
            //                       -1, 0, 1);
            //ApplyKernel(Image, kernel);


            //TODO merge all edge images

            //convolve
            //kernel.SetKernelValues(0, 1, 0, 1, -4, 1, 0, 1, 0);
            //ApplyKernel(Image, kernel);

        }

    }
}
