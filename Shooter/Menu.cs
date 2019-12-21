using System.Drawing;
using System.Windows.Forms;

namespace Shooter
{
    public class Menu : Panel
    {
        private readonly Label nameLabel;
        private readonly GroupBox contentInUse;
        private readonly GroupBox fastTurnBox;
        public readonly CheckBox AmmoBox;
        public readonly RadioButton DisabledMode;
        public readonly RadioButton EnabledMode;
        public readonly RadioButton OnlyFastMode;
        public readonly Button ChangeThemeButton;
        public readonly CheckBox SightBox;
        public readonly CheckBox LevelBox;
        private readonly Label controls;
        public Menu()
        {
            controls = new Label { Text = @"Controls:
w - shoot,
a - turn right,
d - turn left,
p - pause,
r - restart,
z - fast turn left,
x - fast turn right", Font = new Font(MainForm.FontFamily, 15) };
            controls.AutoSize = true;
            BorderStyle = BorderStyle.Fixed3D;
            nameLabel = new Label { Text = "Options", Font = new Font(MainForm.FontFamily, 18) };
            nameLabel.AutoSize = true;
            contentInUse = new GroupBox();
            contentInUse.Font = Font = new Font(MainForm.FontFamily, 13);
            contentInUse.Text = "Gameplay";

            fastTurnBox = new GroupBox();
            fastTurnBox.Font = Font = new Font(MainForm.FontFamily, 14);
            fastTurnBox.Text = "Fast turn mode";
            ChangeThemeButton = new Button { Text = "Change theme" };

            Update();
            DisabledMode = new RadioButton();
            OnlyFastMode = new RadioButton();
            EnabledMode = new RadioButton();
            InitializeFastTurnButtons();

            AmmoBox = new CheckBox();
            LevelBox = new CheckBox();
            SightBox = new CheckBox();
            InitializeCheckBoxes();
            //ChangeThemeButton.Size = new Size(Width / 6, Width / 18);
            //ChangeThemeButton.Location = new Point(Width - Width / 12 - ChangeThemeButton.Width, Height - 2 * ChangeThemeButton.Height);
            Controls.Add(ChangeThemeButton);
            AddControls();

        }

        private void InitializeFastTurnButtons()
        {
            DisabledMode.Text = "Disabled";
            DisabledMode.Location = fastTurnBox.Location;
            DisabledMode.Size = new Size(fastTurnBox.Width, fastTurnBox.Height / 3 + 15);
            DisabledMode.Checked = true;
            EnabledMode.Checked = false;
            OnlyFastMode.Checked = false;
            fastTurnBox.Controls.Add(DisabledMode);
            EnabledMode.Text = "Enabled";
            EnabledMode.Location = new Point(fastTurnBox.Location.X, DisabledMode.Bottom + 15);
            EnabledMode.Size = new Size(fastTurnBox.Width, fastTurnBox.Height / 3 + 15);
            fastTurnBox.Controls.Add(EnabledMode);

            OnlyFastMode.Text = "Only fast mode";
            OnlyFastMode.Location = new Point(fastTurnBox.Location.X, EnabledMode.Bottom + 15); //fastTurnBox.Location;
            OnlyFastMode.Size = new Size(fastTurnBox.Width + 15, fastTurnBox.Height / 3 + 15);
            fastTurnBox.Controls.Add(OnlyFastMode);
        }

        private void AddControls()
        {
            Controls.Add(contentInUse);
            Controls.Add(fastTurnBox);
            Controls.Add(nameLabel);
            Controls.Add(controls);
        }

        private void InitializeCheckBoxes()
        {
            AmmoBox.Text = "Ammo";
            AmmoBox.Location = contentInUse.Location;
            LevelBox.Text = "Level";
            LevelBox.Location = new Point(AmmoBox.Location.X, AmmoBox.Bottom);
            SightBox.Text = "Sight";
            SightBox.Location = new Point(LevelBox.Location.X, LevelBox.Bottom);
            contentInUse.Controls.Add(AmmoBox);
            contentInUse.Controls.Add(LevelBox);
            contentInUse.Controls.Add(SightBox);
            AmmoBox.Checked = true;
            LevelBox.Checked = true;
            SightBox.Checked = true;
        }

        private void UpdateGroupBox()
        {
            contentInUse.Size = new Size(Width / 2, (Height - nameLabel.Height) / 2);
            contentInUse.Location = new Point(0, nameLabel.Bottom);
        }

        private void UpdateFastTurnBox()
        {
            fastTurnBox.Size = contentInUse.Size;//new Size(Width / 2, (Height - nameLabel.Height) / 2);
            fastTurnBox.Location = new Point(0, contentInUse.Bottom);
        }

        public void Update()
        {
            UpdateLabel();
            UpdateGroupBox();
            UpdateControls();
            UpdateFastTurnBox();
            ChangeThemeButton.Size = new Size(Width / 4, Width / 8);
            ChangeThemeButton.Location = new Point(Width - Width / 4 - ChangeThemeButton.Width, Height - 2 * ChangeThemeButton.Height);

        }

        void UpdateControls()
        {
            controls.Location = new Point(contentInUse.Right, contentInUse.Top);

        }
        void UpdateLabel()
        {
            //nameLabel.Size = new Size(Width / 2, Width / 8);
            nameLabel.Location = new Point(Width / 2 - nameLabel.Width / 2, 0);
        }

        /*void UpdateAmmoBox()
        {
            ammoBox.Location = contentInUse.Location;
            ammoBox.Size = new Size(contentInUse.Width - 15, contentInUse.Height / 3);
        }*/
    }
}