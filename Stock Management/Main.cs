using StockManagementCore;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Resources;
using System.Reflection;

namespace StockManagement
{
    public partial class Main : Form
    {
        #region Variables
        // *************************
        // ******** Classes ********
        // *************************
        private StockManagement stockManagementMenu = null;
        private Settings settingsMenu = null;

        private FileLoggingSystem fileLogging = new FileLoggingSystem();
        private FileLocking confugurationFileLocking = null;
        public FileLocking ConfugurationFileLocking
        {
            get { return confugurationFileLocking; }
            set { confugurationFileLocking = value; }
        }
        // *************************
        // *************************
        // *************************

        private FileSystemWatcher FileLockNotification = null;

        // Variables for the moveable form
        private int moveFormXDifference = 0;
        private int moveFormYDifference = 0;

        // Localisation
        private string assemblyPath = CoreFunctions.GetExecutingDirectoryName() + "Localisation.dll";
        private ResourceManager localisation = null;

        // Configuration file variables
        private string applicationConfigurationFile = CoreFunctions.GetExecutingDirectoryName() + "config\\configuration.xml";
        private string currentUser = "";

        // Loading and locking boolean
        private bool isConfigurationFileLocked = false;
        private bool hasProgrammeLoaded = false;
        private bool doesConfigurationFileExists = false;

        private Exception applicationWideException = null;

        // XML file related variables
        private XDocument xdoc = null;
        public XDocument Xdoc
        {
            get { return xdoc; }
            set { xdoc = value; }
        }

        // Language variables
        public enum Languages
        {
            English,
            German
        }

        public Languages CurrentLanguagePublic { get; set; } = Languages.English;
        #endregion

        public Main()
        {
            // Check if libraries are available
            if (!File.Exists("StockManagementCore.dll"))
            {
                MessageBox.Show("Die erforderliche Datei \"StockManagementCore.dll\" wurde nicht gefunden, das Programm wird nun beendet.", "Fehler beim Laden", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
            }

            if (!File.Exists("DocumentFormat.OpenXml.dll"))
            {
                MessageBox.Show("Die erforderliche Datei \"DocumentFormat.OpenXml.dll\" wurde nicht gefunden, das Programm wird nun beendet", "Fehler beim Laden", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();
            }

            // Application wide exception catcher (is used if no try-catch expression is used and an error occurs)
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CoreFunctions.UnhandledExceptionCatcher);

            #region Configuration file locking and checking
            confugurationFileLocking = new FileLocking();

            // Try to lock the configuration file
            if (File.Exists(applicationConfigurationFile))
            {
                if (confugurationFileLocking.OpenAndLock(applicationConfigurationFile, FileAccess.ReadWrite, FileShare.Read, ref applicationWideException))
                {
                    CreateBackup();

                    // If there is no root element asign null to Xdoc
                    try
                    {
                        xdoc = XDocument.Load(confugurationFileLocking.LockedFile);
                    }
                    catch (Exception _error)
                    {
                        xdoc = null;
                        // TODO: Protocol it!
                    }

                    // If Xdoc equals null or not equals "ConfigFile" the whole document is about to be recreated
                    if (xdoc != null)
                    {
                        if (xdoc.Root.Name == "ConfigFile")
                        {
                            ReadXMLConfiguration(true);
                        }
                        else
                        {
                            ReadXMLConfiguration(false);
                        }
                    }
                    else
                    {
                        ReadXMLConfiguration(false);
                    }

                    // Set the current user of the program and save it to the configuration file
                    xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("GeneralSettings").Element("CurrentUser").Value = Environment.UserName;
                    confugurationFileLocking.WriteXDocument(xdoc);
                }
                else
                {
                    xdoc = XDocument.Load(confugurationFileLocking.ReadFile(applicationConfigurationFile, ref applicationWideException));

                    currentUser = xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("GeneralSettings").Element("CurrentUser").Value.ToString();

                    isConfigurationFileLocked = true;

                    TrackNotificationFile();
                }

                doesConfigurationFileExists = true;
            }
            else
            {
                // Set the language to english because the configuration file is missing and english is the default language
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            }
            #endregion

            InitializeComponent();

            // Form Events
            this.FormClosing += new FormClosingEventHandler(MainMenu_FormClosing);
            this.Load += new EventHandler(MainMenu_Load);
            this.MouseDown += new MouseEventHandler(MainMenu_MouseDown);
            this.MouseMove += new MouseEventHandler(MainMenu_MouseMove);
            this.Paint += new PaintEventHandler(MainMenu_Paint);
            this.Shown += new EventHandler(MainMenu_Shown);
        }

        #region Form events
        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (doesConfigurationFileExists && !isConfigurationFileLocked && !hasProgrammeLoaded)
            {
                // Empty the CurrentUser element in the configuration file when exiting the program
                xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("GeneralSettings").Element("CurrentUser").Value = "";
                confugurationFileLocking.WriteXDocument(xdoc);

                confugurationFileLocking.Dispose();
            }
            else
            {
                // TODO: Should i keep the else statement or should i remove it?
            }


            //// TODO: Do I need this?
            //if (!programmLoad)
            //{
            //    ConfugurationFile_Locking.Dispose();
            //}
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            // Set all text regarding the current culture info
            SetLocalisation();

            // Screen resolution variables
            int screenWidth = Screen.FromControl(this).Bounds.Width;
            int screenHeight = Screen.FromControl(this).Bounds.Height;

            // Form settings
            this.Width = 840; // screenWidth / 2;
            this.Height = 525; // screenHeight / 2;
            // TODO: Clear - this.Left = (screenWidth - this.Width) / 2;
            // TODO: Clear - this.Top = (screenHeight - this.Height) / 2;


            // Stock Management button Position
            StockManagementButton.Left = (this.Width - StockManagementButton.Width) / 2;
            StockManagementButton.Top = (((this.Height - StockManagementButton.Height) - StockManagementButton.Height - SettingsButton.Height) / 6) * 4;

            // Settings button position
            SettingsButton.Left = (this.Width - SettingsButton.Width) / 2;
            SettingsButton.Top = (this.Height - SettingsButton.Height) - StockManagementButton.Top;

            // Locations for the titel bar buttons
            CloseForm.Left = this.Width - CloseForm.Width - 1;
            CloseForm.Top = 1;
            MinimizeForm.Left = this.Width - CloseForm.Width - MinimizeForm.Width - 1;
            MinimizeForm.Top = 1;
        }

