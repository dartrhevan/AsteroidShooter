using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shooter
{
    public class Game
    {
        private int width;
        private int height;
        public PointF? BangPlace { get; set; }

        public int Level { get; set; }
        //private int s = 0;
        public static uint MaxScores { get; set; }
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

        public Game(int width = 400, int height = 600)
        {
            Level = 0;
            this.width = width;
            this.height = height;
            Human = new Human(this);
            Bot = new Bot(this);

        }

        public void Restart()
        {
            Human = new Human(this);
            Bot = new Bot(this);
            Level = 0;
        }

        public void Act()
        {
            Bot.Shoot();
            Human.Shell?.Move();
            Bot.Shell?.Move();
            if (Bot.Shell != null && Human.Shell != null)
                CheckCrash(Human.Shell, Bot.Shell);
            CheckMissing();
        }

        public bool CheckMissing()
        {
            if (Bot.Shell != null && Bot.Shell.Location.Y <= Level + Bot.Shell.Height)
            {
                Human.Life--;
                Bot.Shell.Disappear();
                return true;
            }

            return false;
        }

        private const uint s = 3;
        public bool CheckCrash(Shell s1, Shell s2)
        {
            var d = Shell.GetDistance(s1, s2);
            if (d < s1.Height / 2 + s2.Height / 2 + s)
            {
                s1.Disappear();
                s2.Disappear();
                Human.Scores++;
                if (Human.Scores % 5 == 0)
                    Bot.ShellVelocity++;
                return true;
            }
            return false;
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