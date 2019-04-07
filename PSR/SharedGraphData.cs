using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSR
{
    // klasa do przechowywania danych współdzielonych między wątkami
    class SharedGraphData
    {
        private int[,] matrix;
        private int vertices;
        private int size;
        private int lowestDist = 99999;
        private readonly object block = new object();

        public int[,] Matrix
        {
            get
            {
                return matrix;
            }
        }
        public int GetNextVertice
        {
            get
            {
                lock(block)
                {
                    return vertices--;
                }
            }
        }
        public int GetVertices
        {
            get
            {
                return vertices;
            }
        }
        public int MatrixSize
        {
            get
            {
                return size;
            }
        }  
        public int Dist
        {
            set
            {
                lock (block)
                {
                    lowestDist = (lowestDist > value) ? value : lowestDist;
                }
            }
            get
            {
                return lowestDist;
            }
        }


        public SharedGraphData(int [,] matrix)
        {
            this.matrix = matrix;
            size = this.matrix.GetLength(0);
            vertices = size-1;
         
        }
    }
}
