using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public class Node
        {
            public Node() { }
            public Node(int x, int y)
            {
                X = x;
                Y = y;
            }
            public int X { get; set; }
            public int Y { get; set; }
            public int getIndex()
            {
                return (width * Y) + X;
            }
        }
        public class CostDestPair
        {
            public CostDestPair() { }
            public CostDestPair(double cost, Node node)
            {
                this.cost = cost;
                this.destination = node.getIndex();
            }
            public CostDestPair(double cost, int x, int y)
            {
                this.cost = cost;
                destination = getIndex(x, y);
            }
            public double cost { get; set; }
            public int destination { get; set; }
        }

        public static Node nodeOfIndex(int index)
        {
            int x = index % width;
            int y = (index - x) / width;
            return new Node(x, y);
        }
        public static int getIndex(int x, int y)
        {
            return (width * y) + x;
        }
        public static void generateGraph(RGBPixel[,] ImageMatrix)
        {
            graph = new Graph();
            width = ImageOperations.GetWidth(ImageMatrix);
            height = ImageOperations.GetHeight(ImageMatrix);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    graph[getIndex(x, y)] = new List<CostDestPair>();
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //graph[(X,Y)] is the node
                    //has list of ( cost, destination node )
                    // ( cost, (X,Y) )

                    Vector2D cost = ImageOperations.CalculatePixelEnergies(x, y, ImageMatrix);

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

        public static List<int> shortestPath(int anchorPoint, int freePoint)
        {
            int size = width * height;
            int count = 0;
            CostDestPair pair = new CostDestPair(5, 2, 0);
            List<double> cost = new List<double>(Enumerable.Repeat(INF, size).ToArray());
            parent = new List<int>(Enumerable.Repeat(-1, size).ToArray());
            List<bool> visited = new List<bool>(Enumerable.Repeat(false, size).ToArray());
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
            List<int> path = new List<int>();
            if (!visited[freePoint])
            {
                Console.WriteLine("invalid free point try againt!");
                return path;
            }

            //Get Path
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
    }
}


    
