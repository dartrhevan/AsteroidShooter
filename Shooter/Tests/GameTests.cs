using System.Drawing;
using System.Reflection;
using NUnit.Framework;

namespace Shooter
{
    [TestFixture]
    public class GameTests
    {
        [TestCase(100, 100, 600, 600, false, 0, 0, 0, 0)]
        [TestCase(100, 100, 100, 100, true, 0, 0, 0, 0)]
        [TestCase(100, 100, 92, 75, true, 0, 0, 0, 0)]
        [TestCase(100, 100, 92, 75, true, 10, 1, 7, 0)]

        public void CrushTest(int aimX, int aimY, int bulletX, int bulletY, bool res, 
            double vbx, double vby, double vax, double vay)
        {
            var game = new Game();
            game.Bot.Shell = new Aim(new Point(aimX, aimY), game, new Vector(vax, vay));
            game.Human.Shell = new Bullet(new PointF(bulletX, bulletY), game, new Vector(vbx, vby));
            Assert.AreEqual(res, game.CheckCrash(game.Bot.Shell, game.Human.Shell));
        }
        
        [TestCase(0, 0, true, 0, 0)]
        [TestCase(500, 500, false, 0, 0)]
        [TestCase(500, 20, true, 0, 0)]
        [TestCase(0, 100, false, 0, 0)]

        public void MissingTest(int aimX, int aimY, bool res,
            double vax, double vay)
        {
            var game = new Game();
            game.Bot.Shell = new Aim(new Point(aimX, aimY), game, new Vector(vax, vay));
            Assert.AreEqual(res, game.CheckMissing());
        }
    }
}