using System;

namespace ImageProcessing.Structures
{
    /// <summary>
    /// Structure2D class. A structure object with 2 dimensions
    /// </summary>
    public class Structure2D: Structure
    {
        public Structure2D(): base()
        {

        }
        public Structure2D(params byte[] values) : base()
        {
            SetValues(values);
        }

        /// <summary>
        /// Set the values of this structure
        /// </summary>
        /// <param name="values"></param>
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
                    array[row][column] = Convert.ToBoolean(values[((row * StructureSize.Width) + column)]);
                }
            }
        }
    }
}
