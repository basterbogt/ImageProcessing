using System;

namespace ImageProcessing.Kernels
{
    public class Kernel2D : Kernel
    {

        public Kernel2D(double multiplier) : base(multiplier)
        {

        }

        public Kernel2D(double multiplier, double[][] array) //: base()
        {
            this.multiplier = multiplier;
            SetArray(array);
        }

        override public void SetKernelValues(params double[] values)
        {
            if (!Toolbox.IsPerferctSquare(values.Length))
            {
                throw new Exception("Kernel wrong size");
            }
            int newKernelSize = (int)(Math.Sqrt(values.Length));
            this.kernelSize = new System.Drawing.Size(newKernelSize, newKernelSize);
            this.array = new double[newKernelSize][];

            for (int row = 0; row < kernelSize.Height; row++)
            {
                array[row] = new double[newKernelSize];
                for (int column = 0; column < kernelSize.Width; column++)
                {
                    array[row][column] = values[((row + 1) * (column + 1)) - 1]; //adding 1 to get actual number, removing 1 to get the index number of that number
                }
            }
        }

        private void SetArray(double[][] array)
        {
            if (!array.Length.Equals(array[0].Length))
            {
                throw new Exception("Kernel wrong size: " + array.ToString());
            }
            this.array = array;
            this.kernelSize = new System.Drawing.Size(array.Length, array.Length);
        }


        override public double GetValue(int column, int row = 0)
        {
            return this.array[row][column];
        }
    }
}
