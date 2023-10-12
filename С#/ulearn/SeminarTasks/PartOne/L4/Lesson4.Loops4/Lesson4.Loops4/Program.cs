using System;
using System.Linq;
/*В массиве чисел найдите самый длинный подмассив из одинаковых чисел.
*/

namespace Lesson4.Loops4
{
    internal class Program
    {
        static void Main()
        {
            int[][] massiv =
            {
               new int[] { 1, 2, 2, 3 ,3 ,3 , 2, 2, 1 }, // 4 двойки
               new int[] {4, 4, 4, 4, 5, 6, 4, 44, 55, 66 }, // 5 четвёрок
               new int[] { 7, 8, 9 }, // нет одинаковых
            };

            Loops4(massiv);
        }

        static void Loops4(int[][] massiv)
        {
            int numberOfSubMassiv = 0; // Номер массива, в котором больше одинаковых чисел
            int minSameNumbers = 0;
            for (int i = 0; i < massiv.Length; i++)
            {
                foreach (int j in massiv[i])
                {
                    int maxSameNumbers = 0;
                    for (int k = 0; k < massiv[i].Length; k++)
                        if (j == massiv[i][k]) maxSameNumbers++;

                    if (maxSameNumbers > minSameNumbers)
                    {
                        minSameNumbers = maxSameNumbers;
                        numberOfSubMassiv = i;
                    }
                }
            }
            Console.WriteLine(numberOfSubMassiv);
        }
    }
}
