using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelling_salesman_problem
{
    internal class Graph
    {
        public static void Test(int[] n, double[] maxLen)
        {
            DateTime now0, now1;
            TimeSpan diff, total;
            
            for (int i = 0; i < n.Length; i++)
            {
                double[,] m = Graph.GenerateCompleteEuclideanGraph(n[i], maxLen[i]);
                Console.WriteLine("Вершин: {0}\nМаксимальная длина ребра: {1}", n[i], maxLen[i]);
                for (int j = 0; j < 1; j++)
                {
                    now0 = DateTime.Now;
                    Jadina.Jadin(m);
                    now1 = DateTime.Now;
                    diff = now1 - now0;
                    Console.WriteLine(diff.ToString());
                }
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
    }
}
