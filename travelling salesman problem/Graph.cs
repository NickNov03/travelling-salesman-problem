using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelling_salesman_problem
{
    internal class Graph
    {
        double[,] AdjMatrix;
        int n;

        //public Graph(int n)
        //{
        //    this.AdjMatrix = new double[n, n];
        //    this.n = n;
        //}

        //public Graph(double[,] adjMatrix) 
        //{
        //    this.AdjMatrix = adjMatrix;
        //    this.n = adjMatrix.GetLength(0);
        //}

        //public Graph(Graph G)
        //{
        //    this.AdjMatrix = G.AdjMatrix;
        //    this.n = G.n;
        //}

        //private void AddEdge(int v1, int v2, double weight)
        //{
        //    AdjMatrix[v1, v2] = weight;
        //}

        //private void RemoveEdge(int v1, int v2) 
        //{
        //    AdjMatrix[v1, v2] = 0;
        //}

        public static void ToString(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("{0}: ", i);
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (!(matrix[i, j] == 0))
                        Console.Write("{0} ({1}), ", j, matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void Print(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write("{0,2:f8} ", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static double[,] GenerateCompleteEuclideanGraph(int n, double MaxEdgeLen)
        {
            double[,] matrix = new double[n, n];
            List<(double, double)> coords = new List<(double, double)>();
            double x, y;
            double maxCoord = MaxEdgeLen / Math.Sqrt(2);
            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                x = maxCoord * rnd.NextDouble();
                y = maxCoord * rnd.NextDouble();
                coords.Add((x, y));
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    matrix[i, j] = Math.Sqrt((coords[i].Item1 - coords[j].Item1) * (coords[i].Item1 - coords[j].Item1)
                        + (coords[i].Item2 - coords[j].Item2) * (coords[i].Item2 - coords[j].Item2));
                    matrix[j, i] = matrix[i, j];
                }
            }

            return matrix;
        }

        private (int, int, double) MinEdge()
        {
            double min = double.MaxValue;
            int min_i = 0, min_j = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (AdjMatrix[i,j] < min)
                    {
                        min = AdjMatrix[i,j];
                        min_i = i;
                        min_j = j;
                    }
                }
            }
            return (min_i, min_j, min);
        }
    }
}
