
using System;
using System.Drawing;

namespace Shooter
{
    public class Player
    {
        public float Angle {
            get => angle;
            private set
            {
                angle = value;
                UpdateLine();
            }
        }
        public PointF Location { get; private set; }
        //private bool isShoot = false;
        private float dAngle = 0.01f;
        private Line line;
        private float angle;
        private readonly Game game;
        private readonly Func<Game, PointF> getLocation;

        public Player(Game game, Func<Game, PointF> getLocation)
        {
            this.getLocation = getLocation;
            this.game = game;
            angle = (float)Math.PI / 2;
            UpdateLine();
        }

        public void UpdateLine()
        {
            //Location = getLocation(game);//new PointF(game.Width / 2, game.Height);
            line = new Line(Math.Tan(Angle), new PointF(game.Width / 2, game.Height));
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }

        public void TurnRight()
        {
            Angle += dAngle;
        }

        public void TurnLeft()
        {
            Angle -= dAngle;
        }

        public void Draw(Graphics g)
        {
            var secondDot = new PointF((float) (-line.Shift / line.AngleCoeficient), 0);
            g.DrawLine(Pens.Red, line.Dot, secondDot);
        }
    }
}