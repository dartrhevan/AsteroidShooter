using System;
using System.Drawing;
using System.Windows.Forms;

namespace Shooter
{
    partial class MainForm
    {
        private void DrawLifes(Graphics g)
        {
            var textStatus = string.Format("L: {0} S: {1} MAX: {2}", game.Human.Life.ToString()
                , game.Human.Scores, Game.MaxScores);
            if (game.IsAmmo)
                textStatus += string.Format(" A:{0}", game.Human.Ammo);
            g.DrawString(textStatus, new Font(FontFamily, 23), Brushes.DarkRed,
                status, StringFormat.GenericTypographic);
        }

        private void DrawLevel(Graphics g)
        {
            var x = new PointF(0, game.Level).Convert(game.Height);
            var y = new PointF(game.Width, game.Level).Convert(game.Height);
            g.DrawLine(Pens.Red, x, y);
        }

        private void DrawBang(Graphics g)
        {
            var random = new Random(DateTime.Now.Millisecond);
            if (game.BangPlace != null)
            {
                var angle = (float)(2 * random.NextDouble() * Math.PI);
                var bangLocation = game.BangPlace.Value.Convert(game.Height);
                var dx = bangLocation.X + bangImage.Width / 2;
                var dy = bangLocation.Y + bangImage.Height / 2;
                g.TranslateTransform(dx, dy);
                g.RotateTransform(angle);
                g.DrawImage(bangImage, new Point(0, 0));
                g.RotateTransform(-angle);
                g.TranslateTransform(-dx, -dy);
                if (--bangTime <= 0)
                {
                    bangTime = 15;
                    game.BangPlace = null;
                }
            }
        }

    }
}