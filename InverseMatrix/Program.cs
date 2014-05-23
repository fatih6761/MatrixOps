using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverseMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int nRow, nCol;
            double[][] matrix;
            double[][] identity;

            Console.WriteLine("Matrisin Tersinin Alınması");
            Console.WriteLine("2014, Fatih YAZICI");
            Console.WriteLine("-----------------------------------------------------------");

            Console.WriteLine("KARE matris boyutu (n x n için sadece n girin) :");
            nRow = nCol = int.Parse(Console.ReadLine());

            matrix = new double[nRow][];
            identity = new double[nRow][];
            for (int i = 0; i < nRow; i++)
                identity[i] = new double[nCol];

                for (int i = 0; i < nRow; i++)
                    for (int j = 0; j < nCol; j++)
                        identity[i][j] = (i == j) ? (1.0) : (0.0);

            Console.WriteLine("Matrisi girin :");

            for (int i = 0; i < nRow; i++)
                matrix[i] = Console.ReadLine()
                    .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => Convert.ToDouble(s))
                    .ToArray();

            for (int i = 0; i < nRow; i++)
            {
                // i. diyogonal eleman
                double d = matrix[i][i];

                for (int j = 0; j < nCol; j++)
                {
                    // tüm diyagonal elemanları 1 yapmak için böl
                    matrix[i][j] /= d;
                    identity[i][j] /= d;
                }

                for (int n = 0; n < nRow; n++)
                {
                    if (n != i)
                    {
                        double k = matrix[n][i];

                        for (int j = 0; j < nCol; j++)
                        {
                            matrix[n][j] -= matrix[i][j] * k;
                            identity[n][j] -= identity[i][j] * k;
                        }
                    }
                }
            }

            // ters matrisi göster
            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    Console.Write("{0:F2}\t", identity[i][j]);
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
