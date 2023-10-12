using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson11.Dp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (var e in Dp.MaxSubarray(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9})) Console.WriteLine(e);
        }
    }
    static class Dp
    {
        public static int[] MaxSubarray(int[] nums)
        {
            int n = nums.Length;
            int[] dp = new int[n];
            dp[0] = nums[0];
            int[] best = new int[] { 0, 0 };
            for (int i = 1, start = 0; i < n; i++)
            {
                if (dp[i - 1] > 0)
                {
                    dp[i] = dp[i - 1] + nums[i];
                }
                else
                {
                    dp[i] = nums[i];
                    start = i;
                }
                if (dp[i] > dp[best[0]])
                {
                    best = new int[] { start, i };
                }
            }
            return best;
        }
    }
}
