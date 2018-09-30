using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagementCore
{
    public class CustomMessageBox
    {
        // Method to refer the MessageBox quicker
        public static DialogResult Show(object MessageBoxContent, string WindowTitel = "", MessageBoxButtons MessageBoxButtons = MessageBoxButtons.OK, MessageBoxIcon MessageBoxIcon = MessageBoxIcon.None, MessageBoxDefaultButton MessageBoxDefaultButton = MessageBoxDefaultButton.Button1, MessageBoxOptions MessageBoxOptions = MessageBoxOptions.DefaultDesktopOnly)
        {
            switch (MessageBox.Show(MessageBoxContent.ToString(), WindowTitel, MessageBoxButtons, MessageBoxIcon, MessageBoxDefaultButton))
            {
                case DialogResult.None:
                    {
                        return DialogResult.None;
                    }
                case DialogResult.OK:
                    {
                        return DialogResult.OK;
                    }
                case DialogResult.Cancel:
                    {
                        return DialogResult.Cancel;
                    }
                case DialogResult.Abort:
                    {
                        return DialogResult.Abort;
                    }
                case DialogResult.Retry:
                    {
                        return DialogResult.Retry;
                    }
                case DialogResult.Ignore:
                    {
                        return DialogResult.Ignore;
                    }
                case DialogResult.Yes:
                    {
                        return DialogResult.Yes;
                    }
                case DialogResult.No:
                    {
                        return DialogResult.No;
                    }
                default:
                    {
                        return DialogResult.Abort;
                    }
            }
        }
    }
}
