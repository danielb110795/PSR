using System;
using System.Linq;

namespace PSR
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfThreads = 1;
            /*
             * Parametr wejściowy można zmienić w nazwa_projektu->Properties->Debug->Command Line
             * */
            if(args.Length>0)
            {
                if(!int.TryParse(args[0],out numberOfThreads))
                {
                    Console.WriteLine("Błąd argumentu liczby wątków! Wybrano wartość domyślną.");
                }
            }

            Console.WriteLine("Liczba wątków do uruchomienia algorytmu: " + numberOfThreads);

            ConcurrentProgram concurrent = new ConcurrentProgram(numberOfThreads);
            concurrent.Start();

            /*
            Graph graph = new Graph();

            var watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < graph.matrix.GetLength(0); i++)
            {
                int[] suma = DijkstraAlgorithm.Dijkstra(graph.matrix, i, graph.matrix.GetLength(0));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Łączna długość najkrótszych ścieżek: " + suma.Sum());
                Console.ResetColor();
            }

            watch.Stop();
            var elapsedMiliseconds = watch.ElapsedMilliseconds;
            Console.WriteLine("Total time:"+elapsedMiliseconds+"ms");
            */
            Console.ReadKey();
        }
    }
}
