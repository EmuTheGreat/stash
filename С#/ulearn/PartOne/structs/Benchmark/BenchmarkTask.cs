using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            GC.Collect();                   // Эти две строчки нужны, чтобы уменьшить вероятность того,
            GC.WaitForPendingFinalizers();  // что Garbadge Collector вызовется в середине измерений
                                            // и как-то повлияет на них.
            var time = new Stopwatch();
            task.Run();
            time.Start();
            for (int i = 0; i < repetitionCount; i++) 
                task.Run();
            time.Stop();

            return time.Elapsed.TotalMilliseconds / repetitionCount;
        }
    }

    public class StringBuilderTask : ITask
    {
        public void Run()
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < 10000; i++) 
                stringBuilder.Append("a");
            stringBuilder.ToString();
        }
    }

    public class StringTask : ITask
    {
        public void Run()
        {
            new string('a', 10000);
        }
    }

    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var benchmark = new Benchmark();
            var str = new StringTask();
            var stringBuilder = new StringBuilderTask();
            Assert.Less(benchmark.MeasureDurationInMs(str, 1), benchmark.MeasureDurationInMs(stringBuilder, 1));
        }
    }
}