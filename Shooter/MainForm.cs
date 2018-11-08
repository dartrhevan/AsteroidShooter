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
        private Rectangle status;// = new RectangleF(new PointF(0, He), );
        //private bool pause = false;
        public MainForm()
        {
            MessageBox.Show(
                @"Controls:
w - shoot,
a - turn right,
d - turn left,
p - pause,
r - restart,
z - fast turn left,
x - fast turn right", "Controls", MessageBoxButtons.OK, MessageBoxIcon.Information);
            status = new Rectangle(new Point(0, Height), new Size(Width, 60));
            DoubleBuffered = true;
            BackColor = Color.DarkGreen;
            game = new Game(Width, Height);
            MinimumSize = new Size(400, 600);
            timer = new Timer { Interval = 4};
            timer.Tick += (sender, args) =>
            {

                if (game.Human.Life == 0)
                {
                    var s = game.Human.Scores;
                    Restart();
                    Pause();
                    MessageBox.Show(string.Format("Game over, your result is {0}", s), "Game over", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                game.Act();
                Invalidate();
            };
            timer.Start();
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            switch (char.ToLower(e.KeyChar))
            {
                case 'a':
                case 'ф':
                    game.Human.TurnLeft();
                    break;
                case 'd':
                case 'в':
                    game.Human.TurnRight();
                    break;
                case 'w':
                case 'ц':
                    game.Human.Shoot();
                    break;
                case 'z':
                case 'я':
                    game.Human.FastTurnLeft();
                    break;
                case 'x':
                case 'ч':
                    game.Human.FastTurnRight();
                    break;
                case 'p':
                case 'з':
                    Pause();
                    break;
                case 'r':
                case 'к':
                    Restart();
                    break;

            }
        }

        private void Pause()
        {
            if (timer.Enabled) timer.Stop();
            else timer.Start();
        }

        void Restart() => game.Restart();
        
        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateGame(e);
            game.Human.Draw(e.Graphics);
            game.Human.Shell?.Draw(e.Graphics, game.Height);
            game.Bot.Shell?.Draw(e.Graphics, game.Height);
            e.Graphics.FillRectangle(Brushes.Black, status);
            DrawLifes(e.Graphics);
            /*
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(0, 0), new Size(100, 100)));
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(new Point(
            e.ClipRectangle.Width - 100,
            e.ClipRectangle.Height - 100),new Size(100, 100)));*/
        }

        void UpdateGame(PaintEventArgs e)
        {
            status.Location = new Point(0, e.ClipRectangle.Height - 60);
            status.Size = new Size(Width, 60);
            game.Height = e.ClipRectangle.Height - status.Height;
            game.Width = e.ClipRectangle.Width;
            //game.Player.InitLine(game);
        }

        void DrawLifes(Graphics g)
        {
            g.DrawString(string.Format("L: {0} S: {1} MAX: {2}", game.Human.Life.ToString(), game.Human.Scores, Game.MaxScores), new Font("Arial", 22), Brushes.DarkGreen, status, StringFormat.GenericTypographic);
        }
    }
}
