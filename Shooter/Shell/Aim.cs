using System;
using System.Drawing;

namespace Shooter
{
    public class Aim : Shell
    {
        readonly Bitmap Image;

        //public Vector Velocity;
        public const int StandartHeight = 40;

        public Aim(PointF location, Game g, Vector velocity, int height = StandartHeight, int width = StandartHeight)
        {
            game = g;
            Image = new Bitmap("Images/Aim.png");
            //Brush = new TextureBrush(Image);//Brushes.YellowGreen;
            Width = width;
            Height = height;
            Location = location;
            Velocity = velocity ?? Vector.Zero;
        }

        public override void Disappear()
        {
            game.BangPlace = Location;
            game.Bot.Shell = null;
        }

        public override void Draw(Graphics g, int height)
        {
            var location = this.location.Convert(height);
            g.DrawImage(Image, location);
            //g.FillEllipse(Brush, location.X, location.Y, Width, Height);
        }

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
