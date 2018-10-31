using System.Drawing;
using System.Windows.Forms;

namespace Shooter
{
    public class Human : Player
    {
        public uint Life { get; set; } = 5;

        public uint Scores { get; set; }
        public Human(Game game) :  base(game)
        {
            ShellVelocity = 50;
        }

        public override void Shoot()
        {
            UpdateLine();
            if (Shell == null)
                Shell = GetShell();
        }

        protected override PointF GetLocation() => new PointF(game.Width / 2, 0);

        protected override Shell GetShell()
        {
            var dir = line.Direction.Y > 0 ? line.Direction.Normalize() : -line.Direction.Normalize();
            return new Bullet(Location, game, dir * ShellVelocity);
        }

        
    }
}