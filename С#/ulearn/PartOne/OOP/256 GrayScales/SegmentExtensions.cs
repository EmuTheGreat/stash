using System.Collections.Generic;
using System.Drawing;
using GeometryTasks;

namespace GeometryPainting
{
    //Напишите здесь код, который заставит работать методы segment.GetColor и segment.SetColor
    public static class SegmentExtensions
    {
        private static Dictionary<Segment, Color> Dictionary = new Dictionary<Segment, Color>();
        
        public static void SetColor(this Segment segment, Color color)
        {
            Dictionary[segment] = color;
        }

        public static Color GetColor(this Segment segment)
        {
            var c = Color.Black;
            if (Dictionary.ContainsKey(segment)) c = Dictionary[segment];
            return c;
        }


        /* private static readonly Dictionary<Segment, Color> storedColors = new Dictionary<Segment, Color>();

         public static void SetColor(this Segment s, Color c)
         {
             storedColors[s] = c;
         }

         public static Color GetColor(this Segment s)
         {
             storedColors.TryGetValue(s, out var c);
             return c;
         }*/

    }
}
