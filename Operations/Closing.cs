namespace ImageProcessing.Operations
{
    /// <summary>
    /// Cloosing: Dilation followed by Erosion
    /// </summary>
    public class Closing: Operation
    {
        public Closing()
        {

        }

        /// <summary>
        /// Apply Dilation to the target image
        /// </summary>
        /// <param name="image">target image</param>
        public override void Apply(Image image)
        {

            Image original = new Image(image.GetPixels(), image.Size); //backup, used for reconstruction later

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
