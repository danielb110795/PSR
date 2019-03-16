using System;
using System.Linq;

namespace PSR
{
    class Program
    {
        static void Main()
        {            
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
