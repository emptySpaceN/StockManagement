using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

// Export to xlsx
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using StockManagementCore;
using Updater;
using System.Threading;
using System.Globalization;
using System.Drawing.Design;
using System.Resources;

namespace StockManagement
{

    public partial class StockManagement : Form
    {
        #region Variables
        // *************************
        // ******** Classes ********
        // *************************
        public SearchWindow SearchWindowMenu { get; set; } = null;
        public OldCustomers OldCustomersMenu { get; set; } = null;
        public Settings SettingsMenu { get; set; } = null;
        public FileLocking ConfugurationFileLocking { get; set; } = null;
        public FileLoggingSystem FileLogging { get; set; }
        // *************************
        // *************************
        // *************************

        // Localisation
        public ResourceManager Localisation { get; set; } = null;

        // Integers
        private short topDifference = 0;                                                    // Integer for moving the form (top difference)
        private short leftDifference = 0;                                                   // Integer for moving the form (left difference)
        private short tabPageCount = 1;
        private int differenceCheckBasis = 0;                                               // The basis int for comparing if a filter should be set or not
        private int differenceCheckCompare = 0;                                             // The int that is compared with for setting the filter boolean
        private int filterColumnNumber = 0;                                                 // Contains the number of the last column you have clicked on (when setting a filter)
        private int currentCellColumn = 0;                                                  // Asign the current column here????????
        private int currentCellDataGridRow = 0;                                             // Asign the current row here???????????
        private int currentCellDataTableRow = 0;
        private int currentChangesPosition = 0;
        private int SheetOnecolumnCount = 0;

        // Lists
        private List<List<string>> changesValueHolder = new List<List<string>>();
        private List<string> filteredValues = new List<string>();                      //Contains all the values available to the user
        private List<int> filteredColumNumbers = new List<int>();

        // Strings
        private string applicationConfigurationFile = CoreFunctions.GetExecutingDirectoryName() + "config\\configuration.xml"; // Location of the configuration file
        private string filterColumnName = "";                                           // Contains the name of the last column you have clicked on (when setting a filter)
        private string columnFilterSyntax = "";                                         // The string with the whole rowfilter syntax
        private string cellValueBeforeEdit = "";

        // Bitmaps
        Bitmap[] saveIcon = new Bitmap[3];
        Bitmap[] reloadIcon = new Bitmap[3];

        // Booleans
        private bool isFilterSet = false;                                               // Is set to false if no filter is applied and true if at least one filter is set
        private bool hasFilterControlFocus = false;
        private bool hastableChanged = false;

        // Objects
        private TextBox sheetOneCellContent = null;                                     // Textbox control for the text changed event

        // Enums
        private enum BarStatusState
        {
            Sum = 1,
            Count = 2
        }

        private enum FileExtension
        {
            csv,
            txt,
            xlsx
        }

        private enum RightClickMenuCase
        {
            ColumnHeader,
            TabPage
        }

        private enum RightClickMenuItem
        {
            RenameColumn,
            FormatColumn,
            RenameTabPage,
            DeleteTabPage
        }

        private BarStatusState barStatusSheetOne = BarStatusState.Count;

        // ************************
        // ******** Public ********
        // ************************
        public FileSystemWatcher FileLockNotificationPublic { get; set; }

        public int CurrentSheetIndexPublic { get; set; } = 0;                                                  // Holds the index of the currently used sheet

        public StringComparison UpperLowerCase { get; set; } = StringComparison.OrdinalIgnoreCase;

        public Languages CurrentLanguage { get; set; } = Languages.English;

        public XDocument Xdoc { get; set; } = null;

        public DataGridViewCell NewCurrentCell { get; set; } = null;

        public bool IsConfigurationFileLocked { get; set; } = false;
        public bool DoesConfigurationFileExist { get; set; } = false;

        public bool HasSearchCellChanged { get; set; } = false;
        public string SearchString { get; set; } = "";

        public List<DataGridView> SheetGridHolder { get; set; } = new List<DataGridView>();
        public List<BindingSource> SheetBindingSourceHolder { get; set; } = new List<BindingSource>();
        public List<DataTable> SheetdataHolder { get; set; } = new List<DataTable>();

        public DataGridView CurrentSheetGrid { get; private set; } = new DataGridView();
        public BindingSource CurrentSheetBindingSource { get; set; } = new BindingSource();
        public DataTable CurrentSheetData { get; set; } = new DataTable();

        public enum Languages
        {
            English,
            German
        }
        #endregion

        public StockManagement()
        {
            InitializeComponent();
            ResizeRedraw = true;
            
            // Events
            this.FormClosing += new FormClosingEventHandler(EdiMenu_FormClosing);
            this.KeyDown += new KeyEventHandler(StockManagementMenu_KeyDown);
            this.Load += new EventHandler(EdiMenu_Load);
            this.Resize += new EventHandler(EdiMenu_Resize);
            this.ResizeBegin += new EventHandler(EdiMenu_ResizeBegin);
            this.ResizeEnd += new EventHandler(EdiMenu_ResizeEnd);
        }

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    base.OnPaintBackground(e);
        //    Rectangle lasttabrect = SheetContainer.GetTabRect(SheetContainer.TabPages.Count - 1);
        //    RectangleF emptyspacerect = new RectangleF(
        //            lasttabrect.X + lasttabrect.Width + SheetContainer.Left,
        //            SheetContainer.Top + lasttabrect.Y,
        //            SheetContainer.Width - (lasttabrect.X + lasttabrect.Width),
        //            lasttabrect.Height);

        //    Brush b = Brushes.BlueViolet; // the color you want
        //    e.Graphics.FillRectangle(b, emptyspacerect);
        //}

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
        //        return cp;
        //    }
        //}

