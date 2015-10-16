using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing.Operations
{
    public class Closing: Operation
    {
        public Closing()
        {

        }

        public override void Apply(Image Image)
        {

            Image copy = new Image(Image.GetPixels(), Image.Size);


            //while (copy.GetPixels() != Image.GetPixels()) //While we aren't done doing the erosion -> this is still wrongly implemented
            Image.Apply(Operation.Operations.Dilation);
            

        }
    }
}
