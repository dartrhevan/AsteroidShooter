using System.Drawing;

namespace Shooter
{
    public class Bot : Player
    {
        public Bot(Game game) : base(game)
        {
            
            TurnRight();
        }

        protected override PointF GetLocation() => new PointF(game.Width / 2, game.Height);

        protected override Shell GetShell() => new Aim(Location, game, line.Direction * ShellVelocity);

    }
}