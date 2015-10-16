using ImageProcessing.Operations;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class GUI : Form
    {
        private Bitmap InputImage;
        private Bitmap OutputImage;

        public GUI()
        {
            InitializeComponent();
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
           if (openImageDialog.ShowDialog() == DialogResult.OK)             // Open File Dialog
            {
                string file = openImageDialog.FileName;                     // Get the file name
                imageFileName.Text = file;                                  // Show file name
                if (InputImage != null) InputImage.Dispose();               // Reset image
                InputImage = new Bitmap(file);                              // Create new Bitmap from file
                if (InputImage.Size.Height <= 0 || InputImage.Size.Width <= 0 ||
                    InputImage.Size.Height > 512 || InputImage.Size.Width > 512) // Dimension check
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                    pictureBox1.Image = (System.Drawing.Image) InputImage;                 // Display input image
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (InputImage == null) return;                                 // Get out if no input image
            if (OutputImage != null) OutputImage.Dispose();                 // Reset output image
            OutputImage = new Bitmap(InputImage.Size.Width, InputImage.Size.Height); // Create new output image

            Image image = new Image(InputImage);

            
            /**********************/
            /* Setup progress bar */
            /**********************/

            //ResetProgressBar();
            //progressBar.PerformStep();
            //HideProgressBar();

            //==========================================================================================
            // TODO: include here your own code
            // Todo: Remove this code from the fucking GUI and put logic elsewhere...

            // create a grayscale image
            //image.Apply(Image.Operations.GreyScale);
            image.Apply(Operation.Operations.Smoothing);

            //Find Highest/Lowest Value
            //FindHighestLowestValue(Image);

            //Negative thresholding of the background
            image.Apply(Operation.Operations.NegativeThreshold);

            //Negative thresholding of the background
            //image.Apply(Operation.Operations.Opening);
            image.Apply(Operation.Operations.Closing);


            //Display Image
            DisplayOutputImage(image);

            //==========================================================================================


        }



        private void saveButton_Click(object sender, EventArgs e)
        {
            if (OutputImage == null) return;                                // Get out if no output image
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                OutputImage.Save(saveImageDialog.FileName);                 // Save the output image
        }

        private void ResetProgressBar()
        {
            progressBar.Visible = true;
            progressBar.Minimum = 1;
            //progressBar.Maximum = InputImage.Size.Width * InputImage.Size.Height;
            progressBar.Maximum = 4;
            progressBar.Value = 1;
            progressBar.Step = 1;
        }
        private void HideProgressBar()
        {
            progressBar.Visible = false;                                    // Hide progress bar
        }


        //private void FindHighestLowestValue(ref Image Image)
        //{
        //    //lowest / highest result
        //    int highest = Int32.MinValue;
        //    int lowest = Int32.MaxValue;
        //    for (int x = 0; x < InputImage.Size.Width; x++)
        //    {
        //        for (int y = 0; y < InputImage.Size.Height; y++)
        //        {
        //            Color pixelColor = Image.GetColor(x, y);                         // Get the pixel color at coordinate (x,y)
        //            int pixelIntValue = pixelColor.ToArgb();
        //            if (highest < pixelIntValue) highest = pixelIntValue;
        //            if (lowest > pixelIntValue) lowest = pixelIntValue;
        //            progressBar.PerformStep();                              // Increment progress bar
        //        }
        //    }
        //}

        


        private void DisplayOutputImage(Image Image)
        {
            // Copy array to output Bitmap
            for (int x = 0; x < InputImage.Size.Width; x++)
            {
                for (int y = 0; y < InputImage.Size.Height; y++)
                {
                    OutputImage.SetPixel(x, y, GreyScale.CreateColorFromGrayValue(Image.GetPixelColor(x, y)));               // Set the pixel color at coordinate (x,y)
                }
            }
            pictureBox2.Image = (System.Drawing.Image)OutputImage;                         // Display output image
        }

    }
}
