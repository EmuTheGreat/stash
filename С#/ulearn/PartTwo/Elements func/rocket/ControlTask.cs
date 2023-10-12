using System;

namespace func_rocket;

public class ControlTask
{
    static double angle = 0;
    public static Turn ControlRocket(Rocket rocket, Vector target)
    {
        CalculateAngle(rocket, target);
        if (angle < 0) return Turn.Left;
        else if (angle > 0) return Turn.Right;
        
        return Turn.None;
    }

    private static void CalculateAngle(Rocket rocket, Vector target)
    {
        var distance = target - rocket.Location;
        
        if (CheckAngle(distance, rocket))
            angle = distance.Angle - (rocket.Velocity.Angle + rocket.Direction) / 2;
        else angle = distance.Angle - rocket.Direction;
    }

    private static bool CheckAngle(Vector distance, Rocket rocket)
    {
        return Math.Abs(distance.Angle - rocket.Direction) < 0.5 ||
            Math.Abs(distance.Angle - rocket.Velocity.Angle) < 0.5;
    }
}
