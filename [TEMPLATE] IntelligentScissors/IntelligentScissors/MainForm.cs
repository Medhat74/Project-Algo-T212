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
        bool isDrawLine = false, isDrawLiveWire = false, isDoubleClick = false, isAutoAnchor = false;
        public static int current_X_Position, current_Y_Position, clicked_X_Position, clicked_Y_Position;
        private int startPoint, anchorPoint = -1, freePoint = -1, currentPoint, liveWireAnchor, frequency;
        private List<int> path, liveWire;
        
        Pen drawPen = new Pen(Color.FromArgb(0, 255, 0), 2);
        Pen liveWirePen = new Pen(Color.FromArgb(255, 0, 0), 2);

        private List<Point> fullPathPoints = new List<Point>();
        Point[] liveWirePoints;
        
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            anchorPoint = -1;
            isDrawLiveWire = false;
            isDrawLine = false;
            isDoubleClick = false;
            
            pictureBox1.Invalidate();
            fullPathPoints.Clear();
            

        }

        private void btnAutoAnchor_Click(object sender, EventArgs e)
        {
            frequency = (int)frequencyValue.Value;
            isAutoAnchor = true;
        }

        private void frequencyValue_ValueChanged(object sender, EventArgs e)
        {
            frequency = (int)frequencyValue.Value;
            Console.WriteLine(frequency);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            current_X_Position = e.X;
            current_Y_Position = e.Y;
            textOriginalX.Text = e.X.ToString();
            textOriginalY.Text = e.Y.ToString();
            textPixelNum.Text = Graphs.getIndex(current_X_Position, current_Y_Position).ToString();

            if (anchorPoint != -1)
            {
                currentPoint = Graphs.getIndex(current_X_Position, current_Y_Position);
                //Graphs.shortestPathTwoPoints(liveWireAnchor, currentPoint);
                liveWire = Graphs.getPath(liveWireAnchor, currentPoint);
                if (liveWire != null)
                {
                    liveWirePoints = new Point[liveWire.Count];
                    for (int i = liveWire.Count - 1; i >= 0; i--)
                    {
                        liveWirePoints[i] = Graphs.nodeOfIndex(liveWire[i]);
                    }
                    
                }
                
                pictureBox1.Invalidate();  
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            clicked_X_Position = e.X;
            clicked_Y_Position = e.Y;

            // Frist click
            if (anchorPoint == -1)
            {
                isDrawLiveWire = true;
                anchorPoint = Graphs.getIndex(clicked_X_Position, clicked_Y_Position);
                liveWireAnchor = startPoint = anchorPoint;
                Console.WriteLine("anchor point   " + anchorPoint);
                Graphs.shortestPath(anchorPoint);
            }
            
            else if(!isAutoAnchor)
            {
                freePoint = Graphs.getIndex(clicked_X_Position, clicked_Y_Position);
                Console.WriteLine("free point   " + freePoint);
                putAnchor(freePoint);
            }
        }

        private void putAnchor(int atPoint)
        {
            
            path = Graphs.getPath(anchorPoint, atPoint);
            if (Graphs.isValidPoint(atPoint))
            {
                if (isDoubleClick)
                {
                    path = Graphs.getPath(anchorPoint, atPoint);
                    Point point;
                    for (int i = path.Count - 1; i >= 0; i--)
                    {
                        point = Graphs.nodeOfIndex(path[i]);
                        fullPathPoints.Add(point);
                    }
                }
                else
                {
                    for (int i = path.Count - 1; i >= 0; i--)
                    {
                        fullPathPoints.Add(Graphs.nodeOfIndex(path[i]));
                        //fullPathPoints.Add(liveWirePoints[i]);
                    }
                }
                

                isDrawLine = true;
                pictureBox1.Invalidate();

                //New Anchor Point
                anchorPoint = liveWireAnchor = atPoint;
                //liveWireAnchor = anchorPoint;
                Graphs.shortestPathTwoPoints(anchorPoint, startPoint);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (isDrawLiveWire)
            {
                if (isDrawLine)
                {
                    Console.WriteLine("draw green");

                    e.Graphics.DrawLines(drawPen, fullPathPoints.ToArray());
                }
                if (liveWirePoints != null)
                {
                    e.Graphics.DrawLines(liveWirePen, liveWirePoints);
                    if (isAutoAnchor)
                    {
                        for (int i = 0; i < liveWirePoints.Length / frequency; i++)
                        {
                            putAnchor(Graphs.getIndex(liveWirePoints[frequency * i].X, liveWirePoints[frequency * i].Y));
                        }
                    }
                }

                e.Graphics.DrawLine(liveWirePen, liveWirePoints[0].X,
                liveWirePoints[0].Y, current_X_Position, current_Y_Position);

            }

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            isDoubleClick = true;
            freePoint = startPoint;
            putAnchor(startPoint);
            anchorPoint = -1;
        }
    }
}