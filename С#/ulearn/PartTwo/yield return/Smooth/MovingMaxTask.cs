using System.Collections.Generic;

namespace yield;

public static class MovingMaxTask
{
	public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
	{
		LinkedList<double> maxList = new LinkedList<double>();
		Queue<double> queue = new Queue<double>();
		foreach (var e in data)
		{
            var value = e.OriginalY;
            queue.Enqueue(value);
			if (queue.Count > windowWidth && queue.Dequeue() == maxList.First.Value) 
                maxList.RemoveFirst();
            
            while (maxList.Count > 0 && value > maxList.Last.Value) 
                maxList.RemoveLast();
            maxList.AddLast(value);

            yield return e.WithMaxY(maxList.First.Value);
        }
    }
}