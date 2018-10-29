using System;
using System.Drawing;

namespace Shooter
{
    public class Aim : Shell
    {
        //public Vector Velocity;


        public Aim(PointF location, Game g, Vector velocity, int height = 40, int width = 40)
        {
            game = g;
            Brush = Brushes.Black;
            Width = width;
            Height = height;
            Location = location;
            Velocity = velocity ?? Vector.Zero;
        }

        public override void Disappear() => game.Bot.Shell = null;

        public override void Move()
        {

            //if (Velocity.Length < 0.1) Velocity = Vector.Zero;
            if ((Location.Y <= Height && Velocity.Y < 0)  || 
                (Location.Y >= game.Height && Velocity.Y > 0))
                Velocity = new Vector(Velocity.X, -Velocity.Y);
            if ((Location.X <= 0 && Velocity.X < 0)
                || (Location.X + Width >= game.Width && Velocity.X > 0))
                Velocity = new Vector(-Velocity.X, Velocity.Y);

            Location = new PointF((float)(Location.X + Velocity.X), (float)(Location.Y + Velocity.Y));

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
