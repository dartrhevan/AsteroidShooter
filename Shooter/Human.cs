using System.Drawing;

namespace Shooter
{
    public class Human : Player
    {
        public Human(Game game) :  base(game)
        {
            ShellVelocity = 50;
        }

        protected override PointF GetLocation() => new PointF(game.Width / 2, 0);

        protected override Shell GetShell()
        {
            var dir = line.Direction.Y > 0 ? line.Direction.Normalize() : -line.Direction.Normalize();
            return new Bullet(Location, game, dir * ShellVelocity);
        }
    }
}