using System.Drawing;

namespace Shooter
{
    public class Bullet : Shell
    {

        public Bullet(PointF location, Game g, Vector velocity, int height = 10, int width = 10)
        {
            game = g;
            Brush = Brushes.Red;
            Width = width;
            Height = height;
            Location = location;
            Velocity = velocity ?? Vector.Zero;
        }

        public override void Disappear() => game.Human.Shell = null;
        public Brush Brush { get; protected set; }

        public override void Draw(Graphics g, int height)
        {
            var location = this.location.Convert(height);
            //g.DrawImage(new Bitmap("Images/Aim1.png"), location);
            g.FillEllipse(Brush, location.X, location.Y, Width, Height);
            //g.DrawEllipse(new Pen(Color.Black, 4), location.X, location.Y, Width, Height);
            /*g.DrawLine(new Pen(Color.Black, 6), location.X, location.Y + Height / 2, location.X + Width,
                location.Y + Height / 2);*/
        }

        public override void Move()
        {
            Location = new PointF((float)(Location.X + Velocity.X), (float)(Location.Y + Velocity.Y));
            var center = GetCenter();

            if (center.X > game.Width || center.X < 0 || center.Y > game.Height || center.Y < 0)
                Disappear();
        }
    }
}