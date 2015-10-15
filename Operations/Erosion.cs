using ImageProcessing.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessing.Operations
{
    public class Erosion: Operation
    {
        public Erosion()
        {

        }


        public override void Apply(Image Image)
        {

            Structure structure = new Structure2D();
            structure.SetValues(1, 1, 1, 1, 1, 1, 1, 1, 1);

            ApplyErosion(Image, structure);

        }


        public void ApplyErosion(Image Image, Structure structure)
        {
            //todo: apply Erosion
        }
    }
}
