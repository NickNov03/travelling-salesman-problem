﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Poln_Perebor
    {
        static int[] path;       // Путь коммивояжера
        static int minPathLength = int.MaxValue; // Длина оптимального пути
        static int numCities;
        static int[,] distances;

        // BruteForce Traveling Salesman Problem
        public static void BruteForce()
        {
            // Матрица расстояний между городами

            distances = new int[,] {
            {0, 29, 20, 21},
            {29, 0, 15, 12},
            {20, 15, 0, 13},
            {21, 12, 13, 0}};

            numCities = distances.GetLength(0);
            List<int> cities = Enumerable.Range(0, numCities).ToList();
            List<int> used = new List<int>(numCities);
            for (int i = 0; i < numCities; i++) used.Add(0);
            Rec(cities, used, new List<int>());
            Console.WriteLine("Min lenght: {0}", minPathLength);
            Console.Write("Path: "); 
            for (int i = 0; i < numCities; i++) Console.Write(path[i].ToString() + ' ');
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
                    if (inuse.Count == 4)
                    {
                        Print_list(inuse);
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
        static void Print_list(List<int> list)
        {
            foreach (var item in list) Console.Write(item.ToString() + ' ');
            Console.WriteLine();
        }
    }
}