namespace StockManagement
{
    partial class FileImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChooseFile = new System.Windows.Forms.Button();
            this.HasColumnNamesCsvTxt = new System.Windows.Forms.CheckBox();
            this.DelimiterText = new System.Windows.Forms.TextBox();
            this.DelimiterCsvTxt = new System.Windows.Forms.Label();
            this.FileImportTitle = new System.Windows.Forms.Label();
            this.ImportData = new System.Windows.Forms.Button();
            this.SelectedFilePath = new System.Windows.Forms.Label();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.ChoseExcelSheetOne = new System.Windows.Forms.ComboBox();
            this.XlsxSheetTitle = new System.Windows.Forms.Label();
            this.CSV_TXT_Title = new System.Windows.Forms.Label();
            this.XLSX_Title = new System.Windows.Forms.Label();
            this.SelectedFileFormat = new System.Windows.Forms.Label();
            this.SeparatorLineTwo = new System.Windows.Forms.Label();
            this.SeparatorLineThree = new System.Windows.Forms.Label();
            this.SeparatorLineOne = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChooseFile
            // 
            this.ChooseFile.Location = new System.Drawing.Point(558, 291);
            this.ChooseFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChooseFile.Name = "ChooseFile";
            this.ChooseFile.Size = new System.Drawing.Size(112, 35);
            this.ChooseFile.TabIndex = 0;
            this.ChooseFile.Text = "_choose";
            this.ChooseFile.UseVisualStyleBackColor = true;
            this.ChooseFile.Click += new System.EventHandler(this.GetSheetOne_Click);
            // 
            // HasColumnNamesCsvTxt
            // 
            this.HasColumnNamesCsvTxt.AutoSize = true;
            this.HasColumnNamesCsvTxt.Checked = true;
            this.HasColumnNamesCsvTxt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HasColumnNamesCsvTxt.Enabled = false;
            this.HasColumnNamesCsvTxt.Location = new System.Drawing.Point(18, 134);
            this.HasColumnNamesCsvTxt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.HasColumnNamesCsvTxt.Name = "HasColumnNamesCsvTxt";
            this.HasColumnNamesCsvTxt.Size = new System.Drawing.Size(174, 24);
            this.HasColumnNamesCsvTxt.TabIndex = 1;
            this.HasColumnNamesCsvTxt.Text = "_hasColumnNames";
            this.HasColumnNamesCsvTxt.UseVisualStyleBackColor = true;
            // 
            // DelimiterText
            // 
            this.DelimiterText.Enabled = false;
            this.DelimiterText.Location = new System.Drawing.Point(18, 194);
            this.DelimiterText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DelimiterText.Name = "DelimiterText";
            this.DelimiterText.Size = new System.Drawing.Size(148, 26);
            this.DelimiterText.TabIndex = 2;
            this.DelimiterText.Text = ";";
            // 
            // DelimiterCsvTxt
            // 
            this.DelimiterCsvTxt.AutoSize = true;
            this.DelimiterCsvTxt.Location = new System.Drawing.Point(18, 169);
            this.DelimiterCsvTxt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DelimiterCsvTxt.Name = "DelimiterCsvTxt";
            this.DelimiterCsvTxt.Size = new System.Drawing.Size(77, 20);
            this.DelimiterCsvTxt.TabIndex = 3;
            this.DelimiterCsvTxt.Text = "_delimiter";
            // 
            // FileImportTitle
            // 
            this.FileImportTitle.AutoSize = true;
            this.FileImportTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileImportTitle.Location = new System.Drawing.Point(18, 14);
            this.FileImportTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FileImportTitle.Name = "FileImportTitle";
            this.FileImportTitle.Size = new System.Drawing.Size(177, 37);
            this.FileImportTitle.TabIndex = 5;
            this.FileImportTitle.Text = "_fileImport";
            // 
            // ImportData
            // 
            this.ImportData.Location = new System.Drawing.Point(254, 360);
            this.ImportData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ImportData.Name = "ImportData";
            this.ImportData.Size = new System.Drawing.Size(210, 35);
            this.ImportData.TabIndex = 7;
            this.ImportData.Text = "_importData";
            this.ImportData.UseVisualStyleBackColor = true;
            this.ImportData.Click += new System.EventHandler(this.ApplyData_Click);
            // 
            // SelectedFilePath
            // 
            this.SelectedFilePath.AutoSize = true;
            this.SelectedFilePath.Location = new System.Drawing.Point(18, 298);
            this.SelectedFilePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SelectedFilePath.Name = "SelectedFilePath";
            this.SelectedFilePath.Size = new System.Drawing.Size(71, 20);
            this.SelectedFilePath.TabIndex = 9;
            this.SelectedFilePath.Text = "_filePath";
            // 
            // FilePath
            // 
            this.FilePath.Location = new System.Drawing.Point(111, 294);
            this.FilePath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            this.FilePath.Size = new System.Drawing.Size(436, 26);
            this.FilePath.TabIndex = 10;
            // 
            // ChoseExcelSheetOne
            // 
            this.ChoseExcelSheetOne.Enabled = false;
            this.ChoseExcelSheetOne.FormattingEnabled = true;
            this.ChoseExcelSheetOne.Location = new System.Drawing.Point(362, 165);
            this.ChoseExcelSheetOne.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChoseExcelSheetOne.Name = "ChoseExcelSheetOne";
            this.ChoseExcelSheetOne.Size = new System.Drawing.Size(150, 28);
            this.ChoseExcelSheetOne.TabIndex = 16;
            // 
            // XlsxSheetTitle
            // 
            this.XlsxSheetTitle.AutoSize = true;
            this.XlsxSheetTitle.Location = new System.Drawing.Point(357, 140);
            this.XlsxSheetTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.XlsxSheetTitle.Name = "XlsxSheetTitle";
            this.XlsxSheetTitle.Size = new System.Drawing.Size(58, 20);
            this.XlsxSheetTitle.TabIndex = 18;
            this.XlsxSheetTitle.Text = "_sheet";
            // 
            // CSV_TXT_Title
            // 
            this.CSV_TXT_Title.AutoSize = true;
            this.CSV_TXT_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CSV_TXT_Title.Location = new System.Drawing.Point(18, 89);
            this.CSV_TXT_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CSV_TXT_Title.Name = "CSV_TXT_Title";
            this.CSV_TXT_Title.Size = new System.Drawing.Size(137, 29);
            this.CSV_TXT_Title.TabIndex = 19;
            this.CSV_TXT_Title.Text = "CSV / Text";
            // 
            // XLSX_Title
            // 
            this.XLSX_Title.AutoSize = true;
            this.XLSX_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XLSX_Title.Location = new System.Drawing.Point(356, 89);
            this.XLSX_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.XLSX_Title.Name = "XLSX_Title";
            this.XLSX_Title.Size = new System.Drawing.Size(80, 29);
            this.XLSX_Title.TabIndex = 20;
            this.XLSX_Title.Text = "XLSX";
            // 
            // SelectedFileFormat
            // 
            this.SelectedFileFormat.AutoSize = true;
            this.SelectedFileFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedFileFormat.Location = new System.Drawing.Point(18, 258);
            this.SelectedFileFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SelectedFileFormat.Name = "SelectedFileFormat";
            this.SelectedFileFormat.Size = new System.Drawing.Size(93, 20);
            this.SelectedFileFormat.TabIndex = 21;
            this.SelectedFileFormat.Text = "_fileFormat";
            // 
            // SeparatorLineTwo
            // 
            this.SeparatorLineTwo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeparatorLineTwo.Location = new System.Drawing.Point(270, 89);
            this.SeparatorLineTwo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SeparatorLineTwo.Name = "SeparatorLineTwo";
            this.SeparatorLineTwo.Size = new System.Drawing.Size(2, 138);
            this.SeparatorLineTwo.TabIndex = 22;
            // 
            // SeparatorLineThree
            // 
            this.SeparatorLineThree.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeparatorLineThree.Location = new System.Drawing.Point(18, 242);
            this.SeparatorLineThree.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SeparatorLineThree.Name = "SeparatorLineThree";
            this.SeparatorLineThree.Size = new System.Drawing.Size(600, 2);
            this.SeparatorLineThree.TabIndex = 23;
            // 
            // SeparatorLineOne
            // 
            this.SeparatorLineOne.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeparatorLineOne.Location = new System.Drawing.Point(18, 68);
            this.SeparatorLineOne.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SeparatorLineOne.Name = "SeparatorLineOne";
            this.SeparatorLineOne.Size = new System.Drawing.Size(600, 2);
            this.SeparatorLineOne.TabIndex = 24;
            // 
            // FileImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 414);
            this.Controls.Add(this.SeparatorLineOne);
            this.Controls.Add(this.SeparatorLineThree);
            this.Controls.Add(this.SeparatorLineTwo);
            this.Controls.Add(this.SelectedFileFormat);
            this.Controls.Add(this.XLSX_Title);
            this.Controls.Add(this.CSV_TXT_Title);
            this.Controls.Add(this.XlsxSheetTitle);
            this.Controls.Add(this.ChoseExcelSheetOne);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.SelectedFilePath);
            this.Controls.Add(this.ImportData);
            this.Controls.Add(this.FileImportTitle);
            this.Controls.Add(this.DelimiterCsvTxt);
            this.Controls.Add(this.DelimiterText);
            this.Controls.Add(this.HasColumnNamesCsvTxt);
            this.Controls.Add(this.ChooseFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileImport";
            this.Text = "_fileImport";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ChooseFile;
        private System.Windows.Forms.CheckBox HasColumnNamesCsvTxt;
        private System.Windows.Forms.TextBox DelimiterText;
        private System.Windows.Forms.Label DelimiterCsvTxt;
        private System.Windows.Forms.Label FileImportTitle;
        private System.Windows.Forms.Button ImportData;
        private System.Windows.Forms.Label SelectedFilePath;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.ComboBox ChoseExcelSheetOne;
        private System.Windows.Forms.Label XlsxSheetTitle;
        private System.Windows.Forms.Label CSV_TXT_Title;
        private System.Windows.Forms.Label XLSX_Title;
        private System.Windows.Forms.Label SelectedFileFormat;
        private System.Windows.Forms.Label SeparatorLineTwo;
        private System.Windows.Forms.Label SeparatorLineThree;
        private System.Windows.Forms.Label SeparatorLineOne;
    }
}