using NUnit.Framework;

namespace BracketsLesson
{
    [TestFixture]
    class BracketsTest
    {
        
        [TestCase("((()))", true)]
        //public static void RunTests(string input, bool expectedOutput)
        //{
        //    Test(input, expectedOutput);
        //}
        public static void Test(string input, bool expectedResult)
        {
            var actualResult = Program.IsCorrectBrackets(input);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
