namespace ImageProcessing.Operations
{
    public class Closing: Operation
    {
        public Closing()
        {

        }

        public override void Apply(Image image)
        {

            Image original = new Image(image.GetPixels(), image.Size);

            int intensity = 4;
            
            //Apply Dilation
            for(int i = 0; i < intensity; i++)
            image.Apply(Operation.Operations.Dilation);

            //Apply Erosion
            for (int i = 0; i < intensity; i++)
                image.Apply(Operation.Operations.Erosion);

            //fix border:
            FixBorder.Fix(image, intensity);
        

        }
    }
}
