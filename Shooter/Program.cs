using System;
using System.Net.Mime;
using System.Windows.Forms;
namespace Shooter
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            /*MessageBox.Show(
                @"Controls:
w - shoot,
a - turn right,
d - turn left,
p - pause,
r - restart", "Controls", MessageBoxButtons.OK, MessageBoxIcon.Information);*/

            Application.Run(new MainForm());
        }
    }
}