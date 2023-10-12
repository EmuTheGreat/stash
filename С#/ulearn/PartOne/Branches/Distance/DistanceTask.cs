using System;

namespace DistanceTask
{
    public static class DistanceTask
    {
        // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)

        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {


            //Перпендикуляр нельзя провести, если точки совпадают, поэтому возвращаем минимальное из расстояний до точек A или B.
            //  Если нельзя опустить перпенидкуляр на отрезок, то возвращаем минимальное из расстояний до точек A или B.

            if (ay == by && ax == bx || (bx - ax) * (x - ax) + (y - ay) * (by - ay) < 0 ||
                                        (ax - bx) * (x - bx) + (y - by) * (ay - by) < 0)

                return Math.Min(DistancePoints(ax, x, ay, y),
                                DistancePoints(bx, x, by, y));

            // В ином случае применям стандартную формулу нахождения длины перпендикуляра на отрезок.
            return Math.Abs((ax - bx) * (ay - y) - (ay - by) * (ax - x)) /
                  DistancePoints(ax, bx, ay, by);
        }

        static double DistancePoints(double x, double x0, double y, double y0)
        {
            return Math.Sqrt((x - x0) * (x - x0) + (y - y0) * (y - y0));
        }
    }
}