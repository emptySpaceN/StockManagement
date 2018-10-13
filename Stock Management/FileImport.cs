using System;
using Microsoft.VisualBasic.FileIO;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Xml.Linq;
using StockManagementCore;
using System.Linq;
using System.Threading;
using System.Globalization;

namespace StockManagement
{
    public partial class FileImport : Form
    {
        #region Variables
        // *************************
        // ******** Classes ********
        // *************************
        public StockManagement StockManagementMenu { get; set; } = null;
        // *************************
        // *************************
        // *************************

        public int SheetIndex { get; set; } = 0;

        private enum FileFormat
        {
            None,
            CSV,
            Txt,
            Xlsx
        }

        private FileFormat loadedFileFormat = FileFormat.None;
        #endregion

        public FileImport()
        {
            InitializeComponent();

            // Events
            this.KeyDown += new KeyEventHandler(FileImport_KeyDown);
            this.Load += new EventHandler(FileImport_Load);
        }

        #region Form events
        private void FileImport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                this.Close();
            }
        }

        private void FileImport_Load(object sender, EventArgs e)
        {
            // Set all text regarding the current culture info
            SetLocalisation();
        }
        #endregion

        #region Control events
        private void GetSheetOne_Click(object sender, EventArgs e)
        {
            GetCSVFilePath();
        }

        private void ApplyData_Click(object sender, EventArgs e)
        {
            switch (loadedFileFormat)
            {
                case FileFormat.None:
                {
                    MessageBox.Show(Localisation.Localisation.strings.FileImport_FileChoose, Localisation.Localisation.strings.FileImport_ImportError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                break;
                case FileFormat.CSV:
                {
                    FillSheetWithCSVData();
                }
                break;
                case FileFormat.Txt:
                {
                    DataTable sheetDatatable = new DataTable();
                    sheetDatatable.ExtendedProperties.Add("Tag", 1);

                    string test = "'" + DelimiterText.Text + "'";

                    char[] delimeter = test.ToCharArray();

                    // Get the column count from the first line of the file
                    int SheetOnecolumnCount = (File.ReadLines(FilePath.Text).First().Split(delimeter).Length);

                    // TODO: Currently rowCount isn't needed - probably clean it
                    //int rowCount = File.ReadLines(getPathFile.Element("CustomerFile").Value).Count();

                    using (StreamReader sr = File.OpenText(FilePath.Text))
                    {
                        int lineCounter = 0;
                        int tabulatorCounter = 0;
                        string s = String.Empty;
                        string[] wholeLine = { String.Empty };
                        char[] separator;
                        DataRow dr = null;

                        StockManagementMenu.CurrentSheetGrid.AllowUserToAddRows = false;     // Disable the default row
                        StockManagementMenu.CurrentSheetGrid.DoubleBuffered(true);           // Enable the boublebuffering for fast scrolling

                        while ((s = sr.ReadLine()) != null)
                        {
                            separator = DelimiterText.Text.ToCharArray();
                            wholeLine = s.Split(new string[] { DelimiterText.Text }, StringSplitOptions.None);
                            tabulatorCounter = 0;
                            dr = null;

                            lineCounter++;

                            if (string.IsNullOrWhiteSpace(s) == false)
                            {
                                foreach (string tabulator in wholeLine)
                                {
                                    tabulatorCounter++;

                                    // The tabulatorCounter represents the columns and is always 1 time bigger because the tabulator is inbetween the columns
                                    if (lineCounter == 1)
                                    {
                                        // TODO: Check if it is possible to add multiple columns with the same name
                                        sheetDatatable.Columns.Add(tabulator);
                                    }
                                    else if (lineCounter != 1)
                                    {
                                        if (dr == null) { dr = sheetDatatable.NewRow(); }

                                        dr[tabulatorCounter - 1] = tabulator;
                                        dr[sheetDatatable.Columns.Count - 1] = (lineCounter - 2);
                                    }
                                }

                                if (dr != null) { sheetDatatable.Rows.Add(dr); }
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    // Create a BindingSource the datatable to it
                    BindingSource sheetBindingsource = new BindingSource
                    {
                        DataSource = sheetDatatable,
                    };

                    // Set the new data source to the binding source
                    StockManagementMenu.CurrentSheetGrid.DataSource = null;
                    StockManagementMenu.CurrentSheetGrid.DataSource = sheetBindingsource;

                    // Add the current sheets, binding sources and data tables to the lists
                    StockManagementMenu.SheetBindingSourceHolder[SheetIndex] = null;
                    StockManagementMenu.SheetBindingSourceHolder[SheetIndex] = StockManagementMenu.CurrentSheetBindingSource;
                    StockManagementMenu.SheetdataHolder[SheetIndex] = null;
                    StockManagementMenu.SheetdataHolder[SheetIndex] = sheetDatatable;

                    // Set current sheets, binding sources and data tables
                    StockManagementMenu.CurrentSheetBindingSource = sheetBindingsource;
                    StockManagementMenu.CurrentSheetData = sheetDatatable;


                    StockManagementMenu.TabSheetHolder.TabPages[SheetIndex].Cursor = Cursors.Arrow;
                    StockManagementMenu.TabSheetHolder.TabPages[SheetIndex].Controls.RemoveByKey("AddLabel");
                    StockManagementMenu.TabSheetHolder.TabPages[SheetIndex].Tag = "";
                    StockManagementMenu.CurrentSheetGrid.Visible = true;
                    StockManagementMenu.TitleBarImportData.Enabled = true;
                    // TODO: Clear
                    //MessageBox.Show(StockManagementMenuPublic.SheetdataHolder_Public.AddToFront(StockManagementMenuPublic.currentSheetIndexPublic).ToString());

                    this.Close();
                }
                break;
                case FileFormat.Xlsx:
                {

                }
                break;
                default:
                {
                    // TODO: Protocol it
                }
                break;
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

            FileImportTitle.Text = Localisation.Localisation.strings.FileImport_Heading;
            HasColumnNamesCsvTxt.Text = Localisation.Localisation.strings.FileImport_ColumnNames;
            DelimiterCsvTxt.Text = Localisation.Localisation.strings.FileImport_DelimiterCsvTxt;

            XlsxSheetTitle.Text = Localisation.Localisation.strings.FileImport_Sheet;

            SelectedFileFormat.Text = Localisation.Localisation.strings.FileImport_FileFormat;
            SelectedFilePath.Text = Localisation.Localisation.strings.FileImport_FilePath;
            ChooseFile.Text = Localisation.Localisation.strings.FileImport_BrowseFile;
            ImportData.Text = Localisation.Localisation.strings.FileImport_ImportData;
        }

        private void GetCSVFilePath()
        {
            using (OpenFileDialog importFile = new OpenFileDialog())
            {
                importFile.Filter = "CSV (Trennzeichen getrennt) (*.csv)|*.csv|Textdateien (*.txt)|*.txt|Microsoft Excel 2007-2016 XML (*.xlsx)|*.xlsx";
                importFile.Multiselect = false;

                if (importFile.ShowDialog() == DialogResult.OK)
                {
                    FilePath.Text = importFile.FileName;

                    if (importFile.FilterIndex == 1)                // .csv - file
                    {
                        ChoseExcelSheetOne.Enabled = false;
                        HasColumnNamesCsvTxt.Enabled = true;
                        DelimiterText.Enabled = true;

                        loadedFileFormat = FileFormat.CSV;
                        SelectedFileFormat.Text = "Dateiformat: .csv";
                    }
                    else if (importFile.FilterIndex == 2)           // .txt - file
                    {
                        ChoseExcelSheetOne.Enabled = false;
                        HasColumnNamesCsvTxt.Enabled = true;
                        DelimiterText.Enabled = true;

                        loadedFileFormat = FileFormat.Txt;
                        SelectedFileFormat.Text = "Dateiformat: .txt";
                    }
                    else if (importFile.FilterIndex == 3)           // .xlsx - file
                    {
                        HasColumnNamesCsvTxt.Enabled = false;
                        DelimiterText.Enabled = false;

                        loadedFileFormat = FileFormat.Xlsx;
                        SelectedFileFormat.Text = "Dateiformat: .xlsx";
                    }
                }
            }
        }

        private void FillSheetWithCSVData()
        {
            string pathOne = FilePath.Text;

            if (StockManagementMenu.CurrentSheetGrid != null)
            {
                if (StockManagementMenu.CurrentSheetGrid.Columns.Count > 0 || StockManagementMenu.CurrentSheetGrid.Rows.Count > 0)
                {
                    StockManagementMenu.CurrentSheetGrid.Columns.Clear();
                    StockManagementMenu.CurrentSheetGrid.Rows.Clear();
                    StockManagementMenu.CurrentSheetGrid.Refresh();
                }
            }

            // *********************************************
            // **************** Import file ****************
            // *********************************************
            using (TextFieldParser csvParser = new TextFieldParser(pathOne, System.Text.Encoding.Default))
            {
                int currentLine;

                csvParser.CommentTokens = new string[] { "#" };
                csvParser.TextFieldType = FieldType.Delimited;
                csvParser.SetDelimiters(new string[] { DelimiterText.Text });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                if (HasColumnNamesCsvTxt.Checked)
                {
                    csvParser.ReadLine();
                    string[] fields = csvParser.ReadFields();

                    StockManagementMenu.CurrentSheetGrid.ColumnCount = fields.Length;        // Datagridview with all of the data

                    for (int i = 0; i < fields.Length; i++)
                    {
                        StockManagementMenu.CurrentSheetGrid.Columns[i].Name = fields[i].ToString();
                        StockManagementMenu.CurrentSheetGrid.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                    }

                    currentLine = 0;// (int)(csvParser.LineNumber - 2);
                }
                else
                {
                    string[] fields = csvParser.ReadFields();

                    StockManagementMenu.CurrentSheetGrid.ColumnCount = fields.Length;

                    for (int i = 0; i < fields.Length; i++)
                    {
                        StockManagementMenu.CurrentSheetGrid.Columns[i].Name = "Spalte " + (i + 1);
                        StockManagementMenu.CurrentSheetGrid.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                    }

                    currentLine = 0;// (int)(csvParser.LineNumber - 1);
                }

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();

                    StockManagementMenu.CurrentSheetGrid.Rows.Add();

                    for (int j = 0; j < fields.Length; j++)
                    {
                        StockManagementMenu.CurrentSheetGrid.Rows[currentLine].Cells[j].Value = fields[j].ToString();
                    }

                    currentLine++;
                }
            }

            this.Close();
        }

        private void ImportDataFromFile()
        {
            using (OpenFileDialog importFile = new OpenFileDialog())
            {
                importFile.Filter = "CSV (Trennzeichen getrennt) (*.csv)|*.csv|Textdateien (*.txt)|*.txt|Microsoft Excel 2007-2016 XML (*.xlsx)|*.xlsx";
                importFile.Multiselect = false;

                try
                {
                    if (importFile.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show(Path.GetExtension(importFile.FileName));

                        DataTable newDataTable = new DataTable();
                        newDataTable.ExtendedProperties.Add("SheetIndex", /*currentSheetIndexPublic*/"");

                        switch (importFile.FilterIndex)
                        {
                            case 1: // Import .csv
                            {
                                //ImportDataFromFile(importFile.FileName, FileExtensions.csv);

                                // ============================ Import first file ============================
                                using (TextFieldParser csvParser = new TextFieldParser(importFile.FileName, System.Text.Encoding.Default))
                                {
                                    int currentLine;

                                    //csvParser.CommentTokens = new string[] { "#" };
                                    csvParser.TextFieldType = FieldType.Delimited;
                                    csvParser.SetDelimiters(new string[] { ";" });
                                    csvParser.HasFieldsEnclosedInQuotes = true;

                                    // Skip the row with the column names
                                    //if (CheckColumnHeaderOne.Checked)
                                    //{
                                    //    //csvParser.ReadLine();
                                    //    string[] fields = csvParser.ReadFields();

                                    //    CurrentDataGridViewPublic.ColumnCount = fields.Length;        // Datagridview with all of the data
                                    //    CurrentDataGridViewPublic.ColumnCount = fields.Length;     // Just contains the sum of the DataGridView
                                    //    CurrentDataGridViewPublic.Rows.Add();

                                    //    for (int i = 0; i < fields.Length; i++)
                                    //    {
                                    //        CurrentDataGridViewPublic.Columns[i].Name = fields[i].ToString();
                                    //        CurrentDataGridViewPublic.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                                    //    }

                                    //    currentLine = 0;// (int)(csvParser.LineNumber - 2);
                                    //}
                                    //else
                                    //{
                                    //    string[] fields = csvParser.ReadFields();

                                    //    CurrentDataGridViewPublic.ColumnCount = fields.Length;

                                    //    for (int i = 0; i < fields.Length; i++)
                                    //    {
                                    //        CurrentDataGridViewPublic.Columns[i].Name = "Spalte " + (i + 1);
                                    //        CurrentDataGridViewPublic.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                                    //    }

                                    //    currentLine = 0;// (int)(csvParser.LineNumber - 1);
                                    //}

                                    while (!csvParser.EndOfData)
                                    {
                                        // Read current line fields, pointer moves to the next line.
                                        string[] fields = csvParser.ReadFields();

                                        //CurrentSheet_Public.Rows.Add();

                                        for (int j = 0; j < fields.Length; j++)
                                        {
                                            //CurrentDataGridViewPublic.Rows[currentLine].Cells[j].Value = fields[j].ToString();
                                        }

                                        //currentLine++;
                                    }
                                }

                                //if (CurrentSheet_Public.Columns.Count > 0 || CurrentSheet_Public.Rows.Count > 0)
                                //{
                                //    CurrentSheet_Public.Columns.Clear();
                                //    CurrentSheet_Public.Rows.Clear();
                                //    CurrentSheet_Public.Refresh();
                                //}

                                // Try to calculate the sum
                                //mainForm.SheetOneTitlePublic.Text = Path.GetFileName(pathOne);
                                //mainForm.SheetOnePublic.Tag = Path.GetFileNameWithoutExtension(pathOne);

                            }
                            break;
                            case 2: // Import .txt
                            {
                                //LoadOrderFile(importFile.FileName, FileExtensions.txt);
                            }
                            break;
                            case 3: // Import .xlsx
                            {
                                //LoadOrderFile(importFile.FileName, FileExtensions.xlsx);
                            }
                            break;
                            default:
                                return;
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("Beim exportieren der Liste ist ein Fehler aufgetreten. Die Fehlermeldung lautet wie folgt:\n\n" + err.Message, "Liste exportieren", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //if (!TitleBarImportData.Enabled) { TitleBarImportData.Enabled = true; }

            // TODO: Clear
            //using (StreamReader sr = File.OpenText(orderFileLocation + @"/" + ".txt"))
            //{
            //    int lineCounter = 0;

            //    //Disable the default row
            //    currentDataGridView.AllowUserToAddRows = false;
            //    //Enable the boublebuffering for fast scrolling
            //    currentDataGridView.DoubleBuffered(true);

            //    string s = String.Empty;
            //    while ((s = sr.ReadLine()) != null)
            //    {
            //        DataRow dr = null;
            //        string[] wholeLine = s.Split('\t');
            //        int tabulatorCounter = 0;

            //        lineCounter++;

            //        if (string.IsNullOrWhiteSpace(s) == false)
            //        {
            //            foreach (string tabulator in wholeLine)
            //            {

            //                tabulatorCounter++;

            //                //Loads the whole file into the datagridview
            //                //if (lineCounter == 1 && tabulatorCounter > s.Count(x => x == '\t'))
            //                //{
            //                //    sheetOneDatatable.Columns.Add(tabulator);
            //                //}
            //                //else 
            //                if (lineCounter == 1)
            //                {
            //                    newDataTable.Columns.Add(tabulator);
            //                }
            //                else if (lineCounter != 1)
            //                {
            //                    if (dr == null) { dr = newDataTable.NewRow(); }

            //                    dr[tabulatorCounter - 1] = tabulator;
            //                    //dr[sheetOneDatatable.Columns.Count - 1] = (lineCounter - 2);
            //                }
            //            }

            //            if (dr != null) { newDataTable.Rows.Add(dr); }

            //            //If the file is already filled with customers then this code loads the contained text into the cells otherwise the code will skip this
            //            //if (lineCounter > 1 && xdoc.Root.Elements("CustomerData").Elements("Customer").Count() > 0)
            //            //{
            //            //    foreach (var x in xdoc.Root.Elements("CustomerData").Elements("Customer"))
            //            //    {
            //            //        //Compare the customer in the DataGridView and in the XML file
            //            //        if (x.FirstAttribute.Value == sheetOneDatatable.Rows[lineCounter - 2][2].ToString())
            //            //        {
            //            //            string commentXML = x.Element("Comment").Value;
            //            //            string lastStocktakingXML = x.Element("Last_stocktaking").Value;
            //            //            string stocktakingInfoXML = x.Element("Stocktaking_info").Value;

            //            //            sheetOneDatatable.Rows[lineCounter - 2][7] = commentXML;
            //            //            sheetOneDatatable.Rows[lineCounter - 2][8] = lastStocktakingXML;
            //            //            sheetOneDatatable.Rows[lineCounter - 2][9] = stocktakingInfoXML;

            //            //            continue;
            //            //        }
            //            //    }
            //            //}
            //        }
            //        else
            //        {
            //            continue;
            //        }
            //    }
            //}

            //// Asigning the current datatable
            //currentDatatable.Clear();
            //currentDatatable = newDataTable;

            //// Create a BindingSource the datatable to it
            //sheetOneBindingSource.DataSource = null;
            //sheetOneBindingSource.DataSource = newDataTable;

            //// Asign the BindingSource to the DataGridView
            //currentDataGridView.DataSource = sheetOneBindingSource;

            //datatableHolder.Add(newDataTable);






            //return;

            //string fileName = orderFileLocation + @"\";
            //string[] columns = null;
            //var lines = File.ReadAllLines(fileName + ".txt");
            //BindingSource newBindingSource = new BindingSource();

            //if (currentDataGridView.Rows.Count > 0)
            //{
            //    currentDataGridView.DataSource = null;

            //    currentDatatable.Columns.Clear();
            //    currentDatatable.Rows.Clear();

            //    int tabulatorCounter = 0;

            //    // assuming the first row contains the columns information
            //    if (lines.Count() > 0)
            //    {
            //        columns = lines[0].Split(new char[] { '\t' });

            //        foreach (var column in columns)
            //        {
            //            tabulatorCounter++;
            //            currentDatatable.Columns.Add("Column_").ColumnName = column.ToString();
            //        }
            //    }

            //    // Reading rest of the data
            //    for (int i = 1; i < lines.Count(); i++)
            //    {
            //        DataRow dr = currentDatatable.NewRow();
            //        string[] values = lines[i].Split(new char[] { '\t' });

            //        for (int j = 0; j < values.Count() && j < columns.Count(); j++)
            //        {
            //            dr[j] = values[j];
            //        }

            //        currentDatatable.Rows.Add(dr);
            //    }

            //    currentDataGridView.DoubleBuffered(true);                         // Enable the boublebuffering for fast scrolling
            //    currentDataGridView.VirtualMode = true;                           // Activate the virtual mode
            //    currentDataGridView.ReadOnly = true;
            //    currentDataGridView.AllowUserToAddRows = false;                   // Disable the default row
            //    currentDataGridView.AllowUserToDeleteRows = false;
            //    currentDataGridView.ColumnCount = currentDatatable.Columns.Count;
            //    currentDataGridView.RowCount = currentDatatable.Rows.Count;

            //    // Create a BindingSource and asign the datatable to it

            //    newBindingSource.DataSource = currentDatatable;

            //    // Asign the BindingSource to the DataGridView
            //    currentDataGridView.DataSource = newBindingSource;
            //}
            //else if (currentDataGridView.Rows.Count == 0)
            //{
            //    DataTable newDataTable1 = new DataTable();
            //    newDataTable.ExtendedProperties.Add("Tag", currentTab);

            //    using (StreamReader sr = File.OpenText(fileName + ".txt"))
            //    {
            //        int lineCounter = 0;


            //        //Disable the default row
            //        currentDataGridView.AllowUserToAddRows = false;
            //        //Enable the boublebuffering for fast scrolling
            //        currentDataGridView.DoubleBuffered(true);

            //        string s = String.Empty;
            //        while ((s = sr.ReadLine()) != null)
            //        {
            //            DataRow dr = null;
            //            string[] wholeLine = s.Split('\t');
            //            int tabulatorCounter = 0;

            //            lineCounter++;

            //            if (string.IsNullOrWhiteSpace(s) == false)
            //            {
            //                foreach (string tabulator in wholeLine)
            //                {

            //                    tabulatorCounter++;

            //                    //newDataTable.Columns.Add(tabulator).ColumnName = tabulator;

            //                    //Loads the whole file into the datagridview
            //                    if (lineCounter == 1)
            //                    {
            //                        newDataTable.Columns.Add("Column_" + tabulatorCounter);

            //                    }
            //                    else if (lineCounter != 1)
            //                    {
            //                        if (dr == null) { dr = newDataTable.NewRow(); }

            //                        dr[tabulatorCounter - 1] = tabulator;
            //                        //dr[newDataTable.Columns.Count - 1] = (lineCounter - 2);
            //                    }
            //                }

            //                if (dr != null) { newDataTable.Rows.Add(dr); }

            //            }
            //            else
            //            {
            //                continue;
            //            }
            //        }
            //    }

            //    currentDataGridView.DoubleBuffered(true);                         // Enable the boublebuffering for fast scrolling
            //    currentDataGridView.VirtualMode = true;                           // Activate the virtual mode
            //    currentDataGridView.ReadOnly = true;
            //    currentDataGridView.AllowUserToAddRows = false;                   // Disable the default row
            //    currentDataGridView.AllowUserToDeleteRows = false;
            //    currentDataGridView.ColumnCount = newDataTable.Columns.Count;
            //    currentDataGridView.RowCount = newDataTable.Rows.Count;

            //    // Create a BindingSource and asign the datatable to it

            //    newBindingSource.DataSource = newDataTable;

            //    // Asign the BindingSource to the DataGridView
            //    currentDataGridView.DataSource = newBindingSource;

            //    currentDatatable = newDataTable;
            //    datatableHolder.Add(newDataTable);

            //    return;
            //    // assuming the first row contains the columns information
            //    if (lines.Count() > 0)
            //    {
            //        columns = lines[0].Split(new char[] { '\t' });

            //        foreach (var column in columns)
            //        {
            //            newDataTable.Columns.Add(column).ColumnName = column;
            //        }
            //    }

            //    // Reading rest of the data
            //    for (int i = 1; i < lines.Count(); i++)
            //    {
            //        DataRow dr = newDataTable.NewRow();
            //        string[] values = lines[i].Split(new char[] { '\t' });

            //        if (i == 1)
            //        {
            //            foreach (var column in columns)
            //            {
            //                CoreFunctions.Msgbox(column);
            //                //newDataTable.Columns.Add(column).ColumnName = column.ToString();
            //            }
            //        }

            //        for (int j = 0; j < values.Count() && j < columns.Count(); j++)
            //        {
            //            dr[j] = values[j];
            //        }

            //        newDataTable.Rows.Add(dr);
            //    }


            //}
        }
        #endregion
    }
}
