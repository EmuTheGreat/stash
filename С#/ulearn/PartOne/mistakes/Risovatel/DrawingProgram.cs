using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Painter
    {
        static float x, y;
        static Graphics graphic;

        public static void DoInitialization(Graphics newGraphic)
        {
            graphic = newGraphic;
            graphic.SmoothingMode = SmoothingMode.None;
            graphic.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        { x = x0; y = y0; }

        public static void DrawIt(Pen pen, double length, double angle)
        {
            //Делает шаг длиной length в направлении angle и рисует пройденную траекторию
            var x1 = x;
            var y1 = y;
            Change(length, angle);
            graphic.DrawLine(pen, x, y, x1, y1);
        }

        public static void Change(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double angleOfRotation, Graphics graphic)
        {
            Painter.DoInitialization(graphic);

            var size = Math.Min(width, height);
            float bigSide = 0.375f;
            float smallSide = 0.04f;

            var diagonalLength = Math.Sqrt(2) * (size * bigSide + size * smallSide) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            Painter.SetPosition(x0, y0);

            //Рисуем весь квадрат
            for (int i = 0; i < 4; i++)
            {
                DrawSide(size, angleOfRotation, bigSide, smallSide);
                angleOfRotation += -Math.PI / 2;
            }
        }

        private static void DrawSide(int size, double angleOfRotation, float bigSide, float smallSide)
        {
            Painter.DrawIt(Pens.Yellow, size * bigSide, 0 + angleOfRotation);
            Painter.DrawIt(Pens.Yellow, size * smallSide * Math.Sqrt(2), Math.PI / 4 + angleOfRotation);
            Painter.DrawIt(Pens.Yellow, size * bigSide, Math.PI + angleOfRotation);
            Painter.DrawIt(Pens.Yellow, size * bigSide - size * smallSide, Math.PI / 2 + angleOfRotation);

            Painter.Change(size * smallSide, Math.PI + angleOfRotation);
            Painter.Change(size * smallSide * Math.Sqrt(2), 3 * Math.PI / 4 + angleOfRotation);
        }
    }
}