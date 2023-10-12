using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public class StatisticsTask
{
    public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
    {
        return visits.OrderBy(visit => visit.DateTime)
            .GroupBy(visit => visit.UserId)
            .SelectMany(group => group.Bigrams().Where(tuple => tuple.First.SlideType == slideType))
            .Select(tuple => (tuple.Second.DateTime - tuple.First.DateTime).TotalMinutes)
            .Where(x => x >= 1 && x <= 120)
            .DefaultIfEmpty()
            .Median();
    }
}