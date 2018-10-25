using System.Drawing;

namespace Shooter
{
    public abstract class Shell
    {

        public PointF GetCenter() => new PointF(Location.X + Width / 2, Location.Y + Height / 2);
        public Brush Brush { get; protected set; }
        protected Game game;
        public Vector Velocity { get; protected set; }
        protected PointF location;

        public int Width { get; protected set; }

        public int Height { get; protected set; }
        public PointF Location { get => location; set => location = value; }

        public abstract void Move(Game g);
        public void Draw(Graphics g, int height)
        {
            var location = this.location.Convert(height);
            g.FillEllipse(Brush, location.X, location.Y, Width, Height);
            g.DrawEllipse(new Pen(Color.Black, 4), location.X, location.Y, Width, Height);
            g.DrawLine(new Pen(Color.Black, 6), location.X, location.Y + Height / 2, location.X + Width,
                location.Y + Height / 2);
        }

        public void Disappear()
        {
            
        }

        //public abstract PointF GetValidMove(PointF location, Game game);

    }
}