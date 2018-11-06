using System;
using System.Drawing;
//using AerohockeyGame;
using NUnit.Framework;

namespace Shooter
{
    [TestFixture]
    public class LineTests
    {
        [TestCase(1, 2, 1, 0, 2, -2)]
        [TestCase(1, 0, 2, 2, 0, 2)]
        [TestCase(0, 1, -2, 5, double.PositiveInfinity, -2)]
        public void CreatingNewLineTest(int vx, int vy, int px, int py, double a, double s)
        {
            var line = new Line(new Vector(vx, vy), new Point(px, py));
            Assert.AreEqual(a, line.AngleCoeficient);
            Assert.AreEqual(s, line.Shift);
        }
        
        //[TestCase(0, 1, 3, 0, 1, 1, 2)]
        //[TestCase(1, 1, 0, 0, -1, 1, 1.4142135623730952d)]
        //[TestCase(1, 0, 2, 2, 1, 1, 1)]
        //public void GetDistanceTest(int vx, int vy, int px, int py, int dx, int dy, double d)
        //{
        //    var line = new Line(new Vector(vx, vy), new Point(px, py));
        //    Assert.AreEqual(d, line.GetDistance(new Point(dx, dy)));
        //}
        

    }
}
