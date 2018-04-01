using System;
using System.Windows.Forms;
using TMF_ftp.TMFLicensing;

namespace TMF_ftp
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
            //Application.Run(new FormMain());
            Application.Run(new Generator());
        }
    }
}
