namespace ImageProcessing.Operations
{
    public class Opening : Operation
    {
        public Opening()
        {

        }

        public override void Apply(Image Image)
        {
            Image copy = new Image(Image.GetPixels(), Image.Size);

            //while (copy.GetPixels() != Image.GetPixels()) //While we aren't done doing the erosion -> this is still wrongly implemented
                Image.Apply(Operation.Operations.Erosion);
            Image.Apply(Operation.Operations.Erosion);
            Image.Apply(Operation.Operations.Erosion);
            Image.Apply(Operation.Operations.Erosion);

            Reconstruction.Apply(copy, Image);



        }
    }
}
