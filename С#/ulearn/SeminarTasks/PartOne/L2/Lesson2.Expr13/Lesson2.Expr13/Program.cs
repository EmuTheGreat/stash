using System;
using System.Security.Policy;
/*
 Козла пустили в квадратный огород и привязали к колышку. 
Колышек воткнули точно в центре огорода. Козёл ест всё, до чего дотянется, 
не перелезая через забор огорода и не разрывая веревку. 
Какая площадь огорода будет объедена? Даны длина веревки и размеры огорода.
 */

namespace Lesson2.Expr13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Expr13();
        }
        
        static void Expr13()
        {
            double sideOfSquare = 10;
            double radius = 6;
            var halfOfSide = sideOfSquare / 2d;

            var angleA = Math.Asin(halfOfSide /  radius) * 180 / Math.PI; // Угол между радиусом и стороной квадрата
            var angleB = 2 * angleA - 90; // Угол сектора
            
            var sectorArea = Math.PI * radius * radius * angleB / 360d;
            var areaOfTriangle = halfOfSide * radius * Math.Sin((90d - angleA) * Math.PI / 180);

            var pieceArea = (halfOfSide * halfOfSide - sectorArea - areaOfTriangle) * 4;
            var area = sideOfSquare * sideOfSquare - pieceArea;
            
            Console.WriteLine(Math.Round(area, 3));
        }
    }
}
