using System.Net.Mime;
using System.Windows.Forms;
namespace Shooter
{
    public class Program
    {
        public static void Main()
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

            Application.Run(new MainForm());
        }
    }
}