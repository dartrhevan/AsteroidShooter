using System;
using System.Drawing;

namespace Shooter
{
    public class Game
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Player Player { get; private set; }

        public Game()
        {
            Player = new Player(this);
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