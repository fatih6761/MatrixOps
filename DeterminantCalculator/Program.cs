using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeterminantCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            int nRow, nCol;
            double[][] matrix;

            Console.WriteLine("Matris Determinant Hesaplama Aracı");
            Console.WriteLine("2014, Fatih YAZICI");
            Console.WriteLine("-----------------------------------------------------------");

            Console.WriteLine("KARE matris boyutu (n x n için sadece n girin) :");
            nRow = nCol = int.Parse(Console.ReadLine());

            matrix = new double[nRow][];

            Console.WriteLine("Matrisi girin :");

            for (int i = 0; i < nRow; i++)
                matrix[i] = Console.ReadLine()
                    .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => Convert.ToDouble(s))
                    .ToArray();

            List<int[]> Sigma = VectorPermutations(
                Enumerable.Range(0, nRow).ToArray());

            double Determinant = 0.0;

            for (int i = 0; i < Sigma.Count; i++)
            {
                // sigma permütasyonları ile çarpımların çarpımının toplamı
                int[] SigmaPerm = Sigma[i];
                double SigmaSign = LeviCivitaSymbol(SigmaPerm);
                double SigmaSum = SigmaSign;

                //debug
                //Console.Write(SigmaSign + " ");
                //foreach (var item in SigmaPerm)
                //    Console.Write((item+1) + " ");
                //Console.WriteLine();

                for (int j = 0; j < SigmaPerm.Length; j++)
                {
                    SigmaSum *= matrix[SigmaPerm[j]][j];
                }

                Determinant += SigmaSum;
            }

            Console.WriteLine("Determinant : {0}", Determinant);

            Console.ReadLine();
        }

        private static double LeviCivitaSymbol(int[] SigmaPerm)
        {
            // Levi-Civita sembolü
            int sign = 1;
            for (int i = 1; i < SigmaPerm.Length; i++)
                for (int j = 0; j < i; j++)
                    sign *= Math.Sign(SigmaPerm[i] - SigmaPerm[j]);
            return (sign > 0) ? (1.0) : (-1.0);
        }

        static List<int[]> VectorPermutations(int[] vector)
        {
            LinkedList<int> elems = new LinkedList<int>(vector);

            return PermutateOrderOfVector(elems, elems.Count).Select(x => x.ToArray()).ToList();
        }

        static List<LinkedList<int>> PermutateOrderOfVector(LinkedList<int> vector, int a)
        {
            if (a < 1)
            {
                // Yalnızca son eleman için tek bir permütasyon var
                // O da gelen dizilimin kendisi
                return (new LinkedList<int>[] { vector }).ToList();
            }
            else
            {
                List<LinkedList<int>> sum = new List<LinkedList<int>>();
                for (int i = 0; i < a; i++)
                {
                    // Permüte edilecek vektör
                    LinkedList<int> A = new LinkedList<int>(vector);

                    // Permütasyon dizisi
                    int R = A.ElementAt(i);
                    A.Remove(R);

                    // a-1 elemanlı alt permütasyonları hesapla
                    List<LinkedList<int>> sPerm = PermutateOrderOfVector(A, a - 1);

                    // Tüm permütasyonların başına R ekle ve sum'da topla
                    sum.AddRange(sPerm.Select(x => { x.AddFirst(R); return x; }).ToList());
                }
                return sum;
            }
        }
    }
}
