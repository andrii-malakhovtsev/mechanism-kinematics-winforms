using System;
using System.Windows.Forms;

namespace MechanismKinematics
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm View = new MainForm();
            _ = new MainInteractions(View);
            Application.Run(View);
        }
    }
}