        private void MainMenu_MouseDown(object sender, MouseEventArgs e)
        {
            moveFormXDifference = (Cursor.Position.X - this.Left);
            moveFormYDifference = (Cursor.Position.Y - this.Top);
        }

        private void MainMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left = Cursor.Position.X - moveFormXDifference;
                this.Top = Cursor.Position.Y - moveFormYDifference;
            }
        }

        private void MainMenu_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.FromArgb(0, 0, 0), ButtonBorderStyle.Solid);
        }

        private void MainMenu_Shown(object sender, EventArgs e)
        {
            if (doesConfigurationFileExists)
            {
                if (isConfigurationFileLocked)
                {
                    CustomMessageBox.Show("Das Programm wird gerade von " + currentUser + " benutzt.\nSie werden benachrichtigt, sobald das Programm wieder verfügbar ist.", "Programmverwendung", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                settingsMenu = new Settings
                {
                    // User defined variables
                    ConfigurationFileExists = doesConfigurationFileExists,
                    ConfugurationFileLocking = null,
                    MainMenuPublic = this,
                    Localisation = localisation,

                    // Program defined variables
                    Owner = this
                };

                settingsMenu.Show();
                this.Visible = false;
            }
        }
        #endregion

        #region Control events
        #region Control: StockManagement
        private void StockManagement_Click(object sender, EventArgs e)
        {
            if (isConfigurationFileLocked)
            {
                int screenWidth = Screen.FromControl(this).WorkingArea.Width;
                int screenHeigth = Screen.FromControl(this).WorkingArea.Height;

                stockManagementMenu = new StockManagement
                {
                    // User defined variables
                    DoesConfigurationFileExist = doesConfigurationFileExists,
                    IsConfigurationFileLocked = isConfigurationFileLocked,
                    CurrentLanguage = (StockManagement.Languages)CurrentLanguagePublic,
                    FileLogging = fileLogging,
                    Xdoc = xdoc,
                    Localisation = localisation,

                    // Program defined variables
                    Height = (screenHeigth / 10) * 9,
                    Location = Screen.FromControl(this).Bounds.Location,
                    StartPosition = FormStartPosition.CenterScreen,
                    Width = (screenWidth / 10) * 9
                };

                // Initialize the file watcher to track the current-user file
                stockManagementMenu.FileLockNotificationPublic = new FileSystemWatcher();
                stockManagementMenu.FileLockNotificationPublic.Path = CoreFunctions.GetExecutingDirectoryName() + "config";
                stockManagementMenu.FileLockNotificationPublic.Filter = Path.GetFileName(applicationConfigurationFile);
                stockManagementMenu.FileLockNotificationPublic.NotifyFilter = NotifyFilters.LastWrite;
                stockManagementMenu.FileLockNotificationPublic.Changed += new FileSystemEventHandler(stockManagementMenu.FileLockNotification_Changed);
                stockManagementMenu.FileLockNotificationPublic.EnableRaisingEvents = true;

                // Close the file watcher of this class
                FileLockNotification.EnableRaisingEvents = false;
                FileLockNotification = null;

                hasProgrammeLoaded = true;

                if (settingsMenu != null) { settingsMenu = null; }

                stockManagementMenu.Show();

                this.Close();
            }
            else
            {
                // TODO: Clear
                //CustomMessageBox.Show("Das Programm wird gerade von " + currentUser + " benutzt.\nSie werden benachrichtigt, sobald das Programm wieder verfügbar ist.", "Programmverwendung", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                //return;

                XElement xmlElements = xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("DataReferences");

                if (File.Exists(xmlElements.Element("CustomerFile").Value))
                {
                    int screenWidth = Screen.FromControl(this).WorkingArea.Width;
                    int screenHeigth = Screen.FromControl(this).WorkingArea.Height;

                    stockManagementMenu = new StockManagement
                    {
                        // User defined variables
                        DoesConfigurationFileExist = doesConfigurationFileExists,
                        IsConfigurationFileLocked = isConfigurationFileLocked,
                        ConfugurationFileLocking = confugurationFileLocking,
                        CurrentLanguage = (StockManagement.Languages)CurrentLanguagePublic,
                        FileLogging = fileLogging,
                        Xdoc = xdoc,
                        Localisation = localisation,

                        // Program defined variables
                        Height = (screenHeigth / 10) * 9,
                        Location = Screen.FromControl(this).Bounds.Location,
                        StartPosition = FormStartPosition.CenterScreen,
                        Width = (screenWidth / 10) * 9
                    };

                    hasProgrammeLoaded = true;
                    if (settingsMenu != null) { settingsMenu = null; }

                    stockManagementMenu.Show();

                    this.Close();
                }
                else
                {
                    // TODO: Localisize this
                    CustomMessageBox.Show("Der Pfad der Quelldatei ist nicht korrekt gesetzt, setzen Sie den korrekten Pfad", "Quelldatei Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    if (settingsMenu == null)
                    {
                        settingsMenu = new Settings
                        {
                            ConfigurationFileExists = doesConfigurationFileExists,
                            ConfugurationFileLocking = confugurationFileLocking,
                            MainMenuPublic = this,
                            Localisation = localisation,

                            Owner = this,
                            StartPosition = FormStartPosition.CenterParent,
                            Visible = true
                        };

                        this.Visible = false;
                    }
                    else if (!settingsMenu.Visible)
                    {
                        //int screenWidth = Screen.FromControl(this).WorkingArea.Width;
                        //int screenHeigth = Screen.FromControl(this).WorkingArea.Height;

                        settingsMenu.Owner = this;
                        settingsMenu.StartPosition = FormStartPosition.CenterScreen;
                        settingsMenu.Visible = true;

                        this.Visible = true;
                    }
                }
            }
        }
        #endregion

        #region Control: Settings
        private void Settings_Click(object sender, EventArgs e)
        {
            // If this is true there is an instance of the filewatcher running - if not you declined to open the file after it's was available again
            if (isConfigurationFileLocked)
            {
                // TODO: Test this functionality!
                currentUser = CoreFunctions.ReloadCurrentUser(ref confugurationFileLocking, ref xdoc, currentUser, ref applicationWideException);

                CustomMessageBox.Show("Das Programm wird gerade von " + currentUser + " benutzt.\nSie werden benachrichtigt, sobald das Programm wieder verfügbar ist.", "Programmverwendung", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            // This only gets evaluated if there is no instance of the filewatcher is running
            //if (ConfugurationFile_Locking.OpenAndLock(applicationConfigurationFile, FileAccess.ReadWrite, FileShare.Read, ref error))
            //{
            //if (CurrentUserFile_Locking.CreateAndLock(notificationFilename, FileAccess.ReadWrite, FileShare.ReadWrite, ref error))
            //{
            //    // Create the current-user file
            //    CurrentUserFile_Reader.IniWriteValue("FileLocking", "CurrentUser", Environment.UserName, false);

            //    File.SetAttributes(notificationFilename, File.GetAttributes(notificationFilename) | FileAttributes.Hidden | FileAttributes.Temporary);

            if (settingsMenu == null)
            {
                settingsMenu = new Settings
                {
                    // User defined variables
                    ConfigurationFileExists = doesConfigurationFileExists,
                    ConfugurationFileLocking = confugurationFileLocking,
                    Xdoc = xdoc,
                    MainMenuPublic = this,
                    Localisation = localisation,
                    
                    // Program defined variables
                    Owner = this
                };

                settingsMenu.FormClosed += new FormClosedEventHandler(Settings_FormClosed);

                settingsMenu.ShowDialog();
            }
            else
            {
                // TODO: Create protocol
            }

            //        MessageBox.Show(FileLockNotification.EnableRaisingEvents.ToString());

            //        configurationFileLocked = false;
            //        FileLockNotification.EnableRaisingEvents = false;
            //    }
            //    else
            //    {
            //        // TODO: Implement a message or a protocol that the current user file could not be opened and locked
            //        ConfugurationFile_Locking.Dispose();
            //    }
            //}
            //else
            //{
            //    // TODO: Implement a message or a protocol that the configuration file could not be opened and locked
            //    CustomMessageBox.Show("failure = " + error.Message);
            //}
            //System.Diagnostics.Debug.Print(error.Message.ToString());
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            settingsMenu = null;
        }
        #endregion

        #region Control: MinimizeForm
        private void MinimizeForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MinimizeForm_MouseEnter(object sender, EventArgs e)
        {
            MinimizeForm.Image = global::StockManagement.Properties.Resources.MinimizeButtonMouseEnter_29x22;
        }

        private void MinimizeForm_MouseLeave(object sender, EventArgs e)
        {
            MinimizeForm.Image = global::StockManagement.Properties.Resources.MinimizeButtonNormal_29x22;
        }
        #endregion

        #region Control: CloseForm
        private void CloseForm_Click(object sender, EventArgs e)
        {
            try
            {
                if (settingsMenu != null)
                {
                    settingsMenu.Close();
                }

                this.Close();
            }
            catch (Exception _error)
            {
                // TODO: Protocol entry
                CustomMessageBox.Show(_error.Message, "Fehler beim Beenden", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CoreFunctions.DebugWrite(_error.Message);
            }

            this.Close();
        }

        private void CloseForm_MouseEnter(object sender, EventArgs e)
        {
            CloseForm.Image = global::StockManagement.Properties.Resources.CloseButtonMouseEnter_29x22;
        }

        private void CloseForm_MouseLeave(object sender, EventArgs e)
        {
            CloseForm.Image = global::StockManagement.Properties.Resources.CloseButtonNormal_29x22;
        }
        #endregion
        #endregion

        #region Custom events
        // Control: FileLockNotification
        private void FileLockNotification_Changed(object sender, FileSystemEventArgs e)
        {
            MessageBox.Show("asdasd");
            if (isConfigurationFileLocked)
            {
                // Try to lock the configuration file
                Thread.Sleep(3000);
                if (hasProgrammeLoaded) { return; }
                if (confugurationFileLocking.OpenAndLock(applicationConfigurationFile, FileAccess.ReadWrite, FileShare.Read, ref applicationWideException))
                {
                    if (CustomMessageBox.Show("Das Programm ist nun wieder verfügbar, möchten Sie es nun öffnen?", "Verfügbarkeit", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        confugurationFileLocking.Dispose();
                    }

                    // TODO IMPORTANT: Read the current user or wirte it
                    isConfigurationFileLocked = false;
                    FileLockNotification.EnableRaisingEvents = false;
                }
                else
                {
                    // TODO: Implement a message or a protocol that the configuration file could not be opened and locked

                    currentUser = "";

                    CustomMessageBox.Show("Das Programm wird gerade von " + currentUser + " benutzt.\nSie werden benachrichtigt, sobald das Programm wieder verfügbar ist.", "Programmverwendung", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
        }
        #endregion

        #region Functions
        private void TrackNotificationFile()
        {
            // Initialize the file watcher to track the current-user file
            FileLockNotification = new FileSystemWatcher();
            FileLockNotification.Path = CoreFunctions.GetExecutingDirectoryName() + "config";
            FileLockNotification.Filter = Path.GetFileName(applicationConfigurationFile);
            FileLockNotification.NotifyFilter = NotifyFilters.LastWrite;
            FileLockNotification.Changed += new FileSystemEventHandler(FileLockNotification_Changed);
            FileLockNotification.EnableRaisingEvents = true;
        }

        public void SetLocalisation()
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
                    break;
            }

            // Initialise the localisation from the "Localisation.dll" assembly file
            localisation = new ResourceManager("Localisation.Localisation.strings", Assembly.LoadFrom(assemblyPath));

            StockManagementButton.Text = localisation.GetString("MainMenu_Button_StockManagementButton");
            SettingsButton.Text = localisation.GetString("MainMenu_Button_SettingsButton");
        }

        private void GetLanguage()
        {
            if (doesConfigurationFileExists)
            {
                string retrievedLanguage;

                // TODO important: Search for a method that checks for null value - conditional operator
                try
                {
                    retrievedLanguage = xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("GeneralSettings").Element("Language").Value.ToString();
                }
                catch (Exception _error)
                {
                    // TODO: Write imediately to the XML file and set a protocol entry that there was no language
                    retrievedLanguage = "English";
                    CoreFunctions.DebugWrite(_error.Message);
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
                            xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("GeneralSettings").Element("Language").Value = "English";
                            confugurationFileLocking.WriteXDocument(xdoc);
                        // TODO:
                        // Create an entry in the protocol file that the language could not be retrieved
                    }
                        break;
                }
            }
            else
            {
                CurrentLanguagePublic = Languages.English;
            }
        }

        private void ReadXMLConfiguration(bool _RootAvailable)
        {
            if (_RootAvailable)
            {
                List<XElement> fileElements = xdoc.Element("ConfigFile").Descendants().ToList();

                bool ApplicationSettingsElement = false;
                bool DataReferencesElement = false;
                bool CustomerFileElement = false;
                bool GeneralSettingsElement = false;
                bool CurrentUserElement = false;
                bool LanguageElement = false;
                bool CustomerData = false;

                foreach (XElement singleElement in fileElements)
                {
                    if (singleElement.Parent.Name.ToString() != "CustomerData" && singleElement.Parent.Name.ToString() != "Customer")
                    {
                        if (singleElement.Name.ToString() == "ApplicationSettings")
                        {
                            ApplicationSettingsElement = true;
                        }
                        else if (singleElement.Name.ToString() == "DataReferences")
                        {
                            DataReferencesElement = true;
                        }
                        else if (singleElement.Name.ToString() == "CustomerFile")
                        {
                            CustomerFileElement = true;
                        }
                        else if (singleElement.Name.ToString() == "GeneralSettings")
                        {
                            GeneralSettingsElement = true;
                        }
                        else if (singleElement.Name.ToString() == "CurrentUser")
                        {
                            CurrentUserElement = true;
                        }
                        else if (singleElement.Name.ToString() == "Language")
                        {
                            LanguageElement = true;
                        }
                        else if (singleElement.Name.ToString() == "CustomerData")
                        {
                            CustomerData = true;
                        }
                    }
                }

                if (!ApplicationSettingsElement || !DataReferencesElement || !CustomerFileElement || !GeneralSettingsElement || !CurrentUserElement || !LanguageElement || !CustomerData)
                {
                    repeatCheck:

                    if (!ApplicationSettingsElement)
                    {
                        xdoc.Element("ConfigFile").Add(new XElement("ApplicationSettings", new XElement("DataReferences", new XElement("CustomerFile", ""))));

                        ApplicationSettingsElement = true;
                        DataReferencesElement = true;
                        CustomerFileElement = true;

                        goto repeatCheck;
                    }
                    else if (!DataReferencesElement)
                    {
                        xdoc.Element("ConfigFile").Element("ApplicationSettings").Add(new XElement("DataReferences", new XElement("CustomerFile", "")));

                        DataReferencesElement = true;
                        CustomerFileElement = true;

                        goto repeatCheck;
                    }
                    else if (!CustomerFileElement)
                    {
                        xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("DataReferences").Add(new XElement("CustomerFile", ""));

                        CustomerFileElement = true;

                        goto repeatCheck;
                    }
                    else if (!GeneralSettingsElement)
                    {
                        xdoc.Element("ConfigFile").Element("ApplicationSettings").Add(new XElement("GeneralSettings", new XElement("CurrentUser", Environment.UserName), new XElement("Language", "English")));

                        GeneralSettingsElement = true;
                        LanguageElement = true;
                        CurrentUserElement = true;

                        goto repeatCheck;
                    }
                    else if (!CurrentUserElement)
                    {
                        xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("GeneralSettings").Add(new XElement("CurrentUser", Environment.UserName));

                        CurrentUserElement = true;

                        goto repeatCheck;
                    }
                    else if (!LanguageElement)
                    {
                        xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("GeneralSettings").Add(new XElement("Language", "English"));

                        LanguageElement = true;

                        goto repeatCheck;
                    }
                    else if (!CustomerData)
                    {
                        xdoc.Element("ConfigFile").Add(new XElement("CustomerData", ""));
                    }

                    xdoc.Save(confugurationFileLocking.LockedFile);
                }
            }
            else
            {
                // TODO: Create the whole file
                CoreFunctions.DebugWrite("Configuration file recreated, because there was no root element.");
            }

        }

        private void CreateBackup()
        {
            string backupFolder = CoreFunctions.GetExecutingDirectoryName() + "backup";
            string backupFileName = backupFolder + "\\[Sicherungskopie vom " + DateTime.Today.ToString("yyyy-MM-dd") + "] - " + Path.GetFileName(applicationConfigurationFile);

            if (Directory.Exists(backupFolder))
            {
                if (!File.Exists(backupFileName))
                {
                    File.Copy(applicationConfigurationFile, backupFileName);

                    // TODO: Template to copy locked data
                    //using (var fileStream = File.Create(backupFileName))
                    //{
                    //    ConfugurationFile_Locking.LockedFile.Seek(0, SeekOrigin.Begin);
                    //    ConfugurationFile_Locking.LockedFile.CopyTo(fileStream);
                    //}
                }

                repeatCheck:

                int fileCount = Directory.EnumerateFiles(backupFolder, "*" + Path.GetFileName(applicationConfigurationFile), System.IO.SearchOption.TopDirectoryOnly).Count();

                if (fileCount > 30) // TODO: Create a variable for the end user to choose the days but let 20 be the minimum value!!
                {
                    DirectoryInfo directory = new DirectoryInfo(backupFolder);
                    string oldestFile = directory.GetFiles("*" + Path.GetFileName(applicationConfigurationFile), System.IO.SearchOption.TopDirectoryOnly).OrderByDescending(f => f.LastWriteTime).Last().ToString();

                    File.Delete(backupFolder + "/" + oldestFile);

                    goto repeatCheck;
                }
            }
            else
            {
                Directory.CreateDirectory(backupFolder);
                File.Copy(applicationConfigurationFile, backupFileName);
            }
        }
        #endregion
    }
}