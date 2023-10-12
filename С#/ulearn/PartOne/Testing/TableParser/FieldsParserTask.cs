using System.Collections.Generic;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("\"\"", new[] { "" })]
        [TestCase("", new string[0])]
        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]

        [TestCase("hello    world", new[] { "hello", "world" })]
        [TestCase("hello 'world'", new[] { "hello", "world" })]
        [TestCase("'hello' world", new[] { "hello", "world" })]

        [TestCase("' '", new[] { " " })]
        [TestCase("\"'hello world'\"", new[] { "'hello world'" })]
        [TestCase("'\"hello world\"'", new[] { "\"hello world\"" })]
        [TestCase("\"hello", new[] { "hello" })]

        [TestCase("\"world ", new[] { "world " })]
        [TestCase(" hello world   ", new[] { "hello", "world" })]
        [TestCase("hello'world", new[] { "hello", "world" })]

        [TestCase("\'\\\'world\\\'\'", new[] { "\'world\'" })]
        [TestCase("\"\\\"hello\\\"\"", new[] { "\"hello\"" })]
        [TestCase("a\"b c d e\"", new[] { "a", "b c d e" })]

        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            var tokenList = new List<Token>();
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsWhiteSpace(line[i])) continue;
                if (line[i] == '\'' || line[i] == '\"') tokenList.Add(QuotedFieldTask.ReadQuotedField(line, i));
                else tokenList.Add(ReadField(line, i));
                i += tokenList[tokenList.Count - 1].Length - 1;
            }
            return tokenList;
        }

        private static Token ReadField(string line, int startIndex)
        {
            int count = 0;
            for (int i = startIndex; i < line.Length; i++)
            {
                if (line[i] == '\'' || line[i] == '\"' || line[i] == ' ') break;
                count++;
            }
            return new Token(line.Substring(startIndex, count), startIndex, count);
        }
    }
}