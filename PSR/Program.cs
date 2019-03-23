using System;
using System.Linq;

namespace PSR
{
    class Program
    {
        static void Main()
        {

            //proba odczytu
            ReadFile read = new ReadFile();
            int [][] matrix = read.readData(@"C:\Users\Daniel\Desktop\macierz.txt");

            Console.WriteLine(matrix[1][1]);

            Graph graph = new Graph();
            DijkstraAlgorithm dijkstra = new DijkstraAlgorithm();

            for (int i = 0; i < graph.matrix.GetLength(0); i++)
            {
                int[] suma = dijkstra.Dijkstra(graph.matrix, i, graph.matrix.GetLength(0));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Łączna długość najkrótszych ścieżek: " + suma.Sum());
                Console.ResetColor();
            }

            Console.ReadKey();
        }
    }
}
