using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace StockManagement
{
    public partial class AboutWindow : Form
    {
        #region Variables
        // Classes
        public StockManagement StockManagementMenu { get; set; } = null;
        #endregion

        public AboutWindow()
        {
            InitializeComponent();

            // Events
            this.KeyDown += new KeyEventHandler(AboutWindow_KeyDown);
            this.Load += new EventHandler(AboutWindow_Load);
        }

        #region Form events
        private void AboutWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //Prevent the "Ding"
                e.SuppressKeyPress = true;

                this.Visible = false;
            }
        }

        private void AboutWindow_Load(object sender, EventArgs e)
        {
            // Set all text regarding the current culture info
            SetLocalisation();
        }
        #endregion

        #region Control events
        // Control: EMailLink
        private void EMailLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:" + Localisation.Localisation.strings.AboutWindow_AuthorEmail);
        }

        // Control: CreatorLink
        private void CreatorLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.freepik.com/");
        }

        // Control: SourceWebsiteLink
        private void SourceWebsiteLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.flaticon.com/");
        }
        #endregion

        #region Functions
        public void SetLocalisation()
        {
            switch (StockManagementMenu.CurrentLanguage)
            {
                case StockManagement.Languages.English:
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                }
                break;
                case StockManagement.Languages.German:
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
                }
                break;
                default:
                {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                    // TODO:
                    // Create an entry in the protocol file that the language could not be retrieved
                    // Probably change the language in the settings file?
                }
                break;
            }

            // Set all the control's text's
            this.Text = Localisation.Localisation.strings.AboutWindowTitle;

            ApplicationName.Text = Localisation.Localisation.strings.AboutWindow_ApplicationName;
            ApplicationVersion.Text = "Version: " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
            AuthorE_MailTitle.Text = Localisation.Localisation.strings.AboutWindow_Author;
            AuthorE_MailLink.Text = Localisation.Localisation.strings.AboutWindow_AuthorEmail;
        }
        #endregion
    }
}
