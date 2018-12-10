using System.Windows.Forms;

namespace Shooter
{
    partial class MainForm
    {
        protected override void OnKeyDown(KeyEventArgs e) => HandleKeys(e, true);

        protected override void OnKeyUp(KeyEventArgs e) => HandleKeys(e, false);

        private void HandleKeys(KeyEventArgs e, bool flag)
        {
            if (e.KeyCode == Keys.A)
                left = flag;
            if (e.KeyCode == Keys.D)
                right = flag;
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

    }
}