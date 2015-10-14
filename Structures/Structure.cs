using System.Drawing;

namespace ImageProcessing.Structures
{
    public abstract class Structure
    {

        public Size StructureSize { set; get; }
        public bool[][] array;

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
