using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
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
        string OpenedFilePath;
        bool isDrawLine = false, isDrawLiveWire = false, isDoubleClick = false, isAutoAnchor = false;
        public static int current_X_Position, current_Y_Position, clicked_X_Position, clicked_Y_Position;
        private int startPoint, anchorPoint = -1, currentPoint, liveWireAnchor, frequency;
        
        Pen liveWirePen = new Pen(Color.FromArgb(255, 0, 0), 2);
        Pen drawPen = new Pen(Color.FromArgb(0, 255, 0), 2);
        Pen rectanglePen = new Pen(Color.FromArgb(0, 0, 255), 2);

        Rectangle anchor;
        Size anchorSize = new Size(3,3);

        private List<Point> fullPathPoints = new List<Point>();
        private List<Point> anchorPointsList = new List<Point>();
        private List<Point> liveWirePoints;

        Point[] linePoints;
        Point lastPointOfValidLiveWire;

        Stopwatch stopwatch;

        /// <summary>
        /// Load the image and construct the graph 
        /// Complexity O(N^2)
        /// </summary>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            stopwatch = new Stopwatch();
            stopwatch.Start();
            Graphs.generateGraph(ImageMatrix);    //O(N^2)
            stopwatch.Stop();
            Console.WriteLine("Time of graph construction = " + stopwatch.Elapsed.ToString());
            //Graphs.printGraph();    //O(N^2)
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
            frequency = (int)frequencyValue.Value;     // O(1)
            isAutoAnchor = !isAutoAnchor;   // O(1)
            if (isAutoAnchor)
                ((Control)sender).BackColor = System.Drawing.SystemColors.GradientActiveCaption;   // O(1)
            else
                ((Control)sender).BackColor = System.Drawing.SystemColors.ControlLightLight;    // O(1)

        }

        /// <summary>
        /// Get the new frequency value
        /// Complexity O(1)
        /// </summary>
        private void frequencyValue_ValueChanged(object sender, EventArgs e)
        {
            frequency = (int)frequencyValue.Value;
        }

        /// <summary>
        /// Crop the selected area
        /// Complexity O(N)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCrop_Click(object sender, EventArgs e)
        {
            int largest_X = fullPathPoints[0].X, largest_Y = fullPathPoints[0].Y;
            int smallest_X = largest_X, smallest_Y = largest_Y;
            for (int i = 0; i < fullPathPoints.Count; i++)   // O(N) ** N is the number of points of the full path
            {
                if (fullPathPoints[i].X < smallest_X)
                    smallest_X = fullPathPoints[i].X;
                if (fullPathPoints[i].Y < smallest_Y)
                    smallest_Y = fullPathPoints[i].Y;
                if (fullPathPoints[i].X > largest_X)
                    largest_X = fullPathPoints[i].X;
                if (fullPathPoints[i].Y > largest_Y)
                    largest_Y = fullPathPoints[i].Y;

            }
            int width = (largest_X - smallest_X) + 1;
            int height = (largest_Y - smallest_Y) + 1;

            GraphicsPath gp = new GraphicsPath();
            gp.AddPolygon(fullPathPoints.ToArray());
            Bitmap orignalImage = (Bitmap)Bitmap.FromFile(OpenedFilePath);
            TextureBrush brush = new TextureBrush(orignalImage);
            Bitmap whiteBackGroundImage = new Bitmap(orignalImage.Width, orignalImage.Height);
            using (Graphics g = Graphics.FromImage(whiteBackGroundImage))
            {
                g.FillPolygon(brush, fullPathPoints.ToArray());
            }

            Rectangle cropRect = new Rectangle(smallest_X, smallest_Y, width, height);
            Bitmap cropedImage = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics G = Graphics.FromImage(cropedImage))
            {
                G.DrawImage(whiteBackGroundImage, new Rectangle(0, 0, cropedImage.Width, cropedImage.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }

            pictureBox2.Image = cropedImage;  
            gp.Dispose();

        }

        /// <summary>
        /// get the shortest path between anchor point and current mouse position
        /// Complexity O(N)
        /// </summary>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            current_X_Position = e.X;
            current_Y_Position = e.Y;
            textOriginalX.Text = e.X.ToString();
            textOriginalY.Text = e.Y.ToString();
            currentPoint = Graphs.getIndex(e.X, e.Y);    //O(1)
            textPixelNum.Text = currentPoint.ToString();

            
            if (anchorPoint != -1)
            {
                
                liveWirePoints = Graphs.getPath(liveWireAnchor, currentPoint);   // O(N) ** N is the number of pixels between 2 points **
                if (liveWirePoints != null)
                {
                    lastPointOfValidLiveWire = liveWirePoints[0];
                }
                
                pictureBox1.Invalidate();  //O(1)
            }
        }


        /// <summary>
        /// Get the position where the mouse clicked to add anchor
        /// Complexity O(E Log(V))
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            clicked_X_Position = e.X;
            clicked_Y_Position = e.Y;

            // O(E Log(V))
            if (anchorPoint == -1)
            {
                isDrawLiveWire = true;
                anchorPoint = Graphs.getIndex(clicked_X_Position, clicked_Y_Position);  //O(1)

                stopwatch.Restart();
                if (!Graphs.isValidAnchorPoint(anchorPoint))   //O(E Log(V))
                {
                    anchorPoint = Graphs.getAnchorPoint(anchorPoint);   //O(E Log(V))
                }
                stopwatch.Stop();
                Point point = Graphs.nodeOfIndex(anchorPoint);     // O(1)
                Console.WriteLine("Time of the shortest path from start point " + anchorPoint +
                    " (" + point.X + ", " + point.X + ") to all nodes = " + stopwatch.Elapsed.ToString());
                liveWireAnchor = startPoint = anchorPoint;
                anchorPointsList.Add(point); 
            }
            // O(E Log(V))
            else if (!isAutoAnchor)
            {
                putAnchor(Graphs.getIndex(lastPointOfValidLiveWire.X, lastPointOfValidLiveWire.Y));  // O(E Log(V))
            }
        }


        /// <summary>
        /// Place anchor at specific position (point)
        /// Complexity O(E Log(V))
        /// </summary>
        /// <param name="atPoint"></param>
        private void putAnchor(int atPoint)
        {

            linePoints = Graphs.getPath(anchorPoint, atPoint).ToArray();   // O(N)

            for (int i = linePoints.Length - 1; i >= 0; i--)  // O(N)
            {
                fullPathPoints.Add(linePoints[i]);
            }
            Point point = Graphs.nodeOfIndex(atPoint);   // O(1)
            anchorPointsList.Add(point);
            isDrawLine = true;
            pictureBox1.Invalidate();  // O(1)

            //New Anchor Point
            anchorPoint = liveWireAnchor = atPoint;

            stopwatch.Restart();

            Graphs.shortestPathTwoPoints(anchorPoint, startPoint);   // O(E Log(V))

            stopwatch.Stop();
            Point point2 = Graphs.nodeOfIndex(startPoint);   // O(1)
            Console.WriteLine("Time of the shortest path from point " + atPoint + " (" + point.X + ", " + point.X + ") to " +
                "the start point " + startPoint + " (" + point2.X + ", " + point2.X + ") = " + stopwatch.Elapsed.ToString());

        }


        /// <summary>
        /// Draw tha anchor points and the live wire and
        /// the line that connect between two anchor points
        /// Complexity O(N) as a worst case
        /// Complexity O(1) as best case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (isDrawLiveWire)
            {
                if (isDrawLine)
                {
                    
                    e.Graphics.DrawLines(drawPen, fullPathPoints.ToArray());   // O(N) ** where N is the number of lines


                    for (int i = 0; i < anchorPointsList.Count; i++)    // O(N) ** where N is the number of anchor points
                    {
                        anchor = new Rectangle(anchorPointsList[i], anchorSize);
                        e.Graphics.DrawRectangle(rectanglePen, anchor);  //O(1)

                    }
                }

                // O(N)
                if (liveWirePoints != null && !isDoubleClick)
                {
                    
                    e.Graphics.DrawLines(liveWirePen, liveWirePoints.ToArray());    // O(N) ** where N is the number of lines
                    if (isAutoAnchor)
                    {
                        
                        for (int i = 0; i < liveWirePoints.Count / frequency; i++)   // O(N) 
                        {
                            putAnchor(Graphs.getIndex(liveWirePoints[frequency * i].X, liveWirePoints[frequency * i].Y));
                        }
                    }
                }

                if (!isDoubleClick)
                    e.Graphics.DrawLine(liveWirePen, lastPointOfValidLiveWire.X,
                    lastPointOfValidLiveWire.Y, current_X_Position, current_Y_Position);  //O(1)

            }
        }

        /// <summary>
        /// Close the path 
        /// Comlexity O(E Log(V))
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            isDoubleClick = true;
            putAnchor(startPoint);  // O(E Log(V))
            anchorPoint = -1;
        }
    }
}