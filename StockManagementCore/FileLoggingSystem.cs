using System;
using System.IO;

namespace StockManagementCore
{
    public class FileLoggingSystem
    {
        #region Variables
        private string eventFile = CoreFunctions.GetExecutingDirectoryName() + "..\\config\\protocol.log";
        #endregion

        public void Log(string _loggingText)
        {
            // TODO: Implement a folder exist check and create the folder if it doesn't exist
            if (File.Exists(eventFile))
            {
                using (StreamWriter sw = File.AppendText(eventFile))
                {
                    sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd hh:mm:ss:ms] ") + _loggingText);
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(eventFile))
                {
                    sw.WriteLine(DateTime.Now.ToString("[yyyy.MM.dd - hh:mm:ss] ") + "Logging file created");
                    sw.WriteLine(DateTime.Now.ToString("[yyyy.MM.dd - hh:mm:ss] ") + _loggingText);
                }
            }
        }
    }
}
