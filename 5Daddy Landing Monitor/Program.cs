using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5Daddy_Landing_Monitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        internal static string ErrorLog = GlobalData.ErrorLog;
        [STAThread]
        static void Main()
        {
            try
            {
                if (!File.Exists(ErrorLog)) { File.Create(ErrorLog); }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch(Exception ex)
            {
                GlobalData.ErrorLogInput(ex, "FATAL");
                MessageBox.Show("Fatal Error: " + ex.Message + " Please send the file: " + ErrorLog +" to quin#3017 on discord, Press \"OK\" to close the program", "Uh Oh!", MessageBoxButtons.OK);
                Environment.Exit(-1);
            }
            
        }
    }
}
