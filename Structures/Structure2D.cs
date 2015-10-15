using System;

namespace ImageProcessing.Structures
{
    class Structure2D: Structure
    {
        public Structure2D(): base()
        {

        }


        public override void SetValues(params byte[] values)
        {
            if (!Toolbox.IsPerferctSquare(values.Length))
            {
                throw new Exception("Structure wrong size");
            }

            int newSize = (int)(Math.Sqrt(values.Length));
            this.StructureSize = new System.Drawing.Size(newSize, newSize);
            this.array = new bool[newSize][];

            for (int row = 0; row < StructureSize.Height; row++)
            {
                array[row] = new bool[newSize];
                for (int column = 0; column < StructureSize.Width; column++)
                {
                    array[row][column] = Convert.ToBoolean(values[((row + 1) * (column + 1)) - 1]); //adding 1 to get actual number, removing 1 to get the index number of that number
                }
            }
        }
    }
}
