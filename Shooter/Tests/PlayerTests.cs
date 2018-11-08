using System;
using System.Drawing;
using System.Reflection;
using NUnit.Framework;

namespace Shooter
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void ShootHumanTest()
        {
            var game = new Game(400, 600);
            game.Human.Shoot();
            game.Act();
            Assert.AreEqual(game.Human.Location.X, game.Human.Shell.Location.X);
            Assert.AreEqual(game.Human.Location.Y + 49, game.Human.Shell.Location.Y);
        }
        
        [Test]
        public void ShootBotTest()
        {
            var game = new Game(400, 600);
            //game.Human.Shoot();
            game.Bot.Shell = new Aim(game.Bot.Location, game, new Vector(0, -1));
            game.Act();
            Assert.AreEqual(game.Bot.Location.X, game.Bot.Shell.Location.X);
            Assert.AreEqual(game.Bot.Location.Y - 1, game.Bot.Shell.Location.Y);
        }

        [TestCase(Math.PI / 4)]
        [TestCase(Math.PI / 3)]
        [TestCase(Math.PI / 2)]
        [TestCase(2 * Math.PI / 3)]
        [TestCase(3 * Math.PI / 4)]

        public void ShootHumanTest2(double angle)
        {
            var game = new Game(400, 600);
            var type = typeof(Player);
            var field = type.GetField("angle", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(game.Human, angle);
            game.Human.UpdateLine();
            game.Human.Shoot();
            game.Act();
            Assert.AreEqual(Math.Tan(game.Human.Angle), game.Human.Shell.Velocity.Y / game.Human.Shell.Velocity.X, 1e-7);
            //Assert.AreEqual(game.Human.Location.Y + 49, game.Human.Shell.Location.Y);
        }

        [TestCase(Math.PI / 4)]
        [TestCase(Math.PI / 3)]
        [TestCase(Math.PI / 2)]
        [TestCase(2 * Math.PI / 3)]
        [TestCase(3 * Math.PI / 4)]

        public void UpdateLineTest(double angle)
        {
            var game = new Game(400, 600);
            var type = typeof(Player);
            var angleField = type.GetField("angle", BindingFlags.NonPublic | BindingFlags.Instance);
            angleField.SetValue(game.Human, angle);
            var lineField = type.GetField("line", BindingFlags.NonPublic | BindingFlags.Instance);
            game.Human.UpdateLine();

            Assert.AreEqual(Math.Tan(game.Human.Angle),((Line)lineField.GetValue(game.Human)).AngleCoeficient , 1e-7);
            //Assert.AreEqual(game.Human.Location.Y + 49, game.Human.Shell.Location.Y);
        }

        [TestCase(400, 600, 200, 600, 200, 0)]
        [TestCase(700, 600, 350, 600, 350, 0)]
        [TestCase(800, 800, 400, 800, 400, 0)]

        public void GetLocationTest(int width, int height, int xb, int yb, int xh, int yh)
        {
            var game = new Game(width, height);
            Assert.AreEqual(new PointF(xb, yb), game.Bot.Location);
            Assert.AreEqual(new PointF(xh, yh), game.Human.Location);
            Assert.AreEqual(xb, xh);
        }


    }
}