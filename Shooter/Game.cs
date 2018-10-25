using System;
using System.Drawing;

namespace Shooter
{
    public class Game
    {
        private int width;
        private int height;

        public int Height
        {
            get => height;
            set
            {
                height = value;
                Human.UpdateLine();

            }
        }

        public int Width
        {
            get => width;
            set
            {
                width = value;
                Human.UpdateLine();
            }
        }

        public Player Human { get; private set; }
        public Player Bot { get; private set; }

        public Game()
        {
            Human = new Player(this, game => new PointF(game.Width / 2, 0));
            Bot = new Player(this, game => new PointF(game.Width / 2, game.Height));

        }

        public static double GetDistance(double dx, double dy) =>
            Math.Sqrt(dx * dx + dy * dy);

        public static double GetDistance(Point d1, Point d2) =>
            GetDistance(d1.X - d2.X, d1.Y - d2.Y);

        public static double GetDistance(PointF d1, PointF d2) =>
            GetDistance(d1.X - d2.X, d1.Y - d2.Y);
    }


    public static class VectorExtension
    {
        public static bool IsCoDirectional(this Vector vector, Vector otherVector) =>
            (vector.X > 0 && otherVector.X > 0) || (vector.X < 0 && otherVector.X < 0);
    }

    public static class PointFExtension
    {
        public static PointF Convert(this PointF point, float height) =>
            new PointF(point.X, height - point.Y);

        public static Point Convert(this Point point, int height) =>
            new Point(point.X, height - point.Y);
    }
}