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
        private int startPoint, anchorPoint = -1, currentPoint, liveWireAnchor, frequency;
        private List<int> path, liveWire;
        
        Pen liveWirePen = new Pen(Color.FromArgb(255, 0, 0), 2);
        Pen drawPen = new Pen(Color.FromArgb(0, 255, 0), 2);
        Pen rectanglePen = new Pen(Color.FromArgb(0, 0, 255), 2);

        Rectangle anchor;
        Size anchorSize = new Size(3,3);

        private List<Point> fullPathPoints = new List<Point>();
        private List<Point> anchorPointsList = new List<Point>();
        Point[] liveWirePoints;
        
        /// <summary>
        /// Load the image and construct the graph 
        /// Complexity O(1) + O(N^2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            Graphs.generateGraph(ImageMatrix);    //O(N^2)
        }

        /// <summary>
        /// Clear drawn wire
        /// Complexity O(1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            anchorPoint = -1;
            isDrawLiveWire = false;
            isDrawLine = false;
            isDoubleClick = false;
        
            pictureBox1.Invalidate();
            fullPathPoints.Clear();
            anchorPointsList.Clear();
        }

        /// <summary>
        /// Start Auto Anchor mode
        /// Complexity O(1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoAnchor_Click(object sender, EventArgs e)
        {
            frequency = (int)frequencyValue.Value;
            isAutoAnchor = true;
        }

        /// <summary>
        /// Get the new frequency value
        /// Complexity O(1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frequencyValue_ValueChanged(object sender, EventArgs e)
        {
            frequency = (int)frequencyValue.Value;
            Console.WriteLine(frequency);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            current_X_Position = e.X;
            current_Y_Position = e.Y;
            textOriginalX.Text = e.X.ToString();
            textOriginalY.Text = e.Y.ToString();
            textPixelNum.Text = Graphs.getIndex(current_X_Position, current_Y_Position).ToString();

            if (anchorPoint != -1)
            {
                currentPoint = Graphs.getIndex(current_X_Position, current_Y_Position);  //O(1)
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
                
                
                if (!Graphs.isValidAnchorPoint(anchorPoint))
                {
                    anchorPoint = Graphs.getAnchorPoint(anchorPoint);
                }
                liveWireAnchor = startPoint = anchorPoint;
                anchorPointsList.Add(Graphs.nodeOfIndex(anchorPoint));
                Console.WriteLine("anchor point   " + anchorPoint);

            }
            
            else if(!isAutoAnchor)
            {
                putAnchor(Graphs.getIndex(liveWirePoints[0].X, liveWirePoints[0].Y));
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
                    Console.WriteLine("start poin =   "+atPoint+"anchor point =   "+anchorPoint);

                }
                else
                {
                    for (int i = path.Count - 1; i >= 0; i--)
                    {
                        fullPathPoints.Add(Graphs.nodeOfIndex(path[i]));
                    }
                }

                anchorPointsList.Add(Graphs.nodeOfIndex(atPoint));
                isDrawLine = true;
                pictureBox1.Invalidate();

                //New Anchor Point
                anchorPoint = liveWireAnchor = atPoint;
                Graphs.shortestPathTwoPoints(anchorPoint, startPoint);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (isDrawLiveWire)
            {
                if (isDrawLine)
                {
                    e.Graphics.DrawLines(drawPen, fullPathPoints.ToArray());
                    for(int i = 0; i < anchorPointsList.Count; i++)
                    {
                        anchor = new Rectangle(anchorPointsList[i], anchorSize);
                        e.Graphics.DrawRectangle(rectanglePen, anchor);

                    }
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
                if (!isDoubleClick)
                    e.Graphics.DrawLine(liveWirePen, liveWirePoints[0].X,
                    liveWirePoints[0].Y, current_X_Position, current_Y_Position);

            }

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            isDoubleClick = true;
            putAnchor(startPoint);
            anchorPoint = -1;
        }
    }
}