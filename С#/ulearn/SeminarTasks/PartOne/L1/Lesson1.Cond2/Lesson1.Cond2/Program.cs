using System;
/*
 * Задается натуральное трехзначное число (гарантируется, что трехзначное). 
 * Развернуть его, т.е. получить трехзначное число, 
 * записанное теми же цифрами в обратном порядке.
 */
namespace Lesson1.Cond2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Expr2();
        }
        public static void Expr2()
        {
            Console.Write("Введите число: ");
            int number = int.Parse(Console.ReadLine());
            string reverseNumber = null;
            
            while (number != 0)
            {
                reverseNumber += (number % 10).ToString();
                number = number / 10;
            }

            Console.WriteLine(reverseNumber);
        }
    }
}
