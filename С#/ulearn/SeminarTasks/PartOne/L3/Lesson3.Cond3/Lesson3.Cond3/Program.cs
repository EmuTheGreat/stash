using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Пролезет ли брус со сторонами x, y, z в отверстие со сторонами a, b, если его разрешается поворачивать на 90 градусов?
*/

namespace Lesson3.Cond3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double x = 1;
            double y = 2;    // Стороны бруса
            double z = 3;

            double a = 1;    // Стороны отверстия 
            double b = 2;

            Cond4(x, y, z, a, b);
        }

        // Если брус можно вращать на 90 градусов, то повернём двумя минимальными его сторонами и сравним с длинами отверстия.
        static void Cond4(double x, double y, double z, double a, double b)
        {
            var firstMinSideBar = Math.Min(Math.Min(x, y), z);
            var secondMinSideBar = x + y + z - firstMinSideBar - Math.Max(Math.Max(x, y), z);

            if (firstMinSideBar <= a && secondMinSideBar <= b || secondMinSideBar <= a && firstMinSideBar <= b)
                Console.WriteLine("Пролезет.");
            else Console.WriteLine("Не пролезет.");
        }
    }
}
