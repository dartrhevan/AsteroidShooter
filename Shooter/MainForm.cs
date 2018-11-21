using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Shooter
{
    class MainForm : Form
    {
        private Game game;
        private Timer timer;
        private Rectangle status;// = new RectangleF(new PointF(0, He), );

        private FileInfo[] images;
        private int imageNum = 0;

        void ChangeTheme()
        {
            var image = images[imageNum++ % images.Length];
            BackgroundImage = new Bitmap(image.FullName);
        }
        //private bool pause = false;
        public MainForm()
        {
            status = new Rectangle(new Point(0, Height), new Size(Width, 60));
            DoubleBuffered = true;
            //BackColor = Color.DarkGreen;
            //BackgroundImage = new Bitmap("wall.png");
            var p = new DirectoryInfo("Images");
            images = p.EnumerateFiles().ToArray();
            ChangeTheme();
            game = new Game(Width, Height);
            MinimumSize = new Size(400, 600);
            timer = new Timer { Interval = 5};
            timer.Tick += OnTimerTick;
            timer.Start();
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void OnTimerTick(object sender, EventArgs args)
        {
            if(left)
                game.Human.TurnLeft();
            if(right)
                game.Human.TurnRight();
            if(shoot)
                game.Human.Shoot();

            if (game.Human.Life == 0)
            {
                var s = game.Human.Scores;
                Restart();
                Pause();
                MessageBox.Show(string.Format("Game over, your result is {0}", s), "Game over", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            game.Act();
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            HandleKeys(e, true);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            HandleKeys(e, false);
        }

        private bool left;
        private bool right;
        private bool shoot;

        private void HandleKeys(KeyEventArgs e, bool flag)
        {
            if (e.KeyCode == Keys.A)
                left = flag;
                //game.Human.TurnLeft();
            if (e.KeyCode == Keys.D)
                right = flag;
            //game.Human.TurnRight();
            if (e.KeyCode == Keys.W)
                shoot = flag;

            //game.Human.Shoot();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            switch (char.ToLower(e.KeyChar))
            {/*
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
                    break;*/
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

        void Restart()
        {
            game.Restart();
            ChangeTheme();
        }

        //public bool IsBang { get; set; }
        private int bangTime = 15;
        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateGame(e);
            game.Human.Draw(e.Graphics);
            game.Human.Shell?.Draw(e.Graphics, game.Height);
            game.Bot.Shell?.Draw(e.Graphics, game.Height);
            e.Graphics.FillRectangle(Brushes.Black, status);
            DrawLifes(e.Graphics);
            if(game.BangPlace != null)
            {
                e.Graphics.DrawImage(new Bitmap("bang.png"), game.BangPlace.Value.Convert(game.Height));
                if(--bangTime <= 0)
                {
                    bangTime = 15;
                    game.BangPlace = null;
                }
            }
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
