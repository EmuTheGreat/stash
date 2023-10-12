using System;
/*
 * Найти сумму всех положительных чисел меньше 1000 кратных 3 или 5.
 */

namespace Lesson2.Expr10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberN = 1000;
            int numberX = 3;
            int numberY = 5;
            
            Expr10(numberN, numberX, numberY);
        }
        static void Expr10(int numberN, int numberX, int numberY)
        {
            var sum = MathArithmeticProgression(numberN, numberX);
            sum += MathArithmeticProgression(numberN, numberY);
            sum -= MathArithmeticProgression(numberN, numberX * numberY);
            Console.WriteLine(sum);
        }

        static int MathArithmeticProgression(int numberN, int progressionStep)
        {
            var numbersOfMembers = (numberN - 1) / progressionStep; // количество суммируемых членов
            return (progressionStep + progressionStep * numbersOfMembers) * numbersOfMembers / 2;
        }
    }
}
