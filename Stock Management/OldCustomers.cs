using StockManagementCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace StockManagement
{
    public partial class OldCustomers : Form
    {
        #region Variables
        // *************************
        // ******** Classes ********
        // *************************
        public StockManagement StockManagementMenu { get; set; } = null;
        // *************************
        // *************************
        // *************************

        // Variables for the moveable form
        private int moveFormXDifference = 0;
        private int moveFormYDifference = 0;

        private bool isFirstLoad = true;

        // Datagridview to show the unused customers
        public DataGridView DefaultSheet { get; set; } = null;
        #endregion

        public OldCustomers()
        {
            InitializeComponent();
            
            // Form Events
            this.FormClosing += new FormClosingEventHandler(OldCustomers_FormClosing);
            this.KeyDown += new KeyEventHandler(OldCustomers_KeyDown);
            this.Load += new EventHandler(OldCustomers_Load);
            this.VisibleChanged += new EventHandler(OldCustomers_VisibleChanged);
            this.MouseDown += new MouseEventHandler(OldCustomers_MouseDown);
            this.MouseMove += new MouseEventHandler(OldCustomers_MouseMove);
        }

        #region Form events
        private void OldCustomers_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;

                if (StockManagementMenu.SearchWindowMenu != null && StockManagementMenu.SearchWindowMenu.Visible)
                {
                    StockManagementMenu.AdvancedSearchWindow(StockManagementMenu.CurrentSheetGrid, true, StockManagementMenu.CurrentSheetGrid.Tag.ToString(), StockManagementMenu.SheetGridHolder);
                }               

                this.Visible = false;
                this.Owner.BringToFront();
            }
        }

        private void OldCustomers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Visible = false;
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                List<DataGridView> bufferGrid = new List<DataGridView>(StockManagementMenu.SheetGridHolder);

                bufferGrid.Add(DefaultSheet);

                StockManagementMenu.AdvancedSearchWindow(DefaultSheet, false, this.Text, bufferGrid);
            }
        }

        private void OldCustomers_Load(object sender, EventArgs e)
        {
            // Set all text regarding the current culture info
            SetLocalisation();

            //this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(222, 222, 222);
            this.KeyPreview = true;

            this.Left = ((StockManagementMenu.Left + StockManagementMenu.Width) - this.Width) - ((StockManagementMenu.Width - this.Width) / 2);
            this.Top = ((StockManagementMenu.Top + StockManagementMenu.Height) - this.Height) - ((StockManagementMenu.Height - this.Height) / 2);

            // Datagridview
            DefaultSheet = new DataGridView();

            DefaultSheet.DoubleBuffered(true);
            DefaultSheet.ReadOnly = true;
            DefaultSheet.Left = 20;
            DefaultSheet.Top = 20;
            DefaultSheet.Width = this.ClientSize.Width - 40;
            DefaultSheet.Height = this.Height - (this.Height - CloseForm.Top) - DefaultSheet.Top - (this.Height - (CloseForm.Top + CloseForm.Height));
            DefaultSheet.AllowUserToAddRows = false;
            DefaultSheet.Tag = this.Text;

            this.Controls.Add(DefaultSheet);
        }

        private void OldCustomers_VisibleChanged(object sender, EventArgs e)
        {
            if (isFirstLoad)
            {
                LoadContent();

                isFirstLoad = false;
            }
        }

        private void OldCustomers_MouseDown(object sender, MouseEventArgs e)
        {
            moveFormXDifference = (Cursor.Position.Y - this.Top);
            moveFormYDifference = (Cursor.Position.X - this.Left);
        }

        private void OldCustomers_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Cursor.Position.Y - moveFormXDifference;
                this.Left = Cursor.Position.X - moveFormYDifference;
            }
        }
        #endregion

        #region Control events
        // Control: CloseForm
        private void CloseForm_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        // Control: ReloadContent
        private void ReloadContent_Click(object sender, EventArgs e)
        {
            LoadContent();
        }

        // Control: Defaultsheet
        private void Defaultsheet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (DefaultSheet.Columns.Count - 1) && e.RowIndex >= 0)
            {
                // TODO: Localisize it

                if (MessageBox.Show("Durch löschen dieser Position, wird diese entgültig entfernt.\nMöchten Sie fortfahren?", "Position löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {                  
                    DataGridView currentControl = sender as DataGridView;

                    foreach (XElement x in StockManagementMenu.Xdoc.Root.Elements("CustomerData").Elements("Customer"))
                    {
                        // Compare the customer in the DataGridView to the one in the XML file
                        if (x.FirstAttribute.Value.ToString() == currentControl.Rows[e.RowIndex].Cells[1].Value.ToString())
                        {
                            x.Remove();
                            DefaultSheet.Rows.RemoveAt(e.RowIndex);
                            StockManagementMenu.ConfugurationFileLocking.WriteXDocument(StockManagementMenu.Xdoc);

                            MessageBox.Show("You have sucessfully deleted the position");
                            break;
                        }
                    }
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
            this.Text = Localisation.Localisation.strings.FileImportTitle;

            ReloadContent.Text = Localisation.Localisation.strings.OldCustomersMenu_Button_ReloadData;
            CloseForm.Text = Localisation.Localisation.strings.OldCustomersMenu_Button_Close;
        }

        private void LoadContent()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();

            bool customerFound = false;
            int loopCounter = 0;

            customerFound = false;

            DefaultSheet.Columns.Clear();
            DefaultSheet.Rows.Clear();
            DefaultSheet.ColumnCount = 5;
            DefaultSheet.CellContentClick += new DataGridViewCellEventHandler(Defaultsheet_CellContentClick);

            // Columnheader names
            DefaultSheet.Columns[0].HeaderText = "Name";
            DefaultSheet.Columns[1].HeaderText = "Kundennummer";
            DefaultSheet.Columns[2].HeaderText = "Bemerkung";
            DefaultSheet.Columns[3].HeaderText = "Letzte Inventur";
            DefaultSheet.Columns[4].HeaderText = "Inventurübersicht";
            buttonColumn.HeaderText = "Löschen";

            DefaultSheet.Columns.Insert(DefaultSheet.Columns.Count, buttonColumn);

            // Get the old/not used anymore customers
            foreach (XElement x in StockManagementMenu.Xdoc.Root.Elements("CustomerData").Elements("Customer"))
            {
                customerFound = false;

                try
                {
                    for (int i = 0; i < StockManagementMenu.CurrentSheetData.Rows.Count; i++)
                    {
                        // Compare the customer in the DataGridView to the one in the XML file
                        if (x.FirstAttribute.Value.ToString() == StockManagementMenu.CurrentSheetData.Rows[i][2].ToString())
                        {
                            customerFound = true;

                            break;
                        }
                    }

                    if (!customerFound)
                    {
                        DefaultSheet.Rows.Add();

                        DefaultSheet.Rows[loopCounter].Cells[0].Value = x.FirstAttribute.NextAttribute.Value.ToString();
                        DefaultSheet.Rows[loopCounter].Cells[1].Value = x.FirstAttribute.Value.ToString();
                        DefaultSheet.Rows[loopCounter].Cells[2].Value = x.Element("Comment").Value;
                        DefaultSheet.Rows[loopCounter].Cells[3].Value = x.Element("Last_stocktaking").Value;
                        DefaultSheet.Rows[loopCounter].Cells[4].Value = x.Element("Stocktaking_info").Value;
                        DefaultSheet.Rows[loopCounter].Cells[5].Value = "Löschen";

                        loopCounter++;
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("Es ist ein Fehler aufgetreten. Die Fehlermeldung lautet:\n" + err.Message, "Fehler beim erstellen der Liste", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
}
