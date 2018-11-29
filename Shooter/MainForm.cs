using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Shooter
{
    internal class MainForm : Form
    {
        private readonly Bitmap bangImage;
        private readonly Game game;
        private readonly FileInfo[] images;
        private readonly Button optionsButton = new Button();
        private readonly Menu optionsPanel = new Menu();
        private readonly Timer timer;
        
        private int bangTime = 15;

        private int imageNum;

        private bool left;
        //private bool needUpadateOptions;

        private bool pause;
        private bool right;
        //private bool shoot;
        private Rectangle status;

        public MainForm()
        {
            KeyPreview = true;
            status = new Rectangle(new Point(0, Height), new Size(Width, 40));
            DoubleBuffered = true;
            var p = new DirectoryInfo("Images/Themes");
            images = p.EnumerateFiles().ToArray();
            ChangeTheme();
            game = new Game(Width, Height);
            MinimumSize = new Size(400, 600);
            timer = new Timer {Interval = 10};
            timer.Tick += OnTimerTick;
            timer.Start();
            FormBorderStyle = FormBorderStyle.Sizable;
            bangImage = new Bitmap("Images/bang.png");
            InitializeOptionsButton();
        }

        private void ChangeTheme()
        {
            var image = images[imageNum++ % images.Length];
            BackgroundImage = new Bitmap(image.FullName);
        }

        private void InitializeOptionsButton()
        {
            optionsButton.Image = new Bitmap("Images/opt.png");
            //needUpadateOptions = true;
            optionsButton.Size = new Size(status.Height, status.Height);
            optionsButton.Click += OptionsButtonOnClick;
            Controls.Add(optionsButton);
        }

        private void OptionsButtonOnClick(object sender, EventArgs e)
        {
            UpdateOptionsPanel();
            if (!Controls.Contains(optionsPanel))
            {
                pause = true;
                Controls.Add(optionsPanel);
            }
            else
            {
                pause = false;
                game.IsAmmo = optionsPanel.AmmoBox.Checked;
                game.IsLevel = optionsPanel.LevelBox.Checked;
                game.IsSight = optionsPanel.SightBox.Checked;
                game.IsFastMode = optionsPanel.OnlyFastMode.Checked;
                game.IsFastTurn = optionsPanel.EnabledMode.Checked;
                Controls.Remove(optionsPanel);
            }
        }

        private void UpdateOptionsPanel()
        {
            optionsPanel.Size = new Size((int)(game.Width / 1.25), (int)(game.Height / 1.25));
            optionsPanel.Location = new Point(game.Width / 2 - optionsPanel.Width / 2,
                game.Height / 2 - optionsPanel.Height / 2);
            optionsPanel.Update();
        }

        private void UpdateOptionButton() => optionsButton.Location = new Point(status.Right - optionsButton.Width, status.Y);

        private void OnTimerTick(object sender, EventArgs args)
        {
            if (!pause)
            {
                if (left && !game.IsFastMode)
                    game.Human.TurnLeft();
                if (right && !game.IsFastMode)
                    game.Human.TurnRight();
                if (game.Human.Life <= 0)
                {
                    var s = game.Human.Scores;
                    Restart();
                    Pause();
                    MessageBox.Show(string.Format("Game over, your result is {0}", s), "Game over",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                game.Act();
            }
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e) => HandleKeys(e, true);

        protected override void OnKeyUp(KeyEventArgs e) => HandleKeys(e, false);

        private void HandleKeys(KeyEventArgs e, bool flag)
        {
            if (e.KeyCode == Keys.A)
                left = flag;
            if (e.KeyCode == Keys.D)
                right = flag;/*
            if (e.KeyCode == Keys.W)
                shoot = flag;*/
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            switch (char.ToLower(e.KeyChar))
            {
                case 'w':
                case 'ц':
                    game.Human.Shoot();
                    break;
                case 'p':
                case 'з':
                    Pause();
                    break;
                case 'r':
                case 'к':
                    Restart();
                    break;
                case 'z':
                case 'я':
                    game.Human.FastTurnLeft();
                    break;
                case 'x':
                case 'ч':
                    game.Human.FastTurnRight();
                    break;
            }
        }

        private void Pause() => pause = !pause;

        private void Restart()
        {
            game.Restart();
            ChangeTheme();
            left = right = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            UpdateGame(e);
            game.Human.Draw(e.Graphics);
            game.Human.Shell?.Draw(e.Graphics, game.Height);
            game.Bot.Shell?.Draw(e.Graphics, game.Height);
            e.Graphics.FillRectangle(Brushes.Black, status);
            DrawLifes(e.Graphics);
            DrawLevel(e.Graphics);
            DrawBang(e.Graphics);
        }

        private void DrawBang(Graphics g)
        {
            var r = new Random(DateTime.Now.Millisecond);
            if (game.BangPlace != null)
            {
                var a = (float) (2 * r.NextDouble() * Math.PI);
                var bangLocation = game.BangPlace.Value.Convert(game.Height);
                var dx = bangLocation.X + bangImage.Width / 2;
                var dy = bangLocation.Y + bangImage.Height / 2;
                g.TranslateTransform(dx, dy);
                g.RotateTransform(a);
                g.DrawImage(bangImage, new Point(0, 0));
                g.RotateTransform(-a);
                g.TranslateTransform(-dx, -dy);
                if (--bangTime <= 0)
                {
                    bangTime = 15;
                    game.BangPlace = null;
                }
            }
        }

        private void DrawLevel(Graphics g)
        {
            var x = new PointF(0, game.Level).Convert(game.Height);
            var y = new PointF(game.Width, game.Level).Convert(game.Height);
            g.DrawLine(Pens.Red, x, y);
        }

        private int f;
        //public static Font Font;

        private void UpdateGame(PaintEventArgs e)
        {
            status.Location = new Point(0, e.ClipRectangle.Height - 40);
            status.Size = new Size(e.ClipRectangle.Width, 40);
            if (++f % 5 == 0)
            {
                UpdateOptionButton();
                UpdateOptionsPanel();
                //needUpadateOptions = false;
            }

            game.Height = e.ClipRectangle.Height - status.Height;
            game.Width = e.ClipRectangle.Width;
        }

        private void DrawLifes(Graphics g)
        {
            var textStatus = string.Format("L: {0} S: {1} MAX: {2}", game.Human.Life.ToString()
                , game.Human.Scores, Game.MaxScores);
            if (game.IsAmmo)
                textStatus += string.Format(" A:{0}", game.Human.Ammo); 
            g.DrawString(textStatus, new Font("Determination Mono(RUS BY LYAJK", 23), Brushes.DarkRed,
                status, StringFormat.GenericTypographic);
        }

        //protected override void OnSizeChanged(EventArgs e) => needUpadateOptions = true;
    }
}