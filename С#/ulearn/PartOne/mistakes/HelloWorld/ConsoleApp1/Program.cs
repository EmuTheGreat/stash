using System;

//https://metanit.com/sharp/tutorial/7.2.php Методы строк C#
namespace HelloWorld
{
    class Program
    {
        static void Main()
        {
            if (DoChoice() == 1) // Из долларов в рубли.
            {
                Console.Write("Введите колличетсво: ");
                float dollar = float.Parse(Console.ReadLine());
                float rubl = dollar * 61.15f; 
                Console.WriteLine($"{dollar} долларов эквивалентно {rubl} рублям");
            }
            else // Из рублей в доллары.
            {
                Console.Write("Введите колличетсво: ");
                float rubl = float.Parse(Console.ReadLine());
                float dollar = rubl / 61.15f;
                Console.WriteLine($"{rubl} рублей эквивалентно {dollar} долларам");
            }
        }
        static int DoChoice()
        {
            Console.Write("Доллары в Рубли - 1, Рубли в Доллары - 2: ");
            return int.Parse(Console.ReadLine());
        }
    }
}