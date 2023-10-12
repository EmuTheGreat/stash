namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector vector)
        {
            return Geometry.Add(this, vector);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }

    public class Geometry
    {
        /*public static bool IsVectorInSegment(Vector vector, Segment segment)
        { 
            /*return (GetLength(new Segment { Begin = segment.Begin, End = vector }) + 
                GetLength(new Segment { Begin = vector, End = segment.End })).CompareTo(GetLength(segment)) == 0ж

        }*/
        public static bool IsVectorInSegment(Vector v, Segment s)
        {
            return Math.Abs(GetLength(s) - GetLength(new Segment { Begin = v, End = s.Begin }) - GetLength(new Segment { Begin = v, End = s.End })) <= 100 * double.Epsilon;
        }

        public static double GetLength(Segment segment)
        {
            return GetLength(new Vector() { X = segment.End.X - segment.Begin.X, Y = segment.End.Y - segment.Begin.Y });
        }

        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector { X = vector1.X + vector2.X, Y = vector1.Y + vector2.Y };
        }
    }
}