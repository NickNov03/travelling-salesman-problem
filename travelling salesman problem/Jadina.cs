using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    internal class Jadina
    {
        static List<int> path = new List<int>();       // Путь коммив
        static int minPathLength = 0;
        static int numCities;
        static int[,] distances;

        public static void Jadin()
        {
            // Матрица расстояний между городам

            distances = new int[,] {
                    {0, 29, 20, 21},
                    {29, 0, 15, 12},
                    {20, 15, 0, 13},
                    {21, 12, 13, 0}};

            numCities = distances.GetLength(0);
            List<int> cities = Enumerable.Range(0, numCities).ToList();
            List<int> used = new List<int>(numCities);
            for (int i = 0; i < numCities; i++) used.Add(0);
            Find_path(0, used);
            Console.WriteLine("Min lenght: " + Calc(path).ToString());
        }

        static void Find_path(int v, List<int> used)
        {
            if (path.Count == numCities) return;
            path.Add(v);
            used[v] = 1;
            Find_path(min_edge(v, used), used);
        }

        static int min_edge(int v, List<int> used)
        {
            int min = int.MaxValue;
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
        static int Calc(List<int> list)
        {
            int path_cost = 0; int t = -1;
            foreach (var item in list)
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
