using System.Drawing;

namespace Shooter
{
    public class Bullet : Shell
    {

        public Bullet(Point location, Game g, int height = 10, int width = 10)
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
            Location = new Point((int)(Location.X + Velocity.X), (int)(Location.Y + Velocity.Y));
            var center = GetCenter();
            if (center.X > Width || center.X < 0 || center.Y > Height || center.Y < 0) Disappear();
        }
    }
}