using System;
/*Rec1. Возвести число в натуральную степень P за \Theta(\log P)Θ(logP). Рекурсивный и нерекурсивный варианты.*/

namespace Lesson9.Rec1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Rec1(2, 6));
        }

        static long Rec1(long n, int p)
        {
            if (p == 0)
                return 1;
            if (p % 2 == 1)
                return Rec1(n, p - 1) * n;
            else
            {
                var b = Rec1(n, p / 2);
                return b * b;
            }
        }
    }
}
