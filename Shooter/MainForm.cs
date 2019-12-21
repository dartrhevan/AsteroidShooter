using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Shooter
{
    partial class MainForm : Form
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
        static MainForm()
        {
            var customFont = new PrivateFontCollection();
            customFont.AddFontFile("Determination.ttf");
            FontFamily = customFont.Families[0];
            //customFont.AddFontFile("Font/MonsterFriend.ttf");
            /*MonsterFriend = new Font(customFont.Families[1], 30);
            DeterminationBase = new Font(customFont.Families[0], 20);
            DeterminationMini = new Font(customFont.Families[0], 15);
            */
        }
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
            optionsPanel.ChangeThemeButton.Click += (sender, args) => ChangeTheme();
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

        private int updatingCounter;

        public static readonly FontFamily FontFamily;

        private void UpdateGame(PaintEventArgs e)
        {
            status.Location = new Point(0, e.ClipRectangle.Height - 40);
            status.Size = new Size(e.ClipRectangle.Width, 40);
            if (++updatingCounter % 5 == 0)
            {
                UpdateOptionButton();
                UpdateOptionsPanel();
            }

            game.Height = e.ClipRectangle.Height - status.Height;
            game.Width = e.ClipRectangle.Width;
        }
    }
}