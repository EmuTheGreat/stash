using System;
using System.Collections.Generic;


namespace StructBenchmarking
{
    public class IExperiment
    {
        public static List<ExperimentResult> GetResult(
            Func<int, ITask> task, IBenchmark benchmark, int repetitionsCount)
        {
            var times = new List<ExperimentResult>();
            for (int size = 16; size <= 512; size *= 2)
            {
                times.Add(new ExperimentResult(size, benchmark.MeasureDurationInMs(task(size), repetitionsCount)));
            }
            return times;
        }
    }
    public class ArrayResult : IExperiment
    {
        static public ITask ClassTask(int size)
        {
            return new ClassArrayCreationTask(size);
        }

        static public ITask StructTask(int size)
        {
            return new StructArrayCreationTask(size);
        }
    }

    public class MethodResult : IExperiment
    {
        static public ITask ClassTask(int size)
        {
            return new MethodCallWithClassArgumentTask(size);
        }

        static public ITask StructTask(int size)
        {
            return new MethodCallWithStructArgumentTask(size);
        }
    }

    public class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(
            IBenchmark benchmark, int repetitionsCount)
        {
            return new ChartData
            {
                Title = "Create array",
                ClassPoints = IExperiment.GetResult(ArrayResult.ClassTask, benchmark, repetitionsCount),
                StructPoints = IExperiment.GetResult(ArrayResult.StructTask, benchmark, repetitionsCount),
            };
        }

        public static ChartData BuildChartDataForMethodCall(
            IBenchmark benchmark, int repetitionsCount)
        {
            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = IExperiment.GetResult(MethodResult.ClassTask, benchmark, repetitionsCount),
                StructPoints = IExperiment.GetResult(MethodResult.StructTask, benchmark, repetitionsCount),
            };
        }
    }
}