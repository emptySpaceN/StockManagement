using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace StockManagementCore
{
    public class CoreFunctions
    {
        #region Variables
        private static FileLoggingSystem FileLogging = new FileLoggingSystem();
        //private static string notificationFilename = "_StockManagementFileLock.ini";  ======== OBSOLETE - Have a look over it again ========
        #endregion

        #region Functions
        // Method to refer the debug window directly
        public static void DebugWrite(object content)
        {
            System.Diagnostics.Debug.WriteLine(content);
        }

        public static void UnhandledExceptionCatcher(object sender, UnhandledExceptionEventArgs e)
        {
            CustomMessageBox.Show("Unhandled exception!\nException object: " + e.ExceptionObject.ToString());
            FileLogging.Log("Unhandled exception occured, the exception message/object is as follows: " + e.ExceptionObject.ToString());

            // TODO: Check if the following is still needed ======== OBSOLETE - Have a look over it again ========
            //try
            //{
            //    File.Delete(Path.GetTempPath() + notificationFilename);
            //}
            //catch (Exception)
            //{
            //}
        }

        public static string GetExecutingDirectoryName()
        {
            return Environment.CurrentDirectory+ "\\";
        }

        // TODO: There is an input box in the StockManagement class - choose one of those two and delete the other
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public static DialogResult SetColumnFormat(DataGridView _passedDataGridView = null, int _PassedColumnIndex = 0)
        {
            
            Form form = new Form();
            Label headingCategorie = new Label();
            Label headingFormat = new Label();
            ListBox listCategorie = new ListBox();
            ListBox listFormat = new ListBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            buttonOk.Click += (sender, e) =>
            {
                switch (listCategorie.SelectedItem.ToString())
                {
                    case "Date":
                    {
                        _passedDataGridView.Columns[_PassedColumnIndex].ValueType = typeof(DateTime);
                        _passedDataGridView.Columns[_PassedColumnIndex].DefaultCellStyle.Format = "dd.MM.yyyy";


                        foreach (DataGridViewRow asd in _passedDataGridView.Rows)
                        {
                            DateTime asdasdasd = new DateTime();
                            if (DateTime.TryParse(_passedDataGridView.Rows[asd.Index].Cells[_PassedColumnIndex].Value.ToString(), out asdasdasd))
                            {
                                DebugWrite(_passedDataGridView.Rows[asd.Index].Cells[_PassedColumnIndex].ValueType);
                                _passedDataGridView.Rows[asd.Index].Cells[_PassedColumnIndex].ValueType = typeof(DateTime);
                                _passedDataGridView.Rows[asd.Index].Cells[_PassedColumnIndex].Value = Convert.ToDateTime(_passedDataGridView.Rows[asd.Index].Cells[_PassedColumnIndex].Value.ToString()).ToString("dd.MM.yyyy");
                            }

                            ;
                        }
                    }
                    break;
                    case "Standard":
                    {
                        CustomMessageBox.Show("Standard selected");
                    }
                    break;
                    default:
                    {
                        //TODO: protocol it!
                        CustomMessageBox.Show(">" + listCategorie.SelectedItem.ToString() + "<");
                    }
                    break;
                }
                CustomMessageBox.Show("Name: " + _passedDataGridView.Columns[_PassedColumnIndex].Name + "\nFormat: " + _passedDataGridView.Columns[_PassedColumnIndex].DefaultCellStyle.Format + "\nValueType: " + _passedDataGridView.Columns[_PassedColumnIndex].ValueType.Name);
            };

            form.Text = "Set the column format";
            headingCategorie.Text = "Categorie";
            headingFormat.Text = "Format";
            headingCategorie.Font = new Font(headingCategorie.Font, FontStyle.Bold);
            headingFormat.Font = new Font(headingFormat.Font, FontStyle.Bold);
            listCategorie.Items.Add("Date");
            listCategorie.Items.Add("Standard");
            listCategorie.SelectedIndex = 0;
            listFormat.Items.Add("dd.MM.yyyy");

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            headingCategorie.SetBounds(9, 20, 372, 13);
            headingFormat.SetBounds(220, 20, 372, 13);
            listCategorie.SetBounds(12, 36, 200, 20);
            listFormat.SetBounds(220, 36, 200, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            headingCategorie.AutoSize = true;
            headingFormat.AutoSize = true;
            listCategorie.Anchor = listCategorie.Anchor | AnchorStyles.Right;
            listFormat.Anchor = listCategorie.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(500, 200);
            form.Controls.AddRange(new Control[] { headingCategorie, headingFormat, listCategorie, listFormat, buttonOk, buttonCancel });
            //form.ClientSize = new Size(Math.Max(300, headingCategorie.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;




            DialogResult dialogResult = form.ShowDialog();
            return dialogResult;
        }

        public static void SaveChanges(DataTable _PassedDatatableSheetOne, FileLocking _ConfigurationFile, XDocument _PassedXMLStream, int _passedColumnCount)
        {
            foreach (DataRow row in _PassedDatatableSheetOne.Rows)
            {
                List<string> xmlFileArray = new List<string>();

                foreach (XElement x in _PassedXMLStream.Element("ConfigFile").Element("CustomerData").Elements("Customer"))
                {
                    string commentRow = x.Element("Comment").Value ?? "";

                    xmlFileArray.Add(x.FirstAttribute.Value);
                    xmlFileArray.Add(commentRow);
                }

                //string toDisplay = string.Join(Environment.NewLine, xmlFileArray);
                //MessageBox.Show(toDisplay);


                // Checks the array for customer ids, if they're in overwrite all elements within every customer id
                if (xmlFileArray.Contains(row[2].ToString()))
                {
                    try
                    {
                        string commentRow = (row[_passedColumnCount].ToString() == null) ? "" : row[_passedColumnCount].ToString();
                        string inventoryRow = (row[_passedColumnCount + 1].ToString() == null) ? "" : row[_passedColumnCount + 1].ToString();
                        string dateRow = (row[_passedColumnCount + 2].ToString() == null) ? "" : row[_passedColumnCount + 2].ToString();
                        XElement xmlElements = _PassedXMLStream.Element("ConfigFile").Element("CustomerData").Elements("Customer").Single(x => x.Attribute("ID").Value == row[2].ToString());
                        //MessageBox.Show(commentRow + Environment.NewLine + inventoryRow + dateRow);
                        xmlElements.Element("Comment").Value = commentRow;
                        xmlElements.Element("Last_stocktaking").Value = inventoryRow;
                        xmlElements.Element("Stocktaking_info").Value = dateRow;
                    }
                    catch (Exception _error)
                    {
                        // TODO: Localisize it
                        CustomMessageBox.Show("Failure: " + _error.Message.ToString());
                        //TODO: Protocol it
                    }
                }
                else    // If they're not in the XML file, create them
                {
                    string customerID = (row[2].ToString() == null) ? "" : row[2].ToString();
                    string customerName = (row[0].ToString() == null) ? "" : row[0].ToString();
                    string commentRow = (row[_passedColumnCount].ToString() == null) ? "" : row[_passedColumnCount].ToString();
                    string inventoryRow = (row[_passedColumnCount + 1].ToString() == null) ? "" : row[_passedColumnCount + 1].ToString();
                    string dateRow = (row[_passedColumnCount + 2].ToString() == null) ? "" : row[_passedColumnCount + 2].ToString();

                    _PassedXMLStream.Element("ConfigFile").Element("CustomerData").Add(new XElement("Customer", new XAttribute("ID", customerID), new XAttribute("Name", customerName), new XElement("Comment", commentRow), new XElement("Last_stocktaking", inventoryRow), new XElement("Stocktaking_info", dateRow)));
                }
            }

            _ConfigurationFile.WriteXDocument(_PassedXMLStream);
            //_ConfigurationFile.SetLength(0);
            //_PassedXMLStream.Save(_ConfigurationFile);

            // TODO: Clear - isn't needed anymore, cause when the configuration file is locked it already exists and there is no need that I check that
            //// Save all EDI relevant customer data
            //using (XmlWriter writer = XmlWriter.Create(_ConfigurationFile))
            //{
            //    writer.WriteStartDocument();

            //    writer.WriteComment("This file is generated by the program. It contains all necessary data for the EDI customers");

            //    writer.WriteStartElement("ConfigFile");
            //    writer.WriteStartElement("FileData");
            //    writer.WriteStartElement("File_content");

            //    writer.WriteElementString("CustomerFile", "");

            //    writer.WriteEndElement();
            //    writer.WriteEndElement();

            //    writer.WriteStartElement("CustomerData");

            //    foreach (DataRow row in _PassedDatatableSheetOne.Rows)
            //    {
            //        string idRow = (row[2].ToString() == null) ? "" : row[2].ToString();
            //        string commentRow = (row[7].ToString() == null) ? "" : row[7].ToString();
            //        string inventoryRow = (row[8].ToString() == null) ? "" : row[8].ToString();
            //        string dateRow = (row[9].ToString() == null) ? "" : row[9].ToString();

            //        writer.WriteStartElement("Customer");
            //        writer.WriteAttributeString("ID", idRow);
            //        writer.WriteElementString("Comment", commentRow);
            //        writer.WriteElementString("Last_stocktaking", inventoryRow);
            //        writer.WriteElementString("Stocktaking_info", dateRow);
            //        writer.WriteEndElement();
            //    }

            //    writer.WriteEndElement();
            //    writer.WriteEndElement();
            //    writer.WriteEndDocument();
            //}
        }

        public static void SearchFunction(DataGridView passedDataGridView, DataTable passedDatatable, string passedSearchString, StringComparison passedUpperLowerCase, Form _PassedSearchWindow, out bool _currentCellChanged, out DataGridViewCell _newCurrentCell)
        {
            _currentCellChanged = false;
            _newCurrentCell = null;

            bool found = false;

            List<int> searchArray = new List<int>();

            int columnCount = passedDataGridView.Columns.Count;
            int rowCount = 0;
            int cellCount = 0;
            uint occurrenceCount = 0;
            int currentRow = passedDataGridView.CurrentCell.RowIndex;
            int currentCell = passedDataGridView.CurrentCell.ColumnIndex + 1;

            // If textbox has some text
            if (passedSearchString != "")
            {
                // Loop through the datagridview if string exists
                for (int i = 0; i < passedDataGridView.Rows.Count; i++) // Loop over the rows.
                {
                    rowCount++;

                    for (int j = 0; j < passedDataGridView.Columns.Count; j++)
                    {
                        cellCount++;

                        if (passedDataGridView.Rows[i].Cells[j].Value == null ? false : (passedDataGridView.Rows[i].Cells[j].Value.ToString()).Contains(passedSearchString, passedUpperLowerCase))
                        {
                            occurrenceCount++;

                            if (!found) { found = true; }

                            searchArray.Add(columnCount * i + (j + 1));                     // Contains the counted cell number
                            searchArray.Add(i + 1);                                         // Contains the row number
                            searchArray.Add(j + 1);                                         // Contains the row number
                        }
                    }
                }

                // If the string was found
                if (found)
                {
                    if (occurrenceCount == 1)
                    {
                        // Check for next cellPosition             
                        int result = searchArray[0];

                        // Get the correct column- and rownumber out of the list
                        int rowIndex = searchArray.IndexOf(result) + 1;
                        int columnIndex = searchArray.IndexOf(result) + 2;

                        _newCurrentCell = passedDataGridView.Rows[searchArray[rowIndex] - 1].Cells[searchArray[columnIndex] - 1];
                        _currentCellChanged = true;
                        //passedDataGridView.CurrentCell = passedDataGridView.Rows[searchArray[rowIndex] - 1].Cells[searchArray[columnIndex] - 1];
                    }
                    else
                    {
                        int firstElement = searchArray[0];

                        // CurrentCell number
                        int cellNumber = ((currentRow) * passedDataGridView.ColumnCount) + currentCell;

                        // Check for next cellPosition             
                        int result = (searchArray.Where(x => x > cellNumber).Take(1).SingleOrDefault() == 0 ? firstElement : searchArray.Where(x => x > cellNumber).Take(1).SingleOrDefault());
                        int rowIndex = searchArray.IndexOf(result) + 1;
                        int columnIndex = searchArray.IndexOf(result) + 2;
                        
                        _newCurrentCell = passedDataGridView.Rows[searchArray[rowIndex] - 1].Cells[searchArray[columnIndex] - 1];
                        _currentCellChanged = true;
                        //passedDataGridView.CurrentCell = passedDataGridView.Rows[searchArray[rowIndex] - 1].Cells[searchArray[columnIndex] - 1];
                    }
                    
                    // TODO: Implement the rectangle
                    if (_PassedSearchWindow != null)
                    {
                        Rectangle selectedCellRectangle = new Rectangle();
                        Rectangle searchWindowRectangle = _PassedSearchWindow.ClientRectangle;
                        Debug.Print(_PassedSearchWindow.Width + " - " + _PassedSearchWindow.Top + " - " + _PassedSearchWindow.PointToScreen(Point.Empty) + " - " + _PassedSearchWindow.ClientSize.Width);
                        //_PassedSearchWindow.Left = 0;
                        //_PassedSearchWindow.Top = 0;

                        selectedCellRectangle.Location = new Point(passedDataGridView.Left + passedDataGridView.GetCellDisplayRectangle(passedDataGridView.CurrentCell.ColumnIndex, passedDataGridView.CurrentCell.RowIndex, false).Left, passedDataGridView.Top + passedDataGridView.GetCellDisplayRectangle(passedDataGridView.CurrentCell.ColumnIndex, passedDataGridView.CurrentCell.RowIndex, false).Top);

                        selectedCellRectangle.Width = passedDataGridView.GetCellDisplayRectangle(passedDataGridView.CurrentCell.ColumnIndex, passedDataGridView.CurrentCell.RowIndex, false).Width;

                        selectedCellRectangle.Height = passedDataGridView.GetCellDisplayRectangle(passedDataGridView.CurrentCell.ColumnIndex, passedDataGridView.CurrentCell.RowIndex, false).Height;

                        searchWindowRectangle.Width = _PassedSearchWindow.Width;
                        searchWindowRectangle.Height = _PassedSearchWindow.Height;
                        searchWindowRectangle.Location = _PassedSearchWindow.Owner.PointToClient(_PassedSearchWindow.Location);
                        
                        //MessageBox.Show(_PassedSearchWindow.Height.ToString());

                        if (selectedCellRectangle.IntersectsWith(searchWindowRectangle))
                        {
                            //MessageBox.Show(selectedCellRectangle.ToString() + Environment.NewLine + searchWindowRectangle.ToString() + Environment.NewLine + passedDataGridView.Parent.Name + Environment.NewLine + _PassedSearchWindow.Owner.Name);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Text nicht gefunden.");
                }
            }
            else if (passedSearchString == "")
            {
                MessageBox.Show("Sie haben keinen Suchtext eingegeben.");
            }
        }

        public static void UpdateCellContent(DataGridView passedDataGridView, DataTable passedDataTable, int columnIndex, int rowIndex)
        {
            //if (customerOverview.Columns[columnIndex].Name == "Letzte Inv." /*&& DateTime.TryParse((customerOverview.Rows[rowIndexDataGridView].Cells[columnIndex].Value == null) ? "" : customerOverview.Rows[rowIndexDataGridView].Cells[columnIndex].Value.ToString(), out datetime)*/ && DateTime.TryParseExact((customerOverview.Rows[rowIndexDataGridView].Cells[columnIndex].Value == null) ? "" : customerOverview.Rows[rowIndexDataGridView].Cells[columnIndex].Value.ToString(), "dd.mm.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out datetime))
            //MessageBox.Show(passedDataTable.Rows[rowIndexDataGridView][columnIndex].ToString());
            
            int dataTableRowIndex = int.Parse(passedDataGridView.Rows[rowIndex].Cells[passedDataGridView.Columns.Count - 1].Value.ToString());
            //MessageBox.Show((passedDataTable.Columns.Count - 2).ToString() + " - " + columnIndex);
            if ((passedDataTable.Columns.Count - 3) == columnIndex && DateTime.TryParseExact(passedDataTable.Rows[dataTableRowIndex][columnIndex].ToString(), "dd.mm.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out DateTime datetime))
            {
                DateTime start = DateTime.Now.Date;
                DateTime end = Convert.ToDateTime(passedDataTable.Rows[dataTableRowIndex][columnIndex].ToString());
                DateTime threeMonths = DateTime.Now.Date.AddMonths(-3);
                
                if (end <= threeMonths)
                {
                    // Asign the new cell content to the datatable
                    passedDataTable.Rows[dataTableRowIndex][columnIndex + 1] = "Inventur fällig";

                    passedDataGridView.Rows[rowIndex].Cells[columnIndex + 1].Style.BackColor = Color.Red;
                }
                else if (end > threeMonths)
                {
                    string days = ("Inventur in " + (start - end.AddMonths(3)).ToString("dd") + " Tagen fällig" == "Inventur in 00 Tagen fällig") ? "Inventur fällig" : "Inventur in " + (start - end.AddMonths(3)).ToString("dd") + " Tagen fällig";

                    if (days == "Inventur fällig")
                    {
                        passedDataTable.Rows[dataTableRowIndex][columnIndex + 1] = days;
                        passedDataGridView.Rows[rowIndex].Cells[columnIndex + 1].Style.BackColor = Color.Red;
                    }
                    else if (days != "Inventur fällig")
                    {
                        passedDataTable.Rows[dataTableRowIndex][columnIndex + 1] = days;
                        passedDataGridView.Rows[rowIndex].Cells[columnIndex + 1].Style.BackColor = Color.Empty;
                    }

                }
                //else if (passedDataTable.Rows[dataTableRowIndex][columnIndex - 3].ToString() == "Konsignation")
                //{
                //    passedDataTable.Rows[dataTableRowIndex][columnIndex + 1] = "Konsignation";

                //    passedDataGridView.Rows[rowIndex].Cells[columnIndex + 1].Style.BackColor = Color.FromArgb(222, 222, 222);
                //}
            }
            else if (passedDataTable.Columns[columnIndex].ColumnName == "Letzte Inv.")//  && (customerOverview.Rows[rowIndexDatatable].Cells[columnIndex + 1].Value.ToString().Length > 0));       // Add a check option to the options menu, to choose if the value will be deleted or not
            {
                passedDataTable.Rows[dataTableRowIndex][columnIndex + 1] = "";
                passedDataGridView.Rows[rowIndex].Cells[columnIndex + 1].Style.BackColor = Color.Empty;
            }
        }

        public static string ReloadCurrentUser(ref FileLocking _PassedFileStream, ref XDocument _PassedXdoc, string _PassedCurrentUser, ref Exception _PassedError)
        {
            _PassedFileStream = null;
            _PassedXdoc = null;
            // TODO: Check the empty string in "ReadFile"
            _PassedXdoc = XDocument.Load(_PassedFileStream.ReadFile("", ref _PassedError));

            _PassedCurrentUser = _PassedXdoc.Element("ConfigFile").Element("ApplicationSettings").Element("GeneralSettings").Element("CurrentUser").Value.ToString();

            return _PassedCurrentUser;
        }
        #endregion
    }

    // New extension method for string
    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }

    // Fixes the datagridview slow scrolling problem
    public static class ExtensionMethods
    {
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        public static int AddToFront(this List<DataTable> list, int item)
        {
            // omits validation, etc.
            MessageBox.Show("asd");

            return item;
        }
    }

    // Class to hold the donwload address and application name
    public class Track
    {
        public string ApplicationName { get; set; }
        public string DownloadAddress { get; set; }
    }
}
