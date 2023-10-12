using System;
using RectangleGeometry;

//Спроектируйте класс Прямоугольника со сторонами не обязательно параллельными осям координат.
//В классе должен быть способ получения вершин (Vector) и сторон(Segment). Какие методы изменения прямоугольника могут быть в таком классе?
//Какие методы контроля целостности стоит применить в этом классе.


namespace RectangleGeometry
{
    public class Vector
    {
        public double X;
        public double Y;

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class Rectangle : Geometry
    {
        // В данном классе должны находится приватные поля и их свойства,
        // через которые будут изменятся значения полей так,
        // чтобы не нарушалось условие прямоугольника .
        private Vector a;
        private Vector b;
        private Vector c;
        private Vector d;

        // Например: Изменение вдоль луча AD
        public Vector A
        {
            get { return a; }
            set
            {
                var newPointB = Add(b, new Vector(value.X - a.X, value.Y - a.Y));
                if (!IsRectangle(value, newPointB, c, d)) throw new ArgumentException();
                a = value;
                b = newPointB;
            }
        }
        /*public Vector B { get { return b; } set { b = value; } }
        
        public Vector C { get { return c; } set { c = value; } }*/
        
        // Изменение вдоль луча DC
        public Vector D
        {
            get { return d; }
            set 
            { 
                var newPointA = Add(a, new Vector(value.X - d.X, value.Y - d.Y));
                if (!IsRectangle(newPointA, b, c, value)) throw new ArgumentException();
                d = value;
                a = newPointA;
            }
        }

        // Если изначально точки не будут образовывать прямоуго
        public Rectangle(Vector a, Vector b, Vector c, Vector d)
        {
            if (!IsRectangle(a, b, c, d)) throw new ArgumentException();
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        // Готовый прямоугольник
        public Rectangle() : this(new Vector(4, 6), new Vector(7, 5), new Vector(5, 1), new Vector(2, 2)) { }
    }

    public class Geometry
    {
        const double eps = 100 * double.Epsilon;
        
        // Является ли прямоугольником
        public bool IsRectangle(Vector A, Vector B, Vector C, Vector D)
        {
            return Math.Pow(GetSegmentLength(A, D), 2) + Math.Pow(GetSegmentLength(D, C), 2) -
                (Math.Pow(GetSegmentLength(A, B), 2) + Math.Pow(GetSegmentLength(B, C), 2)) <= eps
                && Math.Abs(GetSegmentLength(A, B) - GetSegmentLength(B, C)) > eps;
        }

        public double GetVectorLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public double GetSegmentLength(Vector vec1, Vector vec2)
        {
            return GetVectorLength(new Vector(vec2.X - vec1.X, vec2.Y - vec1.Y));
        }

        public Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }
    }
}

namespace Lesson14.Cons2
{
    class Program
    {
        static void Main()
        {
            //var rec = new Rectangle(new Vector(0, 0), new Vector(0,1), new Vector(1, 1), new Vector(1, 0)); - квадрат, вызовет ошибку аргумента
            var rec = new Rectangle();
        }
    }
}
