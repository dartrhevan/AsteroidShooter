using System.Drawing;

namespace Shooter
{
    public class Human : Player
    {
        private uint life = 5;

        public uint Life
        {
            get => life;
            set
            {
                //if(value == 0) game.
                life = value;
            }
        }

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