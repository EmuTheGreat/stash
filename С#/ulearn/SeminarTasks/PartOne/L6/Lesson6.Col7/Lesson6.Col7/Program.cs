using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Дан массив, более половины элементов которого равны X. X неизвестно, нужно его найти. 
 * Можно ли решить задачу, не используя дополнительных коллекций (массивов, списков, словарей)*/

namespace Lesson6.Col7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 10;
            int[] mas = { 3, 3, 3, 3, 2, 2, 2, 2, 2, 2};
            Col7(n, mas);
        }

        static void Col7(int n, int[] mas)
        {
            int maxValue = 0;
            int x = 0; 
            int count;

            for (int i = 0; i < n / 2 + 1; i++) // Цикл проходит по значениям в интервале чуть больше половины.
            {
                count = 0;
                for (int j = 0; j < n; j++)
                    if (mas[i] == mas[j]) count++;

                if (count > maxValue)
                {
                    maxValue = count;
                    x = mas[i];
                }
            }
            Console.WriteLine(x);
        }
    }
}
