using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;

namespace Greedy;

public class NotGreedyPathFinder : IPathFinder
{
	public List<Point> FindPathToCompleteGoal(State state)
	{
        var result = new List<Point>();
        var dijkstra = new DijkstraPathFinder();
        var pathsBetweenChests = new Dictionary<Point, IEnumerable<PathWithCost>>
        {
            [state.Position] = dijkstra.GetPathsByDijkstra(state, state.Position, state.Chests)
        };

        foreach (var chest in state.Chests)
        {
            pathsBetweenChests[chest] = dijkstra.GetPathsByDijkstra(state, chest, state.Chests);
        }

        for (var i = 1; i <= 7; i++)
        {
            var reachableChestPermutations = GetPermutations(state.Chests.Take(7).ToList(), i)
                .Where(perm => PathsPermutationStaminaCost(state, perm, pathsBetweenChests) <= state.Energy);

            if (!reachableChestPermutations.Any())
                break;

            result = reachableChestPermutations
                .First()
                .ToList();
        }

        return GetTotalPathByPermutation(state, result, pathsBetweenChests);
    }


    private static IEnumerable<IEnumerable<Point>> GetPermutations<Point>(List<Point> list, int length)
    {
        if (length == 1) return list.Select(t => new[] { t });
        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(o => !t.Contains(o)),
                (t1, t2) => t1.Concat(new[] { t2 }));
    }

    private static int PathsPermutationStaminaCost(State state, IEnumerable<Point> chests,
            Dictionary<Point, IEnumerable<PathWithCost>> pathsBetweenChests)
    {
        var current = state.Position;
        var total = 0;

        foreach (var chest in chests)
        {
            total += PathStaminaCost(state, pathsBetweenChests[current]);
            current = chest;
        }

        return total;
    }
    private static int PathStaminaCost(State state, IEnumerable<PathWithCost> path)
    {
        return path.Select(path => path.Cost).Sum();
    }

    private static List<Point> GetTotalPathByPermutation(State state, ICollection<Point> pointsPermutation,
            Dictionary<Point, IEnumerable<PathWithCost>> pathsBetweenChests)
    {
        var current = state.Position;
        var result = new List<Point>();

        foreach (var point in pointsPermutation)
        {
            result.AddRange(pathsBetweenChests[current].SelectMany(path => path.Path));
            current = point;
        }

        return result;
    }
}
