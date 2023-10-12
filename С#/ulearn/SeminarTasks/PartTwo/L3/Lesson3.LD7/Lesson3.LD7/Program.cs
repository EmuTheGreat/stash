using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3.LD7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            List<string> palindromes = FindPalindromes(s);
            foreach (string palindrome in palindromes)
            {
                Console.WriteLine(palindrome);
            }
        }

        static List<string> FindPalindromes(string s)
        {
            List<string> palindromes = new List<string>();
            int n = s.Length;
            int[] p = new int[n];
            int center = 0, right = 0;
            for (int i = 1; i < n - 1; i++)
            {
                if (right > i)
                {
                    p[i] = Math.Min(right - i, p[2 * center - i]);
                }
                while (s[i + p[i] + 1] == s[i - p[i] - 1])
                {
                    p[i]++;
                }
                if (i + p[i] > right)
                {
                    center = i;
                    right = i + p[i];
                }
            }
            for (int i = 1; i < n - 1; i++)
            {
                if (p[i] > 0)
                {
                    string palindrome = s.Substring(i - p[i], 2 * p[i] + 1);
                    palindromes.Add(palindrome);
                }
            }
            return palindromes;
        }
    }
}
