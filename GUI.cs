using ImageProcessing.Filtering;
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

        private bool ProcessingDone = false;
        private int currentStep = 0;

        private Image image;

        public GUI()
        {
            InitializeComponent();
            applyButton.Enabled = false;
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
                {
                    image = null;
                    if (OutputImage != null)
                    {
                        OutputImage.Dispose();
                        OutputImage = null;
                    }
                    DisplayInputImage();                 // Display input image
                    currentStep = 0;
                    pictureBox2.Image = null;
                    this.Text = "Press 'apply' to start!";
                    applyButton.Enabled = true;
                    ProcessingDone = false;
                }
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (InputImage == null) return;                                 // Get out if no input image
            if (OutputImage != null)
            {
                InputImage.Dispose();
                InputImage = OutputImage;
                OutputImage = null;
                //DisplayInputImage();
            }
            OutputImage = new Bitmap(InputImage.Size.Width, InputImage.Size.Height); // Create new output image

            applyButton.Enabled = false;

            /**********************/
            /* Setup progress bar */
            /**********************/

            //ResetProgressBar();
            //progressBar.PerformStep();
            //HideProgressBar();

            //==========================================================================================
            // TODO: include here your own code
            // Todo: Remove this code from the fucking GUI and put logic elsewhere...


            ProcessImage();



            //==========================================================================================

            if(!ProcessingDone)
                applyButton.Enabled = true;

        }

        private void ProcessImage()
        {
            switch (currentStep)
            {
                case 0:
                    this.Text = "GrayValue";
                    this.image = new Image(InputImage);
                    break;

                case 1:
                    this.Text = "Smoothing";
                    image.Apply(Operation.Operations.Gaussian);
                    image.Apply(Operation.Operations.Gaussian);
                    break;

                case 2:
                    this.Text = "Edges";
                    

                    //image.Apply(Operation.Operations.Edges);
                    image.Apply(Operation.Operations.NegativeThreshold);

                    image.Apply(Operation.Operations.Opening);
                    image.Apply(Operation.Operations.Opening);

                    Image temp = new Image(image.GetPixels(), image.Size);
                    
                    image = new Image(image.GetPixels(), image.Size);

                    //image = Addition.Apply(image, temp);
                    //image = Difference.Apply(image, temp);

                    

                    //Image image2 = Maxval.Apply(new Image(image.GetPixels(), image.Size), temp);
                    image = new Image(image.GetPixels(), image.Size);
                    break;

                case 3:
                    this.Text = "Dilation";
                    /*
                    //deze inverse moet een toggle hebben voor licht of donker van het plaatje
                    if (AverageImageValue(image) > 128)
                    {
                        image.Apply(Operation.Operations.Inverse);
                    }
                    image.Apply(Operation.Operations.Dilation);
                    image.Apply(Operation.Operations.Dilation);
                    image.Apply(Operation.Operations.Dilation);
                    image.Apply(Operation.Operations.Dilation);

                    image.Apply(Operation.Operations.Inverse);
                    */                
                    break;

                case 4:
                    this.Text = "opening";
                    image.Apply(Operation.Operations.Opening);
                    break;

                case 5:
                    this.Text = "Colouring";
                    ObjectDetection od = new ObjectDetection(image, true);
                    od.Apply();
                    ObjectFiltering of = new ObjectFiltering(od.objects);
                    of.Apply();
                    image = new Coloring(of.coffeeMugObjectList).ConstructNewImage(image.Size);
                    image = new Image(image.GetPixels(), image.Size);
                    break;

                default:
                    this.Text = "Done";
                    ProcessingDone = true;
                    return;
            }

            currentStep++;
            //Display Image
            DisplayOutputImage(image);
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

        private void DisplayInputImage()
        {
            pictureBox1.Image = (System.Drawing.Image)InputImage;                 // Display input image
        }

        private int AverageImageValue(Image image)
        {
            int val = 0;

            for (int x = 0; x < image.Size.Width; x++)
            {
                for (int y = 0; y < image.Size.Height; y++)
                {
                    val+= (image.GetPixelColor(x, y));
                }
            }
            val = val / (image.Size.Width * image.Size.Height);
            return val;
        }

        private void DisplayOutputImage(Image Image)
        {
            Bitmap m = new Bitmap(Image.Size.Width, Image.Size.Height);
            // Copy array to output Bitmap
            for (int x = 0; x < m.Size.Width; x++)
            {
                for (int y = 0; y < m.Size.Height; y++)
                {
                    m.SetPixel(x, y, GreyScale.CreateColorFromGrayValue(Image.GetPixelColor(x, y)));               // Set the pixel color at coordinate (x,y)
                }
            }
            pictureBox2.Image = (System.Drawing.Image)m;                         // Display output image
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void GUI_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }
    }
}
