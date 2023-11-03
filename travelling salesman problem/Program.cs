

using travelling_salesman_problem;

class Program
{
    public static void Main()
    {
        double[,] m = { { 0, 2, 3 }, { 2, 0, 4 }, { 3, 4, 0 }, };
        Graph G = new Graph(m);
        G.ToString();

        double[,] m0 = Graph.GenerateCompleteEuclideanGraph(4, 50);
        Graph H = new Graph(m0);
        H.ToString();

        Poln_Perebor.BruteForce();
        Jadina.Jadin();
    }
}