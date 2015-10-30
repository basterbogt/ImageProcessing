﻿using ImageProcessing.Kernels;

namespace ImageProcessing.Operations
{
    class Edges : Operation
    {
        public Edges()
        {
        }

        public override void Apply(Image image)
        {
            //TODO: keep start image in value
            Image temp_img = image;

            Kernel kernel = new Kernel2D(1.0f);

<<<<<<< HEAD
            //edge 1
            kernel.SetKernelValues(-1, -2, -1,
                                    0,  0,  0,
                                    1,  2,  1);
            ApplyKernel(image, kernel);

=======
>>>>>>> refs/remotes/origin/Koen-Edges
            //edge 2
            //kernel.SetKernelValues(-1, 0, 1, 
            //                       -2, 0, 2, 
            //                       -1, 0, 1);
            //ApplyKernel(Image, kernel);

            //edge 2
            //kernel.SetKernelValues(-2, -1, 0,
            //                       -1, 0, 1,
            //                      0, 1, 2);
            //ApplyKernel(Image, kernel);

            //edge 3
            //kernel.SetKernelValues(-1, -2, -1,
            //                        0,  0,  0,
            //                        1,  2,  1);
            //ApplyKernel(Image, kernel);

            //edge 4
            //kernel.SetKernelValues( 0, -1, -2,
            //                        1,  0, -1,
            //                        2,  1,  0);
            //ApplyKernel(Image, kernel);

            //edge 5
            //kernel.SetKernelValues( 1,  0, -1,
            //                        2,  0, -2,
            //                        1,  0, -1);
            //ApplyKernel(Image, kernel);

            //edge 6
            //kernel.SetKernelValues( 2,  1,  0,
            //                        1,  0, -1,
            //                        0, -1, -2);
            //ApplyKernel(Image, kernel);

            //edge 7
            //kernel.SetKernelValues( 1,  2,  1,
            //                        0,  0,  0,
            //                       -1, -2, -1);
            //ApplyKernel(Image, kernel);

            //edge 8
            //kernel.SetKernelValues( 0,  1,  2,
            //                       -1,  0,  1,
            //                       -2, -1,  0);
            //ApplyKernel(Image, kernel);

            //TODO merge all edge images


            //convolve extra step for thicker lines
            kernel.SetKernelValues(1, 2, 1,
                                   2,-12, 2, 
                                   1, 2, 1);
            ApplyKernel(Image, kernel);

        }

    }
}
