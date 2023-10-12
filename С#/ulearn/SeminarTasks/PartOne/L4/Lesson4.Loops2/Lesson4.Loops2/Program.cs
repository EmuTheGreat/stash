using System;
/*Дано N (1 ≤ N ≤ 27). Найти количество трехзначных натуральных чисел, 
 * сумма цифр которых равна N. Операции деления (/, %) не использовать.
*/

namespace Lesson4.Loops2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = 21;
            Loops2(number);
        }

        static void Loops2(int number)
        {
            int counter = 0;
            for (int i = 1; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    for (int k = 0; k < 10; k++)
                        if (i + j + k == number)
                            counter++;
            Console.WriteLine(counter);
        }
    }
}
