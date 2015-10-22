namespace ImageProcessing.Operations
{
    public class Closing: Operation
    {
        public Closing()
        {

        }

        public override void Apply(Image Image)
        {

            Image original = new Image(Image.GetPixels(), Image.Size);

            int intensity = 1;
            
            //Apply Dilation
            for(int i = 0; i < intensity; i++)
            Image.Apply(Operation.Operations.Dilation);

            //Apply Erosion
            for (int i = 0; i < intensity; i++)
                Image.Apply(Operation.Operations.Erosion);

        }
    }
}
