using System;
/*Если все числа натурального ряда записать подряд каждую цифру в своей позиции, 
 * то необходимо ответить на вопрос: какая цифра стоит в заданной позиции N.*/

namespace Lesson4.Loop3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberN = 10;
            Loops3(numberN);
        }
        static void Loops3(int numberN)
        {
            int count = 0;
            string listOfNumbers = "";

            while (count++ < numberN)
            {
                listOfNumbers += count.ToString();
            }
            Console.WriteLine(listOfNumbers[numberN-1]);
        }
    }
}
