using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace IntelligentScissors
{
    
    public partial class MainForm : Form
    {
        public static int xPosition, Yposition;
        public MainForm()
        {
            Console.WriteLine("Here");
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();

        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value ;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);
        //    if(e.X > 4 && e.X < 431)
        //    {
        //        if(Y > 4 && Y < 364)
        //        {
        //            Console.WriteLine(e.X.ToString());
        //            Console.WriteLine(e.Y.ToString());
        //            label1.Text = "Done";
        //        }
        //    }

        //}
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            xPosition = e.X;
            Yposition = e.Y;

            label9.Text = e.X.ToString();
            label10.Text = e.Y.ToString();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}