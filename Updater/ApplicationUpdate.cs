using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Updater
{
    public class ApplicationUpdate
    {
        public bool ApplicationUpdateProcess(string _passedUpdateState)
        {
            try
            {
                if (_passedUpdateState == "UpdateCheckFromMainApplication")
                {
                    if (!DirectUpdate.GetUpdateFiles(DirectUpdate.GetClient, DirectUpdate.GetOnlineFileList, DirectUpdate.GetApplicationFileList, DirectUpdate.GetTemporaryFileList, true))
                    {
                        DirectUpdate.GetClient.Dispose();
                        return false;
                    }

                    Process.Start("UpdateHelper.exe", string.Join(" ", DirectUpdate.GetTemporaryFileList.ToArray()));
                    Environment.Exit(0);

                }
                else if (_passedUpdateState == "DeleteUpdateHelper")
                {
                    try
                    {
                        File.Delete("UpdateHelper.exe");
                        return true;
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Es ist ein Fehler aufgetreten:\nFehlermeldung: " + e.Message, "Programmaktualisierung", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }

            return false;
        }
    }
}
