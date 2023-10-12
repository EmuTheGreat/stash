using System.Collections.Generic;

namespace yield;

public static class MovingAverageTask
{
	public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
	{
		var queue = new Queue<double>();
		double sum = 0;
		foreach(var e in data)
		{
			var value = e.OriginalY;
			queue.Enqueue(value);
			sum += value;
            if (queue.Count > windowWidth) sum -= queue.Dequeue();         
            yield return e.WithAvgSmoothedY(sum / queue.Count);
		}
	}
}