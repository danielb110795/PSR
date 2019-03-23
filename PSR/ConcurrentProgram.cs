using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System;

namespace PSR
{
    class ConcurrentProgram
    {
        private int numberOfThreads;
        private Graph graph;
        private SharedGraphData sharedGraph;


        public ConcurrentProgram(int numberOfThreads)
        {
            this.numberOfThreads = numberOfThreads;
        }
        public void Start() // wczytywanie danych tymczasowo w tej metodzie
        {
            graph = new Graph(@"../../macierz.txt"); // instancja tej klasy będzie tylko na serwerze
            sharedGraph = new SharedGraphData(graph.matrix);// klasa do przechowywania danych współdzielonych

            this.numberOfThreads = (sharedGraph.GetVertices > this.numberOfThreads) ? this.numberOfThreads : sharedGraph.GetVertices;

 
                Thread[] threads = new Thread[this.numberOfThreads];

                var watch = System.Diagnostics.Stopwatch.StartNew();
                for (int i = 0; i < numberOfThreads; i++)
                {

                    threads[i] = new Thread(() => { doChildWork(sharedGraph.GetNextVertice); });

                    threads[i].Start();
                }

                foreach (Thread t in threads)
                {
                    t.Join();

                }
                watch.Stop();
                var elapsedMiliseconds = watch.ElapsedMilliseconds;
                Console.WriteLine("Total time:" + elapsedMiliseconds + "ms");


        }

        private void doChildWork(int vertice)  // operacje na wątku
        {
            do
            { 
                int[] sum = DijkstraAlgorithm.Dijkstra(sharedGraph.Matrix, vertice, sharedGraph.MatrixSize);
                Console.WriteLine("Łączna długość najkrótszych ścieżek: " + "vert " + vertice+" dist "+sum.Sum());

                vertice = sharedGraph.GetNextVertice; // pobranie kolejnego wierzchołka do obliczeń

            } while (vertice >= 0); 

        }
    }
}
