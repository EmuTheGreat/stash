using System;
using System.Collections.Generic;

namespace func_rocket;

public class LevelsTask
{
    static readonly Physics standardPhysics = new();
    static readonly Rocket rocket = new(new(200, 500), Vector.Zero, -0.5 * Math.PI);
    static readonly Vector standardTargetPosition = new(700, 500);

    public static IEnumerable<Level> CreateLevels()
    {
        yield return CreateLevel("Zero", (size, l) => Vector.Zero);
        yield return CreateLevel("Heavy", (size, l) => new(0, 0.9));
        yield return CreateLevel("Up", (size, l) => new(0, -300 / (300 + (size.Y - l.Y))));
        yield return CreateLevel("WhiteHole", (size, l) => WhiteHole(l));
        yield return CreateLevel("BlackHole", (size, l) => BlackHole(l));
        yield return CreateLevel("BlackAndWhite", (size, l) => (WhiteHole(l) + BlackHole(l)) / 2);
    }

    public static Vector BlackHole(Vector location)
    {
        var anomaly = (standardTargetPosition + rocket.Location) / 2;
        var d = (anomaly - location).Length;
        return (anomaly - location).Normalize() * 300 * d / (d * d + 1);
    }

    public static Vector WhiteHole(Vector location)
    {
        var d = (location - standardTargetPosition).Length;
        return (location - standardTargetPosition).Normalize() * (140 * d) / (d * d + 1);
    }

    public static Level CreateLevel(string name, Gravity gravity)
    {
        return new(name, rocket, standardTargetPosition, gravity, standardPhysics);
    }
}