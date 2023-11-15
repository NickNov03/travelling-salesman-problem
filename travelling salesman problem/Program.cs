

using travelling_salesman_problem;

class Program
{
    public static void Main()
    {
        int n = 100;
        double maxLen = 50;
        //Console.WriteLine("Вершин: {0}\nМаксимальная длина ребра: {1}", n, maxLen);
        DateTime now0, now1;
        TimeSpan diff;
        double[,] m = Graph.GenerateCompleteEuclideanGraph(n, maxLen);
        //Graph.Print(m);

        //now0 = DateTime.Now;
        //Jadina.Jadin(m);
        //now1 = DateTime.Now;
        //diff = now1 - now0;
        //Console.WriteLine("Жадный: {0}", diff);
        //Console.WriteLine();

        //now0 = DateTime.Now;
        //Poln_Perebor.BruteForce(m);
        //now1 = DateTime.Now;
        //Console.WriteLine("Время: {0}", now1 - now0);
        //Console.WriteLine();

        int[] tests_n = { 5, 8, 8, 10, 11, 11 };
        double[] tests_ML = { 100, 100, 100000, 1000, 100, 100000 };
        Graph.Test(tests_n, tests_ML);

    }
}