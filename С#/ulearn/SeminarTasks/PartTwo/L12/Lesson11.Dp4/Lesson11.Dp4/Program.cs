using System;
/*Задача о рюкзаке. Есть N золотых слитков, i-й имеет массу W_i килограмм и стоимость L_i рублей. Нужно унести в рюкзаке, 
 * выдерживающем W килограмм, множество слитков максимальной суммарной стоимости. 
 * Можно считать, что все W_i целые и лежат в пределах от 1 до 100.
*/

namespace Lesson11.Dp4
{
    class Program
    {
        static void Main()
        {
            int N = 5; // Количество золотых слитков
            int W = 15; // Максимальный вес рюкзака
            int[] weights = new int[] { 2, 3, 4, 5, 9 }; // Веса слитков
            int[] values = new int[] { 3, 4, 5, 8, 10 }; // Стоимости слитков

            DP.Dp4(N, W, weights, values);
        }
    }
    static class DP
    {
        public static void Dp4(int N, int W, int[] weights, int[] values)
        {
            int[,] matrix = new int[N + 1, W + 1];

            // Заполнение матрицы
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= W; j++)
                {
                    if (weights[i - 1] > j)
                    {
                        matrix[i, j] = matrix[i - 1, j];
                    }
                    else
                    {
                        matrix[i, j] = Math.Max(matrix[i - 1, j], matrix[i - 1, j - weights[i - 1]] + values[i - 1]);
                    }
                }
            }
            Console.WriteLine("Максимальная стоимость золотых слитков: " + matrix[N, W]);
        }
    }
}
