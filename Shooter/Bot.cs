﻿using System;
using System.Drawing;

namespace Shooter
{
    public class Bot : Player
    {
        public Bot(Game game) : base(game)
        {
        }

        public override void Shoot()
        {
            Rotate();
            if (Shell == null)
                Shell = GetShell();
        }

        void Rotate()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            Angle = Math.Pow(-1, rnd.Next(2)) * rnd.NextDouble() * Math.PI / 2;
        }

        protected override PointF GetLocation() => new PointF(game.Width / 2, game.Height);

        protected override Shell GetShell()
        {
            var dir = line.Direction.Y > 0 ? line.Direction.Normalize() : -line.Direction.Normalize();
            return new Aim(Location, game, dir * ShellVelocity);
        }
    }
}