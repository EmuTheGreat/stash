namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            for (int i = 0; ; i++)
            {
                var dir = i % 2 == 0 ? Direction.Right : Direction.Left;
                MoveToDirection(robot, width - 3, dir);
                if (robot.Finished) break;
                MoveToDirection(robot, 2, Direction.Down);
            }
        }

        static void MoveToDirection(Robot robot, int countOfStep, Direction direction)
        {
            while (countOfStep != 0)
            {
                robot.MoveTo(direction);
                countOfStep--;
            }
        }
    }
}