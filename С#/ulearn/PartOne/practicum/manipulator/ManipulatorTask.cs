using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        /// <summary>
        /// Возвращает массив углов (shoulder, elbow, wrist),
        /// необходимых для приведения эффектора манипулятора в точку x и y 
        /// с углом между последним суставом и горизонталью, равному alpha (в радианах)
        /// См. чертеж manipulator.png!
        /// </summary>
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            double wristPosX = x - Manipulator.Palm * Math.Cos(alpha);
            double wristPosY = y + Manipulator.Palm * Math.Sin(alpha);
            var elbow = TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, Math.Sqrt(wristPosX * wristPosX + wristPosY * wristPosY));
            var shoulder = TriangleTask.GetABAngle(Math.Sqrt(wristPosX * wristPosX + wristPosY * wristPosY), Manipulator.UpperArm, Manipulator.Forearm) +
                Math.Atan2(wristPosY, wristPosX);
            var wrist = -alpha - shoulder - elbow;

            return new[] { shoulder, elbow, wrist };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            var rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                var x = rnd.NextDouble() * rnd.Next(101);
                var y = rnd.NextDouble() * rnd.Next(101);
                var angle = rnd.NextDouble() + rnd.Next(2);

                var actualAngles = ManipulatorTask.MoveManipulatorTo(x, y, angle);
                var actualEndPos = AnglesToCoordinatesTask.GetJointPositions(actualAngles[0], actualAngles[1], actualAngles[2])[2];

                Assert.AreEqual(x, (double)actualEndPos.X, 9e-5, "x");
                Assert.AreEqual(y, (double)actualEndPos.Y, 9e-5, "y");
            }
        }
    }
}