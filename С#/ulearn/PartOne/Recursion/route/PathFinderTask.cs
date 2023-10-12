using System;
using System.Drawing;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            int[] path = new int[checkpoints.Length];
            double shortestDistance = double.PositiveInfinity;
            int[] bestPath = new int[checkpoints.Length];

            MakePermutations(bestPath, path, 1, shortestDistance, checkpoints);
            return bestPath;
        }

        private static double MakePermutations(int[] shortestPath,
             int[] path, int position, double shortestDistance, Point[] checkpoints)
        {
            var totalCheckpoints = checkpoints.Length;
            var pathLength = checkpoints.GetPathLength(path);

            if (position == path.Length && pathLength < shortestDistance)
            {
                shortestDistance = pathLength;
                Array.Copy(path, shortestPath, totalCheckpoints);
                return shortestDistance;
            }
            for (var i = 0; i < totalCheckpoints; i++)
            {
                var index = Array.IndexOf(path, i, 0, position);
                if (index == -1)
                {
                    path[position] = i;
                    shortestDistance = MakePermutations(shortestPath, path, position + 1, shortestDistance, checkpoints);
                }
            }
            return shortestDistance;
        }
    }
}