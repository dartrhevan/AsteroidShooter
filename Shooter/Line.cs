using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    public class Line
    {
        public readonly Vector Direction;
        public readonly PointF Dot;
        public readonly double AngleCoeficient;
        public readonly double Shift;

        public Line(Vector direction, PointF dot)
        {
            //direction.Normalize();
            Direction = direction.Normalize();
            Dot = dot;
            AngleCoeficient = (double)direction.Y / direction.X;
            Shift = -AngleCoeficient * dot.X + dot.Y;
            if (direction.X == 0) Shift = dot.X;
            else if (direction.Y == 0) Shift = dot.Y;
        }

        public Line(PointF p1, PointF p2) 
            : this(new Vector(p1.X - p2.X, p1.Y - p2.Y), p1)
        {
        }

        public Line(double angleCoeficient, PointF dot)
        {
            if (angleCoeficient == double.PositiveInfinity ||
                angleCoeficient == double.NegativeInfinity)
                Direction = new Vector(1, 0);
            else
                Direction = new Vector(1, angleCoeficient);//.Normalize();
            Dot = dot;
            AngleCoeficient = angleCoeficient;
            Shift = !double.IsInfinity(angleCoeficient) ?
                - angleCoeficient * dot.X + dot.Y :
                dot.X;
            /*if (Direction.X == double.NaN || Direction.X == double.PositiveInfinity ||
                Direction.X == double.NegativeInfinity) Direction.X = 0;*/
            
        }

        public Line GetPerpendicular(PointF dot)
        {
            return new Line(-Direction.X / Direction.Y, dot);
        }
        //MATH  COORDINATES  REQUIRED!!!!!!!!!!!!!!!!!!!!!

        public PointF GetIntersection(Line other)
        {
            var x = (-other.Shift + Shift) / (other.AngleCoeficient - AngleCoeficient);
            
            var y = x * AngleCoeficient + Shift;
            if (AngleCoeficient == 0) x = other.Shift;
            else if (Math.Abs(AngleCoeficient) == double.PositiveInfinity)
            {
                x = Shift;
                y = other.Shift;
            }
            return new PointF((float)x, (float)y);
        }

        /*public Point GetIntersectionPoint(Line other)
        {
            var point = GetIntersection(other);
            return new Point((int)point.X, (int)point.Y);
        }*/

        public double GetDistance(PointF dot)
        {
            if (double.IsInfinity(AngleCoeficient)) return Math.Abs(dot.X - Dot.X);
            else if (AngleCoeficient == 0) return Math.Abs(dot.Y - Dot.Y);
            var perpendicular = GetPerpendicular(dot);
            return Game.GetDistance(dot, GetIntersection(perpendicular));
        }
    }
}
