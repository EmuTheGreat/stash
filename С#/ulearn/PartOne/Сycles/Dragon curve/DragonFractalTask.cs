// В этом пространстве имен содержатся средства для работы с изображениями. 
// Чтобы оно стало доступно, в проект был подключен Reference на сборку System.Drawing.dll
using System;
using System.Drawing;

namespace Fractals
{

    internal static class DragonFractalTask
    {
        /*
        Начните с точки (1, 0)
        Создайте генератор рандомных чисел с сидом seed

        На каждой итерации:

        1. Выберите случайно одно из следующих преобразований и примените его к текущей точке:

            Преобразование 1. (поворот на 45° и сжатие в sqrt(2) раз):
            x' = (x · cos(45°) - y · sin(45°)) / sqrt(2)
            y' = (x · sin(45°) + y · cos(45°)) / sqrt(2)

            Преобразование 2. (поворот на 135°, сжатие в sqrt(2) раз, сдвиг по X на единицу):
            x' = (x · cos(135°) - y · sin(135°)) / sqrt(2) + 1
            y' = (x · sin(135°) + y · cos(135°)) / sqrt(2)

        2. Нарисуйте текущую точку методом pixels.SetPixel(x, y)

        */


        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            double x = 1;
            double y = 0;
            double angle = Math.PI / 4;

            var randomNum = new Random(seed);

            for (int i = 0; i < iterationsCount; i++)
            {
                var proirX = x;
                if (randomNum.Next(2) == 0)
                {
                    x = ChangeCoordX(x, y, angle, 0);
                    y = ChangeCoordY(proirX, y, angle);
                }
                else
                {
                    x = ChangeCoordX(x, y, angle*3, 1);
                    y = ChangeCoordY(proirX, y, angle*3);
                }
                pixels.SetPixel(x, y);
            }
        }

        static double ChangeCoordX(double x, double y, double angle, int oneOrZero)
        {
            return (x * Math.Cos(angle) - y * Math.Sin(angle)) / Math.Sqrt(2) + oneOrZero;
            
        }
        static double ChangeCoordY(double previousX, double y, double angle)
        {
            return (previousX * Math.Sin(angle) + y * Math.Cos(angle)) / Math.Sqrt(2);
        }
    }
}