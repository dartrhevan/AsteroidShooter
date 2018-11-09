﻿using System;
using System.Drawing;

namespace Shooter
{
    public class Human : Player
    {
        private uint scores;
        private Shell shell;
        public Human(Game game) : base(game) => ShellVelocity = 49;

        public uint Life { get; set; } = 5;

        public uint Scores
        {
            get => scores;
            set
            {
                if (value > Game.MaxScores)
                    Game.MaxScores = value;
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
                Shell = GetShell();
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
            g.FillRectangle(Brushes.Gray, new RectangleF(-width / 2, -height, width, height * 2));
            g.RotateTransform(-a);
            g.TranslateTransform(-windowLocation.X, -windowLocation.Y);
        }
        /*
        private void UpadateSize()
        {
            width = game.Width / 10;
            height = 2 * width;
        }*/

        private int height = 40;
        private int width = 20;
    }
}