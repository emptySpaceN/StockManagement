using System;
using System.Drawing;
using System.Windows.Forms;
using StockManagementCore;
using System.Collections.Generic;
using System.Threading;
using System.Globalization;

namespace StockManagement
{
    public partial class SearchWindow : Form
    {
        #region Variables
        // *************************
        // ******** Classes ********
        // *************************
        public StockManagement StockManagementMenu { get; set; } = null;
        // *************************
        // *************************
        // *************************

        public List<DataGridView> AvailableGrids { get; set; } = null;
        public DataGridView DataGridViewToSearch { get; set; } = null;
        public bool UpdateCurrentCell { get; set; } = false;
        public string CurrentGridName { get; set; } = "";

        // Variables for the moveable form
        private int moveFormXDifference = 0;
        private int moveFormYDifference = 0;

        public enum Languages
        {
            English,
            Spanish,
            German
        }

        public Languages CurrentLanguagePublic { get; set; } = Languages.English;
        #endregion

        public SearchWindow()
        {
            InitializeComponent();

            // Form events
            this.FormClosing += new FormClosingEventHandler(SearchWindow_FormClosing);
            this.Load += new EventHandler(SearchWindow_Load);
            this.KeyDown += new KeyEventHandler(SearchWindow_KeyDown);
            this.MouseDown += new MouseEventHandler(SearchWindow_MouseDown);
            this.MouseMove += new MouseEventHandler(SearchWindow_MouseMove);
            this.VisibleChanged += new EventHandler(SearchWindow_VisibleChanged);
        }

        #region Form events
        private void SearchWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;

