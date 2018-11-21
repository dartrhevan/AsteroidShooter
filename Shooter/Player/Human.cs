using System;
using System.Drawing;

namespace Shooter
{
    public class Human : Player
    {
        private uint scores;
        private Shell shell;

        public int Ammo
        {
            get => ammo;
            private set
            {
                ammo = value;
                if (ammo <= 0)
                    Life = 0;
            }
        }

        public Human(Game game) : base(game)
        {
            Ammo = 20;
            ShellVelocity = 49;
        }

        public uint Life { get; set; } = 5;

        public uint Scores
        {
            get => scores;
            set
            {
                if (value > Game.MaxScores)
                    Game.MaxScores = value;
                if (value % 10 == 0)
                {
                    if(game.Height > game.Level + 2 * Aim.StandartHeight)
                        game.Level += game.Height / 5;
                    Ammo += 20;
                }

                scores = value;
            }
        }

        public override Shell Shell
        {
            get => shell;
            set
            {
                if(value != null && !(value is Bullet)) throw new ArgumentException("Shell does not match! Shell must be bullet");
                shell = value;
            }
        }

        public override void Shoot()
        {
            UpdateLine();
            if (Shell == null)
            {
                --Ammo;
                Shell = GetShell();
            }
        }

        protected override PointF GetLocation() => new PointF(game.Width / 2, 0);

        protected override Shell GetShell()
        {
            var dir = (line.Direction.Y > 0 ? line.Direction : -line.Direction).Normalize();
            return new Bullet(Location, game, dir * ShellVelocity);
        }

        public void FastTurnRight() => Angle -= (float)Math.PI / 3;

        public void FastTurnLeft() => Angle += (float)Math.PI / 3; public void Draw(Graphics g)
        {
            var secondDot = new PointF((float)((game.Height - line.Shift) / line.AngleCoeficient), game.Height);
            g.DrawLine(Pens.Black, line.Dot.Convert(game.Height), secondDot.Convert(game.Height));
            DrawGun(g);
        }


        void DrawGun(Graphics g)
        {
            //UpadateSize();
            float a = (float)((Math.PI / 2 - Angle) / Math.PI) * 180;
            var windowLocation = Location.Convert(game.Height);
            g.TranslateTransform(windowLocation.X, windowLocation.Y);
            g.RotateTransform(a);
            var shift = (float)-width / 2;
            var h = (float)height * 2;
            var shift2 = (float)-1.75 * width / 2;
            g.FillRectangle(Brushes.SaddleBrown, new RectangleF(shift2, -height / 1.75f, (float)1.75 * width, h / 2));
            g.FillRectangle(Brushes.Black, new RectangleF(shift, -height, width, h));
            g.DrawLine(new Pen(Color.Black, 5f), shift - 4, height, - shift + 4, height);
            g.DrawLine(new Pen(Color.Black, 5f), shift - 4, -height, -shift + 4, -height);
            g.DrawLine(new Pen(Color.Black, 3f), shift - 2, height / 1.85f, -shift + 2, height / 1.85f);
            g.DrawLine(new Pen(Color.Black, 3f), shift - 2, -height / 1.85f, -shift + 2, -height / 1.85f);

            //g.FillRectangle(Brushes.Black, new RectangleF(, -width * 2, 2 * width, width * 2));
            //g.FillRectangle(Brushes.Gray, new RectangleF(-width / 2, -height, width, height * 2));

            g.RotateTransform(-a);
            g.TranslateTransform(-windowLocation.X, -windowLocation.Y);
        }
        /*
        private void UpadateSize()
        {
            width = game.Width / 10;
            height = 2 * width;
        }*/

        private int height = 60;
        private int width = 20;
        private int ammo;
    }
}