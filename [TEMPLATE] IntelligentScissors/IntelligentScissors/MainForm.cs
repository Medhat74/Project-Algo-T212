using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Graphs = IntelligentScissors.GraphOperations;

namespace IntelligentScissors
{
    
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;
        bool click1 = false, click2 = false;
        public static int Current_X_Position, Current_Y_Position;
        public static int Clicked_X_Position, Clicked_Y_Position;
        private int anchorPoint, freePoint, currentPoint, liveWireAnchor;
        private List<int> path, liveWire;
        private List<int> fullLiveWire = new List<int>();
        private List<int> fullPath = new List<int>();
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
            Graphs.generateGraph(ImageMatrix);
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Current_X_Position = e.X;
            Current_Y_Position = e.Y;
            //if (click1 || click2)
            //{
            //    currentPoint = Graphs.getIndex(Current_X_Position, Current_Y_Position);
            //    liveWire = Graphs.shortestPath(liveWireAnchor, currentPoint);
            //    for (int i = liveWire.Count - 1; i >= 0; i--)
            //    {
            //        Graphs.Node node;
            //        node = Graphs.nodeOfIndex(liveWire[i]);
            //        Console.WriteLine("point   " + liveWire[i] + "   at position ( " + node.X + ", " + node.Y + " )");
            //        ((Bitmap)pictureBox1.Image).SetPixel(node.X, node.Y, Color.FromArgb(0, 0, 0));

            //        pictureBox1.Refresh();
            //    }
            //    liveWireAnchor = currentPoint;
            //    fullLiveWire.AddRange(liveWire);
            //}
            label1.Text = e.X.ToString();
            label2.Text = e.Y.ToString();
        }

        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            Clicked_X_Position = e.X;
            Clicked_Y_Position = e.Y;
            if (click1)
            {
                click1 = false;
                click2 = true;
                freePoint = Graphs.getIndex(Clicked_X_Position, Clicked_Y_Position);
                path = Graphs.shortestPath(freePoint, freePoint);
            }
            else if (click2)
            {
                if (path.Count != 0)
                    anchorPoint = freePoint;
                freePoint = Graphs.getIndex(Clicked_X_Position, Clicked_Y_Position);
                path = Graphs.shortestPath(anchorPoint, freePoint);
                Graphs.Node node;
                
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    
                    node = Graphs.nodeOfIndex(path[i]);
                    Console.WriteLine("point   " + path[i]+"   at position ( "+node.X+", "+node.Y+" )");
                    ((Bitmap)pictureBox1.Image).SetPixel(node.X, node.Y, Color.FromArgb(0, 255, 0));
                    
                    pictureBox1.Refresh();
                }
                fullPath.AddRange(path);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!click2)
                click1 = true;
            //Graphs.printGraph();
        }
    }
}