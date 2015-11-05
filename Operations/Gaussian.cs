using ImageProcessing.Kernels;

namespace ImageProcessing.Operations
{
    class Gaussian : Operation
    {
        public Gaussian()
        {

        }

        public override void Apply(Image image)
        {
            //TODO: keep start image in value
            Image temp_img = image;

            Kernel kernel = new Kernel2D(1f/273.0f);

            kernel.SetKernelValues(1, 4, 7, 4, 1,
                                   4, 16, 26, 16, 4,
                                   7, 26, 41, 26, 7,
                                   4, 16, 26, 16, 4,
                                   1, 4, 7, 4, 1);
            ApplyKernel(image, kernel);

        }
    }
}
