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
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;
        public static int Current_X_Position, Current_Y_Position;
        public static int Clicked_X_Position, Clicked_Y_Position;
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
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Current_X_Position = e.X;
            Current_Y_Position = e.Y;

            label1.Text = e.X.ToString();
            label2.Text = e.Y.ToString();
        }

        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            Clicked_X_Position = e.X;
            Clicked_Y_Position = e.Y;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GraphOperations.generateGraph(ImageMatrix);
            GraphOperations.printGraph();
        }
    }
}