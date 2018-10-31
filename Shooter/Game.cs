using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shooter
{
    public class Game
    {
        private int width;
        private int height;
        //private int s = 0;

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

        public Human Human { get; private set; }
        public Bot Bot { get; private set; }

        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;
            Human = new Human(this);
            Bot = new Bot(this);

        }

        public void Restart()
        {
            Human = new Human(this);
            Bot = new Bot(this);
        }

        public void Act()
        {
            Bot.Shoot();
            Human.Shell?.Move();
            Bot.Shell?.Move();
            if (Bot.Shell != null && Human.Shell != null)
            {
                CheckCrash(Human.Shell, Bot.Shell);
            }
            if (Bot.Shell != null)
            {
                if (Bot.Shell.Location.Y <= Bot.Shell.Height)
                {
                    Human.Life--;
                    Bot.Shell.Disappear();
                }
            }

        }

        public void CheckCrash(Shell s1, Shell s2)
        {
            var d = Shell.GetDistance(s1, s2);
            if (d < s1.Height / 2 + s2.Height / 2)
            {
                s1.Disappear();
                s2.Disappear();
                Human.Scores++;
                if (Human.Scores % 5 == 0)
                    Bot.ShellVelocity++;
            }
        }

        public static double GetDistance(double dx, double dy) =>
            Math.Sqrt(dx * dx + dy * dy);
        
        public static double GetDistance(PointF d1, PointF d2) =>
            GetDistance(d1.X - d2.X, d1.Y - d2.Y);

        
    }

    
    public static class PointFExtension
    {
        public static PointF Convert(this PointF point, float height) =>
            new PointF(point.X, height - point.Y);
    }
}