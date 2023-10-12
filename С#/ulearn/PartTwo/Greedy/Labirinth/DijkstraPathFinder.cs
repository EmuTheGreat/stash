using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;

namespace Greedy
{
    public class DijkstraData
    {
        public Point Previous { get; set; }
        public double Price { get; set; }
    }

    public class DijkstraPathFinder
    {
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
        {
            var unvisited = new HashSet<Point>() { start };
            var visited = new HashSet<Point>();
            var track = new Dictionary<Point, DijkstraData>
            { [start] = new DijkstraData { Price = 0, Previous = new Point(-1, -1) } };

            while (true)
            {
                Point currentPoint = new Point(-1, -1);
                FindNextUnvisitedPoint(ref currentPoint, unvisited, track);
                if (currentPoint == new Point(-1, -1)) break;
                if (targets.Contains(currentPoint))
                    yield return new PathWithCost((int)track[currentPoint].Price, GetShortestPathToTarget(currentPoint, track));
                UpdateTrack(state, currentPoint, visited, unvisited, track);
                unvisited.Remove(currentPoint);
                visited.Add(currentPoint);
            }
        }

        public IEnumerable<Point> GetAdjacentPoints(Point point)
        {
            var directions = new Point[] { new Point(1, 0), new Point(0, 1), new Point(-1, 0), new Point(0, -1) };
            return directions.Select(dir => point + dir);
        }

        public void UpdateTrack(State state, Point currentPoint, HashSet<Point> visited, HashSet<Point> unvisited, Dictionary<Point, DijkstraData> track)
        {
            foreach (var adjacentPoint in GetAdjacentPoints(currentPoint)
                    .Where(point => state.InsideMap(point) && !state.IsWallAt(point) && !visited.Contains(point)))
            {
                unvisited.Add(adjacentPoint);
                var currentPrice = track[currentPoint].Price + state.CellCost[adjacentPoint.X, adjacentPoint.Y];
                var nextPoint = adjacentPoint;
                if (!track.ContainsKey(nextPoint) || track[nextPoint].Price > currentPrice)
                    track[nextPoint] = new DijkstraData { Price = currentPrice, Previous = currentPoint };
            }
        }

        public void FindNextUnvisitedPoint(ref Point currentPoint, HashSet<Point> unvisited, Dictionary<Point, DijkstraData> track)
        {
            var bestPrice = double.PositiveInfinity;
            foreach (var point in unvisited)
            {
                if (track.ContainsKey(point) && track[point].Price < bestPrice)
                {
                    bestPrice = track[point].Price;
                    currentPoint = point;
                }
            }
        }

        public Point[] GetShortestPathToTarget(Point target, Dictionary<Point, DijkstraData> track)
        {
            var shortestPath = new List<Point>();
            var currentPoint = target;
            while (currentPoint != new Point(-1, -1))
            {
                shortestPath.Add(currentPoint);
                currentPoint = track[currentPoint].Previous;
            }
            shortestPath.Reverse();
            return shortestPath.ToArray();
        }
    }
}