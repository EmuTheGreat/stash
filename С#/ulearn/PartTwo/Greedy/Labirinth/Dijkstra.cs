using System;
using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;

namespace Greedy
{
    public class DijkstraPathFinder1
    {
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
        {
            var pointsToVisit = new HashSet<Point>() { start };
            var visited = new HashSet<Point>();
            var pathCost = new Dictionary<Point, PathData>() { [start] = new PathData(0, null) };
            while (pointsToVisit.Count > 0)
            {
                var lowCostPoint = pointsToVisit.OrderBy(p => pathCost.ContainsKey(p) ? pathCost[p].Cost : double.PositiveInfinity).FirstOrDefault();
                if (targets.Contains(lowCostPoint))
                    yield return GetPathByTrack(pathCost, start, lowCostPoint);
                UpdateTrackAndPossibleToVisit(state, pathCost, lowCostPoint, visited, pointsToVisit);
                pointsToVisit.Remove(lowCostPoint);
                visited.Add(lowCostPoint);
            }
        }

        public class PathData
        {
            public int Cost { get; set; }
            public Point? PreviousPoint { get; set; }
            public PathData(int cost, Point? previousPoint)
            {
                Cost = cost;
                PreviousPoint = previousPoint;
            }
        }

        private static PathWithCost GetPathByTrack(Dictionary<Point, PathData> track, Point? start, Point? end)
        {
            var result = new List<Point>();
            var endCost = track[end.Value].Cost;
            while (end.HasValue)
            {
                result.Add(end.Value);
                end = track[end.Value].PreviousPoint;
            }
            result.Reverse();
            return new PathWithCost(endCost, result.ToArray());
        }

        private static IEnumerable<Point> GetBoundaryPoints(State state, Point point, HashSet<Point> visited)
        {
            var directions = new Point[] { new Point(-1, 0), new Point(1, 0), new Point(0, -1), new Point(0, 1) };
            return directions.Select(x => point + x)
            .Where(x => IsValidPoint(state, x, visited));
        }

        private static bool IsValidPoint(State state, Point point, HashSet<Point> visited)
        {
            return state.InsideMap(point) && !state.IsWallAt(point) && !visited.Contains(point);
        }

        private static int GetPrice(State state, Point point)
        {
            return state.CellCost[point.X, point.Y];
        }

        private static void UpdateTrackAndPossibleToVisit(State state, Dictionary<Point, PathData> track, Point point,
        HashSet<Point> visited, HashSet<Point> possibleToVisit)
        {
            foreach (var newPoint in GetBoundaryPoints(state, point, visited))
            {
                var currentPrice = track[point].Cost + GetPrice(state, newPoint);
                if (!track.ContainsKey(newPoint) || track[newPoint].Cost > currentPrice)
                    track[newPoint] = new PathData(currentPrice, point);
                possibleToVisit.Add(newPoint);
            }
        }
    }
}