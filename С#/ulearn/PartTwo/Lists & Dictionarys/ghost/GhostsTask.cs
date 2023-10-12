using System;
using System.Text;

namespace hashes;

public class GhostsTask :
    IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
    IMagic
{
    Vector vector = new(1, 1);
    Segment segment = new(new(1, 1), new(2, 2));
    Robot robot = new("robot1", 2);
    Cat cat = new("cat1", "breed1", new DateTime(2004, 2, 3));
    byte[] content = new byte[] { 1, 2, 3 };

    public Document Create()
    {
        return new Document("doc1", Encoding.UTF8, content);
    }

    public void DoMagic()
    {
        vector.Add(new Vector(1, 1));
        segment.Start.Add(new(5, 5));
        Robot.BatteryCapacity -= 50;
        content[0] = 10;
        cat.Rename("cat2");
    }

    Vector IFactory<Vector>.Create()
    {
        return vector;
    }

    Segment IFactory<Segment>.Create()
    {
        return segment;
    }

    Cat IFactory<Cat>.Create()
    {
        return cat;
    }

    Robot IFactory<Robot>.Create()
    {
        return robot;
    }
}