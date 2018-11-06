using System;
using System.Drawing;

namespace Shooter
{
    public class Human : Player
    {
        private uint scores;
        private Shell shell;
        public Human(Game game) : base(game) => ShellVelocity = 49;

        public uint Life { get; set; } = 5;

        public uint Scores
        {
            get => scores;
            set
            {
                if (value > Game.MaxScores)
                    Game.MaxScores = value;
                scores = value;
            }
        }

        public override Shell Shell
        {
            get => shell;
            set
            {
                if(value != null && !(value is Bullet)) throw new ArgumentException("Shell does not match! Shell must be bullet");
                shell = value;
            }
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

        public void AtRight() => Angle = Math.PI / 3;

        public void AtLeft() => Angle = 2 * Math.PI / 3;
    }
}