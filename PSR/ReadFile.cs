using System;
using System.IO;
using System.Linq;

namespace PSR
{
    public class ReadFile
    {
        //funkcja odczytujaca dane z pliku i tworząca obiekt grafu z tymi danymi
        public int[][] readData(string path)
        {
            int[][] matrix = File.ReadAllLines(path)
                   .Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray())
                   .ToArray();

            String report = String.Join(Environment.NewLine, matrix
                .Select(line => String.Join(" ", line)));

            Console.Write(report);

            return matrix;
        }

        public T[,] JaggedToMultidimensional<T>(T[][] jaggedArray)
        {
            int rows = jaggedArray.Length;
            int cols = jaggedArray.Max(subArray => subArray.Length);
            T[,] array = new T[rows, cols];
            for (int i = 0; i < rows; i++)
            { 
                cols = jaggedArray[i].Length;
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = jaggedArray[i][j];
                }
            }
            return array;
        }
    }
}