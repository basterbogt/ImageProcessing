using ImageProcessing.Kernels;

namespace ImageProcessing.Operations
{
    public class Smoothing: Operation
    {
        public Smoothing()
        {

        }


        public override void Apply(Image image)
        {
            Kernel kernel = new Kernel2D(1.0f / 9.0f);
            //kernel.SetKernelValues(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            kernel.SetKernelValues(1, 1, 1, 1, 1, 1, 1, 1, 1);

            ApplyKernel(image, kernel);
      
        }
    }
}
