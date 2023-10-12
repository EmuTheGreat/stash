using System;
/*
 * Посчитать расстояние от точки до прямой, заданной двумя разными точками.
 */

namespace Lesson1.Cond5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Expr6();
        }
        static void Expr6()
        {
            Console.Write("Введите координаты точки (x0, y0), удалённой от прямой, через пробел: ");
            string[] userInput = Console.ReadLine().Split(' ');
            double x0 = double.Parse(userInput[0]);
            double y0 = double.Parse(userInput[1]);
 
            Console.Write("Введите координаты первой точки (x1, y1), задающей прямую, через пробел: ");
            userInput = Console.ReadLine().Split(' ');
            double x1 = double.Parse(userInput[0]);
            double y1 = double.Parse(userInput[1]);

            Console.Write("Введите координаты второй точки (x2, y2), задающей прямую, через пробел: ");
            userInput = Console.ReadLine().Split(' ');
            double x2 = double.Parse(userInput[0]);
            double y2 = double.Parse(userInput[1]);

            DoMath(x1, y1, x2, y2, x0, y0);

        }

        static void DoMath(double x1, double y1, double x2, double y2, double x0, double y0)
        {
            double[][] matrix = new double[2][];
            matrix[0] = new double[3];
            matrix[1] = new double[3];
            
            //Заполняем матрицу. Применяем метод Гаусса.
            matrix[0][0] = x1;
            matrix[0][1] = y1;
            matrix[0][2] = 4;
            matrix[1][0] = x2;
            matrix[1][1] = y2;
            matrix[1][2] = 4;

            double subtractionVariable = -(x2 / x1);
            matrix[1][0] += x1 * subtractionVariable;
            matrix[1][1] += y1 * subtractionVariable;
            matrix[1][2] += 4 * subtractionVariable;

            double B = matrix[1][2] / matrix[1][1];
            double A = (4 - y1 * B) / x1;


            Console.WriteLine($"Расстояние равно {Math.Abs(x0*A + y0*B + (-matrix[0][2]))/Math.Sqrt(A*A + B*B)}");
        }
    }
}