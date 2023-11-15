

using travelling_salesman_problem;

class Program
{
    public static void Main()
    {
        int n = 2000;
        double maxLen = 50000;
        Console.WriteLine("Вершин: {0}\nМаксимальная длина ребра: {1}", n, maxLen);
        DateTime now0, now1;
        double[,] m = Graph.GenerateCompleteEuclideanGraph(n, maxLen);
        //Graph.Print(m);

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
    }
}