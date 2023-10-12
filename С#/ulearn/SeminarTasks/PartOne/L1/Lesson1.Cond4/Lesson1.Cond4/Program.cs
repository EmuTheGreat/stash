using System;
/*Найти количество чисел меньших N, 
 * которые имеют простые делители X или Y.
 */

namespace Lesson1.Cond4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Expr4Another();
        }
        public static void Expr4()
        {
            Console.Write("Число N: ");
            int numberN = int.Parse(Console.ReadLine());
            Console.Write("Число X: ");
            int numberX = int.Parse(Console.ReadLine());
            Console.Write("Число Y: ");
            int numberY = int.Parse(Console.ReadLine());

            int count = 0;

            for (int i = 1; i < numberN; i++)
            {
                if (i % numberX == 0 || i % numberY == 0)
                {
                    count += 1;
                }
            }
            Console.WriteLine(count);
        }
        public static void Expr4Another()
        {
            Console.Write("Число N: ");
            int numberN = int.Parse(Console.ReadLine());
            Console.Write("Число X: ");
            int numberX = int.Parse(Console.ReadLine());
            Console.Write("Число Y: ");
            int numberY = int.Parse(Console.ReadLine());

            int count = 0;

            count += (numberN - 1) / numberX;
            count += (numberN - 1) / numberY;
            count -= (numberN - 1) / (numberY * numberX);
            Console.WriteLine(count);

        }
    }
}