        #region Form events
        private void EdiMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsConfigurationFileLocked)
            {
                if (hastableChanged)
                {
                    DialogResult result = MessageBox.Show("Sie haben Änderungen vorgenommen, diese aber noch nicht gespeichert. Möchten Sie jetzt speichern?", "Änderungen speichern?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        // Save the changes
                        CoreFunctions.SaveChanges(CurrentSheetData, ConfugurationFileLocking, Xdoc, SheetOnecolumnCount);
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                // Empty the CurrentUser element in the configuration file when exiting the program
                Xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("GeneralSettings").Element("CurrentUser").Value = "";
                ConfugurationFileLocking.WriteXDocument(Xdoc);
            }

            if (SearchWindowMenu != null)
            {
                SearchWindowMenu.Close();
            }

            if (OldCustomersMenu != null)
            {
                OldCustomersMenu.Close();
            }

            if (SettingsMenu != null)
            {
                SettingsMenu.Close();
            }
        }

        private void StockManagementMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                if (OldCustomersMenu != null && OldCustomersMenu.Visible)
                {
                    List<DataGridView> bufferGrid = new List<DataGridView>(SheetGridHolder);

                    bufferGrid.Add(OldCustomersMenu.DefaultSheet);

                    AdvancedSearchWindow(CurrentSheetGrid, true, TabSheetHolder.TabPages[TabSheetHolder.SelectedIndex].Text, bufferGrid);
                }
                else
                {
                    AdvancedSearchWindow(CurrentSheetGrid, true, TabSheetHolder.TabPages[TabSheetHolder.SelectedIndex].Text, SheetGridHolder);
                }
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                if (hastableChanged)
                {
                    // Saves the changes of the datagridview table
                    CoreFunctions.SaveChanges(CurrentSheetData, ConfugurationFileLocking, Xdoc, SheetOnecolumnCount);

                    // Reset the save button image and boolean
                    TitleBarSaveChanges.Image = saveIcon[2];
                    hastableChanged = false;
                }
            }
        }

        private void EdiMenu_Load(object sender, EventArgs e)
        {

            // Set all text regarding the current culture info
            SetLocalisation();

            TabSheetHolder.TabPages[0].Text = Localisation.GetString("StockManagementMenu_TabPage_SheetTitle") + tabPageCount;

            // First settings, like location and other stuff
            LocationAndPrimarySettings();

            if (!IsConfigurationFileLocked)
            {
                // Create and fill the sheet
                SheetOneLoad();
            }

            // Final formating of the controls (tab, label etc.) and the main sheet
            FinalSettings();
        }

        private void EdiMenu_Resize(object sender, EventArgs e)
        {
            //ContainingPanel.Visible = false;

            // Screen resolution variables
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeigth = Screen.PrimaryScreen.Bounds.Height;

            // Form settings
            //this.Width = (screenWidth / 4) * 3;
            //this.Height = (screenHeigth / 4) * 3;
            //this.Left = (screenWidth - this.Width) / 2;
            //this.Top = (screenHeigth - this.Height) / 2;

            // Forces the control to repaint, so the border gets redrawn
            //ContainingPanel.Refresh();

            //Panel settings
            PanelOptions.Width = this.ClientSize.Width;
            //PanelOptions.Left = 0;

            StatusBar.Left = 0;
            StatusBar.Top = this.ClientSize.Height - StatusBar.Height;
            StatusBar.Width = this.ClientSize.Width;
            //statusBar.Height = "";

            // Setting the location of the input textbox
            TextInputBox.Top = PanelOptions.Top + PanelOptions.Height + 10;
            TextInputBox.Left = 10;

            // Setting the datagridviews properties, position, events etc...

            //Setting the datagridviews properties, position, events etc...
            TabSheetHolder.Width = this.ClientSize.Width - 20;      // **** instead of defaultDataGrid ****
            TabSheetHolder.Height = this.Height - PanelOptions.Top - PanelOptions.Height - TextInputBox.Height - 120;   // **** instead of defaultDataGrid ****
            TabSheetHolder.Left = 10;                                           // **** instead of defaultDataGrid ****
            TabSheetHolder.Top = TextInputBox.Top + TextInputBox.Height + 10;   // **** instead of defaultDataGrid ****

            // Tag page settings
            SelectPreviousSheet.Left = TabSheetHolder.Left + 5;                             // **** instead of defaultDataGrid ****
            SelectPreviousSheet.Top = TabSheetHolder.Top + TabSheetHolder.Height + 5;       // **** instead of defaultDataGrid ****

            SelectNextSheet.Left = SelectPreviousSheet.Left + SelectPreviousSheet.Width + 10;
            SelectNextSheet.Top = SelectPreviousSheet.Top;

            AddNewSheet.Left = SelectNextSheet.Left + SelectNextSheet.Width + 10;
            AddNewSheet.Top = SelectPreviousSheet.Top;

            // ContainingPanel.Visible = true;
        }

        private void EdiMenu_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendLayout();
        }

        private void EdiMenu_ResizeEnd(object sender, EventArgs e)
        {
            this.ResumeLayout();
        }
        #endregion

        #region Control events
        // Control: DefaultDataGrid
        private void DefaultDataGrid_KeyDown(object sender, KeyEventArgs e)
        {

            if (DefaultDataGrid.EditingControl != null)
            {
                MessageBox.Show("asd");
            }

            if (!IsConfigurationFileLocked)
            {
                // Get the row and column of the current selected cell
                currentCellColumn = CurrentSheetGrid.CurrentCell.ColumnIndex;
                currentCellDataGridRow = CurrentSheetGrid.CurrentCell.RowIndex;
                currentCellDataTableRow = int.Parse(CurrentSheetGrid.Rows[currentCellDataGridRow].Cells[CurrentSheetGrid.Columns.Count - 1].Value.ToString());

                if (e.KeyCode == Keys.Delete)
                {
                    // Empty the textbox above the sheet
                    TextInputBox.Text = "";

                    // If no column-filter is set
                    if (!isFilterSet)
                    {
                        if (DefaultDataGrid.SelectedCells.Count == 1 && (currentCellColumn > 6 && currentCellColumn < (DefaultDataGrid.Columns.Count - 1)))
                        {
                            if (CurrentSheetGrid.CurrentCell.Value.ToString() != "")
                            {
                                changesValueHolder.Add(new List<String>());

                                changesValueHolder[changesValueHolder.Count - 1].Add("1");
                                changesValueHolder[changesValueHolder.Count - 1].Add(CurrentSheetGrid.CurrentCell.Value.ToString());     // Undo value
                                changesValueHolder[changesValueHolder.Count - 1].Add("");                                                   // Redo value
                                changesValueHolder[changesValueHolder.Count - 1].Add(currentCellColumn.ToString());
                                changesValueHolder[changesValueHolder.Count - 1].Add(currentCellDataTableRow.ToString());

                                currentChangesPosition++;

                                CurrentSheetData.Rows[currentCellDataTableRow][currentCellColumn] = "";
                                CoreFunctions.UpdateCellContent(CurrentSheetGrid, CurrentSheetData, currentCellColumn, currentCellDataGridRow);

                                if (!hastableChanged)
                                {
                                    TitleBarSaveChanges.Image = saveIcon[0];
                                    TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                                    hastableChanged = true;
                                }
                            }
                        }
                        else if (DefaultDataGrid.SelectedCells.Count > 1)
                        {
                            int valueCounter = 0;

                            changesValueHolder.Add(new List<String>());
                            changesValueHolder[changesValueHolder.Count - 1].Add("1");

                            foreach (DataGridViewCell cell in DefaultDataGrid.SelectedCells)
                            {
                                if (cell.ColumnIndex > 6 && cell.ColumnIndex < (DefaultDataGrid.Columns.Count - 1) && cell.Value.ToString() != "")
                                {
                                    // Update the current DataTable row index
                                    currentCellDataTableRow = int.Parse(CurrentSheetGrid.Rows[cell.RowIndex].Cells[CurrentSheetGrid.Columns.Count - 1].Value.ToString());

                                    valueCounter++;

                                    changesValueHolder[changesValueHolder.Count - 1][0] = valueCounter.ToString();
                                    changesValueHolder[changesValueHolder.Count - 1].Add(cell.Value.ToString());     // Undo value
                                    changesValueHolder[changesValueHolder.Count - 1].Add("");                                                   // Redo value
                                    changesValueHolder[changesValueHolder.Count - 1].Add(cell.ColumnIndex.ToString());
                                    changesValueHolder[changesValueHolder.Count - 1].Add(currentCellDataTableRow.ToString());

                                    CurrentSheetData.Rows[currentCellDataTableRow][cell.ColumnIndex] = "";

                                    CoreFunctions.UpdateCellContent(CurrentSheetGrid, CurrentSheetData, cell.ColumnIndex, cell.RowIndex);

                                    if (!hastableChanged)
                                    {
                                        TitleBarSaveChanges.Image = saveIcon[0];
                                        TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                                        hastableChanged = true;
                                    }
                                }
                            }

                            if (valueCounter == 0)
                            {
                                if (changesValueHolder.Count > 0) { changesValueHolder.RemoveAt(changesValueHolder.Count - 1); }
                            }
                            else
                            {
                                currentChangesPosition++;
                            }
                        }
                    }
                    // If at least one column-filter is set
                    else if (isFilterSet)
                    {
                        if (DefaultDataGrid.SelectedCells.Count == 1 && (currentCellColumn > 6 && currentCellColumn < (DefaultDataGrid.Columns.Count - 1)))
                        {
                            if (CurrentSheetGrid.CurrentCell.Value.ToString() != "")
                            {
                                changesValueHolder.Add(new List<String>());

                                changesValueHolder[changesValueHolder.Count - 1].Add("1");
                                changesValueHolder[changesValueHolder.Count - 1].Add(CurrentSheetGrid.CurrentCell.Value.ToString()); // Undo value
                                changesValueHolder[changesValueHolder.Count - 1].Add("");                                               // Redo value
                                changesValueHolder[changesValueHolder.Count - 1].Add(currentCellColumn.ToString());
                                changesValueHolder[changesValueHolder.Count - 1].Add(CurrentSheetGrid.Rows[currentCellDataGridRow].Cells[CurrentSheetGrid.Columns.Count - 1].Value.ToString());

                                currentChangesPosition++;

                                CurrentSheetData.Rows[int.Parse(DefaultDataGrid.Rows[currentCellDataGridRow].Cells[DefaultDataGrid.Columns.Count - 1].Value.ToString())][currentCellColumn] = "";
                                CoreFunctions.UpdateCellContent(CurrentSheetGrid, CurrentSheetData, currentCellColumn, currentCellDataGridRow);

                                if (!hastableChanged)
                                {
                                    TitleBarSaveChanges.Image = saveIcon[0];
                                    TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                                    hastableChanged = true;
                                }
                            }
                        }
                        else if (DefaultDataGrid.SelectedCells.Count > 1)
                        {
                            foreach (DataGridViewCell cell in DefaultDataGrid.SelectedCells)
                            {
                                if (cell.ColumnIndex > 6 && cell.ColumnIndex < (DefaultDataGrid.Columns.Count - 1) && cell.Value.ToString() != "")
                                {
                                    CoreFunctions.DebugWrite("RowIndex before: " + cell.RowIndex);
                                    CurrentSheetData.Rows[int.Parse(DefaultDataGrid.Rows[cell.RowIndex].Cells[DefaultDataGrid.Columns.Count - 1].Value.ToString())][cell.ColumnIndex] = "";
                                    CoreFunctions.DebugWrite("RowIndex after1: " + cell.RowIndex);
                                    CoreFunctions.UpdateCellContent(CurrentSheetGrid, CurrentSheetData, cell.ColumnIndex, cell.RowIndex);
                                    CoreFunctions.DebugWrite("RowIndex after2: " + cell.RowIndex);

                                    if (!hastableChanged)
                                    {
                                        TitleBarSaveChanges.Image = saveIcon[0];
                                        TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                                        hastableChanged = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (e.Control && e.KeyCode == Keys.C)
                {
                    //System.Windows.Forms.Clipboard.SetData(DataFormats.Text, customerOverview.CurrentCell.Value.ToString());
                    if (DefaultDataGrid.CurrentCell.Value.ToString() != "")
                    {
                        Clipboard.SetText(DefaultDataGrid.CurrentCell.Value.ToString(), TextDataFormat.Text);
                    }
                    else if (DefaultDataGrid.CurrentCell.Value.ToString() == "")
                    {
                        Clipboard.Clear();
                    }
                }
                else if (e.Control && e.KeyCode == Keys.X)
                {
                    //System.Windows.Forms.Clipboard.SetData(DataFormats.Text, customerOverview.CurrentCell.Value.ToString());
                    Clipboard.SetText(DefaultDataGrid.CurrentCell.Value.ToString(), TextDataFormat.Text);

                    switch (DefaultDataGrid.CurrentCell.ColumnIndex)
                    {
                        case 7:
                        {
                            TextInputBox.Text = "";

                            TitleBarSaveChanges.Image = saveIcon[0];
                            TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                            hastableChanged = true;
                        }
                        break;
                        case 8:
                        {
                            TextInputBox.Text = "";

                            TitleBarSaveChanges.Image = saveIcon[0];
                            TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                            hastableChanged = true;
                        }
                        break;
                        case 9:
                        {
                            TextInputBox.Text = "";

                            TitleBarSaveChanges.Image = saveIcon[0];
                            TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                            hastableChanged = true;
                        }
                        break;
                    }

                    //sheetOneBindingSource.ResetBindings(false);
                }
                else if (e.Control && e.KeyCode == Keys.V)
                {
                    switch (DefaultDataGrid.CurrentCell.ColumnIndex)
                    {
                        case 7:
                        {
                            TitleBarSaveChanges.Image = saveIcon[0];
                            TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                            hastableChanged = true;
                            //TextInputBox.Text = "";
                        }
                        break;
                        case 8:
                        {
                            TitleBarSaveChanges.Image = saveIcon[0];
                            TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                            hastableChanged = true;
                            //TextInputBox.Text = "";
                        }
                        break;
                        case 9:
                        {
                            TitleBarSaveChanges.Image = saveIcon[0];
                            TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                            hastableChanged = true;
                            //TextInputBox.Text = System.Windows.Forms.Clipboard.GetText();
                        }
                        break;
                    }


                    //sheetOneBindingSource.ResetBindings(false);
                }
                else if (e.Control && e.KeyCode == Keys.Z)
                {
                    UndoChanges();
                    CoreFunctions.DebugWrite("reversed!");
                }
                else if (e.Control && e.KeyCode == Keys.Y)
                {
                    RedoChanges();
                    CoreFunctions.DebugWrite("forward");
                }
            }
        }

        private void DefaultDataGrid_RightClickMenu(object sender, MouseEventArgs e)
        {
            bool cellInRange = false;
            bool multipleColumns = false;

            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip rightClickMenu = new System.Windows.Forms.ContextMenuStrip();
                int position_xy_mouse_row = CurrentSheetGrid.HitTest(e.X, e.Y).RowIndex;
                int position_xy_mouse_column = CurrentSheetGrid.HitTest(e.X, e.Y).ColumnIndex;

                if (position_xy_mouse_column < 0)
                {
                    CurrentSheetGrid.ClearSelection();
                    CurrentSheetGrid.Rows[position_xy_mouse_row].Selected = true;
                }
                else if (position_xy_mouse_row >= 0)
                {
                    foreach (DataGridViewCell selectedCells in DefaultDataGrid.SelectedCells)
                    {
                        if (selectedCells == DefaultDataGrid.Rows[position_xy_mouse_row].Cells[position_xy_mouse_column])
                        {
                            cellInRange = true;
                        }

                        if (DefaultDataGrid.Rows[position_xy_mouse_row].Cells[position_xy_mouse_column].ColumnIndex != selectedCells.ColumnIndex)
                        {
                            multipleColumns = true;
                        }
                    }

                    if (!cellInRange)
                    {
                        CurrentSheetGrid.CurrentCell = CurrentSheetGrid.Rows[position_xy_mouse_row].Cells[position_xy_mouse_column];
                    }
                }

                if (position_xy_mouse_row >= 0)
                {
                    if (position_xy_mouse_column < 0)
                    {
                        //rightClickMenu.Items.Add("Löschen").Name = "delete";
                    }

                    if (!multipleColumns && DefaultDataGrid.SelectedCells.Count > 1 && position_xy_mouse_column >= 0)
                    {
                        rightClickMenu.Items.Add("Werte zusammenführen").Name = "combine_values";
                    }

                    rightClickMenu.Items.Add("Kopieren").Name = "copy";
                }

                rightClickMenu.ItemClicked += RightClickMenu_ItemClicked;
                rightClickMenu.Show(CurrentSheetGrid, new Point(e.X, e.Y));
            }
        }

        private void DefaultDataGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Remove the triangle
            DataGridView grid = sender as DataGridView;

            string rowIndex = (int.Parse(grid.Rows[e.RowIndex].Cells[grid.Columns.Count - 1].Value.ToString()) + 1).ToString();

            StringFormat centerFormat = new StringFormat()
            {
                // Right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);

            e.Graphics.FillRectangle(new SolidBrush(grid.RowHeadersDefaultCellStyle.BackColo‌​r), e.RowBounds.Left + 1, e.RowBounds.Top + 1, grid.Rows[e.RowIndex].HeaderCell.Size.Width - 2, grid.Rows[e.RowIndex].HeaderCell.Size.Height - 2);

            e.Graphics.DrawString(rowIndex, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void DefaultDataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CoreFunctions.UpdateCellContent(DefaultDataGrid, CurrentSheetData, e.ColumnIndex, e.RowIndex);

            if (cellValueBeforeEdit != DefaultDataGrid.CurrentCell.Value.ToString())
            {
                changesValueHolder.Add(new List<String>());

                changesValueHolder[changesValueHolder.Count - 1].Add("1");
                changesValueHolder[changesValueHolder.Count - 1].Add(cellValueBeforeEdit);                              // Undo value
                changesValueHolder[changesValueHolder.Count - 1].Add(DefaultDataGrid.CurrentCell.Value.ToString());     // Redo value
                changesValueHolder[changesValueHolder.Count - 1].Add(e.ColumnIndex.ToString());
                changesValueHolder[changesValueHolder.Count - 1].Add(CurrentSheetGrid.Rows[e.RowIndex].Cells[CurrentSheetGrid.Columns.Count - 1].Value.ToString());

                currentChangesPosition++;

                if (!hastableChanged)
                {
                    TitleBarSaveChanges.Image = saveIcon[0];
                    TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                    hastableChanged = true;
                }

                if (!UndoChange.Enabled) { UndoChange.Enabled = true; }
            }
        }

        private void DefaultDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (sheetOneCellContent == null)
            {
                sheetOneCellContent = (TextBox)e.Control;
            }

            sheetOneCellContent.TextChanged -= SheetOneCellContent_TextChanged;
            sheetOneCellContent.TextChanged += new EventHandler(SheetOneCellContent_TextChanged);

            //e.Control.
        }

        private void DefaultDataGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (isFilterSet)
            {
                if (filteredColumNumbers.Contains(e.ColumnIndex) && e.RowIndex == -1)
                {
                    e.PaintBackground(e.ClipBounds, false);

                    System.Drawing.Font drawFont = new System.Drawing.Font("Microsoft Sans Serif", 8);
                    SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);
                    SizeF stringSize = new SizeF(e.Graphics.MeasureString(e.Value.ToString(), drawFont));
                    StringFormat drawFormat = new StringFormat();

                    float stringX = e.CellBounds.Location.X + 1;
                    float stringY = e.CellBounds.Location.Y + ((e.CellBounds.Height - stringSize.Height) / 2);

                    float imageX = e.CellBounds.Location.X + (e.CellBounds.Width - Properties.Resources.Filter_16x16.Width);
                    float imageY = e.CellBounds.Location.Y + ((e.CellBounds.Height - Properties.Resources.Filter_16x16.Height) / 2);

                    e.Graphics.DrawString(e.Value.ToString(), drawFont, drawBrush, stringX, stringY, drawFormat);
                    e.Graphics.DrawImage(Properties.Resources.Filter_16x16, imageX, imageY);
                    e.Handled = true;

                    drawFont.Dispose();
                    drawBrush.Dispose();
                    drawFormat.Dispose();
                }
            }
        }

        private void DefaultDataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (PanelFilterWindow.Visible) { return; }

                DataGridView currentControl = (DataGridView)sender;
                Point currentControlPoint = currentControl.FindForm().PointToClient(currentControl.Parent.PointToScreen(currentControl.Location));

                FilterInputBox.Text = "";
                filterColumnNumber = e.ColumnIndex;
                filterColumnName = CurrentSheetGrid.Columns[e.ColumnIndex].Name;

                // ---- Common settings for the filter window (positions etc.) ----
                PanelFilterWindow.Left = currentControlPoint.X + currentControl.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left;
                PanelFilterWindow.Top = currentControlPoint.Y + currentControl.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Height;

                //PanelFilterWindow.Width = 170;
                //PanelFilterWindow.Height = 350;

                SortColumnAscending.Left = 5;
                SortColumnAscending.Top = 5;
                SortColumnAscending.Width = PanelFilterWindow.Width - 10;
                SortColumnAscending.Height = 25;
                SortColumnAscending.FlatAppearance.BorderColor = PanelFilterWindow.BackColor;

                SortColumnDescending.Left = 5;
                SortColumnDescending.Top = SortColumnAscending.Top + SortColumnAscending.Height;
                SortColumnDescending.Width = PanelFilterWindow.Width - 10;
                SortColumnDescending.Height = 25;
                SortColumnDescending.FlatAppearance.BorderColor = PanelFilterWindow.BackColor;

                FilterWindowBorderOne.Left = 5;
                FilterWindowBorderOne.Top = SortColumnDescending.Top + SortColumnDescending.Height + 5;
                FilterWindowBorderOne.Width = PanelFilterWindow.Width - 10;

                FilterInputBox.Left = 5;
                FilterInputBox.Top = FilterWindowBorderOne.Top + FilterWindowBorderOne.Height + 5;
                FilterInputBox.Width = PanelFilterWindow.Width - 10;

                FilterCheckedListbox.Left = 5;
                FilterCheckedListbox.Top = FilterInputBox.Top + FilterInputBox.Height + 10;
                FilterCheckedListbox.Width = PanelFilterWindow.Width - 10;

                AllFilterCheck.Left = 5;
                AllFilterCheck.Top = FilterCheckedListbox.Top + FilterCheckedListbox.Height + 5;

                FilterOk.Left = (PanelFilterWindow.Width - (FilterOk.Width + FilterCancel.Width + 5)) / 2;
                FilterOk.Top = AllFilterCheck.Top + AllFilterCheck.Height + 5;

                FilterCancel.Left = FilterOk.Left + FilterOk.Width + 5;
                FilterCancel.Top = AllFilterCheck.Top + AllFilterCheck.Height + 5;

                PanelFilterWindow.Height = FilterOk.Top + FilterOk.Height + 5;




                // ----

                // Sort the whole datagridview after the last, not visible, column
                //currentDataGridView.Sort(currentDataGridView.Columns[currentDataGridView.Columns.Count - 1], System.ComponentModel.ListSortDirection.Ascending);

                // TODO: Is it nescessary to have that function call here?
                //UpdateSheetOne();

                // Clears both string lists and the CheckListBox
                filteredValues.Clear();
                FilterCheckedListbox.Items.Clear();

                if (!isFilterSet)
                {
                    // Get all the values of the current column from the DataGridView
                    for (int i = 0; i < CurrentSheetGrid.Rows.Count; i++)
                    {
                        // Adds a value only one time to the string list, not multiple times
                        if (filteredValues.Contains(CurrentSheetGrid.Rows[i].Cells[e.ColumnIndex].Value.ToString()) == false)
                        {
                            filteredValues.Add(CurrentSheetGrid.Rows[i].Cells[e.ColumnIndex].Value.ToString());
                        }
                    }
                    // Populate all the gathered values in the filterCheckedListbox
                    FilterCheckedListbox.Items.AddRange(filteredValues.OrderBy(i => i).ToArray());

                    // Check only those values which are filtered on the datagridview right now and leave the rest unchecked
                    for (int i = 0; i < FilterCheckedListbox.Items.Count; i++)
                    {
                        FilterCheckedListbox.SetItemChecked(i, true);
                    }

                    AllFilterCheck.CheckState = CheckState.Checked;
                }
                else if (isFilterSet)
                {
                    // Get all the values of the current column from the datatable
                    for (int i = 0; i < CurrentSheetGrid.Rows.Count; i++)
                    {
                        // Adds a value only one time, not multiple times
                        if (filteredValues.Contains(CurrentSheetGrid.Rows[i].Cells[e.ColumnIndex].Value.ToString()) == false)
                        {
                            filteredValues.Add(CurrentSheetGrid.Rows[i].Cells[e.ColumnIndex].Value.ToString());
                        }
                    }

                    // Populate all the gathered values in the filterCheckedListbox
                    FilterCheckedListbox.Items.AddRange(filteredValues.OrderBy(i => i).ToArray());

                    // Check only those values which are filtered on the datagridview right now and leave the rest unchecked
                    for (int i = 0; i < FilterCheckedListbox.Items.Count; i++)
                    {
                        FilterCheckedListbox.SetItemChecked(i, true);
                    }

                    // Setting the "all filter checkbox" to the current state of filtered items
                    if (FilterCheckedListbox.CheckedItems.Count == FilterCheckedListbox.Items.Count)
                    {
                        AllFilterCheck.CheckState = CheckState.Checked;
                    }
                    else if (FilterCheckedListbox.CheckedItems.Count < 1)
                    {
                        AllFilterCheck.CheckState = CheckState.Unchecked;
                    }
                    else if (FilterCheckedListbox.Items.Count > 1 && FilterCheckedListbox.CheckedItems.Count != FilterCheckedListbox.Items.Count)
                    {
                        AllFilterCheck.CheckState = CheckState.Indeterminate;
                    }
                }

                differenceCheckBasis = FilterCheckedListbox.CheckedItems.Count;

                // Custom settings for the FilterPanel
                if (PanelFilterWindow.Visible != true)
                {
                    PanelFilterWindow.Visible = true;
                }

                FilterInputBox.Select();

                PanelFilterWindow.BringToFront();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (PanelFilterWindow.Visible) { PanelFilterWindow.Visible = false; }

                RightClickMenu(RightClickMenuCase.ColumnHeader, CurrentSheetGrid, e.ColumnIndex);
            }
        }

        private void DefaultDataGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            cellValueBeforeEdit = DefaultDataGrid.CurrentCell.Value.ToString();
        }

        private void DefaultDataGrid_Enter(object sender, EventArgs e)
        {
            if (PanelFilterWindow.Visible)
            {
                PanelFilterWindow.Visible = false;
            }
        }

        private void DefaultDataGrid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            // TODO: Clear
            //Debug.Print(e.RowIndex.ToString());
            if (e.RowIndex == -1)
            {
                if (Cursor.Current != Cursors.Hand) { Cursor.Current = Cursors.Hand; };
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void DefaultDataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            CoreFunctions.DebugWrite("selection change begin");
            DataGridView currentControl = sender as DataGridView;

            //if (currentDataGridView == DefaultDataGrid)
            //{
            CoreFunctions.DebugWrite(currentControl.CurrentCell.ToString());
            TextInputBox.Text = currentControl.CurrentCell.Value == null ? "" : currentControl.CurrentCell.Value.ToString();
            //}

            // Disable text input by disabling the TextInputBox for specific columns

            if (currentControl.CurrentCell.ColumnIndex < 7)
            {
                TextInputBox.ReadOnly = true;
            }
            else if (currentControl.CurrentCell.ColumnIndex > 6)
            {
                TextInputBox.ReadOnly = false;
            }

            SetBarText(barStatusSheetOne, currentControl, ref StatusBarSelectionMenu);
        }

        private void DefaultDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView currentControl = sender as DataGridView;

            if (HasSearchCellChanged)
            {
                currentControl.SelectionChanged -= DefaultDataGrid_SelectionChanged;

                currentControl.CurrentCell = NewCurrentCell;
                TextInputBox.Text = currentControl.CurrentCell.Value == null ? "" : currentControl.CurrentCell.Value.ToString();

                currentControl.SelectionChanged += DefaultDataGrid_SelectionChanged;

                HasSearchCellChanged = false;
            }
            else
            {
                TextInputBox.Text = currentControl.CurrentCell.Value == null ? "" : currentControl.CurrentCell.Value.ToString();
            }

            // Disable text input by disabling the TextInputBox for specific columns
            if (currentControl.CurrentCell.ColumnIndex < SheetOnecolumnCount)
            {
                TextInputBox.ReadOnly = true;
            }
            else if (currentControl.CurrentCell.ColumnIndex >= SheetOnecolumnCount)
            {
                TextInputBox.ReadOnly = false;
            }

            SetBarText(barStatusSheetOne, currentControl, ref StatusBarSelectionMenu);

        }







        private void SheetOneCellContent_TextChanged(object sender, EventArgs e)
        {
            string enteredText = ((TextBox)sender).Text;

            TextInputBox.Text = enteredText;
        }




        private void RightClickMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "delete")
            {

            }
            else if (e.ClickedItem.Name == "copy")
            {
                // TODO: Clear
                //CoreFunctions.DebugWrite(DefaultDataGrid.CurrentCell.Value);
                if (DefaultDataGrid.SelectedCells.Count == 1)
                {
                    Clipboard.SetText(DefaultDataGrid.CurrentCell.Value.ToString(), TextDataFormat.Text);
                }
                //Clipboard.SetText(customerOverview.CurrentCell.Value.ToString(), TextDataFormat.Text);
            }
            else if (e.ClickedItem.Name == "combine_values")
            {
                InputBox(-1, -1);
            }
        }

        //private void DefaultDataGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    DataGridView currentControl = sender as DataGridView;
        //    //if (currentDataGridView == DefaultDataGrid)
        //    //{
        //        TextInputBox.Text = currentControl.CurrentCell.Value == null ? "" : currentControl.CurrentCell.Value.ToString();
        //    //}
        //    MessageBox.Show("cellenter");
        //    // Disable text input by disabling the TextInputBox for specific columns
        //    if (e.ColumnIndex < 7)
        //    {
        //        TextInputBox.ReadOnly = true;
        //    }
        //    else if (e.ColumnIndex > 6)
        //    {
        //        TextInputBox.ReadOnly = false;
        //    }
        //}

        // Control: FileLockNotification
        public void FileLockNotification_Changed(object sender, FileSystemEventArgs e)
        {
            if (IsConfigurationFileLocked)
            {
                Exception error = null;

                // Try to lock the configuration file
                Thread.Sleep(3000);

                if (IsConfigurationFileLocked)
                {
                    ConfugurationFileLocking = new FileLocking();
                }

                if (ConfugurationFileLocking.OpenAndLock(applicationConfigurationFile, FileAccess.ReadWrite, FileShare.Read, ref error))
                {
                    // Set the current user of the program and save it to the configuration file
                    Xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("GeneralSettings").Element("CurrentUser").Value = Environment.UserName;
                    ConfugurationFileLocking.WriteXDocument(Xdoc);

                    CustomMessageBox.Show("Das Programm ist nun wieder verfügbar, die Liste wird automatisch geladen.");
                    IsConfigurationFileLocked = false;

                    FileLockNotificationPublic.EnableRaisingEvents = false;

                    // Create and fill the sheet
                    SheetOneLoad();
                }
                else
                {
                    CustomMessageBox.Show("TestFailure = " + error.Message);
                }
            }
        }









        private void PanelFilterWindow_Leave(object sender, EventArgs e)
        {
            // Evaluate if at least one control has focus
            CheckFilterWindowFocus();

            if (!hasFilterControlFocus)
            {
                PanelFilterWindow.Visible = false;
            }
        }

        private void FilterInputBox_Leave(object sender, EventArgs e)
        {
            // Evaluate if at least one control has focus
            CheckFilterWindowFocus();

            if (!hasFilterControlFocus)
            {
                PanelFilterWindow.Visible = false;
            }
        }

        private void filterCheckedListbox_Leave(object sender, EventArgs e)
        {
            // Evaluate if at least one control has focus
            CheckFilterWindowFocus();

            if (!hasFilterControlFocus)
            {
                PanelFilterWindow.Visible = false;
            }
        }

        private void AllFilterCheck_Leave(object sender, EventArgs e)
        {
            // Evaluate if at least one control has focus
            CheckFilterWindowFocus();

            if (!hasFilterControlFocus)
            {
                PanelFilterWindow.Visible = false;
            }
        }

        private void filterOKBtn_Leave(object sender, EventArgs e)
        {
            // Evaluate if at least one control has focus
            CheckFilterWindowFocus();

            if (!hasFilterControlFocus)
            {
                PanelFilterWindow.Visible = false;
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            SearchFunction(CurrentSheetGrid);
        }

        private void findTxtbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Prevent the "Ding"
                e.SuppressKeyPress = true;

                SearchFunction(CurrentSheetGrid);
            }
        }

        private void TextInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DefaultDataGrid.Select();
            }
        }

        private void TextInputBox_TextChanged(object sender, EventArgs e)
        {
            if (TextInputBox.Focused)
            {
                DefaultDataGrid.CurrentCell.Value = TextInputBox.Text;
            }
        }

        private void filterOKBtn_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void filterCancelBtn_Click(object sender, EventArgs e)
        {
            PanelFilterWindow.Visible = false;
        }

        private void filterCheckedListbox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (FilterCheckedListbox.CheckedItems.Count == FilterCheckedListbox.Items.Count)
            {
                AllFilterCheck.CheckState = CheckState.Checked;
            }
            else if (FilterCheckedListbox.CheckedItems.Count < 1)
            {
                AllFilterCheck.CheckState = CheckState.Unchecked;
            }
            else if (FilterCheckedListbox.Items.Count > 1 & FilterCheckedListbox.CheckedItems.Count != FilterCheckedListbox.Items.Count)
            {
                AllFilterCheck.CheckState = CheckState.Indeterminate;
            }
        }

        private void AllFilterCheck_Click(object sender, EventArgs e)
        {
            if (AllFilterCheck.CheckState == CheckState.Checked)
            {
                for (int i = 0; i < FilterCheckedListbox.Items.Count; i++)
                {
                    FilterCheckedListbox.SetItemChecked(i, true);
                }
            }
            else if (AllFilterCheck.CheckState == CheckState.Unchecked)
            {
                for (int i = 0; i < FilterCheckedListbox.Items.Count; i++)
                {
                    FilterCheckedListbox.SetItemChecked(i, false);
                }
            }
        }

        private void ContainingPanel_Click(object sender, EventArgs e)
        {
            if (PanelFilterWindow.Visible == true)
            {
                PanelFilterWindow.Visible = false;
            }
        }

        private void ContainingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            topDifference = (short)(Cursor.Position.Y - this.Top);
            leftDifference = (short)(Cursor.Position.X - this.Left);
        }

        private void ContainingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Top = Cursor.Position.Y - topDifference;
                this.Left = Cursor.Position.X - leftDifference;
            }
        }

        private void PanelOptions_Click(object sender, EventArgs e)
        {
            PanelFilterWindow.Visible = false;
        }

        private void FilterInputBox_TextChanged(object sender, EventArgs e)
        {

            if (FilterInputBox.Text != "")
            {
                if ((FilterInputBox.Text.Length < 2 ? "" : FilterInputBox.Text.Substring(0, 2)) == "<>")
                {
                    FilterCheckedListbox.Items.Clear();

                    // Copy the filter values to the buffer list
                    List<string> bufferValues = new List<string>();                      // Contains all the values available to the user

                    // Get all the values of the current column from the DataGridView
                    for (int i = 0; i < filteredValues.Count; i++)
                    {
                        // Adds a value only one time to the string list, not multiple times
                        if (!filteredValues[i].Contains(FilterInputBox.Text.Substring(2, FilterInputBox.Text.Length - 2), StringComparison.OrdinalIgnoreCase))
                        {
                            bufferValues.Add(filteredValues[i]);
                        }
                    }
                    // Populate all the gathered values in the filterCheckedListbox
                    FilterCheckedListbox.Items.AddRange(bufferValues.OrderBy(i => i).ToArray());

                    // Check only those values which are filtered on the datagridview right now and leave the rest unchecked
                    for (int i = 0; i < FilterCheckedListbox.Items.Count; i++)
                    {
                        FilterCheckedListbox.SetItemChecked(i, true);
                    }
                }

                else
                {
                    FilterCheckedListbox.Items.Clear();

                    // Copy the filter values to the buffer list
                    List<string> bufferValues = new List<string>();                      // Contains all the values available to the user

                    // Get all the values of the current column from the DataGridView
                    for (int i = 0; i < filteredValues.Count; i++)
                    {
                        // Adds a value only one time to the string list, not multiple times
                        if (filteredValues[i].Contains(FilterInputBox.Text, StringComparison.OrdinalIgnoreCase))
                        {
                            bufferValues.Add(filteredValues[i]);
                        }
                    }
                    // Populate all the gathered values in the filterCheckedListbox
                    FilterCheckedListbox.Items.AddRange(bufferValues.OrderBy(i => i).ToArray());

                    // Check only those values which are filtered on the datagridview right now and leave the rest unchecked
                    for (int i = 0; i < FilterCheckedListbox.Items.Count; i++)
                    {
                        FilterCheckedListbox.SetItemChecked(i, true);
                    }
                }

            }
            else if (FilterInputBox.Text == "" || FilterInputBox.Text == "=")
            {
                FilterCheckedListbox.Items.Clear();
                FilterCheckedListbox.Items.AddRange(filteredValues.OrderBy(i => i).ToArray());

                for (int i = 0; i < FilterCheckedListbox.Items.Count; i++)
                {
                    FilterCheckedListbox.SetItemChecked(i, true);
                }
            }
        }

        private void FilterInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Prevent the "Ding" sound
                e.SuppressKeyPress = true;

                if (FilterInputBox.Text != "" && FilterCheckedListbox.Items.Count > 0)
                {
                    ApplyFilter();
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                PanelFilterWindow.Visible = false;
            }
        }



        private void filterCheckedListbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                PanelFilterWindow.Visible = false;
            }
        }

        private void AllFilterCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                PanelFilterWindow.Visible = false;
            }
        }

        private void filterOKBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                PanelFilterWindow.Visible = false;
            }
        }

        private void filterCancelBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                PanelFilterWindow.Visible = false;
            }
        }

        // Control: TitleBarImportData
        private void TitleBarImportData_Click(object sender, EventArgs e)
        {
            FileImport File_Import = new FileImport();

            File_Import.StockManagementMenu = this;
            File_Import.SheetIndex = TabSheetHolder.SelectedIndex;

            File_Import.ShowDialog();

            //ImportDataFromFile();
        }

        private void TitleBarImportData_MouseEnter(object sender, EventArgs e)
        {
            TitleBarImportData.Image = reloadIcon[1];
            TitleBarImportData.BackColor = System.Drawing.Color.FromArgb(222, 222, 222);
        }

        private void TitleBarImportData_MouseLeave(object sender, EventArgs e)
        {
            TitleBarImportData.Image = reloadIcon[0];
            TitleBarImportData.BackColor = System.Drawing.Color.FromArgb(243, 243, 243);
        }







        private void TitleBarSaveChanges_Click(object sender, EventArgs e)
        {
            if (hastableChanged)
            {
                hastableChanged = false;

                // Reset the save button image and boolean
                TitleBarSaveChanges.FlatAppearance.BorderColor = PanelOptions.BackColor;
                TitleBarSaveChanges.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
                TitleBarSaveChanges.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
                TitleBarSaveChanges.Image = saveIcon[2];
                TitleBarSaveChanges.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
                TitleBarSaveChanges.Refresh();

                // Saves the changes of the datagridview table
                CoreFunctions.SaveChanges(CurrentSheetData, ConfugurationFileLocking, Xdoc, SheetOnecolumnCount);
            }
        }

        private void TitleBarSaveChanges_MouseEnter(object sender, EventArgs e)
        {
            if (hastableChanged)
            {
                TitleBarSaveChanges.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(202, 230, 253);
                TitleBarSaveChanges.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(226, 240, 252);
                TitleBarSaveChanges.Image = saveIcon[1];
            }
        }

        private void TitleBarSaveChanges_MouseLeave(object sender, EventArgs e)
        {
            if (hastableChanged)
            {
                TitleBarSaveChanges.FlatAppearance.BorderColor = PanelOptions.BackColor;
                TitleBarSaveChanges.Image = saveIcon[0];
            }
        }

        private void TitleBarSaveChanges_MouseDown(object sender, MouseEventArgs e)
        {
            if (hastableChanged)
            {
                TitleBarSaveChanges.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(152, 208, 254);
                TitleBarSaveChanges.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(200, 228, 251);
            }
        }

        private void TitleBarResetFilters_Click(object sender, EventArgs e)
        {
            if (isFilterSet)
            {
                TitleBarDeleteFilters.FlatAppearance.BorderColor = PanelOptions.BackColor;
                TitleBarDeleteFilters.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
                TitleBarDeleteFilters.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
                TitleBarDeleteFilters.Image = Properties.Resources.DeleteFilterDisabled_16x16;
                TitleBarDeleteFilters.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
                TitleBarDeleteFilters.Refresh();

                ResetFilter();
            }
        }

        private void TitleBarResetFilters_MouseEnter(object sender, EventArgs e)
        {
            if (isFilterSet)
            {
                TitleBarDeleteFilters.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(202, 230, 253);
                TitleBarDeleteFilters.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(226, 240, 252);
            }
        }

        private void TitleBarResetFilters_MouseLeave(object sender, EventArgs e)
        {
            if (isFilterSet)
            {
                TitleBarDeleteFilters.FlatAppearance.BorderColor = PanelOptions.BackColor;
            }
        }

        private void TitleBarResetFilters_MouseDown(object sender, MouseEventArgs e)
        {
            if (isFilterSet)
            {
                TitleBarDeleteFilters.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(152, 208, 254);
                TitleBarDeleteFilters.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(200, 228, 251);
            }
        }

        private void TitleBarAbout_Click(object sender, EventArgs e)
        {
            AboutWindow About_Window = new AboutWindow();

            About_Window.ShowInTaskbar = false;
            About_Window.StartPosition = FormStartPosition.CenterParent;
            About_Window.StockManagementMenu = this;
            About_Window.MinimizeBox = false;
            About_Window.MaximizeBox = false;
            About_Window.ShowDialog();
        }

        private void TitleBarAbout_MouseEnter(object sender, EventArgs e)
        {
            TitleBarAbout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(202, 230, 253);
            TitleBarAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(226, 240, 252);
        }

        private void TitleBarAbout_MouseLeave(object sender, EventArgs e)
        {
            TitleBarAbout.FlatAppearance.BorderColor = PanelOptions.BackColor;
        }

        private void TitleBarAbout_MouseDown(object sender, MouseEventArgs e)
        {
            TitleBarAbout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(152, 208, 254);
            TitleBarAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(200, 228, 251);
        }

        private void TitleBarUnusedValues_Click(object sender, EventArgs e)
        {
            ShowOldCustomers();
        }

        private void TitleBarUnusedValues_MouseEnter(object sender, EventArgs e)
        {
            TitleBarUnusedValues.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(202, 230, 253);
            TitleBarUnusedValues.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(226, 240, 252);
        }

        private void TitleBarUnusedValues_MouseLeave(object sender, EventArgs e)
        {
            TitleBarUnusedValues.FlatAppearance.BorderColor = PanelOptions.BackColor;
        }

        private void TitleBarUnusedValues_MouseDown(object sender, MouseEventArgs e)
        {
            TitleBarUnusedValues.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(152, 208, 254);
            TitleBarUnusedValues.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(200, 228, 251);
        }

        private void TitleBarUnusedValues_MouseUp(object sender, MouseEventArgs e)
        {
            TitleBarUnusedValues.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(202, 230, 253);
        }

        private void SortColumnAscending_Click(object sender, EventArgs e)
        {
            CurrentSheetGrid.Sort(CurrentSheetGrid.Columns[filterColumnNumber], System.ComponentModel.ListSortDirection.Ascending);
        }

        private void SortColumnDescending_Click(object sender, EventArgs e)
        {
            CurrentSheetGrid.Sort(CurrentSheetGrid.Columns[filterColumnNumber], System.ComponentModel.ListSortDirection.Descending);
        }

        private void TitleBarUpdate_Click(object sender, EventArgs e)
        {
            DirectUpdate.InitFileCollection();

            //try
            //{
                if (DirectUpdate.GetUpdateFiles(DirectUpdate.GetClient, DirectUpdate.GetOnlineFileList, DirectUpdate.GetApplicationFileList, DirectUpdate.GetTemporaryFileList, true))
                {
                    DialogResult askForUpdate = MessageBox.Show("Es ist eine neuere Version verfügbar\nUm das Programm zu aktualisieren müssen Sie es jetzt beenden.\nProgramm beenden und das Update herunterladen?", "Programmaktualisierung", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (askForUpdate == DialogResult.Yes)
                    {
                        if (hastableChanged)
                        {
                            DialogResult saveChangesResult = MessageBox.Show("Sie haben Änderungen vorgenommen, diese aber noch nicht gespeichert. Möchten Sie jetzt speichern?", "Änderungen speichern?", MessageBoxButtons.YesNoCancel);
                            if (saveChangesResult == DialogResult.Yes)
                            {
                                // Save the changes
                                CoreFunctions.SaveChanges(CurrentSheetData, ConfugurationFileLocking, Xdoc, SheetOnecolumnCount);
                            }
                            else if (saveChangesResult == DialogResult.Cancel)
                            {
                                // Cancel the update function
                                return;
                            }
                        }

                        Process.Start("Updater.exe", "UpdateCheckFromMainApplication");
                        this.Close();
                        //Environment.Exit(0);
                    }
                    else if (askForUpdate == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    CustomMessageBox.Show("Die neueste Version ist bereits installiert.", "Programmaktualisierung", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            //}
            //catch (Exception exmsg)
            //{
            //    CustomMessageBox.Show(exmsg.Message, "Unerwarteter Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            //}
        }

        private void TitleBarUpdate_MouseEnter(object sender, EventArgs e)
        {
            TitleBarUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(202, 230, 253);
            TitleBarUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(226, 240, 252);
        }

        private void TitleBarUpdate_MouseLeave(object sender, EventArgs e)
        {
            TitleBarUpdate.FlatAppearance.BorderColor = PanelOptions.BackColor;
        }

        private void TitleBarUpdate_MouseDown(object sender, MouseEventArgs e)
        {
            TitleBarUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(152, 208, 254);
            TitleBarUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(200, 228, 251);
            TitleBarUpdate.Refresh();
        }

        private void AddNewSheet_Click(object sender, EventArgs e)
        {
            if (TabSheetHolder.TabPages.Count < 10)
            {
                DataGridView NewSheetDataGridView = new DataGridView();
                Label NewSheetAddedMessage = new Label() { Name = "AddLabel" };              

                tabPageCount++;             // Increment the sheet counter for continuously sheet counting
                TabSheetHolder.TabPages.Add(Localisation.GetString("StockManagementMenu_TabPage_SheetTitle") + tabPageCount);
                TabSheetHolder.TabPages[TabSheetHolder.TabPages.Count - 1].Controls.Add(NewSheetDataGridView);
                TabSheetHolder.TabPages[TabSheetHolder.TabPages.Count - 1].Controls.Add(NewSheetAddedMessage);

                NewSheetDataGridView.Dock = DockStyle.Fill;
                NewSheetDataGridView.ReadOnly = true;
                NewSheetDataGridView.Visible = false;
                NewSheetDataGridView.RowCount = 2;
                NewSheetDataGridView.ColumnCount = 2;

                // Click events & cursors
                TabSheetHolder.TabPages[TabSheetHolder.TabPages.Count - 1].Tag = "EventYes";
                TabSheetHolder.TabPages[TabSheetHolder.TabPages.Count - 1].MouseClick += new MouseEventHandler(FillSheet_MouseClick);
                NewSheetAddedMessage.MouseClick += new MouseEventHandler(FillSheet_MouseClick);
                TabSheetHolder.Cursor = Cursors.Hand;
                NewSheetAddedMessage.Cursor = Cursors.Hand;

                // Format the label
                NewSheetAddedMessage.Text = Localisation.GetString("StockManagementMenu_NewSheetAdded");
                NewSheetAddedMessage.Font = new System.Drawing.Font(NewSheetAddedMessage.Font.FontFamily, 25);
                NewSheetAddedMessage.AutoSize = true;
                NewSheetAddedMessage.Left = (TabSheetHolder.Width - NewSheetAddedMessage.Width) / 2;
                NewSheetAddedMessage.Top = (TabSheetHolder.Height - NewSheetAddedMessage.Height) / 2;

                TabSheetHolder.SelectedTab = TabSheetHolder.TabPages[TabSheetHolder.TabPages.Count - 1];

                //
                NewSheetDataGridView.Tag = TabSheetHolder.TabPages[TabSheetHolder.TabCount - 1].Text;

                // Add the current sheets, binding sources and data tables to the lists
                SheetGridHolder.Add(NewSheetDataGridView);
                SheetBindingSourceHolder.Add(new BindingSource());
                SheetdataHolder.Add(new DataTable());

                // Set current sheets, binding sources and data tables
                CurrentSheetGrid = NewSheetDataGridView;
                CurrentSheetBindingSource = SheetBindingSourceHolder[SheetBindingSourceHolder.Count - 1];
                CurrentSheetData = SheetdataHolder[SheetdataHolder.Count - 1];

                if (TabSheetHolder.TabPages.Count == 10)
                {
                    AddNewSheet.Enabled = false;
                }
            }
            else
            {
                // TODO: Obsolete because the button is deactivated 
                CustomMessageBox.Show("Sie haben bereits 10 Tabellenblätter erstellt und somit das Maximum erreicht.", "Tabelle hinzufügen", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SelectNextSheet_Click(object sender, EventArgs e)
        {
            if (TabSheetHolder.TabPages.Count > 1 && TabSheetHolder.SelectedIndex < (TabSheetHolder.TabPages.Count - 1))
            {
                TabSheetHolder.SelectedIndex += 1;
            }
        }

        private void SelectPreviousSheet_Click(object sender, EventArgs e)
        {
            if (TabSheetHolder.TabPages.Count > 1 && TabSheetHolder.SelectedIndex > 0)
            {
                TabSheetHolder.SelectedIndex -= 1;
            }
        }

        private void TitleBarAdvancedSearch_Click(object sender, EventArgs e)
        {
            if (OldCustomersMenu != null && OldCustomersMenu.Visible)
            {
                List<DataGridView> bufferGrid = new List<DataGridView>(SheetGridHolder);

                bufferGrid.Add(OldCustomersMenu.DefaultSheet);

                AdvancedSearchWindow(CurrentSheetGrid, true, TabSheetHolder.TabPages[TabSheetHolder.SelectedIndex].Text, SheetGridHolder);
            }
            else
            {
                AdvancedSearchWindow(CurrentSheetGrid, true, TabSheetHolder.TabPages[TabSheetHolder.SelectedIndex].Text, SheetGridHolder);
            }
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            if (SettingsMenu == null)
            {
                SettingsMenu = new Settings();

                SettingsMenu.StartPosition = FormStartPosition.CenterParent;
                SettingsMenu.MaximizeBox = false;

                SettingsMenu.ConfugurationFileLocking = ConfugurationFileLocking;
                SettingsMenu.ConfigurationFileExists = DoesConfigurationFileExist;
                SettingsMenu.Localisation = this.Localisation;
                
                SettingsMenu.StockManagementMenu = this;
                SettingsMenu.SeachWindowMenu = SearchWindowMenu;
                SettingsMenu.Owner = this;

                // Events
                SettingsMenu.FormClosed += new FormClosedEventHandler(Settings_Menu_FormClosed);

                SettingsMenu.ShowDialog();
            }
            else
            {
                // TODO: Create a protocol to log the error                
            }
        }

        private void Settings_Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            SettingsMenu = null;
        }

        private void selectionBarText_MouseEnter(object sender, EventArgs e)
        {
            StatusBarSelectionMenu.ForeColor = System.Drawing.Color.FromArgb(0, 151, 251);
        }

        private void selectionBarText_MouseLeave(object sender, EventArgs e)
        {
            StatusBarSelectionMenu.ForeColor = System.Drawing.Color.White;
        }

        private void selectionBarText_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip rightClickMenu = new System.Windows.Forms.ContextMenuStrip();

                rightClickMenu.Items.Add(Localisation.GetString("StockManagementMenu_Button_Sum")).Name = "sum";
                rightClickMenu.Items.Add(Localisation.GetString("StockManagementMenu_Button_Count")).Name = "count";

                rightClickMenu.ItemClicked += new ToolStripItemClickedEventHandler((_sender, _e) => RightClickMenu_ItemClicked(_sender, _e, CurrentSheetGrid, ref barStatusSheetOne, ref StatusBarSelectionMenu));

                rightClickMenu.Show(StatusBarSelectionMenu, new Point(e.X, e.Y));
            }
        }

        private void RightClickMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e, DataGridView _passedDataGridview, ref BarStatusState _passedBarState, ref Label _passedBarText)
        {
            switch (e.ClickedItem.Name)
            {
                case "sum":
                {
                    _passedBarState = BarStatusState.Sum;
                    SetBarText(_passedBarState, _passedDataGridview, ref _passedBarText);
                }
                break;
                case "count":
                {
                    _passedBarState = BarStatusState.Count;
                    SetBarText(_passedBarState, _passedDataGridview, ref _passedBarText);
                }
                break;
                default:
                    break;
            }
        }

        private void selectionBarTextOne_MouseEnter(object sender, EventArgs e)
        {
            StatusBarRecordsFound.ForeColor = System.Drawing.Color.FromArgb(0, 151, 251);
        }

        private void selectionBarTextOne_MouseLeave(object sender, EventArgs e)
        {
            StatusBarRecordsFound.ForeColor = System.Drawing.Color.White;
        }

        private void TitleBarExportData_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog exportToCSV = new SaveFileDialog())
            {
                exportToCSV.Filter = "CSV (Trennzeichen getrennt) (*.csv)|*.csv|Microsoft Excel 2007-2016 XML (*.xlsx)|*.xlsx";
                exportToCSV.FileName = "Tabelle 1";

                try
                {
                    if (exportToCSV.ShowDialog() == DialogResult.OK)
                    {
                        switch (exportToCSV.FilterIndex)
                        {
                            case 1: // Export to .csv
                            {
                                StringBuilder csv = new StringBuilder();

                                string newLine;

                                for (int i = 0; i < CurrentSheetGrid.Rows.Count; i++)
                                {
                                    newLine = "";

                                    if (i == 0)
                                    {
                                        for (int j = 0; j < CurrentSheetGrid.Columns.Count; j++)
                                        {
                                            if (j < (CurrentSheetGrid.Columns.Count - 2))
                                            {
                                                newLine += CurrentSheetGrid.Columns[j].HeaderText + ";";
                                            }
                                            else if (j == (CurrentSheetGrid.Columns.Count - 2))
                                            {
                                                newLine += CurrentSheetGrid.Columns[j].HeaderText;
                                            }
                                        }
                                        csv.AppendLine(newLine);
                                        newLine = "";
                                    }

                                    for (int k = 0; k < CurrentSheetGrid.Columns.Count; k++)
                                    {
                                        if (k < (CurrentSheetGrid.Columns.Count - 2))
                                        {
                                            newLine += (CurrentSheetGrid.Rows[i].Cells[k].Value == null ? "" : CurrentSheetGrid.Rows[i].Cells[k].Value.ToString()) + ";";
                                        }
                                        else if (k == (CurrentSheetGrid.Columns.Count - 2))
                                        {
                                            newLine += (CurrentSheetGrid.Rows[i].Cells[k].Value == null ? "" : CurrentSheetGrid.Rows[i].Cells[k].Value.ToString());
                                        }
                                    }
                                    csv.AppendLine(newLine);
                                }

                                File.WriteAllText(exportToCSV.FileName, csv.ToString());
                            }
                            break;
                            case 2:     // Export to .xlsx
                            {
                                WorkbookPart wBookPart = null;

                                using (SpreadsheetDocument spreadsheetDoc = SpreadsheetDocument.Create(exportToCSV.FileName, SpreadsheetDocumentType.Workbook))
                                {
                                    wBookPart = spreadsheetDoc.AddWorkbookPart();
                                    wBookPart.Workbook = new Workbook();

                                    uint sheetId = 1;

                                    spreadsheetDoc.WorkbookPart.Workbook.Sheets = new Sheets();

                                    Sheets sheets = spreadsheetDoc.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                                    WorksheetPart wSheetPart = wBookPart.AddNewPart<WorksheetPart>();
                                    Sheet sheet = new Sheet() { Id = spreadsheetDoc.WorkbookPart.GetIdOfPart(wSheetPart), SheetId = sheetId, Name = "Übersicht" };

                                    sheets.Append(sheet);

                                    SheetData sheetData = new SheetData();
                                    wSheetPart.Worksheet = new Worksheet(sheetData);

                                    Row headerRow = new Row();
                                    foreach (DataGridViewColumn column in CurrentSheetGrid.Columns)
                                    {
                                        if (column.Index != CurrentSheetGrid.Columns.Count - 1)
                                        {
                                            Cell cell = new Cell();
                                            cell.DataType = CellValues.String;
                                            cell.CellValue = new CellValue(column.HeaderText);
                                            headerRow.AppendChild(cell);
                                        }
                                    }
                                    sheetData.AppendChild(headerRow);

                                    foreach (DataGridViewRow dr in CurrentSheetGrid.Rows)
                                    {
                                        Row row = new Row();
                                        foreach (DataGridViewColumn column in CurrentSheetGrid.Columns)
                                        {
                                            if (column.Index != CurrentSheetGrid.Columns.Count - 1)
                                            {
                                                int n;
                                                Cell cell = new Cell();
                                                cell.DataType = (int.TryParse(dr.Cells[column.Index].Value.ToString(), out n) ? CellValues.Number : CellValues.String);
                                                cell.CellValue = new CellValue(dr.Cells[column.Index].Value.ToString());
                                                row.AppendChild(cell);
                                            }
                                        }
                                        sheetData.AppendChild(row);
                                    }
                                    sheetId++;
                                }
                            }
                            break;
                            default:
                                return;
                        }
                    }
                }
                catch (Exception err)
                {
                    switch (err.HResult)
                    {
                        case -2147024864:       // File already in use
                        {
                            MessageBox.Show("Die Tabelle kann nicht abgespeichert werden, da die Zieldatei bereits geöffnet ist.\nSchließen Sie die Datei und versuchen Sie es erneut.", "Liste exportieren", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                        default:
                        {
                            MessageBox.Show("Beim exportieren der Liste ist ein Fehler aufgetreten.\nDie Fehlermeldung lautet wie folgt:\n\n" + err.Message, "Liste exportieren", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", CoreFunctions.GetExecutingDirectoryName());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // TODO: enable and disable the buttons

            UndoChanges();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // TODO: enable and disable the buttons
            RedoChanges();
        }



        private void FillSheet_MouseClick(object sender, MouseEventArgs e)
        {
            if (TabSheetHolder.TabPages[TabSheetHolder.SelectedIndex].Tag.ToString() == "EventYes")
            {
                FileImport File_Import = new FileImport();

                File_Import.StockManagementMenu = this;
                File_Import.SheetIndex = TabSheetHolder.SelectedIndex;

                File_Import.ShowDialog();

                //ImportDataFromFile();
            }
        }

        private void TabSheetHolder_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TabControl currentControl = (TabControl)sender;
                Rectangle TabRect;
                Rectangle MouseRect = new Rectangle(e.X, e.Y, 1, 1);

                for (int i = 0; i < TabSheetHolder.TabCount; i++)
                {
                    TabRect = TabSheetHolder.GetTabRect(i);

                    if (TabRect.IntersectsWith(MouseRect))
                    {
                        CurrentSheetIndexPublic = i;
                        TabSheetHolder.SelectedIndex = CurrentSheetIndexPublic;
                        break;
                    }
                }

                // TODO: Is this still needed?
                //if (currentTabIndex != currentSheetIndex)
                //{
                //    currentControl.SelectedIndex = currentSheetIndex;
                //}

                RightClickMenu(RightClickMenuCase.TabPage, currentControl, _passedTabPageIndex: CurrentSheetIndexPublic);
            }
        }

        private void TabSheetHolder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            string value = tabControl.TabPages[tabControl.SelectedIndex].Text;

            if (CoreFunctions.InputBox("Rename sheet", "Enter a new sheet name:", ref value) == DialogResult.OK)
            {
                tabControl.TabPages[tabControl.SelectedIndex].Text = value;

                if (tabControl.SelectedIndex == 0)
                {
                    SheetGridHolder[tabControl.SelectedIndex].Tag = value;
                    DefaultDataGrid.Tag = value;
                }
                else
                {
                    SheetGridHolder[tabControl.SelectedIndex].Tag = value;
                }

                if (SearchWindowMenu != null && SearchWindowMenu.Visible)
                {
                    foreach (DataGridView grid in SearchWindowMenu.AvailableGrids)
                    {
                        if (CurrentSheetGrid == grid)
                        {
                            if (SearchWindowMenu.DataGridViewToSearch == grid)
                            {
                                SearchWindowMenu.DataGridViewToSearch.Tag = value;
                                SearchWindowMenu.CurrentGridName = value;
                            }

                            grid.Tag = value;
                            SearchWindowMenu.UpdateSearchGrid();
                            break;
                        }
                    }
                }
            }
        }

        private void TabSheetHolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl currentControl = (TabControl)sender;

            if (currentControl.TabPages.Count < 10 && !AddNewSheet.Enabled)
            {
                AddNewSheet.Enabled = true;
            }

            if (TabSheetHolder.SelectedIndex == 0)
            {
                SelectPreviousSheet.Enabled = false;
                SelectNextSheet.Enabled = true;
            }
            else if (TabSheetHolder.SelectedIndex + 1 == TabSheetHolder.TabPages.Count)
            {
                SelectPreviousSheet.Enabled = true;
                SelectNextSheet.Enabled = false;
            }
            else if (TabSheetHolder.SelectedIndex > 0 && TabSheetHolder.SelectedIndex < TabSheetHolder.TabPages.Count - 1)
            {
                SelectPreviousSheet.Enabled = true;
                SelectNextSheet.Enabled = true;
            }

            string tagContent = TabSheetHolder.TabPages[TabSheetHolder.SelectedIndex].Tag == null ? "" : TabSheetHolder.TabPages[TabSheetHolder.SelectedIndex].Tag.ToString();
            if (tagContent != "EventYes" && tagContent != "DefaultSheet")
            {
                TitleBarImportData.Enabled = true;
            }
        }


        #endregion

        // Sheet one datasource
        //public class ListSource
        //{

        //    public string Kunde { set; get; }
        //    public string Kundenmatch { set; get; }
        //    public string Kundennummer { set; get; }
        //    public string Salesreport { set; get; }
        //    public string Order { set; get; }
        //    public string Kundenart { set; get; }
        //    public string Nachbestückung { set; get; }
        //    public string Bemerkung { set; get; }
        //    public string Letzte_Inventur { set; get; }
        //    public string Inventurübersicht { set; get; }
        //    public string Sorting { set; get; }

        //    public ListSource(string customerName, string customerMatch, string custumberNumber, string salesreportFlag, string orderFlag, string customerType, string restockingType, string descriptionText, string lastStocktakingDate, string inventoryOverview, string sortingNumber)
        //    {
        //        Kunde = customerName;
        //        Kundenmatch = customerMatch;
        //        Kundennummer = custumberNumber;
        //        Salesreport = salesreportFlag;
        //        Order = orderFlag;
        //        Kundenart = customerType;
        //        Nachbestückung = restockingType;
        //        Bemerkung = descriptionText;
        //        Letzte_Inventur = lastStocktakingDate;
        //        Inventurübersicht = inventoryOverview;
        //        Sorting = sortingNumber;
        //    }

        //}

        #region Functions
        // ******** All about the default sheet ********
        private void SheetOneLoad()
        {
            DataTable sheetOneDatatable = new DataTable();
            sheetOneDatatable.ExtendedProperties.Add("Tag", 1);

            // Read the file and display it line by line.
            XElement getPathFile = Xdoc.Element("ConfigFile").Element("ApplicationSettings").Element("DataReferences");

            // Get the column count from the first line of the file
            SheetOnecolumnCount = (File.ReadLines(getPathFile.Element("CustomerFile").Value).First().Split('\t').Length);

            // TODO: Currently rowCount isn't needed - probably clean it
            //int rowCount = File.ReadLines(getPathFile.Element("CustomerFile").Value).Count();

            using (StreamReader sr = File.OpenText(getPathFile.Element("CustomerFile").Value))
            {
                int lineCounter = 0;
                int tabulatorCounter = 0;
                int columnCount = 0;
                string s = String.Empty;
                string[] wholeLine = { String.Empty };
                DataRow dr = null;

                DefaultDataGrid.AllowUserToAddRows = false;     // Disable the default row
                DefaultDataGrid.DoubleBuffered(true);           // Enable the boublebuffering for fast scrolling

                while ((s = sr.ReadLine()) != null)
                {
                    wholeLine = s.Split('\t');
                    tabulatorCounter = 0;
                    columnCount = s.Count(x => x == '\t');
                    dr = null;

                    lineCounter++;

                    if (string.IsNullOrWhiteSpace(s) == false)
                    {
                        foreach (string tabulator in wholeLine)
                        {
                            tabulatorCounter++;

                            // The tabulatorCounter represents the columns and is always 1 time bigger because the tabulator is inbetween the columns
                            if (lineCounter == 1 && tabulatorCounter > columnCount)
                            {
                                // TODO: Localize it
                                sheetOneDatatable.Columns.Add(tabulator);
                                sheetOneDatatable.Columns.Add(Localisation.GetString("AppText_Comment"));
                                sheetOneDatatable.Columns.Add(Localisation.GetString("AppText_LastStocktaking"));
                                sheetOneDatatable.Columns.Add(Localisation.GetString("AppText_StocktakingOverview"));
                                sheetOneDatatable.Columns.Add("Sorting", typeof(int));
                            }
                            else if (lineCounter == 1)
                            {
                                // TODO: Check if it is possible to add multiple columns with the same name
                                sheetOneDatatable.Columns.Add(tabulator);
                            }
                            else if (lineCounter != 1)
                            {
                                if (dr == null) { dr = sheetOneDatatable.NewRow(); }

                                dr[tabulatorCounter - 1] = tabulator;
                                dr[sheetOneDatatable.Columns.Count - 1] = (lineCounter - 2);
                            }
                        }

                        if (dr != null) { sheetOneDatatable.Rows.Add(dr); }

                        //If the file is already filled with customers then this code loads the contained text into the cells otherwise the code will skip this
                        if (lineCounter > 1 && Xdoc.Root.Elements("CustomerData").Elements("Customer").Count() > 0)
                        {
                            foreach (XElement x in Xdoc.Root.Elements("CustomerData").Elements("Customer"))
                            {
                                //Compare the customer in the DataGridView and in the XML file
                                if (x.FirstAttribute.Value == sheetOneDatatable.Rows[lineCounter - 2][2].ToString())
                                {
                                    string commentXML = x.Element("Comment").Value;
                                    string lastStocktakingXML = x.Element("Last_stocktaking").Value;
                                    string stocktakingInfoXML = x.Element("Stocktaking_info").Value;

                                    sheetOneDatatable.Rows[lineCounter - 2][wholeLine.Count()] = commentXML;
                                    sheetOneDatatable.Rows[lineCounter - 2][wholeLine.Count() + 1] = lastStocktakingXML;
                                    sheetOneDatatable.Rows[lineCounter - 2][wholeLine.Count() + 2] = stocktakingInfoXML;

                                    continue;
                                }
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            // Create a BindingSource the datatable to it
            BindingSource sheetOneBindingsSource = new BindingSource
            {
                DataSource = sheetOneDatatable,
            };

            // ***********************************************************************
            // ******** Needed if you're on another thread as the main thread ********
            // ***********************************************************************
            MethodInvoker AsignNewDataSource = delegate
            {
                // Asign the BindingSource to the DataGridView
                DefaultDataGrid.DataSource = sheetOneBindingsSource;
            };

            if (DefaultDataGrid.InvokeRequired)
            {
                Invoke(AsignNewDataSource);
            }
            else
            {
                // Asign the BindingSource to the DataGridView
                DefaultDataGrid.DataSource = sheetOneBindingsSource;
            }
            // ***********************************************************************
            // ***********************************************************************
            // ***********************************************************************

            // 
            TabSheetHolder.TabPages[TabSheetHolder.SelectedIndex].Tag = "DefaultSheet";
            DefaultDataGrid.Tag = TabSheetHolder.TabPages[TabSheetHolder.SelectedIndex].Text;

            // Add the current sheets, binding sources and data tables to the lists
            SheetGridHolder.Add(DefaultDataGrid);                                    // 1
            SheetBindingSourceHolder.Add(sheetOneBindingsSource);                // 2
            SheetdataHolder.Add(sheetOneDatatable);                              // 3

            // Set current sheets, binding sources and data tables
            CurrentSheetGrid = DefaultDataGrid;                                      // 1
            CurrentSheetBindingSource = sheetOneBindingsSource;                  // 2
            CurrentSheetData = sheetOneDatatable;                                // 3

            // Additional settings for sheet one
            SheetOneSettings(SheetOnecolumnCount);
        }

        private void FinalSettings()
        {
            // Loops through every cell and enters the stock inventory text
            UpdateSheetOne();

            // Set every columns sortmode
            foreach (DataGridViewColumn Columns in DefaultDataGrid.Columns)
            {
                Columns.SortMode = DataGridViewColumnSortMode.Programmatic;
            }

            //PanelFilterWindow.LostFocus += new EventHandler(filterPanel_LostFocus);

            //ContainingPanel.Visible = true;
            StatusBarRecordsFound.Text = Localisation.GetString("StockManagementMenu_Button_RecordsFound") + DefaultDataGrid.Rows.Count;

            // TODO: use this variable
            CurrentSheetIndexPublic = 1;
        }

        private void UpdateSheetOne()
        {
            foreach (DataGridViewRow Rows in DefaultDataGrid.Rows)
            {
                CoreFunctions.UpdateCellContent(CurrentSheetGrid, CurrentSheetData, DefaultDataGrid.Columns.Count - 3, Rows.Index);
            }
        }
        // ******** All about the default sheet ********

        public void SetLocalisation()
        {
            // Set the localisation
            this.Text = Localisation.GetString("MainMenu_Button_StockManagementButton");

            TitleBarSaveChanges.Text = Localisation.GetString("StockManagementMenu_Button_SaveChanges");
            TitleBarDeleteFilters.Text = Localisation.GetString("StockManagementMenu_Button_DeleteFilters");
            TitleBarAdvancedSearch.Text = Localisation.GetString("StockManagementMenu_Button_AdvancedSearch");
            TitleBarBasicSearch.Text = Localisation.GetString("StockManagementMenu_Button_BasicSearch");
            TitleBarUnusedValues.Text = Localisation.GetString("StockManagementMenu_Button_UnusedValues");
            TitleBarExportData.Text = Localisation.GetString("StockManagementMenu_Button_ExportData");
            TitleBarImportData.Text = Localisation.GetString("StockManagementMenu_Button_ImportData");
            TitleBarUpdate.Text = Localisation.GetString("StockManagementMenu_Button_Update");
            TitleBarAbout.Text = Localisation.GetString("StockManagementMenu_Button_About");
            TitleBarSettings.Text = Localisation.GetString("StockManagementMenu_Button_Settings");
            TitleBarRootFolder.Text = Localisation.GetString("StockManagementMenu_Button_RootFolder");

            StatusBarRecordsFound.Text = Localisation.GetString("StockManagementMenu_Button_RecordsFound") + CurrentSheetGrid.Rows.Count;
            StatusBarSelectionMenu.Text = Localisation.GetString("StockManagementMenu_Button_SumSelection");

            SortColumnAscending.Text = Localisation.GetString("StockManagementMenu_Button_SortAscending");
            SortColumnDescending.Text = Localisation.GetString("StockManagementMenu_Button_SortDescending");
            AllFilterCheck.Text = Localisation.GetString("StockManagementMenu_CheckBox_SelectAll");
            FilterOk.Text = Localisation.GetString("StockManagementMenu_Button_Ok");
            FilterCancel.Text = Localisation.GetString("StockManagementMenu_Button_Cancel");
        }

        private void SearchFunction(DataGridView _passedDataGridView)
        {
            if (_passedDataGridView.Rows.Count > 0)
            {
                // Get the current search string
                SearchString = findTxtbx.Text;

                CoreFunctions.SearchFunction(_passedDataGridView, CurrentSheetData, SearchString, UpperLowerCase, null, out bool bufferBool, out DataGridViewCell bufferCell);

                if (bufferBool)
                {
                    HasSearchCellChanged = bufferBool;
                    NewCurrentCell = bufferCell;
                    _passedDataGridView.CurrentCell = NewCurrentCell;
                }
            }
            else if (_passedDataGridView.Rows.Count == 0)
            {
                CustomMessageBox.Show("Sie haben die Liste noch nicht geladen, laden Sie diese zuerst.", "Fehler beim suchen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowOldCustomers()
        {
            if (OldCustomersMenu == null)
            {
                OldCustomersMenu = new OldCustomers
                {
                    // TODO: Still needed?
                    //ConfugurationFile_LockingPublic = ConfugurationFile_Locking,
                    StockManagementMenu = this,
                    Owner = this
                };

                OldCustomersMenu.Show();
            }
            else if (OldCustomersMenu != null && !OldCustomersMenu.Visible)
            {
                OldCustomersMenu.Visible = true;
            }
            else if (OldCustomersMenu.Visible && !OldCustomersMenu.Focused)
            {
                OldCustomersMenu.Select();
            }

            if (SearchWindowMenu != null && SearchWindowMenu.Visible)
            {
                List<DataGridView> bufferGrid = new List<DataGridView>(SheetGridHolder);

                bufferGrid.Add(OldCustomersMenu.DefaultSheet);

                AdvancedSearchWindow(SearchWindowMenu.DataGridViewToSearch, false, SearchWindowMenu.CurrentGridName, bufferGrid);
            }
        }

        public void AdvancedSearchWindow(DataGridView _passedGridToSearch, bool _updateCurrentCell, string _currentSheetName, List<DataGridView> _passedGrids)
        {
            if (SearchWindowMenu == null)
            {
                SearchWindowMenu = new SearchWindow
                {
                    StockManagementMenu = this,
                    DataGridViewToSearch = _passedGridToSearch,
                    UpdateCurrentCell = _updateCurrentCell,
                    CurrentGridName = _currentSheetName,
                    AvailableGrids = _passedGrids,

                    Location = new Point(100, 0),
                    Owner = this,
                    StartPosition = FormStartPosition.Manual
                };

                SearchWindowMenu.UpdateSearchGrid();
                SearchWindowMenu.Show();
            }
            else if (SearchWindowMenu != null && !SearchWindowMenu.Visible)
            {
                SearchWindowMenu.DataGridViewToSearch = _passedGridToSearch;
                SearchWindowMenu.UpdateCurrentCell = _updateCurrentCell;
                SearchWindowMenu.CurrentGridName = _currentSheetName;
                SearchWindowMenu.AvailableGrids = _passedGrids;

                SearchWindowMenu.UpdateSearchGrid();
                SearchWindowMenu.Visible = true;
            }
            else if (SearchWindowMenu.Visible && !SearchWindowMenu.Focused)
            {

                SearchWindowMenu.DataGridViewToSearch = _passedGridToSearch;
                SearchWindowMenu.UpdateCurrentCell = _updateCurrentCell;
                SearchWindowMenu.CurrentGridName = _currentSheetName;
                SearchWindowMenu.AvailableGrids = _passedGrids;

                SearchWindowMenu.UpdateSearchGrid();

                SearchWindowMenu.Select();
            }
        }

        private void ApplyFilter()
        {
            // Get the row and column of the current selected cell
            // TODO: Clear - currentCellRow = currentDataGridView.CurrentCell.RowIndex;
            // TODO: Clear - currentCellColumn = currentDataGridView.CurrentCell.ColumnIndex;

            // Get the selected items in the filter window
            differenceCheckCompare = FilterCheckedListbox.CheckedItems.Count;

            // Count every item in the CheckedListBox and compare it with the actual checked items - if theres a difference go into the if statement
            if (differenceCheckBasis != differenceCheckCompare)
            {
                if (!isFilterSet)
                {
                    isFilterSet = true;
                }

                if (!filteredColumNumbers.Contains(filterColumnNumber))
                {
                    filteredColumNumbers.Add(filterColumnNumber);
                }

                TitleBarDeleteFilters.Image = Properties.Resources.DeleteFilterEnabled_16x16;
                TitleBarDeleteFilters.ForeColor = System.Drawing.Color.Black;

                // Creating the filter syntax by concatenating the values that will remain
                for (int i = 0; i <= FilterCheckedListbox.CheckedItems.Count; i++)
                {
                    if (columnFilterSyntax == "")
                    {
                        if (i == 0 && FilterCheckedListbox.CheckedItems.Count == 1)
                        {
                            columnFilterSyntax = "[" + filterColumnName + "] IN ('" + FilterCheckedListbox.CheckedItems[i].ToString() + "') ";
                        }
                        else if (i == 0 && FilterCheckedListbox.CheckedItems.Count > 1)
                        {
                            columnFilterSyntax = "[" + filterColumnName + "] IN ('" + FilterCheckedListbox.CheckedItems[i].ToString() + "', ";
                        }
                    }
                    else if (columnFilterSyntax != "")
                    {
                        if (i == 0 && FilterCheckedListbox.CheckedItems.Count == 1)
                        {
                            columnFilterSyntax += " AND [" + filterColumnName + "] IN ('" + FilterCheckedListbox.CheckedItems[i].ToString() + "')";
                        }
                        else if (i == 0 && FilterCheckedListbox.CheckedItems.Count > 1)
                        {
                            columnFilterSyntax += " AND [" + filterColumnName + "] IN ('" + FilterCheckedListbox.CheckedItems[i].ToString() + "', ";
                        }
                        else if (i < (FilterCheckedListbox.CheckedItems.Count - 1))
                        {
                            columnFilterSyntax += "'" + FilterCheckedListbox.CheckedItems[i].ToString() + "', ";
                        }
                        else if (i == (FilterCheckedListbox.CheckedItems.Count - 1))
                        {
                            columnFilterSyntax += "'" + FilterCheckedListbox.CheckedItems[i].ToString() + "')";
                        }
                    }
                }

                // TODO: Clear - MessageBox.Show(columnFilterSyntax);
                CurrentSheetBindingSource.Filter = columnFilterSyntax;

                UpdateSheetOne();

                StatusBarRecordsFound.Text = Localisation.GetString("StockManagementMenu_Button_RecordsFound") + DefaultDataGrid.Rows.Count;
            }

            PanelFilterWindow.Visible = false;
        }

        private void ResetFilter()
        {
            isFilterSet = false;
            PanelFilterWindow.Visible = false;
            columnFilterSyntax = "";

            CurrentSheetBindingSource.Filter = "";

            UpdateSheetOne();

            StatusBarRecordsFound.Text = Localisation.GetString("StockManagementMenu_Button_RecordsFound") + DefaultDataGrid.Rows.Count;
        }

        /// <summary>
        /// Loops through the column regarding the variable columnCount, sets those columns to read-only, leave the rest editable and hides the last row which includes the real row count. 
        /// </summary>
        private void SheetOneSettings(int columnCount)
        {
            MethodInvoker FormatColumns = delegate
            {
                for (int i = 0; i < columnCount; i++)
                {
                    DefaultDataGrid.Columns[i].ReadOnly = true;
                }

                DefaultDataGrid.Columns[DefaultDataGrid.Columns.Count - 1].Visible = false;
            };

            if (DefaultDataGrid.InvokeRequired)
            {
                Invoke(FormatColumns);
            }
            else
            {
                for (int i = 0; i < columnCount; i++)
                {
                    DefaultDataGrid.Columns[i].ReadOnly = true;
                }

                DefaultDataGrid.Columns[DefaultDataGrid.Columns.Count - 1].Visible = false;
            }
        }

        private void InputBox(int xPos = -1, int yPos = -1)
        {
            // ---- Create the form ----
            Form inputBoxForm = new Form();

            Screen currentScreen = Screen.FromControl(this);

            string concatenateSelectedCells = "";

            inputBoxForm.StartPosition = FormStartPosition.Manual;
            inputBoxForm.Text = "Trennzeichen eingeben";
            inputBoxForm.MinimizeBox = false;
            inputBoxForm.MaximizeBox = false;
            inputBoxForm.ShowIcon = false;
            inputBoxForm.FormBorderStyle = FormBorderStyle.FixedDialog;

            inputBoxForm.Width = 400;
            inputBoxForm.Height = 175;

            if (xPos == -1)
            {
                inputBoxForm.Left = (currentScreen.Bounds.Width - inputBoxForm.Width) / 2;
            }
            else
            {
                inputBoxForm.Left = xPos;
            }

            if (yPos == -1)
            {
                inputBoxForm.Top = (currentScreen.Bounds.Height - inputBoxForm.Height) / 2;
            }
            else
            {
                inputBoxForm.Left = yPos;
            }
            // ---- Create the form ----

            // ---- Create the controls ----
            TextBox enterSeparator = new TextBox();

            enterSeparator.Width = inputBoxForm.ClientSize.Width - 20;
            enterSeparator.Left = inputBoxForm.ClientSize.Width - enterSeparator.Width - 10;
            enterSeparator.Top = inputBoxForm.ClientSize.Height - enterSeparator.Height - 20;

            inputBoxForm.Controls.Add(enterSeparator);


            Label functionDescription = new Label();

            functionDescription.Text = "Geben Sie hier das gewünschte Trennzeichen ein, dass die Werte trennen soll.";
            functionDescription.Width = (inputBoxForm.Width / 8) * 5;
            functionDescription.Height = 26;
            functionDescription.Left = 10;
            functionDescription.Top = (inputBoxForm.Height / 4) - (functionDescription.Height * 2) + 20;
            inputBoxForm.Controls.Add(functionDescription);

            // Create the cancel button
            Button cancelButton = new Button();

            cancelButton.Text = "Abbrechen";
            cancelButton.Left = inputBoxForm.Width - cancelButton.Width - 25;
            cancelButton.Top = functionDescription.Top + cancelButton.Height + 10;

            inputBoxForm.Controls.Add(cancelButton);

            // Create the copy content and close button
            Button copyCloseButton = new Button();

            copyCloseButton.Text = "Kopieren && schließen";
            copyCloseButton.Width = 125;
            // TODO: Clear
            //CoreFunctions.DebugWrite(inputBoxForm.ClientSize.Width + " " + copyCloseButton.Width + " " + 25);
            copyCloseButton.Left = inputBoxForm.ClientSize.Width - copyCloseButton.Width - 25;
            copyCloseButton.Top = functionDescription.Top;

            inputBoxForm.Controls.Add(copyCloseButton);
            copyCloseButton.Visible = false;

            // Create the confirm button
            Button confirmButton = new Button();

            confirmButton.Text = "Bestätigen";
            confirmButton.Left = inputBoxForm.Width - confirmButton.Width - 25;
            confirmButton.Top = functionDescription.Top;

            // ****************
            // **** Events ****
            // ****************
            enterSeparator.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (enterSeparator.Text != "")
                    {
                        foreach (DataGridViewCell selectedCells in CurrentSheetGrid.SelectedCells)
                        {
                            concatenateSelectedCells += concatenateSelectedCells == "" ? selectedCells.Value.ToString() : enterSeparator.Text + selectedCells.Value.ToString();
                        }

                        inputBoxForm.Controls.Remove(cancelButton);

                        inputBoxForm.Text = "Werte erfolgreich zusammengeführt";
                        functionDescription.Text = "Die Werte der ausgewählten Zellen wurden nun mit dem Zeichen \"" + enterSeparator.Text + "\" zusammengeführt.";
                        functionDescription.Width = ((inputBoxForm.Width / 8) * 5) - 25;

                        enterSeparator.Text = concatenateSelectedCells;

                        confirmButton.Visible = false;
                        copyCloseButton.Visible = true;
                    }
                    else
                    {
                        // TODO: Localization
                        CustomMessageBox.Show("_you haven't entered a separator", "_enter seperator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            };
            // ****************

            copyCloseButton.Click += (sender, e) =>
            {
                Clipboard.SetText(enterSeparator.Text, TextDataFormat.Text);
                inputBoxForm.Close();
            };

            cancelButton.Click += (sender, e) =>
            {
                inputBoxForm.Close();
            };

            confirmButton.Click += (sender, e) =>
            {
                foreach (DataGridViewCell selectedCells in CurrentSheetGrid.SelectedCells)
                {
                    concatenateSelectedCells += concatenateSelectedCells == "" ? selectedCells.Value.ToString() : enterSeparator.Text + selectedCells.Value.ToString();
                }

                inputBoxForm.Controls.Remove(cancelButton);

                inputBoxForm.Text = "Werte erfolgreich zusammengeführt";
                functionDescription.Text = "Die Werte der ausgewählten Zellen wurden nun mit dem Zeichen \"" + enterSeparator.Text + "\" zusammengeführt.";
                functionDescription.Width = ((inputBoxForm.Width / 8) * 5) - 25;

                enterSeparator.Text = concatenateSelectedCells;

                confirmButton.Visible = false;
                copyCloseButton.Visible = true;
            };


            // ---- Create the controls ----

            inputBoxForm.Controls.Add(confirmButton);

            // Finally show the form with all before added controls
            inputBoxForm.ShowDialog();
        }

        private void CheckFilterWindowFocus()
        {
            List<System.Windows.Forms.Control> focusControls = new List<System.Windows.Forms.Control>()
            {
                PanelFilterWindow,
                FilterInputBox,
                FilterCheckedListbox,
                AllFilterCheck,
                FilterOk,
                FilterCancel,
                SortColumnAscending,
                SortColumnDescending
            };

            hasFilterControlFocus = false;

            foreach (System.Windows.Forms.Control focusControl in focusControls)
            {
                if (focusControl.Focused)
                {
                    hasFilterControlFocus = true;
                    break;
                }
            }
        }

        private void LocationAndPrimarySettings()
        {
            // ---- Several button images ----
            Bitmap saveButton = new Bitmap(Properties.Resources.SaveChanges_32x96);
            for (int i = 0; i <= 2; i++)
            {
                Bitmap saveButtonBuffer = saveButton.Clone(new Rectangle(0, i * 32, 32, 32), Properties.Resources.SaveChanges_32x96.PixelFormat);

                saveIcon[i] = saveButtonBuffer; //new Bitmap(saveButtonBuffer, 20, 22);
            }
            TitleBarSaveChanges.Image = saveIcon[2];

            Bitmap reloadButton = new Bitmap(Properties.Resources.ReloadData_32x96);
            for (int i = 0; i <= 2; i++)
            {
                Bitmap reloadButtonBuffer = reloadButton.Clone(new Rectangle(0, i * 32, 32, 32), Properties.Resources.ReloadData_32x96.PixelFormat);

                reloadIcon[i] = new Bitmap(reloadButtonBuffer, 20, 22);
            }
            TitleBarImportData.BackColor = System.Drawing.Color.Transparent;
            TitleBarImportData.Image = reloadIcon[0];
            // ---- Several button images ----

            //ContainingPanel.Visible = false;
            PanelFilterWindow.Visible = false;
            // Adding controls to the form
            DefaultDataGrid.Visible = true;

            // Screen resolution variables
            //int screenWidth = Screen.FromControl(this).WorkingArea.Width;
            //int screenHeigth = Screen.FromControl(this).WorkingArea.Height;

            // Form settings
            //this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.FromArgb(222, 222, 222);
            this.KeyPreview = true;

            // Panel settings
            PanelOptions.Width = this.ClientSize.Width;
            PanelOptions.Height = 100;
            PanelOptions.Left = 0;
            PanelOptions.Top = 40;

            UndoChange.Enabled = false;
            RedoChange.Enabled = false;

            SeparatorLineOne.Height = PanelOptions.Height - 10;
            //SeparatorLineOne.Left = 
            SeparatorLineOne.Top = 5;

            SeparatorLineTwo.Height = PanelOptions.Height - 10;
            //SeparatorLineTwo.Left = 
            SeparatorLineTwo.Top = 5;

            SeparatorLineThree.Height = PanelOptions.Height - 10;
            //SeparatorLineThree.Left = 
            SeparatorLineThree.Top = 5;

            SeparatorLineFour.Height = PanelOptions.Height - 10;
            //SeparatorLineFour.Left = 
            SeparatorLineFour.Top = 5;


            //Setting the location of the input textbox
            TextInputBox.Top = PanelOptions.Top + PanelOptions.Height + 10;
            TextInputBox.Left = 10;

            //Setting the datagridviews properties, position, events etc...
            TabSheetHolder.Width = this.ClientSize.Width - 20;      // **** instead of defaultDataGrid ****
            TabSheetHolder.Height = this.Height - PanelOptions.Top - PanelOptions.Height - TextInputBox.Height - 120;   // **** instead of defaultDataGrid ****
            TabSheetHolder.Left = 10;                                           // **** instead of defaultDataGrid ****
            TabSheetHolder.Top = TextInputBox.Top + TextInputBox.Height + 10;   // **** instead of defaultDataGrid ****

            // Tag page settings
            SelectPreviousSheet.Left = TabSheetHolder.Left + 5;                             // **** instead of defaultDataGrid ****
            SelectPreviousSheet.Top = TabSheetHolder.Top + TabSheetHolder.Height + 5;       // **** instead of defaultDataGrid ****
            SelectPreviousSheet.Enabled = false;

            SelectNextSheet.Left = SelectPreviousSheet.Left + SelectPreviousSheet.Width + 10;
            SelectNextSheet.Top = SelectPreviousSheet.Top;
            SelectNextSheet.Enabled = false;

            AddNewSheet.Left = SelectNextSheet.Left + SelectNextSheet.Width + 10;
            AddNewSheet.Top = SelectPreviousSheet.Top;

            TitleBarUnusedValues.Enabled = true;
            TitleBarImportData.Enabled = false;

            //Position label
            StatusBar.Width = this.ClientSize.Width;
            StatusBar.Height = 25;
            StatusBar.Left = 0;
            StatusBar.Top = this.ClientSize.Height - StatusBar.Height;

            StatusBarRecordsFound.Left = TabSheetHolder.Left;                                     // **** instead of defaultDataGrid ****
            StatusBarRecordsFound.Top = (StatusBar.Height - StatusBarRecordsFound.Height) / 2;
            StatusBarSelectionMenu.Left = TabSheetHolder.Left + 200;                               // **** instead of defaultDataGrid ****
            StatusBarSelectionMenu.Top = (StatusBar.Height - StatusBarSelectionMenu.Height) / 2;
        }

        private void RightClickMenu(RightClickMenuCase _PassedCase, dynamic _passedControl = null, int _passedColumnIndex = 0, int _passedTabPageIndex = 0)
        {
            ContextMenuStrip rightClickMenu = new ContextMenuStrip();

            switch (_PassedCase)
            {
                case RightClickMenuCase.ColumnHeader:
                    rightClickMenu.Items.Add("Umbenennen").Name = "renameColumnText";
                    rightClickMenu.Items.Add("Spaltenformat anpassen").Name = "formatColumn";

                    rightClickMenu.Show(_passedControl, new Point(_passedControl.PointToClient(System.Windows.Forms.Control.MousePosition).X, _passedControl.PointToClient(System.Windows.Forms.Control.MousePosition).Y));
                    rightClickMenu.ItemClicked += (sender, e) => RightClickMenu_ItemClicked(sender, e, _passedControl, _passedColumnIndex);

                    break;
                case RightClickMenuCase.TabPage:
                    rightClickMenu.Items.Add("Umbenennen").Name = "renameSheetText";

                    if (_passedTabPageIndex > 0)
                    {
                        rightClickMenu.Items.Add("Löschen").Name = "deleteTabPage";
                    }

                    rightClickMenu.Show(_passedControl, new Point(_passedControl.PointToClient(System.Windows.Forms.Control.MousePosition).X, _passedControl.PointToClient(System.Windows.Forms.Control.MousePosition).Y));
                    rightClickMenu.ItemClicked += (sender, e) => RightClickMenu_ItemClicked(sender, e, _passedControl, _passedTabPageIndex: _passedTabPageIndex);

                    break;
                default:
                    break;
            }
        }

        private void RightClickMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e, dynamic _passedControl = null, int _passedColumnIndex = 0, int _passedTabPageIndex = 0)
        {
            switch (e.ClickedItem.Name)
            {
                case "renameColumnText":
                {
                    string value = _passedControl.Columns[_passedColumnIndex].HeaderText;

                    if (CoreFunctions.InputBox("Spaltenamen ändern", "Geben Sie einen neuen Spaltennamen ein:", ref value) == DialogResult.OK)
                    {
                        _passedControl.Columns[_passedColumnIndex].HeaderText = value;
                    }
                }
                break;
                case "formatColumn":
                {
                    CoreFunctions.SetColumnFormat(_passedControl, _passedColumnIndex);
                }
                break;
                case "renameSheetText":
                {
                    string value = _passedControl.TabPages[_passedTabPageIndex].Text;

                    if (CoreFunctions.InputBox("Rename sheet", "Enter a new sheet name:", ref value) == DialogResult.OK)
                    {
                        _passedControl.TabPages[_passedTabPageIndex].Text = value;

                        if (SearchWindowMenu != null && SearchWindowMenu.Visible)
                        {
                            foreach (DataGridView grid in SearchWindowMenu.AvailableGrids)
                            {
                                if (CurrentSheetGrid == grid)
                                {
                                    if (SearchWindowMenu.DataGridViewToSearch == grid)
                                    {
                                        SearchWindowMenu.DataGridViewToSearch.Tag = value;
                                        SearchWindowMenu.CurrentGridName = value;
                                    }

                                    grid.Tag = value;
                                    SearchWindowMenu.UpdateSearchGrid();
                                    break;
                                }
                            }
                        }
                    }
                }
                break;
                case "deleteTabPage":
                {
                    if (CustomMessageBox.Show("Would you like to delete the selected sheet?", "Delete sheet", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        _passedControl.TabPages.RemoveAt(_passedTabPageIndex);

                        if ((_passedTabPageIndex + 1) > _passedControl.TabPages.Count)
                        {
                            _passedControl.SelectedIndex = _passedTabPageIndex - 1;
                        }
                        else
                        {
                            _passedControl.SelectedIndex = _passedTabPageIndex;
                        }
                    }
                }
                break;
                default:
                {
                    CustomMessageBox.Show("Es wurde keine Aktion gewählt.", "Unerwartete Ausnahme", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                break;
            }
        }

        private void SetBarText(BarStatusState _passedBarState, DataGridView _passedDataGridView, ref Label _passedBarText)
        {
            int bufferValue;
            int selectedCellsSumAndCount = 0;

            switch (_passedBarState)
            {
                case BarStatusState.Sum:
                {
                    foreach (DataGridViewCell currentCell in _passedDataGridView.SelectedCells)
                    {
                        if (int.TryParse(currentCell.Value == null ? "" : currentCell.Value.ToString(), out bufferValue))
                        {
                            selectedCellsSumAndCount += bufferValue;
                        }
                    }

                    _passedBarText.Text = Localisation.GetString("StockManagementMenu_Button_SumSelection") + (selectedCellsSumAndCount == 0 ? "" : selectedCellsSumAndCount.ToString());
                }
                break;
                case BarStatusState.Count:
                {
                    foreach (DataGridViewCell currentCell in _passedDataGridView.SelectedCells)
                    {
                        if ((currentCell.Value == null ? "" : currentCell.Value.ToString()) != "")
                        {
                            selectedCellsSumAndCount++;
                        }
                    }

                    _passedBarText.Text = Localisation.GetString("StockManagementMenu_Button_CountSelection") + (selectedCellsSumAndCount == 0 ? "" : selectedCellsSumAndCount.ToString());
                }
                break;
                default:
                    break;
            }
        }

        private void UndoChanges()
        {
            if (currentChangesPosition > 0)
            {
                string lastValue;
                int datatableColumn;
                int datatableRow;

                if (int.Parse(changesValueHolder[currentChangesPosition - 1][0].ToString()) == 1)
                {
                    lastValue = changesValueHolder[currentChangesPosition - 1][1].ToString();
                    datatableColumn = int.Parse(changesValueHolder[currentChangesPosition - 1][3].ToString());
                    datatableRow = int.Parse(changesValueHolder[currentChangesPosition - 1][4].ToString());

                    CurrentSheetData.Rows[datatableRow][datatableColumn] = lastValue;
                    CurrentSheetGrid.CurrentCell = CurrentSheetGrid.Rows[datatableRow].Cells[datatableColumn];

                    currentChangesPosition--;

                    if (!hastableChanged)
                    {
                        TitleBarSaveChanges.Image = saveIcon[0];
                        TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                        hastableChanged = true;
                    }
                }
                else
                {
                    for (int i = 0; i < int.Parse(changesValueHolder[currentChangesPosition - 1][0].ToString()); i++)
                    {
                        lastValue = changesValueHolder[currentChangesPosition - 1][(i * 4) + 1].ToString();
                        datatableColumn = int.Parse(changesValueHolder[currentChangesPosition - 1][(i * 4) + 3].ToString());
                        datatableRow = int.Parse(changesValueHolder[currentChangesPosition - 1][(i * 4) + 4].ToString());

                        CurrentSheetData.Rows[datatableRow][datatableColumn] = lastValue;
                        CurrentSheetGrid.CurrentCell = CurrentSheetGrid.Rows[datatableRow].Cells[datatableColumn];
                    }

                    currentChangesPosition--;

                    if (!hastableChanged)
                    {
                        TitleBarSaveChanges.Image = saveIcon[0];
                        TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                        hastableChanged = true;
                    }
                }
            }

            // If the first saved change is restored
            if (currentChangesPosition == 0)
            {
                UndoChange.Enabled = false;
            }

            // If there are more than one changes
            if (changesValueHolder.Count > 0)
            {
                RedoChange.Enabled = true;
            }
        }

        private void RedoChanges()
        {
            if (currentChangesPosition < changesValueHolder.Count)
            {
                string nextValue;
                int datatableColumn;
                int datatableRow;

                if (int.Parse(changesValueHolder[currentChangesPosition][0].ToString()) == 1)
                {
                    nextValue = changesValueHolder[currentChangesPosition][2].ToString();
                    datatableColumn = int.Parse(changesValueHolder[currentChangesPosition][3].ToString());
                    datatableRow = int.Parse(changesValueHolder[currentChangesPosition][4].ToString());

                    CurrentSheetData.Rows[datatableRow][datatableColumn] = nextValue;
                    CurrentSheetGrid.CurrentCell = CurrentSheetGrid.Rows[datatableRow].Cells[datatableColumn];

                    currentChangesPosition++;

                    if (!hastableChanged)
                    {
                        TitleBarSaveChanges.Image = saveIcon[0];
                        TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                        hastableChanged = true;
                    }
                }
                else
                {
                    for (int i = 0; i < int.Parse(changesValueHolder[currentChangesPosition][0].ToString()); i++)
                    {
                        nextValue = changesValueHolder[currentChangesPosition][(i * 4) + 2].ToString();
                        datatableColumn = int.Parse(changesValueHolder[currentChangesPosition][(i * 4) + 3].ToString());
                        datatableRow = int.Parse(changesValueHolder[currentChangesPosition][(i * 4) + 4].ToString());

                        CurrentSheetData.Rows[datatableRow][datatableColumn] = nextValue;
                        CurrentSheetGrid.CurrentCell = CurrentSheetGrid.Rows[datatableRow].Cells[datatableColumn];
                    }

                    currentChangesPosition++;

                    if (!hastableChanged)
                    {
                        TitleBarSaveChanges.Image = saveIcon[0];
                        TitleBarSaveChanges.ForeColor = System.Drawing.Color.Black;
                        hastableChanged = true;
                    }
                }
            }

            // If the last saved change is restored
            if (currentChangesPosition == changesValueHolder.Count)
            {
                RedoChange.Enabled = false;
            }

            // If there are more than one changes
            if (changesValueHolder.Count > 0)
            {
                UndoChange.Enabled = true;
            }
        }
        #endregion
    }
}

public class FlatButton : Button
{
    public FlatButton() : base()
    {
        //base.FlatAppearance.BorderColor = base.Parent.BackColor;
        base.FlatAppearance.BorderSize = 1;
        base.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
        base.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
        base.FlatStyle = FlatStyle.Flat;
        base.TabStop = false;
        base.MouseDown += FlatButton_MouseDown;
        base.MouseEnter += FlatButton_MouseEnter;
        base.MouseLeave += FlatButton_MouseLeave;
        base.MouseUp += FlatButton_MouseUp;
    }

    private void FlatButton_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            this.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(132, 172, 221);
            this.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(201, 201, 201);
            this.Refresh();
        }
    }

    private void FlatButton_MouseEnter(object sender, EventArgs e)
    {
        this.FlatAppearance.BorderSize = 1;
        this.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(184, 214, 251);
        this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(222, 227, 233);
    }

    private void FlatButton_MouseLeave(object sender, EventArgs e)
    {
        this.FlatAppearance.BorderSize = 1;
        this.FlatAppearance.BorderColor = base.Parent.BackColor;
    }

    private void FlatButton_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            this.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(184, 214, 251);
        }
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);
        
    }

    // Remove the still aprearing focus cues
    protected override bool ShowFocusCues
    {
        get
        {
            //return base.ShowFocusCues;
            return false;
        }
    }
}

// Customised button text - to make the text area transparent for mouse events
class CustomButtonText : Label
{
    protected override void WndProc(ref Message m)
    {
        const int WM_NCHITTEST = 0x0084;
        const int HTTRANSPARENT = (-1);

        if (m.Msg == WM_NCHITTEST)
        {
            m.Result = (IntPtr)HTTRANSPARENT;
        }
        else
        {
            base.WndProc(ref m);
        }
    }
}

// Completely customised button
public class CustomButton : Button
{
    public enum ButtonTextAlign
    {
        TopLeft,
        TopCenter,
        TopRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight
    }

    // Actual button text
    private CustomButtonText buttonText = new CustomButtonText();

    private System.Drawing.Color buttonMouseLeftBackColour = System.Drawing.Color.FromArgb(221, 221, 221);
    private System.Drawing.Color buttonMouseOverBackColour = System.Drawing.Color.FromArgb(200, 200, 200);

    private bool buttonPainting = false;
    private bool buttonpressed = false;

    private ButtonTextAlign currentTextAlignment = ButtonTextAlign.MiddleCenter;

    public CustomButton() : base()
    {
        // General settings
        buttonText.AutoSize = true;
        buttonText.Font = new System.Drawing.Font(this.Font.FontFamily, 20);
        currentTextAlignment = ButtonTextAlign.MiddleCenter;

        SetTextPosition(ButtonTextAlign.MiddleCenter);

        this.Size = new Size(80, 21);
        this.Controls.Add(buttonText);

        // Events
        this.MouseDown += new MouseEventHandler(CustomButton_MouseDown);
        this.MouseUp += new MouseEventHandler(CustomButton_MouseUp);
        this.MouseMove += new MouseEventHandler(CustomButton_MouseMove);
        this.MouseEnter += new EventHandler(CustomButton_MouseEnter);
        this.MouseLeave += new EventHandler(CustomButton_MouseLeave);
        this.Resize += new EventHandler(CustomButton_Resize);
    }

    private void CustomButton_MouseDown(object sender, EventArgs e)
    {
        buttonpressed = true;

        buttonText.Font = new System.Drawing.Font(this.Font.FontFamily, 25);

        SetTextPosition(currentTextAlignment);
    }

    private void CustomButton_MouseUp(object sender, EventArgs e)
    {
        buttonpressed = false;

        buttonText.Font = new System.Drawing.Font(this.Font.FontFamily, 20);
        this.BackColor = buttonMouseLeftBackColour;

        SetTextPosition(currentTextAlignment);
    }

    private void CustomButton_MouseMove(object sender, EventArgs e)
    {

        if (this.ClientRectangle.Contains(PointToClient(System.Windows.Forms.Control.MousePosition)))
        {
            if (buttonpressed) { buttonText.Font = new System.Drawing.Font(this.Font.FontFamily, 25); };
            this.BackColor = buttonMouseOverBackColour;

            SetTextPosition(currentTextAlignment);
        }
        else
        {

            buttonText.Font = new System.Drawing.Font(this.Font.FontFamily, 20);
            this.BackColor = buttonMouseLeftBackColour;

            SetTextPosition(currentTextAlignment);
        }
    }

    private void CustomButton_MouseEnter(object sender, EventArgs e)
    {
        this.BackColor = buttonMouseOverBackColour;
    }

    private void CustomButton_MouseLeave(object sender, EventArgs e)
    {
        this.BackColor = buttonMouseLeftBackColour;
    }

    private void CustomButton_Resize(object sender, EventArgs e)
    {
        SetTextPosition(currentTextAlignment);
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        buttonPainting = true;

        base.OnPaintBackground(pevent);

        // Do the drawing after the base method was finished, otherwise your drawings will be deleted
        pevent.Graphics.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);

        buttonPainting = false;
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        SetTextPosition(ButtonTextAlign.MiddleCenter);
    }

    public override string Text
    {
        get
        {
            if (!buttonPainting)
            {
                return buttonText.Text;
            }
            else
            {
                return "";
            }
        }
        set
        {
            buttonText.Text = value;
            base.Text = "";
        }
    }

    public override System.Drawing.Font Font
    {
        get
        {
            return buttonText.Font;
        }
        set
        {
            buttonText.Font = value;

            SetTextPosition(currentTextAlignment);
        }
    }

    [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Description("Choose the text position on the button.")]
    public ButtonTextAlign TextPosition
    {
        get
        {
            return currentTextAlignment;
        }
        set
        {
            currentTextAlignment = value;
            SetTextPosition(currentTextAlignment);
        }
    }

    private void SetTextPosition(ButtonTextAlign _PassedValue)
    {
        switch (_PassedValue)
        {
            case ButtonTextAlign.TopLeft:
            {
                buttonText.Left = 1;
                buttonText.Top = 1;
            }
            break;
            case ButtonTextAlign.TopRight:
            {
                buttonText.Left = this.Width - buttonText.Width - 1;
                buttonText.Top = 1;
            }
            break;
            case ButtonTextAlign.TopCenter:
            {
                buttonText.Left = (this.Width - buttonText.Width) / 2;
                buttonText.Top = 1;
            }
            break;
            case ButtonTextAlign.BottomLeft:
            {
                buttonText.Left = 1;
                buttonText.Top = this.Height - buttonText.Height - 1;
            }
            break;
            case ButtonTextAlign.BottomRight:
            {
                buttonText.Left = this.Width - buttonText.Width - 1;
                buttonText.Top = this.Height - buttonText.Height - 1;
            }
            break;
            case ButtonTextAlign.BottomCenter:
            {
                buttonText.Left = (this.Width - buttonText.Width) / 2;
                buttonText.Top = this.Height - buttonText.Height - 1;
            }
            break;
            case ButtonTextAlign.MiddleLeft:
            {
                buttonText.Left = 1;
                buttonText.Top = (this.Height - buttonText.Height) / 2;
            }
            break;
            case ButtonTextAlign.MiddleRight:
            {
                buttonText.Left = this.Width - buttonText.Width - 1;
                buttonText.Top = (this.Height - buttonText.Height) / 2;
            }
            break;
            case ButtonTextAlign.MiddleCenter:
            {
                buttonText.Left = (this.Width - buttonText.Width) / 2;
                buttonText.Top = (this.Height - buttonText.Height) / 2;
            }
            break;
            default:
            {
            }
            break;
        }
    }
}