namespace ImageProcessing.Operations
{
    /// <summary>
    /// Opening: (Erosion followed by Reconstruction)
    /// </summary>
    public class Opening : Operation
    {
        public Opening()
        {

        }

        /// <summary>
        /// Apply opening to the given image
        /// </summary>
        /// <param name="image"></param>
        public override void Apply(Image image)
        {
            Image original = new Image(image.GetPixels(), image.Size);
            
            int intensity = 4;
            
            //Apply Erosion
            for (int i = 0; i < intensity; i++)
                image.Apply(Operation.Operations.Erosion);

            //Reconstruct image
            int[,] newPixels = Reconstruction.Apply(original, image).GetPixels();

            //Update image with new pixels:
            image.SetPixels(newPixels);
            
        }
    }
}
