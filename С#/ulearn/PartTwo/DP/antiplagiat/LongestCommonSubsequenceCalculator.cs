using System;
using System.Collections.Generic;

namespace Antiplagiarism
{
    public static class LongestCommonSubsequenceCalculator
    {
        public static List<string> Calculate(List<string> first, List<string> second)
        {
            var matrix = CreateOptimizationTable(first, second);
            return RestoreAnswer(matrix, first, second);
        }

        private static int[,] CreateOptimizationTable(List<string> first, List<string> second)
        {
            var matrix = new int[first.Count + 1, second.Count + 1];
            for (var i = 1; i <= first.Count; i++)
            {
                for (var j = 1; j <= second.Count; j++)
                {
                    if (first[i - 1] == second[j - 1])
                    {
                        matrix[i, j] = matrix[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        matrix[i, j] = Math.Max(matrix[i - 1, j], matrix[i, j - 1]);
                    }
                }
            }
            return matrix;
        }

        private static List<string> RestoreAnswer(int[,] matrix, List<string> first, List<string> second)
        {
            var res = new List<string>();
            var x = first.Count;
            var y = second.Count;

            while (x > 0 && y > 0)
            {
                if (first[x - 1] == second[y - 1])
                {
                    res.Add(first[x - 1]);
                    x--;
                    y--;
                }
                else if (matrix[x - 1, y] > matrix[x, y - 1]) x--;
                else y--;
            }
            res.Reverse();
            return res;
        }
    }
}
