using System;
using System.Linq;


namespace PSR
{
    class Program
    {


        static void doHostWork(int numberOfThreads)
        {
            Console.WriteLine("Host działa");
            Host host = new Host();
            host.Start();

        }
        static void doClientWork(int numberOfThreads)
        {
            Console.WriteLine("Klient działa");
            Client client = new Client();
            client.Start(numberOfThreads);


        }

        static void Main(string[] args)
        {
            int numberOfThreads = 0;
            /*
             * Parametr wejściowy można zmienić w nazwa_projektu->Properties->Debug->Command Line
             * */
            if(args.Length>1)
            {

                if(!int.TryParse(args[1],out numberOfThreads))
                {
                    Console.WriteLine("Błąd argumentu liczby wątków! Wybrano wartość domyślną.");
                    numberOfThreads = 2;
                }

                Console.WriteLine("Liczba wątków do uruchomienia algorytmu: " + numberOfThreads);

                if (args[0].CompareTo("h") == 0)
                {
                    doHostWork(numberOfThreads);
                }
                else if(args[0].CompareTo("c")==0)
                {
                    doClientWork(numberOfThreads);
                }

                
            }

            


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
