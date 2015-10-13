using System.Drawing;

namespace ImageProcessing.Kernels
{

    public abstract class Kernel
    {
        public double multiplier { set; get; }
        public Size kernelSize { set; get; }

        protected double[][] array;

        public Kernel() {
            this.multiplier = 1;
        }
        public Kernel(double multiplier)
        {
            this.multiplier = multiplier;
        }

        public abstract void SetKernelValues(params double[] values);
        public abstract double GetValue(int column, int row = 0);





    }
    



}
