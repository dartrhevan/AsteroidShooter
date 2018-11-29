using System.Drawing;

namespace Shooter
{
    public abstract class Shell
    {

        public PointF GetCenter() => new PointF(Location.X + Width / 2, Location.Y - Height / 2);
        protected Game game;
        public Vector Velocity { get; protected set; }
        protected PointF location;

        public int Width { get; protected set; }

        public int Height { get; protected set; }
        public PointF Location { get => location; set => location = value; }

        public abstract void Move();
        public abstract void Draw(Graphics g, int height);


        public abstract void Disappear();
        /*
        {
            if (this is Aim)
                game.Bot.Shell = null;
            else if(this is Bullet)
                game.Human.Shell = null;
        }
        */

        

        public static double GetDistance(Shell obj1, Shell obj2)
        {
            var center1 = obj1.GetCenter();
            var center2 = obj2.GetCenter();
            return Game.GetDistance(center1, center2);
        }
        //public abstract PointF GetValidMove(PointF location, Game game);

    }
}