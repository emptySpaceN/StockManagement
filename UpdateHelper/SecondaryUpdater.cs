using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System;

namespace UpdateHelper
{
    class SecondaryUpdater
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    FileInfo arr = new FileInfo(args[i]);

                    //MessageBox.Show(arr.Name.Substring(26));

                    File.Delete(arr.Name.Substring(26));

                    File.Move(args[i], Application.StartupPath + "/" + arr.Name.Substring(26));
                }

                Process.Start("Updater.exe", "DeleteUpdateHelper");
                Environment.Exit(0);
            }
        }
    }
}
