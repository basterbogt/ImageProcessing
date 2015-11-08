using System.Drawing;

namespace ImageProcessing.Structures
{
    /// <summary>
    /// Structure Element, used for applying Dilation, Erosion, Thinning, etc.
    /// </summary>
    public abstract class Structure
    {

        public Size StructureSize { set; get; } //The size of the structure
        public bool[][] array; //The values of the structure

        public Structure()
        {
            
        }
        
        
        public abstract void SetValues(params byte[] values);

        public bool GetValue(int column, int row = 0)
        {
            return this.array[row][column];
        }
        

    }
}
