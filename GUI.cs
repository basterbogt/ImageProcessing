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

        private System.Drawing.Bitmap original;
        private Image image;
        private ObjectFiltering of;

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
                    original = (Bitmap)InputImage.Clone();
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

            ProcessImage();
            
            //==========================================================================================


            if (!ProcessingDone)
            {
                //Display Image
                DisplayOutputImage(image);
                applyButton.Enabled = true;
            }

        }

        private void ProcessImage()
        {
            string text;
            switch (currentStep)
            {
                case 0:
                    text = "Image to Gray";
                    this.Text = text + " <Calculating!>";
                    image = new Image(InputImage);
                    break;
                case 1:
                    text = "HistogramEqualization";
                    this.Text = text + " <Calculating!>";
                    image.Apply(Operation.Operations.HistogramEqualization);
                    break;
                case 2:
                    text = "Smoothing (Gaussian)";
                    this.Text = text + " <Calculating!>";
                    image.Apply(Operation.Operations.Gaussian);
                    image.Apply(Operation.Operations.Gaussian);
                    break;
                case 3:
                    text = "NegativeThreshold && Opening";
                    this.Text = text + " <Calculating!>";
                    image.Apply(Operation.Operations.NegativeThreshold);
                    image.Apply(Operation.Operations.Opening);
                    break;
                case 4:
                    text = "Inverse";
                    this.Text = text + " <Calculating!>";
                    //deze inverse moet een toggle hebben voor licht of donker van het plaatje
                    //if (AverageImageValue(image) > 128)
                    //{
                    //    image.Apply(Operation.Operations.Inverse);
                    //}             
                    break;
                case 5:
                    text = "Opening";
                    this.Text = text + " <Calculating!>";
                    image.Apply(Operation.Operations.Opening);
                    break;
                case 6:
                    text = "Closing";
                    this.Text = text + " <Calculating!>";
                    image.Apply(Operation.Operations.Closing);
                    break;
                case 7:
                    text = "Object Filtering";
                    this.Text = text + " <Calculating!>";
                    ObjectDetection od = new ObjectDetection(image, true);
                    od.Apply();
                    of = new ObjectFiltering(od.objects);
                    of.Apply();
                    image = new Coloring(of.coffeeMugObjectList).ConstructNewImage(image.Size);
                    break;
                default:
                    text = "Result on Original Image";
                    this.Text = text + " <Calculating!>";
                    ProcessingDone = true;
                    DisplayInputImage(new ShowResultOnOriginalImage(original, of.coffeeMugObjectList).ConstructNewImage());
                    break;
            }

            if (text != null && text != "") this.Text = text;

            currentStep++;

            return;
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            if (OutputImage == null) return;                                // Get out if no output image
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                image.SaveFullPath(saveImageDialog.FileName);
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
            Bitmap m = new Bitmap(InputImage.Size.Width, InputImage.Size.Height);
            // Copy array to output Bitmap
            for (int x = 0; x < m.Size.Width; x++)
            {
                for (int y = 0; y < m.Size.Height; y++)
                {
                    m.SetPixel(x, y, InputImage.GetPixel(x, y));               
                }
            }
            pictureBox1.Image = m;                 // Display input image
        }

        private void DisplayInputImage(System.Drawing.Bitmap image)
        {
            Bitmap m = new Bitmap(InputImage.Size.Width, InputImage.Size.Height);
            // Copy array to output Bitmap
            for (int x = 0; x < m.Size.Width; x++)
            {
                for (int y = 0; y < m.Size.Height; y++)
                {
                    m.SetPixel(x, y, image.GetPixel(x, y));
                }
            }
            pictureBox2.Image = m;                 // Display input image
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
