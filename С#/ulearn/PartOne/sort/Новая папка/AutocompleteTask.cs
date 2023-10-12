using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var total = Math.Min(count, GetCountByPrefix(phrases, prefix));
            var startIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var s = new List<string>();

            for (int i = startIndex; total > 0; total--) s.Add(phrases[++i]);

            return s.ToArray();
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            return right - left - 1;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        static List<string> phrases = new List<string> { "a", "ab", "abc", "b" };
        
        public static void CountTest(string prefix, int expectedResult)
        {
            var result = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedResult, result);
        }
        
        public static void TopTests(string prefix, string[] expectedResult, int count)
        {
            var result = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedResult, result);
        }

        // CountTests
        [TestCase("a", 3)]
        [TestCase("c", 0)]

        public static void RunTests(string input, int expectedOutput)
        {
            CountTest(input, expectedOutput);
        }

        //TopTests
        [TestCase("a", new[] { "a", "ab" }, 2)]
        [TestCase("a", new[] { "a", "ab", "abc" }, 4)]
        [TestCase("c", new string[0], 2)]

        public static void RunTests(string input, string[] expectedOutput, int count)
        {
            TopTests(input, expectedOutput, count);
        }

    }
}
