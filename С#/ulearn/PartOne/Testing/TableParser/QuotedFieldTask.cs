using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("'  a'", 0, "  a", 5)]
        [TestCase("'\"'", 0, "\"", 3)]

        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
        // Добавьте свои тесты
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var builder = new StringBuilder();
            int count = 1;
            for (int i = startIndex + 1; i < line.Length; i++)
            {
                count++;
                if (line[i - 1] != '\\' && line[i] == line[startIndex]) break;
                if (line[i - 1] == '\\') builder[builder.Length - 1] = line[i];
                else builder.Append(line[i]);
            }
            return new Token(builder.ToString(), startIndex, count);
        }
    }
}