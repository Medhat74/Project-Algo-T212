using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static IntelligentScissors.GraphOperations;
using System.IO;

namespace IntelligentScissors
{
    using Graph = Dictionary<int, List<Pair>>;
    internal class GraphOperations
    {
        private const double INF = double.PositiveInfinity;
        public static Graph graph;
        public static int width;
        public static int height;
        public static List<int> parent;
        public static List<bool> visited;
        public static int size;
        public class Pair
        {
            public Pair() { }
            public Pair(double cost, Point node)
            {
                this.cost = cost;
                this.destination = getIndex(node.X, node.Y);
            }
            public Pair(double cost, int x, int y)
            {
                this.cost = cost;
                destination = getIndex(x, y);
            }
            public double cost { get; set; }
            public int destination { get; set; }
        }
        

        /// <summary>
        /// Complexity O(1)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Point nodeOfIndex(int index)
        {
            int x = index % width;
            int y = (index - x) / width;
            return new Point(x, y);
        }

        /// <summary>
        /// Complexity O(1)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int getIndex(int x, int y)
        {
            return (width * y) + x;
        }

        /// <summary>
        /// Complexity O(N^2)
        /// </summary>
        /// <param name="ImageMatrix"></param>
        public static void generateGraph(RGBPixel[,] ImageMatrix)
        {
            graph = new Graph();
            width = ImageOperations.GetWidth(ImageMatrix);
            height = ImageOperations.GetHeight(ImageMatrix);
            size = width * height;

            //////O(N^2)
            for (int y = 0; y < height; y++)  //O(N)
            {
                for (int x = 0; x < width; x++)  //O(N)
                {
                    graph[getIndex(x, y)] = new List<Pair>();  //O(1)
                }
            }

            //////O(N^2)
            for (int y = 0; y < height; y++)   //O(N)
            {
                for (int x = 0; x < width; x++)   //O(N)
                {

                    Vector2D cost = ImageOperations.CalculatePixelEnergies(x, y, ImageMatrix);  //O(1)
                    
                    ///////O(1)
                    if ((x == width - 1) && (y < height - 1))
                    {
                        //No Right Nodes only store Bottom cost
                        graph[getIndex(x, y)].Add(new Pair(1 / cost.Y, x, y + 1));
                        graph[getIndex(x, y + 1)].Add(new Pair(1 / cost.Y, x, y));

                    }
                    else if ((x < width - 1) && (y == height - 1))
                    {
                        //No Bottom Nodes only store Right cost
                        graph[getIndex(x, y)].Add(new Pair(1 / cost.X, x + 1, y));
                        graph[getIndex(x + 1, y)].Add(new Pair(1 / cost.X, x, y));

                    }
                    else if ((x < width - 1) && (y < height - 1))
                    {

                        graph[getIndex(x, y)].Add(new Pair(1 / cost.X, x + 1, y));
                        graph[getIndex(x, y)].Add(new Pair(1 / cost.Y, x, y + 1));
                        graph[getIndex(x + 1, y)].Add(new Pair(1 / cost.X, x, y));
                        graph[getIndex(x, y + 1)].Add(new Pair(1 / cost.Y, x, y));

                    }
                }
            }
        }

        /// <summary>
        /// Complexity O(1)
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool isValidPoint(int point)
        {
            return visited[point];
        }