                this.Visible = false;
                this.Owner.BringToFront();
            }
        }

        private void SearchWindow_Load(object sender, EventArgs e)
        {
            // Set all text regarding the current culture info
            SetLocalisation();

            //this.Left = ((edi_Menu.Left + edi_Menu.Width) - this.Width) - ((edi_Menu.Width - this.Width) / 2);
            //this.Top = ((edi_Menu.Top + edi_Menu.Height) - this.Height) - ((edi_Menu.Height - this.Height) / 2);

            //this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(222, 222, 222);
            this.KeyPreview = true;

            // Location settings
            TextBoxSearchString.Left = 120;
            TextBoxSearchString.Top = 10;
            TextBoxSearchString.Width = PanelOptions.Width - 130;

            LabelSearchText.Left = 10;
            LabelSearchText.Top = TextBoxSearchString.Top + ((TextBoxSearchString.Height - LabelSearchText.Height) / 2);

            CheckUpperLowerCase.Left = TextBoxSearchString.Left;
            CheckUpperLowerCase.Top = TextBoxSearchString.Bottom + 10;

            PanelOptions.Left = 10;
            PanelOptions.Top = 10;
            PanelOptions.Width = this.ClientSize.Width - 20;
            PanelOptions.Height = CheckUpperLowerCase.Bottom + 60;
            PanelOptions.BorderStyle = BorderStyle.FixedSingle;

            AvailableSearchSheetsTitle.Left = 10;
            AvailableSearchSheetsTitle.Top = PanelOptions.Height - AvailableSearchSheetsTitle.Height - 10;
            AvailableSearchSheetsHolder.Left = AvailableSearchSheetsTitle.Right + 5;
            AvailableSearchSheetsHolder.Top = AvailableSearchSheetsTitle.Top - 5;

            CloseForm.Left = this.ClientSize.Width - CloseForm.Width - 10;
            CloseForm.Top = PanelOptions.Bottom + 10;

            SearchNext.Left = CloseForm.Left - SearchNext.Width - 10;
            SearchNext.Top = CloseForm.Top;

            SearchAll.Left = SearchNext.Left - SearchAll.Width - 10;
            SearchAll.Top = CloseForm.Top;

            //this.Width = ;
            this.ClientSize = new Size(PanelOptions.Width + (this.ClientSize.Width - PanelOptions.Width), PanelOptions.Height + CloseForm.Height + 30);
            //this.Height = PanelOptions.Height + ButtonCloseForm.Height + 30;
        }

        private void SearchWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //Prevent the "ding" sound
                e.SuppressKeyPress = true;

                this.Visible = false;
            }
        }

        private void SearchWindow_MouseDown(object sender, MouseEventArgs e)
        {
            moveFormXDifference = (Cursor.Position.X - this.Left);
            moveFormYDifference = (Cursor.Position.Y - this.Top);
        }

        private void SearchWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left = Cursor.Position.X - moveFormXDifference;
                this.Top = Cursor.Position.Y - moveFormYDifference;
            }
        }

        private void SearchWindow_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (TextBoxSearchString.Text == "")
                {
                    TextBoxSearchString.Select();
                }
                else
                {
                    TextBoxSearchString.Select();
                    TextBoxSearchString.SelectAll();
                }
            }
            else if (!this.Visible)
            {
                StockManagementMenu.UpperLowerCase = StringComparison.OrdinalIgnoreCase;
            }
        }
        #endregion

        #region Control events
        private void ButtonCloseForm_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void TextBoxSearchString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Prevent the "ding" sound
                e.SuppressKeyPress = true;

                SearchFunction(DataGridViewToSearch);
            }
        }

        private void ButtonSearchNext_Click(object sender, EventArgs e)
        {
            SearchFunction(DataGridViewToSearch);
        }

        private void AvailableSearchSheetsHolder_SelectedValueChanged(object sender, EventArgs e)
        {
            foreach (DataGridView grid in AvailableGrids)
            {
                if (grid.Tag.ToString() == AvailableSearchSheetsHolder.SelectedItem.ToString())
                {
                    DataGridViewToSearch = grid;

                    CurrentGridName = AvailableSearchSheetsHolder.SelectedItem.ToString();

                    if (CurrentGridName != "Old Customers" && CurrentGridName != "<<<<Localize" && CurrentGridName != "Alte Kunden")
                    {
                        UpdateCurrentCell = true;
                    }
                    else
                    {
                        UpdateCurrentCell = false;
                    }
                    
                    break;
                }
            }
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
            this.Text = StockManagementMenu.Localisation.GetString("SearchWindowTitle");

            LabelSearchText.Text = StockManagementMenu.Localisation.GetString("SearchWindowMenu_SearchText");
            CheckUpperLowerCase.Text = StockManagementMenu.Localisation.GetString("SearchWindow_UpperLowerCase");
            AvailableSearchSheetsTitle.Text = StockManagementMenu.Localisation.GetString("SearchWindow_CurrentGrid");
            SearchAll.Text = StockManagementMenu.Localisation.GetString("SearchWindow_Button_SearchAll");
            SearchNext.Text = StockManagementMenu.Localisation.GetString("SearchWindow_Button_SearchNext");
            CloseForm.Text = StockManagementMenu.Localisation.GetString("SearchWindow_Button_Close");
        }

        public void UpdateSearchGrid()
        {
            if (AvailableSearchSheetsHolder.Items.Count > 0)
            {
                AvailableSearchSheetsHolder.Items.Clear();
            }

            foreach (DataGridView grid in AvailableGrids)
            {
                AvailableSearchSheetsHolder.Items.Add(grid.Tag);
            }

            AvailableSearchSheetsHolder.SelectedItem = CurrentGridName;
        }

        private void SearchFunction(DataGridView _passedDataGridView)
        {
            if (_passedDataGridView.Rows.Count > 0)
            {
                if (CheckUpperLowerCase.Checked)
                {
                    StockManagementMenu.UpperLowerCase = StringComparison.Ordinal;
                }
                else if (!CheckUpperLowerCase.Checked)
                {
                    StockManagementMenu.UpperLowerCase = StringComparison.OrdinalIgnoreCase;
                }

                bool bufferBool = false;
                DataGridViewCell bufferCell = null;

                StockManagementMenu.SearchString = TextBoxSearchString.Text;

                CoreFunctions.SearchFunction(_passedDataGridView, StockManagementMenu.CurrentSheetData, StockManagementMenu.SearchString, StockManagementMenu.UpperLowerCase, this, out bufferBool, out bufferCell);

                if (bufferBool)
                {
                    if (UpdateCurrentCell)
                    {
                        StockManagementMenu.HasSearchCellChanged = bufferBool;
                        StockManagementMenu.NewCurrentCell = bufferCell;
                    }

                    _passedDataGridView.CurrentCell = bufferCell;
                }
            }
            else if (_passedDataGridView.Rows.Count == 0)
            {
                CustomMessageBox.Show("Sie haben die Liste noch nicht geladen, laden Sie diese zuerst.", "Fehler beim suchen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}