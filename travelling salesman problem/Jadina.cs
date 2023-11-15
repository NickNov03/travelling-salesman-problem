using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace travelling_salesman_problem
{
    internal class Jadina
    {
        static List<int> path = new List<int>();       // Путь коммив
        static double minPathLength = 0;
        static int numCities;
        static double[,] distances;

        public static void Jadin(double[,] dist)
        {
            // Матрица расстояний между городам
            Console.WriteLine("JADNIY");
            distances = dist;

            numCities = distances.GetLength(0);
            List<int> cities = Enumerable.Range(0, numCities).ToList();
            List<int> used = new List<int>(numCities);
            for (int i = 0; i < numCities; i++) used.Add(0);
            Find_path(0, used);
            Console.WriteLine("Min lenght: " + Calc(path).ToString());
            Console.Write("Min path: ");
            Print_list(path);
            Console.WriteLine();
        }

        static void Find_path(int v, List<int> used)
        {
            if (path.Count == numCities) return;
            path.Add(v);
            used[v] = 1;
            Find_path(min_edge(v, used), used);
        }
        static void Print_list(List<int> list)
        {
            foreach (var item in list) Console.Write(item.ToString() + ' ');
            Console.WriteLine();
        }
        static int min_edge(int v, List<int> used)
        {
            double min = double.MaxValue;
            int u = -1;
            for (int i = 0; i < numCities; i++)
            {
                if (distances[v, i] != 0 && distances[v, i] < min && used[i] != 1)
                {
                    min = distances[v, i];
                    u = i;
                }
            }
            minPathLength += min;
            return u;
        }
        static double Calc(List<int> list)
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
    }
}
