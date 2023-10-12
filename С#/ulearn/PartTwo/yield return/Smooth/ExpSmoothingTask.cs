using System.Collections.Generic;

namespace yield;

public static class ExpSmoothingTask
{
	public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
	{
		DataPoint previousPoint = null;
		double expSmooth;
		foreach(var e in data)
		{
            if (previousPoint == null) expSmooth = e.OriginalY;
			else expSmooth = alpha * e.OriginalY + (1 - alpha) * previousPoint.ExpSmoothedY;
            yield return previousPoint = e.WithExpSmoothedY(expSmooth);
        }
	}
}
