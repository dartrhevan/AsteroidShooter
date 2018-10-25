using System;
using System.Drawing;

namespace Shooter
{
    public class Aim : Shell
    {
        //public Vector Velocity;


        public Aim(Point location, Game g, int height = 40, int width = 40)
        {
            game = g;
            Brush = Brushes.Black;
            Width = width;
            Height = height;
            Location = location;
            Velocity = Vector.Zero;
        }

        public override void Move(Game g)
        {
            if (Velocity.Length < 0.1) Velocity = Vector.Zero;
            if ((Location.Y <= Height + 11 && Velocity.Y < 0)  || 
                (Location.Y >= g.Height - 11 && Velocity.Y > 0))
                Velocity = new Vector(Velocity.X, -Velocity.Y);
            if ((Location.X <= 11 && Velocity.X < 0)
                || (Location.X + Width >= g.Width - 11 && Velocity.X > 0))
                Velocity = new Vector(-Velocity.X, Velocity.Y);

            Location = new Point((int)(Location.X + Velocity.X), (int)(Location.Y + Velocity.Y));

        }
        /*
        public override PointF GetValidMove(PointF location, Game game)
        {

            return location;
            /*if (location.X < -11 || location.X + Width > game.Width + 11)
                location.X = Location.X;*
            if (location.Y < Height - 5 || location.Y > game.Height + 5)
                location.Y = Location.Y;
            return location;
        }*/
    }
}
