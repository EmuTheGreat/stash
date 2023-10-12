/*Дан S — набор целочисленных точек на плоскости. 
 * Нужно получить 1-окрестность этих точек. То есть все точки (по одному разу), 
 * являющиеся соседними хотя бы с одной из точек S.
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson5.LQ2
{
    class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x; Y = y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public override string ToString() => $"{X} {Y}";

        public override bool Equals(object obj)
        {
            var p = obj as Point;
            return X == p.X && Y == p.Y;
        }
    }

    internal class Program
    {
        static void Main()
        {
            var e = new Point[] { new Point(0, 0), new Point(0, 1) };
            LQ2.GetNeighborhood(e).DoAction(Console.WriteLine);
        }
    }

    static class LQ2
    {
        public static IEnumerable<Point> GetNeighborhood(IEnumerable<Point> points)
        {
            int[] d = { -1, 0, 1 };
            return points.Select(p => d.SelectMany(x => d.Select(y => new Point(p.X + x, p.Y + y))).Where(x => !x.Equals(p)))
                .SelectMany(n => n).Distinct();
        }

        public static void DoAction<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach(var e in source) action(e);
        }
    }
}
