namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            MoveInDirection(robot, height - 3, Direction.Down);
            MoveInDirection(robot, width - 3, Direction.Right);
        }

        static void MoveInDirection(Robot robot, int stepCount, Direction dir)
        {
            while (stepCount-- > 0) robot.MoveTo(dir);
        }
    }
}