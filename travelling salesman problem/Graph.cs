using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace travelling_salesman_problem
{
    internal class Graph
    {
        public static void aprox_koef_poln()
        {
            double k = 0;
            for (int i = 4; i < 12; i++)
            {
                
                double[,] m1 = Graph.GenerateCompleteEuclideanGraph(i - 1, 100);
                double[,] m2 = Graph.GenerateCompleteEuclideanGraph(i, 100);
                k += Poln_Perebor.BruteForce(m2) / Poln_Perebor.BruteForce(m1);
            }
            k /= 9;
            Console.WriteLine(k);
        }

        public static void aprox_koef_Jesus()
        {
            double k = 0;
            for (int i = 4; i < 12; i++)
            {

                double[,] m1 = Graph.GenerateCompleteEuclideanGraph(i - 1, 100);
                double[,] m2 = Graph.GenerateCompleteEuclideanGraph(i, 100);
                k += Kristofides.Kristofid(m2) / Kristofides.Kristofid(m1);
            }
            k /= 9;
            Console.WriteLine(k);
        }

        public static void Test(int[] n, double[] maxLen)
        {
            string path = "test.txt";
            
            int iter = 50;

            double[] res_G, res_P, res_K;
            double error_G = 0, error_K = 0;

            TimeSpan diff, totalPoln = TimeSpan.Zero,
                totalGreedy = TimeSpan.Zero, totalJesus = TimeSpan.Zero;

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                res_G = Test_Greedy(n, maxLen, iter, writer);
                writer.Write("\n");
                res_P = Test_Poln(n, maxLen, iter, writer);
                writer.Write("\n");
                res_K = Test_Jesus(n, maxLen, iter, writer);
                writer.Write("\n");

                for (int i = 0; i < n.Length; i++)
                {
                    error_G += (res_G[i] - res_P[i]) / res_P[i];
                    error_K += (res_K[i] - res_P[i]) / res_P[i];
                }
                error_G /= n.Length;
                error_K /= n.Length;

                writer.WriteLine("Среднее отклонение жадного алгоритма: {0}", error_G);
                writer.WriteLine("Среднее отклонение алгоритма Кристофидеса: {0}", error_K);
            }
        }

        public static double[] Test_Greedy(int[] n, double[] maxLen, int iter, StreamWriter writer)
        {
            DateTime now0, now1;
            TimeSpan diff, total = TimeSpan.Zero;
            double[] res = new double[n.Length];

            writer.WriteLine("Жадный алгоритм:");
            writer.WriteLine("Итераций: {0}", iter);
            for (int i = 0; i < n.Length; i++)
            {
                double[,] m = Graph.GenerateCompleteEuclideanGraph(n[i], maxLen[i]);
                Console.WriteLine("Вершин: {0}\nМаксимальная длина ребра: {1}", n[i], maxLen[i]);
                for (int j = 0; j < iter; j++)
                {
                    now0 = DateTime.Now;
                    res[i] = Jadina.Jadin(m);
                    now1 = DateTime.Now;
                    diff = now1 - now0;
                    total += diff;

                }
                total /= iter;
                writer.Write("Вершин: {0}, Макс длина ребра: {1}, Время: ", n[i], maxLen[i]);
                writer.WriteLine(total);
            }
            return res;
        }

        public static double[] Test_Poln(int[] n, double[] maxLen, int iter, StreamWriter writer)
        {
            writer.WriteLine("Переборный алгоритм:");
            writer.WriteLine("Итераций: {0}", iter);
            DateTime now0, now1;
            TimeSpan diff, total = TimeSpan.Zero;
            double[] res = new double[n.Length];
            for (int i = 0; i < n.Length; i++)
            {
                double[,] m = Graph.GenerateCompleteEuclideanGraph(n[i], maxLen[i]);
                Console.WriteLine("Вершин: {0}\nМаксимальная длина ребра: {1}", n[i], maxLen[i]);
                for (int j = 0; j < iter; j++)
                {
                    now0 = DateTime.Now;
                    res[i] = Poln_Perebor.BruteForce(m);
                    now1 = DateTime.Now;
                    diff = now1 - now0;
                    total += diff;

                }
                total /= iter;
                writer.Write("Вершин: {0}, Макс длина ребра: {1}, Время: ", n[i], maxLen[i]);
                writer.WriteLine(total);
            }
            return res;
        }

        public static double[] Test_Jesus(int[] n, double[] maxLen, int iter, StreamWriter writer)
        {
            writer.WriteLine("Алгоритм Кристофидеса:");
            writer.WriteLine("Итераций: {0}", iter);
            DateTime now0, now1;
            TimeSpan diff, total = TimeSpan.Zero;
            double[] res = new double[n.Length];
            for (int i = 0; i < n.Length; i++)
            {
                double[,] m = Graph.GenerateCompleteEuclideanGraph(n[i], maxLen[i]);
                Console.WriteLine("Вершин: {0}\nМаксимальная длина ребра: {1}", n[i], maxLen[i]);
                for (int j = 0; j < iter; j++)
                {
                    now0 = DateTime.Now;
                    res[i] = Kristofides.Kristofid(m);
                    now1 = DateTime.Now;
                    diff = now1 - now0;
                    total += diff;

                }
                total /= iter;
                writer.Write("Вершин: {0}, Макс длина ребра: {1}, Время: ", n[i], maxLen[i]);
                writer.WriteLine(total);
            }
            return res;
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
