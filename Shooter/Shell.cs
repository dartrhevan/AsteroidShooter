using System;
using System.Drawing;

namespace Shooter
{
    public class Shell
    {
        //public Vector Velocity;

        public PointF GetCenter() => new PointF(Location.X + Width / 2, Location.Y + Heigth / 2);
        public Brush Brush { get; protected set; }
        
        public Vector Velocity;
        private PointF location;

        public int Width { get; protected set; }

        public int Heigth { get; protected set; }
        public PointF Location { get => location; set => location = value; }

        public Shell(Point location, int height = 40, int width = 40)
        {
            Brush = Brushes.Black;
            Width = width;
            Heigth = height;
            Location = location;
            Velocity = Vector.Zero;
        }

        public void Move(Game g)
        {
            if (Velocity.Length < 0.1) Velocity = Vector.Zero;
            if ((Location.Y <= Heigth + 11 && Velocity.Y < 0)  || 
                (Location.Y >= g.Height - 11 && Velocity.Y > 0))
                Velocity = new Vector(Velocity.X, -Velocity.Y);
            if ((Location.X <= 11 && Velocity.X < 0)
                || (Location.X + Width >= g.Width - 11 && Velocity.X > 0))
                Velocity = new Vector(-Velocity.X, Velocity.Y);

            Location = GetValidMove(new Point((int)(Location.X + Velocity.X), (int)(Location.Y + Velocity.Y)), g);

        }

        public PointF GetValidMove(PointF location, Game game)
        {

            return location;
            /*if (location.X < -11 || location.X + Width > game.Width + 11)
                location.X = Location.X;*/
            if (location.Y < Heigth - 5 || location.Y > game.Height + 5)
                location.Y = Location.Y;
            return location;
        }

        public void Draw(Graphics g, int height)
        {
            var location = this.location.Convert(height);
            g.FillEllipse(Brush, location.X, location.Y, Width, Heigth);
            g.DrawEllipse(new Pen(Color.Black, 4), location.X, location.Y, Width, Heigth);
            g.DrawLine(new Pen(Color.Black, 6), location.X, location.Y + Heigth / 2, location.X + Width,
                location.Y + Heigth / 2);
        }
    }
}
