using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public class ParsingTask
{
    public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
    {
        return lines.Select(s => s.Split(';'))
            .Where(s => CheckSlideRecord(s[1]) != -1 && s.Length == 3)
            .Select(s => CreateSlide(s))
            .Where(s => s != null)
            .ToDictionary(s => s.SlideId, s => s);
    }

    public static IEnumerable<VisitRecord> ParseVisitRecords(
        IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
    {
        return lines
            .Select(s => CreateVisit(s, slides))
            .Skip(1);
    }

    public static int CheckSlideRecord(string s)
    {
        switch (s)
        {
            case "theory": return 0;
            case "exercise": return 1;
            case "quiz": return 2;
            default: return -1;
        };
    }

    public static SlideRecord CreateSlide(string[] s)
    {
        if (int.TryParse(s[0], out int id))
            return new SlideRecord(id, (SlideType)CheckSlideRecord(s[1]), s[2]);
        return null;
    }

    public static VisitRecord CreateVisit(string line, IDictionary<int, SlideRecord> slide)
    {
        var visitRecord = line.Split(';');
        if (visitRecord.Length == 4 &&
                int.TryParse(visitRecord[0], out int userId) &&
                int.TryParse(visitRecord[1], out int slideId) &&
                DateTime.TryParse(visitRecord[2], out _) &&
                DateTime.TryParse(visitRecord[3], out _) &&
                slide.ContainsKey(slideId))
            return new VisitRecord(userId, slideId,
                DateTime.Parse($"{visitRecord[2]} {visitRecord[3]}"), slide[slideId].SlideType);
        throw new FormatException("Wrong line [" + line + "]");
    }
}