using System.Drawing;

namespace Shooter
{
    public class Bullet : Shell
    {

        public Bullet(PointF location, Game g, Vector velocity, int height = 10, int width = 10)
        {
            game = g;
            Brush = Brushes.Black;
            Width = width;
            Height = height;
            Location = location;
            Velocity = velocity ?? Vector.Zero;
        }

        public override void Disappear() => game.Human.Shell = null;

        public override void Move()
        {
            Location = new PointF((float)(Location.X + Velocity.X), (float)(Location.Y + Velocity.Y));
            var center = GetCenter();

            if (center.X > game.Width || center.X < 0 || center.Y > game.Height || center.Y < 0)
                Disappear();
        }
    }
}