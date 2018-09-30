using System;
using System.Windows.Forms;

namespace StockManagement
{
    static class Program
    {
        /// <summary>
        /// Main entrypoint of this application
        /// </summary>
        [STAThread]
        static void Main()
       {
                /*Original code
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new mainMenu());
                */

                // New code to be able to close every form without getting a failure - program won't terminate anymore if the entry-form is closed
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var main = new Main();
                main.FormClosed += new FormClosedEventHandler(FormClosed);
                main.Show();
                Application.Run();
        }

        static void FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= FormClosed;

            if (Application.OpenForms.Count == 0)
            {
                Application.ExitThread();
            }
            else
            {
                Application.OpenForms[0].FormClosed += FormClosed;
            }
        }
    }
}
