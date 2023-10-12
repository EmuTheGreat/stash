using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var newElbow = shoulder + elbow - Math.PI;
            var newWrist = newElbow + wrist - Math.PI;

            var elbowPos = new PointF(Manipulator.UpperArm * (float)Math.Cos(shoulder), Manipulator.UpperArm * (float)Math.Sin(shoulder));
            var wristPos = new PointF(elbowPos.X + Manipulator.Forearm * (float)Math.Cos(newElbow), elbowPos.Y + Manipulator.Forearm * (float)Math.Sin(newElbow));
            var palmEndPos = new PointF(wristPos.X + Manipulator.Palm * (float)Math.Cos(newWrist), wristPos.Y + Manipulator.Palm * (float)Math.Sin(newWrist));
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI, Math.PI / 2, Manipulator.Palm, Manipulator.Forearm + Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI, Math.PI, 0, Manipulator.Palm + Manipulator.Forearm + Manipulator.UpperArm)]
        [TestCase(0, Math.PI, Math.PI, Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm, 0)]

        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(Manipulator.UpperArm, Math.Sqrt(joints[0].X * joints[0].X + joints[0].Y * joints[0].Y), 1e-5);
            Assert.AreEqual(Manipulator.Forearm, Math.Sqrt(Math.Pow(joints[1].X - joints[0].X, 2) + Math.Pow(joints[1].Y - joints[0].Y, 2)), 1e-5);
            Assert.AreEqual(Manipulator.Palm, Math.Sqrt(Math.Pow(joints[2].X - joints[1].X, 2) + Math.Pow(joints[2].Y - joints[1].Y, 2)), 1e-5);
        }
    }
}