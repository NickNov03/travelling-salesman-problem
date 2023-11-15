using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelling_salesman_problem
{
    class Poln_Perebor
    {
        static int[] path;       // Путь коммивояжера
        static double minPathLength = double.MaxValue; // Длина оптимального пути
        static int numCities;
        static double[,] distances;

        // BruteForce Traveling Salesman Problem
        public static void BruteForce(double[,] dist)
        {
            // Матрица расстояний между городами
            Console.WriteLine("POLNIY");
            distances = dist;

            numCities = distances.GetLength(0);
            List<int> cities = Enumerable.Range(0, numCities).ToList();
            List<int> used = new List<int>(numCities);
            for (int i = 0; i < numCities; i++) used.Add(0);
            Rec(cities, used, new List<int>());
            Console.WriteLine("Min lenght: {0}", minPathLength);
            Console.Write("Path: "); 
            for (int i = 0; i < numCities; i++) Console.Write(path[i].ToString() + ' ');
            Console.WriteLine();
        }

        static void Rec(List<int> cities, List<int> used, List<int> inuse)
        {
            foreach (var item in cities)
            {
                inuse.Add(item);
                if (used[item] != 1)
                {
                    used[item] = 1;
                    Rec(New_city(used), used, inuse);
                    used[item] = 0;
                    if (inuse.Count == numCities)
                    {
                        if (Calc(inuse) < minPathLength)
                        {
                            minPathLength = Calc(inuse);
                            path = inuse.ToArray();
                        }
                    }
                    inuse.Remove(item);
                }  
            }
        }
        static List<int> New_city(List<int> used)
        {
            List<int> new_cities = new List<int>(used.Count);
            for (int i = 0; i < used.Count; i++)
            {
                if (used[i] != 1)
                {
                    new_cities.Add(i);
                }
            }
            return new_cities;
        }
        static double Calc(List<int> list)
        {
            double path_cost = 0; int t = -1;
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
        static void Print_list(List<int> list)
        {
            foreach (var item in list) Console.Write(item.ToString() + ' ');
            Console.WriteLine();
        }
    }
}
