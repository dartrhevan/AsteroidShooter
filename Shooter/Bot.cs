using System.Drawing;

namespace Shooter
{
    public class Bot : Player
    {
        public Bot(Game game) : base(game)
        {
        }

        protected override PointF GetLocation() => new PointF(game.Width / 2, game.Height);

        protected override Shell GetShell()
        {
            var dir = line.Direction.Y > 0 ? line.Direction.Normalize() : -line.Direction.Normalize();
            return new Aim(Location, game, dir * ShellVelocity);
        }
    }
}