using System;

/*
 * Как поменять местами значения двух переменных? 
 * Можно ли это сделать без ещё одной временной переменной. 
 * Стоит ли так делать?
*/

namespace Lesson1.Cond1
{
    class Program
    {
        static void Main(string[] args)
        {
            Expr1Another();
        }

        public static void Expr1()
        {
            Console.WriteLine("Введите x y через пробел");
            string[] userInput = Console.ReadLine().Split(' ');
            var x = int.Parse(userInput[0]);
            var y = int.Parse(userInput[1]);
            Console.WriteLine($"x = {x}, y = {y}");
            (x, y) = (y, x);
            Console.WriteLine($"x = {x}, y = {y}");
        }

        public static void Expr1Another()
        {
            Console.WriteLine("Введите x y через пробел");
            string[] userInput = Console.ReadLine().Split(' ');
            var x = int.Parse(userInput[0]);
            var y = int.Parse(userInput[1]);
            Console.WriteLine($"x = {x}, y = {y}");
            x = x + y;
            y = x - y;
            x = x - y;
            Console.WriteLine($"x = {x}, y = {y}");
        }

    }

}
