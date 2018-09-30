using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace StockManagementCore
{
    /// <summary>
    /// Create a New INI file to store or load data
    /// </summary>
    public class FileHandling_Ini
    {
        #region Variables
        // ******** Dll import section ********
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        // ******** Dll import section ********

        private string iniFilePath = "";
        #endregion

        /// <summary>
        /// INIFile Constructor. When initializing, it creates immediately the file.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public FileHandling_Ini(string INIPath)
        {
            iniFilePath = INIPath;
        }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value, bool _createNewFileNow)
        {
            try
            {
                if (_createNewFileNow)
                {
                    File.Create(iniFilePath).Dispose();
                }
                WritePrivateProfileString(Section, Key, Value, this.iniFilePath);
            }
            catch (System.Exception err)
            {
                CustomMessageBox.Show(err.Message);
            }

        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.iniFilePath);

            return temp.ToString();
        }
    }
}