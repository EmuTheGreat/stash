using System.Collections.Generic;

namespace Dungeon;

public class BfsTask
{
    public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
    {
        var paths = new Dictionary<Point, SinglyLinkedList<Point>>();
        var visited = new HashSet<Point>();
        var queue = new Queue<Point>();
        paths.Add(start, new SinglyLinkedList<Point>(start));
        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count != 0)
        {
            var point = queue.Dequeue();
            if (!map.InBounds(point) || map.Dungeon[point.X, point.Y] == 0) continue;
            foreach (var e in Walker.PossibleDirections)
            {
                var nextPoint = point + e;
                if (visited.Contains(nextPoint)) continue;
                queue.Enqueue(nextPoint);
                visited.Add(nextPoint);
                paths.Add(nextPoint, new SinglyLinkedList<Point>(nextPoint, paths[point]));
            }
        }

        foreach (var p in chests)
            if (paths.ContainsKey(p)) yield return paths[p];
    }
}