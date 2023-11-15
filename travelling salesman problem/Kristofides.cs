using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace travelling_salesman_problem
{
    internal class Kristofides
    {
        class Set
        {
            private int[] parent;

            public Set(int size)
            {
                parent = new int[size];

                for (int i = 0; i < size; i++)
                {
                    parent[i] = i;
                }
            }
            public int Find(int x)
            {
                while (x != parent[x])
                {
                    x = parent[x];
                }
                return x;
            }
            public void Union(int x, int y)
            {
                int rootX = Find(x);
                int rootY = Find(y);

                parent[rootX] = rootY;
            }
        }
        private class Edge : IComparable<Edge>
        {
            internal int v1, v2;
            private double length;
            public Edge(int v1, int v2, double length)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.length = length;
            }
            public int CompareTo(Edge otherEdge)
            {
                if (this.length < otherEdge.length)
                    return -1;
                else if (this.length > otherEdge.length)
                    return 1;
                else
                    return 0;
            }
        }
        private static List<Edge> Krascal(double[,] dist) // alg krascal
        {
            List<Edge> edgeList = new List<Edge>();
            for (int i = 0; i < dist.GetLength(1); i++)
            {
                for (int j = i; j < dist.GetLength(1); j++)
                {
                    if (dist[i, j] != 0)
                    {
                        edgeList.Add(new Edge(i, j, dist[i, j]));
                    }
                }
            }
            Console.WriteLine();
            edgeList.Sort();
            List<Edge> minimumSpanningTree = new List<Edge>();
            Set set = new Set(edgeList.Count);

            foreach (var edge in edgeList)
            {
                if (set.Find(edge.v1) != set.Find(edge.v2))
                {
                    minimumSpanningTree.Add(edge);
                    set.Union(edge.v1, edge.v2);
                }
            }
            return minimumSpanningTree;
        }
        private static void DFS(List<Edge> graph, int vertex, bool[] visited, ref List<int> path)
        {
            visited[vertex]= true;
            path.Add(vertex);

            foreach (var edge in graph)
            {
                if (edge.v1 == vertex && !visited[edge.v2])
                {
                    DFS(graph, edge.v2, visited, ref path);
                }
                else if (edge.v2 == vertex && !visited[edge.v1])
                {
                    DFS(graph, edge.v1, visited, ref path);
                }
            }
        }
        public static void Kristofid(double[,] dist)
        {
            List<Edge> minOstTree = Krascal(dist);
            bool[] visited = new bool[dist.GetLength(1)];
            List <int> path = new List<int>();
            DFS(minOstTree, minOstTree.First().v1, visited, ref path);
            Console.Write("Kristofides\nMin path: ");
            Print_list(path);
            Console.WriteLine("Min lenght: {0}", Calc(path, dist));
        }
        static double Calc(List<int> list, double[,] distances)
        {
            double path_cost = 0; int t = -1;
            foreach (int item in list)
            {
                if (t >= 0)
                {
                    path_cost += distances[t, item];
                }
                t = item;
            }
            path_cost += distances[list.Last(), list.First()];
            return path_cost;
        }
        static void Print_list(List<int> list)
        {
            foreach (var item in list) Console.Write(item.ToString() + ' ');
            Console.WriteLine();
        }
    }
}
