using System.Drawing;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Shooter
{
    [TestFixture()]
    class ShellTests
    {
        [TestCase(150, 150, 5, 5, 155, 155)]
        [TestCase(50, 50, 5, 5, 55, 55)]
        public void ShellMove(int x1, int y1, double vx, double vy, int x2, int y2)
        {
            var game = new Game(400, 600);
            var shell = new Aim(new Point(x1, y1), game, new Vector(vx, vy));
            shell.Move();
            Assert.AreEqual(new PointF(x2, y2), shell.Location);
        }

        [TestCase(0, 0)]
        [TestCase(500, 20)]
        public void AimDisappear(int aimX, int aimY)
        {
            var game = new Game();
            game.Bot.Shell = new Aim(new Point(aimX, aimY), game, Vector.Zero);
            game.Act();
            Assert.AreEqual(null, game.Bot.Shell);
        }

        /*[TestCase(0, 0)]

        public void BulletDisappear(int aimX, int aimY)
        {
            var game = new Game();
            game.Human.Shell = new Aim(new Point(aimX, aimY), game, Vector.Zero);
            game.Act();
            Assert.AreEqual(null, game.Human.Shell);
        }*/

        [Test]
        public void BulletDisappear()
        {
            var game = new Game();
            game.Human.Shell = new Bullet(new Point(100, game.Height + 7), game, Vector.Zero);
            game.Act();
            Assert.AreEqual(null, game.Human.Shell, "Up");
            game.Human.Shell = new Bullet(new Point(game.Width + 2, 200), game, Vector.Zero);
            game.Act();
            Assert.AreEqual(null, game.Human.Shell, "Right");
            game.Human.Shell = new Bullet(new Point(-7, 200), game, Vector.Zero);
            game.Act();
            Assert.AreEqual(null, game.Human.Shell, "Left");
            game.Human.Shell = new Bullet(new Point(200, -2), game, Vector.Zero);
            game.Act();
            Assert.AreEqual(null, game.Human.Shell, "Down");
        }
    }
}
