using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;

namespace Greedy;

    public class GreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            var list = new List<Point>();
            var result = new List<Point>();
            var chests = new HashSet<Point>(state.Chests);
            var dijkstraPathFinder = new DijkstraPathFinder();
            var energy = state.InitialEnergy;

            while (state.Scores < state.Goal)
            {
                var path = dijkstraPathFinder
                    .GetPathsByDijkstra(state, state.Position, chests);
                var bestPath = path.FirstOrDefault();
                if (Equals(bestPath, null)) return list;
                energy -= bestPath.Cost;
                if (energy < 0) return list;
                state.Scores++;
                state.Position = bestPath.Path.Last();
                result.AddRange(bestPath.Path.Skip(1));
                chests.Remove(state.Position);
            }

            return result;
        }
    }

