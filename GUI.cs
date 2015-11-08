using ImageProcessing.Filtering;
using ImageProcessing.Operations;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class GUI : Form
    {
        private Bitmap InputImage;
        private Bitmap OutputImage;

        private bool ProcessingDone = false;
        private int currentStep = 0;

        private Bitmap original;
        private Image image;
        private ObjectFiltering of;

        public GUI()
        {
            InitializeComponent();
            applyButton.Enabled = false;

            LoadInitialImage();
        }

        /// <summary>
        /// Load an initial image to prevent an empty screen
        /// </summary>
        private void LoadInitialImage()
        {
            string initialImage = @"cupotastic.png";
            if (File.Exists(initialImage))
            {
                LoadImage(initialImage);
            }

        }

        /// <summary>
        /// Listener when the user clicks on the 'load image button'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadImageButton_Click(object sender, EventArgs e)
        {
           if (openImageDialog.ShowDialog() == DialogResult.OK)             // Open File Dialog
            {
                string test = openImageDialog.FileName;
                LoadImage(test);
            }
        }

        /// <summary>
        /// Code to load an image
        /// </summary>
        /// <param name="file"></param>
        private void LoadImage(string file)
        {
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
                Array.ForEach(Directory.GetFiles(Program.ImageDirectory + @"\"), File.Delete);
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
            ProcessImage();
            //==========================================================================================


            if (!ProcessingDone)
            {
                //Display Image
                DisplayOutputImage(image);
                applyButton.Enabled = true;
            }

        }

        /// <summary>
        /// Image processing in steps:
        /// </summary>
        private void ProcessImage()
        {
            string text;
            switch (currentStep)
            {
                case 0:
                    text = "Image to Gray";
                    this.Text = text;
                    InputImage.Save(Image.GetFileName("Step " + (currentStep) + " - Original Image"), System.Drawing.Imaging.ImageFormat.Png);
                    image = new Image(InputImage);
                    image.Save("Step " + (currentStep + 1) + " - " + text);
                    break;
                case 1:
                    text = "Smoothing (Gaussian)";
                    this.Text = text;
                    image.Apply(Operation.Operations.Gaussian);
                    image.Apply(Operation.Operations.Gaussian);
                    image.Save("Step " + (currentStep + 1) + " - " + text);
                    break;
                case 2:
                    text = "NegativeThreshold";
                    this.Text = text;
                    image.Apply(Operation.Operations.NegativeThreshold);
                    image.Save("Step " + (currentStep + 1) + " - " + text);
                    break;
                case 3:
                    text = "Opening";
                    this.Text = text;
                    image.Apply(Operation.Operations.Opening);
                    image.Save("Step " + (currentStep + 1) + " - " + text);
                    break;
                case 4:
                    text = "Closing";
                    this.Text = text;
                    image.Apply(Operation.Operations.Closing);
                    image.Save("Step " + (currentStep + 1) + " - " + text);
                    break;
                case 5:
                    text = "Object Filtering";
                    this.Text = text;
                    ObjectDetection od = new ObjectDetection(image, true);
                    od.Apply();
                    of = new ObjectFiltering(od.objects);
                    of.Apply();
                    image = new Coloring(of.coffeeMugObjectList).ConstructNewImage(image.Size);
                    image.Save("Step " + (currentStep + 1) + " - " + text);
                    break;
                default:
                    text = "Result on Original Image";
                    this.Text = text;
                    ProcessingDone = true;
                    Bitmap result = new ShowResultOnOriginalImage(original, of.coffeeMugObjectList).ConstructNewImage();
                    DisplayOutputImage(result);
                    result.Save(Image.GetFileName("Step " + (currentStep + 1) + " - " + text), System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }

            if (text != null && text != "") this.label2.Text = text;

            currentStep++;

            return;
        }

        /// <summary>
        /// Save the image on the right side of the screen (output image)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (OutputImage == null) return;                                // Get out if no output image
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                image.SaveFullPath(saveImageDialog.FileName);
        }

        /// <summary>
        /// Reset the progressbar
        /// </summary>
        private void ResetProgressBar()
        {
            progressBar.Visible = true;
            progressBar.Minimum = 1;
            //progressBar.Maximum = InputImage.Size.Width * InputImage.Size.Height;
            progressBar.Maximum = 4;
            progressBar.Value = 1;
            progressBar.Step = 1;
        }

        /// <summary>
        /// Hide the progress bar
        /// </summary>
        private void HideProgressBar()
        {
            progressBar.Visible = false;                                    // Hide progress bar
        }

        /// <summary>
        /// Display InputImage 
        /// </summary>
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

        /// <summary>
        /// Display OutputImage based on an Bitmap
        /// </summary>
        /// <param name="image"></param>
        private void DisplayOutputImage(System.Drawing.Bitmap image)
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

        /// <summary>
        /// Display OutputImage based on an Image
        /// </summary>
        /// <param name="Image"></param>
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
            pictureBox2.Image = m;                         // Display output image
        }

        /// <summary>
        /// Show the program when clicking on the left bottom of your screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Hide the program from the windows bar, when the user minimizes the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GUI_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }
    }
}
