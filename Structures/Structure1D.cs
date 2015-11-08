using System;

namespace ImageProcessing.Structures
{
    /// <summary>
    /// Structure element with 1 dimension
    /// </summary>
    public class Structure1D: Structure
    {

        /// <summary>
        /// Possible rotations:
        /// </summary>
        public enum Alignment { Horizontal, Vertical }

        /// <summary>
        /// Current rotation
        /// </summary>
        public Alignment CurrentAlignment = Alignment.Horizontal;

        public Structure1D(Alignment alignment) : base()
        {
            CurrentAlignment = alignment;
        }

        /// <summary>
        /// Set the values of this structure
        /// </summary>
        /// <param name="values"></param>
        public override void SetValues(params byte[] values)
        {
            int RowSize = (CurrentAlignment.Equals(Alignment.Horizontal)) ? 1 : values.Length;
            int ColumnSize = (CurrentAlignment.Equals(Alignment.Horizontal)) ? values.Length : 1;

            StructureSize = new System.Drawing.Size(ColumnSize, RowSize);
            array = new bool[RowSize][];

            for (int row = 0; row < StructureSize.Height; row++)
            {
                array[row] = new bool[ColumnSize];
                for (int column = 0; column < StructureSize.Width; column++)
                {
                    array[row][column] = Convert.ToBoolean(values[((row * StructureSize.Width) + column)]);
                }
            }
        }
    }
}
