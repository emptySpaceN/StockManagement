using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using StockManagementCore;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Threading;
using System.Xml.Linq;

namespace Updater
{
    public class DirectUpdate
    {

        #region Variables
        public static List<Track> GetOnlineFileList { get; private set; } = null;
        public static List<string> GetApplicationFileList { get; private set; } = null;
        public static List<string> GetTemporaryFileList { get; private set; } = null;
        public static WebClient GetClient { get; private set; } = null;

        private static string applicationConfigurationFile = CoreFunctions.GetExecutingDirectoryName() + "config\\configuration.xml";
        private static string assemblyPath = CoreFunctions.GetExecutingDirectoryName() + "Localisation.dll";
        private static ResourceManager localisation = null;

        public enum Languages
        {
            English,
            German
        }

        public static Languages CurrentLanguagePublic { get; set; } = Languages.English;
        #endregion

        static void Main(string[] args)
        {
            // Set all text regarding the current culture info
            SetLocalisation();
                    
            // Initialize the file collection
            InitFileCollection();
            
            if (args.Length > 0)
            {
                try
                {
                    if (args[0] == "DeleteUpdateHelper" || args[0] == "UpdateCheckFromMainApplication")
                    {
                        ApplicationUpdate updateProcess = new ApplicationUpdate();

                        if (updateProcess.ApplicationUpdateProcess(args[0]))
                        {
                            MessageBox.Show(localisation.GetString("Updater_ApplicationUpdateMessage"), localisation.GetString("Updater_ApplicationUpdate"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); //(MessageBoxOptions)0x40000  // MB_TOPMOST

                            Process.Start("StockManagement.exe");
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es ist ein Fehler aufgetreten:\nFehlermeldung: " + e.Message, localisation.GetString("Updater_ApplicationUpdate"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            else if (args.Length == 0)
            {
                try
                {
                    if (!GetUpdateFiles(GetClient, GetOnlineFileList, GetApplicationFileList, GetTemporaryFileList, false))
                    {
                        // TOTO: Insert a message box with any message
                        GetClient.Dispose();
                        return;
                    }

                    Process.Start("UpdateHelper.exe", string.Join(" ", GetTemporaryFileList.ToArray()));
                    Environment.Exit(0);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Es ist ein Fehler aufgetreten:\nFehlermeldung: " + e.Message, localisation.GetString("Updater_ApplicationUpdate"), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
            }

            if (GetClient != null) { GetClient.Dispose(); }

            return;
        }

        #region Functions
        private static void SetLocalisation()
        {
            // Initialize the configuration file
            GetLanguage();

            switch (CurrentLanguagePublic)
            {
                case Languages.English:
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                }
                break;
                case Languages.German:
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
                }
                break;
                default:
                {
                }
                break;
            }

            // Initialise the localisation from the "Localisation.dll" assembly file
            localisation = new ResourceManager("Localisation.Localisation.strings", Assembly.LoadFrom(assemblyPath));
        }

        private static void GetLanguage()
        {
            string retrievedLanguage;

            // TODO important: Search for a method that checks for null value - conditional operator
            try
            {
                FileLocking ConfugurationFile_Locking = new FileLocking();
                XDocument Xdoc = null;
                Exception error = null;

                Xdoc = XDocument.Load(ConfugurationFile_Locking.ReadFile(applicationConfigurationFile, ref error));

                retrievedLanguage = Xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("GeneralSettings").Element("Language").Value.ToString();
            }
            catch (Exception _error)
            {
                // TODO: Write imediately to the XML file and set a protocol entry that there was no language
                retrievedLanguage = "English";
            }

            switch (retrievedLanguage)
            {
                case "English":
                {
                    CurrentLanguagePublic = Languages.English;
                }
                break;
                case "German":
                {
                    CurrentLanguagePublic = Languages.German;
                }
                break;
                default:
                {
                    CurrentLanguagePublic = Languages.English;
                    // TODO:
                    // Create an entry in the protocol file that the language could not be retrieved
                }
                break;
            }
        }

        public static void InitFileCollection()
        {
            string[] fileList = new string[5];           // List that holds the file names on the server
            string updateFilesServerPath = "https://github.com/emptySpaceN/StockManagement/releases/download/ReleaseVersion/";    // Holds the path for the update files

            GetOnlineFileList = new List<Track>();
            GetApplicationFileList = new List<string>();
            GetTemporaryFileList = new List<string>();
            GetClient = new WebClient();

            // Fill the online file list
            fileList[0] = "StockManagement.exe";
            fileList[1] = "StockManagementCore.dll";
            fileList[2] = "Localisation.dll";
            fileList[3] = "UpdateHelper.exe";
            fileList[4] = "Updater.exe";

            for (int i = 0; i < fileList.Length; i++)
            {
                GetOnlineFileList.Add(new Track
                {
                    ApplicationName = fileList[i],
                    DownloadAddress = updateFilesServerPath + fileList[i],
                });
            }
        }

        public static bool GetUpdateFiles(WebClient _passedClient, List<Track> _passedOnlineFileList, List<string> _passedApplicationFileList, List<string> _passedTemporaryFileList, bool _passedUpdateFromApplication)
        {
            int temporaryVersion = 0;
            int currentVersion = 0; ;
            string temporaryFileName = "";
            
            foreach (string currentFile in Directory.GetFiles(Environment.CurrentDirectory))
            {
                FileInfo fileInformation = new FileInfo(currentFile);
                FileVersionInfo currentFileVersionInfo = FileVersionInfo.GetVersionInfo(fileInformation.FullName.ToString());

                if (currentFileVersionInfo.FileDescription == "Stock Management")
                {
                    _passedApplicationFileList.Add(fileInformation.Name);
                }
            }

            // Always add the UpdateHelper.exe to the end; it helps during the update process to update the Update.exe itself
            for (int i = 0; i < _passedOnlineFileList.Count; i++)
            {
                for (int j = 0; j < _passedApplicationFileList.Count; j++)
                {
                    if (_passedOnlineFileList[i].ApplicationName == _passedApplicationFileList[j])
                    {
                        temporaryFileName = Path.GetTempPath() + "_temp" + DateTime.Now.ToString("_dd.MM.yyyy_HH-mm-ss_") + _passedApplicationFileList[j];
                        
                        _passedClient.DownloadFile(_passedOnlineFileList[i].DownloadAddress, temporaryFileName);
                        
                        temporaryVersion = Int32.Parse(FileVersionInfo.GetVersionInfo(temporaryFileName).ProductVersion.Replace(".", string.Empty));
                        currentVersion = Int32.Parse(FileVersionInfo.GetVersionInfo(CoreFunctions.GetExecutingDirectoryName() + _passedApplicationFileList[j]).ProductVersion.Replace(".", string.Empty));

                        if (temporaryVersion <= currentVersion)
                        {
                            File.Delete(temporaryFileName);
                        }
                        else
                        {
                            _passedTemporaryFileList.Add(temporaryFileName);
                        }

                        break;
                    }
                }
            }

            if (!Directory.Exists("de"))
            {
                Directory.CreateDirectory("de");

                // TODO: Protocol it, "created from here"
            }

            if (_passedTemporaryFileList.Count < 1)
            {
                if (!_passedUpdateFromApplication)
                {
                    CustomMessageBox.Show("Die neueste Version ist bereits installiert.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); //(MessageBoxOptions)0x40000  // MB_TOPMOST
                }

                _passedClient.Dispose();
                return false;
            }
            else if (_passedTemporaryFileList.Count > 0)
            {
                if (!_passedUpdateFromApplication)
                {
                    DialogResult askForUpdate = MessageBox.Show("Es ist eine neuere Version verfügbar\nMöchten Sie das Update herunterladen?", localisation.GetString("Updater_ApplicationUpdate"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (askForUpdate == DialogResult.No)
                    {
                        return false;
                    }
                }
            }
            
            _passedClient.DownloadFile(_passedOnlineFileList[_passedOnlineFileList.Count - 1].DownloadAddress, CoreFunctions.GetExecutingDirectoryName() + "UpdateHelper.exe");
            _passedClient.Dispose();
            return true;
        }
        #endregion
    }
}