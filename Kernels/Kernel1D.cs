namespace ImageProcessing.Kernels
{
    public class Kernel1D: Kernel
    {
        public enum Alignment { Horizontal, Vertical }

        public Alignment CurrentAlignment = Alignment.Horizontal;

        public Kernel1D(Alignment alignment) : base()
        {
            CurrentAlignment = alignment;
        }

        public Kernel1D(Alignment alignment, double multiplier) : base(multiplier)
        {
            CurrentAlignment = alignment;
        }


        override public void SetKernelValues(params double[] values)
        {
            int kernelRowSize = (CurrentAlignment.Equals(Alignment.Horizontal))? 1: values.Length;
            int kernelColumnSize = (CurrentAlignment.Equals(Alignment.Horizontal)) ? values.Length : 1;

            kernelSize = new System.Drawing.Size(kernelColumnSize, kernelRowSize);
            array = new double[kernelRowSize][];
            
            for (int row = 0; row < kernelSize.Height; row++)
            {
                array[row] = new double[kernelColumnSize];
                for (int column = 0; column < kernelSize.Width; column++)
                {
                    array[row][column] = values[((row * kernelSize.Width) + column)];
                }
            }

        }


        override public double GetValue(int column, int row = 0)
        {
            //todo: pretty sure this doesnt work quite yet: .. bas 14/10/15
            return this.array[0][column];
        }

    }
}
