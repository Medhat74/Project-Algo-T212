using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        bool drawLine = false, drawLiveWire = false, randomPoint = false;
        public static int Current_X_Position, Current_Y_Position;
        public static int Clicked_X_Position, Clicked_Y_Position;
        private int anchorPoint = -1, freePoint = -1, currentPoint, liveWireAnchor;
        private List<int> path, liveWire;
        
        Pen drawPen = new Pen(Color.FromArgb(0, 255, 0), 2);
        Pen liveWirePen = new Pen(Color.FromArgb(255, 0, 0), 2);


        private List<Point> fullPathPoints = new List<Point>();

        private void button1_Click(object sender, EventArgs e)
        {
            anchorPoint = -1;
            drawLiveWire = false;
            drawLine = false;
            pictureBox1.Invalidate();
        }

        Point[] liveWirePoints;
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
            textOriginalX.Text = e.X.ToString();
            textOriginalY.Text = e.Y.ToString();
            textPixelNum.Text = Graphs.getIndex(Current_X_Position, Current_Y_Position).ToString();

            if (anchorPoint != -1)
            {
                currentPoint = Graphs.getIndex(Current_X_Position, Current_Y_Position);
                liveWire = Graphs.getPath(liveWireAnchor, currentPoint);
                if (liveWire != null)
                {
                    liveWirePoints = new Point[liveWire.Count];
                    for (int i = liveWire.Count - 1; i >= 0; i--)
                    {
                        liveWirePoints[i] = Graphs.nodeOfIndex(liveWire[i]);
                    }
                    
                }
                //else
                //    randomPoint = true;
                pictureBox1.Invalidate();  
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Clicked_X_Position = e.X;
            Clicked_Y_Position = e.Y;

            // Frist click
            if (anchorPoint == -1)
            {
                drawLiveWire = true;
                anchorPoint = Graphs.getIndex(Clicked_X_Position, Clicked_Y_Position);
                liveWireAnchor = anchorPoint;
                Console.WriteLine("anchor point   " + anchorPoint);
                Graphs.shortestPath(anchorPoint);
            }
            else
            {
                freePoint = Graphs.getIndex(Clicked_X_Position, Clicked_Y_Position);
                Console.WriteLine("free point   " + freePoint);
                //Graphs.shortestPath(freePoint);
                path = Graphs.getPath(anchorPoint, freePoint);

                if (path != null)
                {
                    Console.WriteLine("coming");
                    Point point;
                    for (int i = path.Count - 1; i >= 0; i--)
                    {
                        Console.WriteLine("having path");

                        point = Graphs.nodeOfIndex(path[i]);
                        fullPathPoints.Add(point);
                    }

                    Console.WriteLine("path is done");

                    drawLine = true;


                    pictureBox1.Invalidate();

                    //New Anchor Point
                    anchorPoint = freePoint;
                    liveWireAnchor = anchorPoint;
                    Graphs.shortestPath(anchorPoint);
                    fullPath.AddRange(path);
                }
                else
                    Console.WriteLine("saaaaaaaaaadddddd");
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //if (!click2)
            //    click1 = true;

            

            //Graphs.printGraph();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            
            if(drawLiveWire)
            {
                if (drawLine)
                {
                    Console.WriteLine("draw green");

                    e.Graphics.DrawLines(drawPen, fullPathPoints.ToArray());
                }
                if (liveWirePoints != null)
                    e.Graphics.DrawLines(liveWirePen, liveWirePoints);

                //if (randomPoint)
                //{
                    e.Graphics.DrawLine(liveWirePen, liveWirePoints[0].X,
                    liveWirePoints[0].Y, Current_X_Position, Current_Y_Position);
                    randomPoint = false;
                //}
            }

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            anchorPoint = -1;
        }
    }
}