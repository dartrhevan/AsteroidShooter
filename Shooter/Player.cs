
using System;
using System.Drawing;

namespace Shooter
{
    public class Player
    {
        public float Angle { get; private set; }
        public PointF Location { get; private set; }
        private bool isShoot = false;
        private float dAngle = 0.001f;
        private Line line;

        public Player(Game game)
        {
            Angle = (float)Math.PI / 2;
            InitLine(game);
        }

        private void InitLine(Game game)
        {
            line = new Line(Math.Tan(Angle), new PointF(game.Height, game.Width / 2));
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
            g.DrawLine(Pens.Red, line.Dot, line.Dot + 10 * line.Direction);
        }
    }
}