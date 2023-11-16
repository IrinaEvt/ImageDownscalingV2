using System;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace ImageDownsizerParallel
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())

            {

                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    originalImgPB.Image = new Bitmap(openFileDialog.FileName);
                }
            }

        }


        private void buttonDownsizeImage_Click(object sender, EventArgs e)
        {

           // int scalePercentage = Int32.Parse(percentageTB.Text);

            //double scale = (double)scalePercentage / 100;

            int scalePercentage;
            double scale = 0.0;
            int rectangeSize = 0;
            if (int.TryParse(percentageTB.Text, out scalePercentage))
            {
                scale = (double)scalePercentage / 100;
            }
            else if(percentageTB.Text is null)
            {
                MessageBox.Show("Invalid input. Please enter a valid percentage.");
            }
            else {
                MessageBox.Show("Invalid input. Please enter a valid percentage.");
            }

            switch (scale)
            {
                case 0.1:
                case 0.2:
                    rectangeSize = 5;
                    break;

                case 0.3:
                case 0.4:
                case 0.5:
                    rectangeSize = 4;
                    break;

                default:
                    rectangeSize = 3;
                    break;
            }





            Bitmap originalImage = (Bitmap)originalImgPB.Image;
            DownscalingService downscalingService = new DownscalingService(originalImage);


            byte[] imageBytes = downscalingService.ConvertImageToArray();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            byte[] downsizedImageBytes = downscalingService.DownscaleImage(imageBytes, scale, rectangeSize);
           

            Bitmap downsizedBitmap = downscalingService.ConvertArrayToBitmap(downsizedImageBytes, scale);


            downscaledImgPB.Image = downsizedBitmap;

            stopwatch.Stop();
            MessageBox.Show("Processing time: " + stopwatch.Elapsed.TotalSeconds);

        }


        
    }
}