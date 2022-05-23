using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static IntelligentScissors.GraphOperations;

namespace IntelligentScissors
{
    using Graph = Dictionary<int, List<CostDestPair>>;
    internal class GraphOperations
    {
        private const double INF = double.PositiveInfinity;
        public static Graph graph;
        public static int width;
        public static int height;
        public static List<int> parent;
        public static List<bool> visited;
        public static int size;

        public class CostDestPair
        {
            public CostDestPair() { }
            public CostDestPair(double cost, Point node)
            {
                this.cost = cost;
                this.destination = getIndex(node.X, node.Y);
            }
            public CostDestPair(double cost, int x, int y)
            {
                this.cost = cost;
                destination = getIndex(x, y);
            }
            public double cost { get; set; }
            public int destination { get; set; }
        }

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
                    graph[getIndex(x, y)] = new List<CostDestPair>();  //O(1)
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
                        graph[getIndex(x, y)].Add(new CostDestPair(1 / cost.Y, x, y + 1));
                        graph[getIndex(x, y + 1)].Add(new CostDestPair(1 / cost.Y, x, y));

                    }
                    else if ((x < width - 1) && (y == height - 1))
                    {
                        //No Bottom Nodes only store Right cost
                        graph[getIndex(x, y)].Add(new CostDestPair(1 / cost.X, x + 1, y));
                        graph[getIndex(x + 1, y)].Add(new CostDestPair(1 / cost.X, x, y));

                    }
                    else if ((x < width - 1) && (y < height - 1))
                    {

                        graph[getIndex(x, y)].Add(new CostDestPair(1 / cost.X, x + 1, y));
                        graph[getIndex(x, y)].Add(new CostDestPair(1 / cost.Y, x, y + 1));
                        graph[getIndex(x + 1, y)].Add(new CostDestPair(1 / cost.X, x, y));
                        graph[getIndex(x, y + 1)].Add(new CostDestPair(1 / cost.Y, x, y));

                    }
                }
            }
        }


        public static int getPoint(int anchorPoint)
        {
            Point point = nodeOfIndex(anchorPoint);

            for (int n = 1; n <= 10; n++)
            {
                for (int i = point.Y - (10*n); i <= point.Y + (10*n); i++)
                {
                    for (int j = point.X - (10 * n); j <= point.X + (10*n); j++)
                    {
                        int index = getIndex(j, i);
                        if (index < size && index > -1)
                        {
                            if (isValidPoint(index))
                                return index;
                        }
                    }
                }
            }
            return -1;
        }

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
                            if (isValidAnchorPoint(index))
                                return index;
                        }
                    }
                }
            }
            return -1;
        }
        public static bool isValidAnchorPoint(int anchor)
        {
            int count = shortestPath(anchor);
            return count > 1;
        }

        public static void printGraph()
        {
            for (int i = 0; i < (width * height); i++)
            {
                Console.WriteLine("\n\nThe  index node " + i + "\nEdges");
                foreach (CostDestPair child in graph[i])
                {
                    //Console.WriteLine(graph[i].Count);
                    Console.WriteLine("edge from   " + i + "  To  " + child.destination + "  With Weights  " + child.cost);
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
        /// 
        /// </summary>
        /// <param name="anchorPoint"></param>
        /// <param name="freePoint"></param>
        /// <returns></returns>
        public static List<int> getPath(int anchorPoint, int freePoint)
        {
            //O(1)
            if (!isValidPoint(freePoint))
            {
                freePoint = getPoint(freePoint);
                if (freePoint == -1) { 
                    Console.WriteLine("invalid free point try againt!");
                    return null;
                }
            }

            List<int> path = new List<int>();
            int nodesOfPath = freePoint;

            while (parent[nodesOfPath] != anchorPoint)
            {
                path.Add(nodesOfPath);
                nodesOfPath = parent[nodesOfPath];
            }
            path.Add(nodesOfPath);
            path.Add(anchorPoint);
            return path;
        }

        public static int shortestPath(int anchorPoint)
        {
            
            int count = 0;
            List<double> cost = new List<double>(Enumerable.Repeat((1/0.0)+1, size).ToArray());
            parent = new List<int>(Enumerable.Repeat(-1, size).ToArray());
            visited = new List<bool>(Enumerable.Repeat(false, size).ToArray());
            //node, cost 
            PriorityQueue<int, double> nextToVisit = new PriorityQueue<int, double>();


            cost[anchorPoint] = 0;
            parent[anchorPoint] = anchorPoint;
            nextToVisit.Enqueue(anchorPoint, 0);
            while (nextToVisit.Count > 0)
            {
                //if (visited[freePoint])
                //    break;
                int node = nextToVisit.Dequeue();
                if (visited[node])
                    continue;
                visited[node] = true;
                count++;
                foreach (CostDestPair child in graph[node])
                {
                    double costOfChild = child.cost;
                    int destination = child.destination;
                    if (cost[node] + costOfChild < cost[destination])
                    {
                        cost[destination] = cost[node] + costOfChild;
                        parent[destination] = node;
                        nextToVisit.Enqueue(destination, cost[destination]);
                    }
                }
            }
            Console.WriteLine("count =   " + count);
            return count;
        }

        public static void shortestPathTwoPoints(int anchorPoint, int freePoint)
        {
            int size = width * height;
            int count = 0;


            List<double> cost = new List<double>(Enumerable.Repeat(INF, size).ToArray());
            parent = new List<int>(Enumerable.Repeat(-1, size).ToArray());
            visited = new List<bool>(Enumerable.Repeat(false, size).ToArray());
            //node, cost 
            PriorityQueue<int, double> nextToVisit = new PriorityQueue<int, double>();


            cost[anchorPoint] = 0;
            parent[anchorPoint] = anchorPoint;
            nextToVisit.Enqueue(anchorPoint, 0);
            while (nextToVisit.Count > 0)
            {
                if (visited[freePoint])
                    break;
                int node = nextToVisit.Dequeue();
                if (visited[node])
                    continue;
                visited[node] = true;
                count++;
                foreach (CostDestPair child in graph[node])
                {
                    double costOfChild = child.cost;
                    int destination = child.destination;
                    if (cost[node] + costOfChild < cost[destination])
                    {
                        cost[destination] = cost[node] + costOfChild;
                        parent[destination] = node;
                        nextToVisit.Enqueue(destination, cost[destination]);
                    }
                }
            }
        }
    }
}


    
