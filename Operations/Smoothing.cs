﻿using ImageProcessing.Kernels;

namespace ImageProcessing.Operations
{
    /// <summary>
    /// Operation used to smooth the image
    /// </summary>
    public class Smoothing: Operation
    {
        public Smoothing()
        {

        }

        /// <summary>
        /// Apply smoothing
        /// </summary>
        /// <param name="image"></param>
        public override void Apply(Image image)
        {
            Kernel kernel = new Kernel2D(1.0f / 25.0f);
            kernel.SetKernelValues(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
           //kernel.SetKernelValues(1, 1, 1, 1, 1, 1, 1, 1, 1);

            ApplyKernel(image, kernel);
      
        }
    }
}
