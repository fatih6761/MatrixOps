using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GaussJordanMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            // birinci indis satır, ikinci indis sütun
            int nRow, nCol;
            double[][] matrix;
            double[] result;

            Console.WriteLine("Lineer Denklemlerin Gauss-Jordan Yöntemiyle Çözülmesi");
            Console.WriteLine("2013, Fatih YAZICI");
            Console.WriteLine("-----------------------------------------------------------");

            Console.WriteLine("Matris boyutlarını girin : ");
            int[] d = Console.ReadLine()
                .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => int.Parse(s))
                .ToArray();
            nRow = d[0]; nCol = d[1];

            matrix = new double[nRow][];
            result = new double[nCol - 1];

            Console.WriteLine("Matrisi girin :");

            for (int i = 0; i < nRow; i++)
                matrix[i] = Console.ReadLine()
                    .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => Convert.ToDouble(s))
                    .ToArray();

            for (int i = 0; i < nRow; i++)
            {
                double R = matrix[i][i];

                if (Math.Abs(R) > 0.0)
                {
                    matrix[i] = Multiply(matrix[i], 1.0 / R);

                    for (int k = i; k < nRow; k++)
                    {
                        double t = matrix[k][i];

                        if (i != k)
                        {
                            matrix[k] = Add(matrix[k], Multiply(matrix[i], -t));
                        }
                    }
                }
            }

            Console.WriteLine("Echelon Matris :");
            for (int i = 0; i < nRow; i++)
            {
                for (int j = 0; j < nCol; j++)
                {
                    Console.Write("{0:F2}\t", matrix[i][j]);
                }
                Console.WriteLine();
            }

            for (int i = 2; i <= nCol; i++)
            {
                double sub = 0.0;
                for (int j = 2; j < i; j++)
                {
                    sub += result[nCol - j] * matrix[nRow - i + 1][nCol - j];
                }
                result[nCol - i] = matrix[nRow - i + 1][nCol - 1] - sub;
            }

            Console.WriteLine("Çözüm :");
            for (int i = 0; i < nCol - 1; i++)
            {
                Console.Write("{0:F2}\t", result[i]);
            }

            Console.ReadLine();
        }

        static void Replace(double[][] matrix, int row1, int row2)
        {
            double[] tmp = matrix[row2];
            matrix[row2] = matrix[row1];
            matrix[row1] = tmp;
        }

        static double[] Multiply(double[] row, double x)
        {
            double[] tmp = new double[row.Length];
            for (int i = 0; i < row.Length; i++)
            {
                tmp[i] = row[i] * x;
            }
            return tmp;
        }

        static double[] Add(double[] row1, double[] row2)
        {
            double[] tmp = new double[row1.Length];
            for (int i = 0; i < row1.Length; i++)
            {
                tmp[i] = row1[i] + row2[i];
            }
            return tmp;
        }
    }
}
