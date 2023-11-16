

using travelling_salesman_problem;

class Program
{
    public static void Main()
    {
        int n = 12;
        double maxLen = 100;
        Console.WriteLine("Вершин: {0}\nМаксимальная длина ребра: {1}", n, maxLen);
        DateTime now0, now1;
        double[,] m = Graph.GenerateCompleteEuclideanGraph(n, maxLen);
        //Graph.Print(m);
        //double[,] m = { { 0, 10, 2 }, { 600, 0, 3 }, { 5, 4, 0 } };

        now0 = DateTime.Now;
        Kristofides.Kristofid(m);
        now1 = DateTime.Now;
        Console.WriteLine("Кристофидес: {0}", now1 - now0);
        Console.WriteLine();

        now0 = DateTime.Now;
        Jadina.Jadin(m);
        now1 = DateTime.Now;
        Console.WriteLine("Жадный: {0}", now1 - now0);

        now0 = DateTime.Now;
        Poln_Perebor.BruteForce(m);
        now1 = DateTime.Now;
        Console.WriteLine("Полный перебор: {0}", now1 - now0);
        Console.WriteLine();

        //int[] tests_n = { 5, 8, 8, 10, 11, 11 };
        //double[] tests_ML = { 100, 100, 1000000000, 100, 100, 1000000000 };
        //Graph.Test(tests_n, tests_ML);
        //Graph.aprox_koef_Jesus();
    }
}