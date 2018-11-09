
using System;
using System.Drawing;

namespace Shooter
{
    public abstract class Player
    {
        public float Angle {
            get => angle;
            protected set
            {
                angle = value;
                UpdateLine();
            }
        }
        public PointF Location { get; private set; }
        //private bool isShoot = false;
        private float dAngle = 0.05f;
        protected Line line;
        private float  angle;
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

        //public void Draw(Graphics g)
        //{
        //    var secondDot = new PointF((float) ((game.Height - line.Shift )/ line.AngleCoeficient), game.Height);
        //    g.DrawLine(Pens.Black, line.Dot.Convert(game.Height), secondDot.Convert(game.Height));
        //    DrawGun(g);
        //}


        //void DrawGun(Graphics g)
        //{
        //    var windowLocation = Location.Convert(game.Height);
        //    g.TranslateTransform(windowLocation.X / 2, windowLocation.Y);
        //    g.RotateTransform(Angle);
        //    g.DrawRectangle(Pens.Red, new Rectangle((int)windowLocation.X, (int)windowLocation.Y - 300, 50, 300));
        //    g.RotateTransform(-Angle);
        //    g.TranslateTransform(-windowLocation.X / 2, -windowLocation.Y);
        //}
    }
}