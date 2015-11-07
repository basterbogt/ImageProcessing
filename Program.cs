using System;
using System.IO;
using System.Windows.Forms;

namespace ImageProcessing
{
    static class Program
    {
        public static string ImageDirectory = @"Images";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Array.ForEach(Directory.GetFiles(ImageDirectory + @"\"), File.Delete);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI());
        }
    }
}
