using System.Collections.Generic;
using System.Linq;
using System;

namespace Dungeon
{
    public class DungeonTask
    {
        public static MoveDirection[] FindShortestPath(Map map)
        {
            var exit = BfsTask.FindPaths(map, map.InitialPosition, new[] { map.Exit }).FirstOrDefault();
            
            if (exit == null)
                return Array.Empty<MoveDirection>();
            
            var chests = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);

            if (map.Chests.Any(chest => exit.ToList().Contains(chest)))
                return exit.ToList()
                    .TransformToDirections();

            var result = chests
                .Select(chest => Tuple.Create(chest, BfsTask.FindPaths(map, chest.Value, new[] { map.Exit })
                .FirstOrDefault()))
                .GetShortestPath();

            if (result == null) return exit.ToList()
                    .TransformToDirections();

            return result.Item1.ToList()
                .TransformToDirections()
                .Concat(result.Item2.ToList().TransformToDirections())
                .ToArray();
        }
    }

    public static class Extentions
    {
        public static Tuple<SinglyLinkedList<Point>, SinglyLinkedList<Point>>?
            GetShortestPath(this IEnumerable<Tuple<SinglyLinkedList<Point>, SinglyLinkedList<Point>>> items)
        {
            if (!items.Any() || items.First().Item2 == null) return null;
            
            var min = int.MaxValue;
            var shortestPath = items.First();
            
            foreach (var tuple in items)
                if (tuple.Item1.Length + tuple.Item2.Length < min)
                {
                    min = tuple.Item1.Length + tuple.Item2.Length;
                    shortestPath = tuple;
                }
            return shortestPath;
        }

        static MoveDirection GetMoveDirection(Point firstPoint, Point secondPoint)
        {
            var point = firstPoint - secondPoint;
            if (point.X == -1) return MoveDirection.Right;
            if (point.Y == -1) return MoveDirection.Down;
            if (point.X == 1) return MoveDirection.Left;
            if (point.Y == 1) return MoveDirection.Up;
            
            throw new ArgumentException();
        }

        public static MoveDirection[] TransformToDirections(this List<Point> items)
        {
            var resultList = new List<MoveDirection>();
            var itemsLength = items.Count;

            if (items == null) return Array.Empty<MoveDirection>();
            for (var i = itemsLength - 1; i > 0; i--)
                resultList.Add(GetMoveDirection(items[i], items[i - 1]));
            
            return resultList.ToArray();
        }
    }
}