using System;
using System.Windows.Forms;

namespace TrackerClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RfMain());
            //Application.Run(new FrmMain());
        }
    }
}
