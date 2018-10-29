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
            DoubleBuffered = true;
            game = new Game();
            timer = new Timer { Interval = 10};
            timer.Tick += (sender, args) =>
            {
                game.Act();
                Invalidate();
            };
            timer.Start();
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'a':
                    game.Human.TurnLeft();
                    break;
                case 'd':
                    game.Human.TurnRight();
                    break;
                case 'w':
                    game.Human.Shoot();
                    break;
                case 's':
                    game.Bot.Shoot();
                    break;
            }
        }

        
        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateGame(e);
            game.Human.Draw(e.Graphics);
            game.Human.Shell?.Draw(e.Graphics, game.Height);
            game.Bot.Shell?.Draw(e.Graphics, game.Height);

            /*
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(0, 0), new Size(100, 100)));
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(
            e.ClipRectangle.Width - 100,
            e.ClipRectangle.Height - 100),new Size(100, 100)));*/
        }

        void UpdateGame(PaintEventArgs e)
        {
            game.Height = e.ClipRectangle.Height;
            game.Width = e.ClipRectangle.Width;
            //game.Player.InitLine(game);
        }


    }
}
