using System;

namespace Mazes
{
	public static class DiagonalMazeTask
	{
        public static void MoveOut(Robot robot, int width, int height)
        {
            Direction dirLong, dirShort;
            int countOfStep;


            if (width > height)
            {
                countOfStep = width / (height - 1);
                (dirLong, dirShort) = (Direction.Right, Direction.Down);
            }
            else
            {
                countOfStep = height / (width - 1);
                (dirLong, dirShort) = (Direction.Down, Direction.Right);
            }
            
            while (true)
            {
                MoveToDirection(robot, countOfStep, dirLong);
                if (robot.Finished) break;
                MoveToDirection(robot, 1, dirShort);
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

        /*static void MoveInDirection(Robot robot, int stepCount, Direction dir)
        {
            while (stepCount-- > 0) robot.MoveTo(dir);
        }*/
    }
}