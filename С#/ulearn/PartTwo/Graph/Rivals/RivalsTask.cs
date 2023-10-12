using System;
using System.Collections.Generic;

namespace Rivals;

public class RivalsTask
{
    public static IEnumerable<OwnedLocation> AssignOwners(Map map)
    {
        var visited = new HashSet<Point>();
        var queue = new Queue<Tuple<Point, int, int>>();

        for (int i = 0; i < map.Players.Length; i++)
            queue.Enqueue(Tuple.Create(map.Players[i], i, 0));

        while (queue.Count != 0)
        {
            var item = queue.Dequeue();
            var point = item.Item1;
            if (visited.Contains(point) 
                || !map.InBounds(point) 
                || map.Maze[point.X, point.Y] == 0) continue;
            visited.Add(point);
            yield return new OwnedLocation(item.Item2, point, item.Item3);
            for (var dx = -1; dx <= 1; dx++)
                for (var dy = -1; dy <= 1; dy++)
                    if (dx == 0 || dy == 0) 
                        queue.Enqueue(Tuple.Create(point + new Point(dx, dy),
                           item.Item2, item.Item3 + 1));
        }
    }
}