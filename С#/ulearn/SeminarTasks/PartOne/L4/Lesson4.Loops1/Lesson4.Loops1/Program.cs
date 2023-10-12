using System;
/*Дано целое неотрицательное число N. Найти число, составленное теми же десятичными цифрами,
 * что и N, но в обратном порядке. Запрещено использовать массивы.
*/

namespace Lesson4.Loops1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = 12345;
            Loops1(number);
        }

        static void Loops1(int number)
        {
            string reverseNumber = "";

            while (number > 0)
            {
                reverseNumber += (number % 10).ToString();
                number /= 10;
            }
            Console.WriteLine(reverseNumber);
        }
    }
}
