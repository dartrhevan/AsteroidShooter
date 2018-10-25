using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Shooter
{
    class MainForm : Form
    {
        private Game game;
        private Timer timer;
        public MainForm()
        {
            game = new Game();
            timer = new Timer { Interval = 10};
            timer.Tick += (sender, args) => { Invalidate(); };
            timer.Start();
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            game.Height = e.ClipRectangle.Height;
            game.Width = e.ClipRectangle.Width;

            game.Player.Draw(e.Graphics);
            /*
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(0, 0), new Size(100, 100)));
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(
            e.ClipRectangle.Width - 100,
            e.ClipRectangle.Height - 100), new Size(100, 100)));*/
        }
    }
}
