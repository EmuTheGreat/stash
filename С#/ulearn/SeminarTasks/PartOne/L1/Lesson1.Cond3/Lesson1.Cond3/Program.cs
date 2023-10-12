using System;
/*Задано время Н часов (ровно). 
 * Вычислить угол в градусах между часовой и минутной стрелками. 
 * Например, 5 часов -> 150 градусов, 20 часов -> 120 градусов. 
 * Не использовать циклы.
 */

namespace Lesson1.Cond3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Expr3();
        }
        public static void Expr3()
        {
            Console.Write("Введите число часов: ");

            int hours = int.Parse(Console.ReadLine());
            int degree = hours * 30 - 360 * (hours / 12);
            degree = Math.Abs(360 * (degree / 180) - degree);
            
            Console.WriteLine(degree);
        }
    }
}