        /// <summary>
        /// Complexity O(1)
        /// </summary>
        /// <param name="anchorPoint"></param>
        /// <returns></returns>
        public static int getPoint(int anchorPoint)
        {
            Point point = nodeOfIndex(anchorPoint);

            for (int n = 1; n <= 10; n++)
            {
                for (int i = point.Y - (10*n); i <= point.Y + (10*n); i++)
                {
                    for (int j = point.X - (10 * n); j <= point.X + (10*n); j++)
                    {
                        int index = getIndex(j, i);  //O(1)
                        if (index < size && index > -1)
                        {
                            if (isValidPoint(index))   //O(1)
                                return index;
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Complexity O(E Log(V))
        /// </summary>
        /// <param name="anchorPoint"></param>
        /// <returns></returns>
        public static int getAnchorPoint(int anchorPoint)
        {
            Point point = nodeOfIndex(anchorPoint);
            for (int n = 1; n <= 10; n++)
            {
                for (int i = point.X - (4 * n); i <= point.X + (4 * n); i++)
                {
                    for (int j = point.Y - (4 * n); j <= point.Y + (4 * n); j++)
                    {
                        int index = getIndex(i, j);
                        if (index < size && index > -1)
                        {
                            if (isValidAnchorPoint(index))   // O(E Log(V))
                                return index;
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Complexity O(E Log(V))
        /// </summary>
        /// <param name="anchor"></param>
        /// <returns></returns>
        public static bool isValidAnchorPoint(int anchor)
        {
            int count = shortestPath(anchor);   // O(E Log(V))
            return count > 1;
        }

        /// <summary>
        /// Complexity O(N^2)
        /// </summary>
        public static void printGraph()
        {
            using (StreamWriter sw = new StreamWriter(@"D:\ASU\semester 6\Algo\project\[1] Intelligent Scissors\Intelligent Scissors Startup Code\[TEMPLATE] IntelligentScissors\IntelligentScissors\graphConstructionOutput.txt")  
)
            {
                sw.AutoFlush = true;
                for (int i = 0; i < (width * height); i++)
                {
                    sw.WriteLine("\n\nThe  index node " + i + "\nEdges");
                    
                    foreach (Pair child in graph[i])
                    {
                        sw.WriteLine("edge from   " + i + "  To  " + child.destination + "  With Weights  " + child.cost);
                    }
                }
            }
            
           
        }


        /// <summary>
        /// Complexity O(N)
        /// </summary>
        public static List<Point> getPath(int anchorPoint, int freePoint)
        {
            //O(1)
            if (!isValidPoint(freePoint))
            {
                freePoint = getPoint(freePoint);
                if (freePoint == -1) { 
                    return null;
                }
            }

            List<Point> path = new List<Point>();
            int nodesOfPath = freePoint;

            
            while (parent[nodesOfPath] != anchorPoint)    //O(N)
            {
                path.Add(nodeOfIndex(nodesOfPath));
                nodesOfPath = parent[nodesOfPath];
            }
            path.Add(nodeOfIndex(nodesOfPath));
            path.Add(nodeOfIndex(anchorPoint));
            return path;
        }

        /// <summary>
        /// Complexity O(E Log(V))
        /// </summary>
        /// <param name="anchorPoint"></param>
        /// <returns></returns>
        public static int shortestPath(int anchorPoint)
        {
            
            int count = 0;
            List<double> cost = new List<double>(Enumerable.Repeat(INF, size).ToArray());
            parent = new List<int>(Enumerable.Repeat(-1, size).ToArray());
            visited = new List<bool>(Enumerable.Repeat(false, size).ToArray());
            PriorityQueue<int, double> nextToVisit = new PriorityQueue<int, double>();


            cost[anchorPoint] = 0;
            parent[anchorPoint] = anchorPoint;
            nextToVisit.Enqueue(anchorPoint, 0);

            //O(V + E Log(V))
            while (nextToVisit.Count > 0)  //O(V)
            {
                int node = nextToVisit.Dequeue();  //O(1)
                if (visited[node])
                    continue;
                visited[node] = true;
                count++;

                //O(E Log(V))
                foreach (Pair child in graph[node])   //O(E)
                {
                    double costOfChild = child.cost;
                    int destination = child.destination;
                    if (cost[node] + costOfChild < cost[destination])
                    {
                        cost[destination] = cost[node] + costOfChild;
                        parent[destination] = node;
                        nextToVisit.Enqueue(destination, cost[destination]);  //O(Log(V))
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Complexity O(E Log(V))
        /// </summary>
        /// <param name="anchorPoint"></param>
        /// <param name="freePoint"></param>
        public static void shortestPathTwoPoints(int anchorPoint, int freePoint)
        {
            int size = width * height;
            int count = 0;

            List<double> cost = new List<double>(Enumerable.Repeat(INF, size).ToArray());
            parent = new List<int>(Enumerable.Repeat(-1, size).ToArray());
            visited = new List<bool>(Enumerable.Repeat(false, size).ToArray());
            PriorityQueue<int, double> nextToVisit = new PriorityQueue<int, double>();

            cost[anchorPoint] = 0;
            parent[anchorPoint] = anchorPoint;
            nextToVisit.Enqueue(anchorPoint, 0);

            //O(V + E Log(V))
            while (nextToVisit.Count > 0)   //O(V)
            {
                if (visited[freePoint])
                    break;
                int node = nextToVisit.Dequeue();   //O(1)
                if (visited[node])
                    continue;
                visited[node] = true;
                count++;

                //O(E Log(V))
                foreach (Pair child in graph[node])    //O(E)
                {
                    double costOfChild = child.cost;
                    int destination = child.destination;
                    if (cost[node] + costOfChild < cost[destination])
                    {
                        cost[destination] = cost[node] + costOfChild;
                        parent[destination] = node;
                        nextToVisit.Enqueue(destination, cost[destination]);    //O(Log(V))
                    }
                }
            }
        }
    }
}


    
