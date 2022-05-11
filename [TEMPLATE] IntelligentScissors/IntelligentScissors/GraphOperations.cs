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
        public static Graph graph;
        public static int width;
        public static int height;
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
                return (width * X) + Y;
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
                destination = getIndex(x,y);
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
            return (width * x) + y;
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
                    Vector2D cost = ImageOperations.CalculateGradientAtPixel(x, y, ImageMatrix);
                    if ((x == width - 1) && (y < height - 1))
                    {
                        graph[getIndex(x, y)].Add(new CostDestPair(cost.Y, x, y + 1));
                        graph[getIndex(x, y + 1)].Add(new CostDestPair(cost.Y, x, y));
                    }
                    else if ((x < width - 1) && (y == height - 1))
                    {
                        graph[getIndex(x, y)].Add(new CostDestPair(cost.X, x + 1, y));
                        graph[getIndex(x + 1, y)].Add(new CostDestPair(cost.X, x, y));
                    }
                    else if ((x < width - 1) && (y < height - 1))
                    {
                        graph[getIndex(x, y)].Add(new CostDestPair(cost.X, x + 1, y));
                        graph[getIndex(x, y)].Add(new CostDestPair(cost.Y, x, y + 1));
                        graph[getIndex(x + 1, y)].Add(new CostDestPair(cost.X, x, y));
                        graph[getIndex(x, y + 1)].Add(new CostDestPair(cost.Y, x, y));
                    }
                }
            }
        }
        public static void printGraph()
        {
            Node node;
            for (int i = 0; i < (width * height); i++)
            {
                node = nodeOfIndex(i);
                Console.WriteLine("\n\nThe  index node " + i+"\nEdges");
                foreach (CostDestPair child in graph[i])
                {
                    //Console.WriteLine(graph[i].Count);
                    Console.WriteLine("edge from   "+i+"  To  "+child.destination+ "  With Weights  "+child.cost);
                }
            }
        }


    }
}
