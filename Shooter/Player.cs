
using System;
using System.Drawing;

namespace Shooter
{
    public abstract class Player
    {
        public double Angle {
            get => angle;
            protected set
            {
                angle = value;
                UpdateLine();
            }
        }
        public PointF Location { get; private set; }
        //private bool isShoot = false;
        private double dAngle = 0.05;
        protected Line line;
        private double angle;
        protected readonly Game game;
        public double ShellVelocity = 5;
        public abstract Shell Shell { get; set; }
        //private readonly Func<Game, PointF> getLocation;

        protected Player(Game game/*, Func<Game, PointF> getLocation*/)
        {
            //this.getLocation = getLocation;
            this.game = game;
            angle = (float)Math.PI / 2;
            UpdateLine();
        }

        public void UpdateLine()
        {
            Location = GetLocation();//new PointF(game.Width / 2, game.Height);
            line = new Line(Math.Tan(Angle), Location);
        }

        protected abstract PointF GetLocation();
        protected abstract Shell GetShell();

        public abstract void Shoot();


        public void TurnRight() => Angle -= dAngle;

        public void TurnLeft() => Angle += dAngle;

        public void Draw(Graphics g)
        {
            var secondDot = new PointF((float) ((game.Height - line.Shift )/ line.AngleCoeficient), game.Height);
            g.DrawLine(Pens.Black, line.Dot.Convert(game.Height), secondDot.Convert(game.Height));
        }
    }
}