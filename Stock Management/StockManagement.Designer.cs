namespace StockManagement
{
    partial class StockManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockManagement));
            this.PanelOptions = new System.Windows.Forms.Panel();
            this.RedoChange = new System.Windows.Forms.Button();
            this.UndoChange = new System.Windows.Forms.Button();
            this.TitleBarRootFolder = new System.Windows.Forms.Button();
            this.SeparatorLineFive = new System.Windows.Forms.Label();
            this.TitleBarImportData = new FlatButton();
            this.TitleBarExportData = new FlatButton();
            this.TitleBarSettings = new FlatButton();
            this.TitleBarAdvancedSearch = new FlatButton();
            this.TitleBarSaveChanges = new System.Windows.Forms.Button();
            this.SeparatorLineOne = new System.Windows.Forms.Label();
            this.TitleBarDeleteFilters = new System.Windows.Forms.Button();
            this.TitleBarUpdate = new System.Windows.Forms.Button();
            this.TitleBarUnusedValues = new System.Windows.Forms.Button();
            this.TitleBarAbout = new System.Windows.Forms.Button();
            this.SeparatorLineFour = new System.Windows.Forms.Label();
            this.SeparatorLineThree = new System.Windows.Forms.Label();
            this.SeparatorLineTwo = new System.Windows.Forms.Label();
            this.findTxtbx = new System.Windows.Forms.TextBox();
            this.TitleBarBasicSearch = new System.Windows.Forms.Button();
            this.DefaultDataGrid = new System.Windows.Forms.DataGridView();
            this.TextInputBox = new System.Windows.Forms.TextBox();
            this.PanelFilterWindow = new System.Windows.Forms.Panel();
            this.FilterWindowBorderOne = new System.Windows.Forms.Label();
            this.SortColumnDescending = new System.Windows.Forms.Button();
            this.SortColumnAscending = new System.Windows.Forms.Button();
            this.FilterInputBox = new System.Windows.Forms.TextBox();
            this.AllFilterCheck = new System.Windows.Forms.CheckBox();
            this.FilterCancel = new System.Windows.Forms.Button();
            this.FilterOk = new System.Windows.Forms.Button();
            this.FilterCheckedListbox = new System.Windows.Forms.CheckedListBox();
            this.StatusBar = new System.Windows.Forms.Panel();
            this.StatusBarRecordsFound = new System.Windows.Forms.Label();
            this.StatusBarSelectionMenu = new System.Windows.Forms.Label();
            this.SelectNextSheet = new System.Windows.Forms.Button();
            this.SelectPreviousSheet = new System.Windows.Forms.Button();
            this.AddNewSheet = new System.Windows.Forms.Button();
            this.TabSheetHolder = new System.Windows.Forms.TabControl();
            this.tabSheetOne = new System.Windows.Forms.TabPage();
            this.PanelOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DefaultDataGrid)).BeginInit();
            this.PanelFilterWindow.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.TabSheetHolder.SuspendLayout();
            this.tabSheetOne.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelOptions
            // 
            this.PanelOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.PanelOptions.Controls.Add(this.RedoChange);
            this.PanelOptions.Controls.Add(this.UndoChange);
            this.PanelOptions.Controls.Add(this.TitleBarRootFolder);
            this.PanelOptions.Controls.Add(this.SeparatorLineFive);
            this.PanelOptions.Controls.Add(this.TitleBarImportData);
            this.PanelOptions.Controls.Add(this.TitleBarExportData);
            this.PanelOptions.Controls.Add(this.TitleBarSettings);
            this.PanelOptions.Controls.Add(this.TitleBarAdvancedSearch);
            this.PanelOptions.Controls.Add(this.TitleBarSaveChanges);
            this.PanelOptions.Controls.Add(this.SeparatorLineOne);
            this.PanelOptions.Controls.Add(this.TitleBarDeleteFilters);
            this.PanelOptions.Controls.Add(this.TitleBarUpdate);
            this.PanelOptions.Controls.Add(this.TitleBarUnusedValues);
            this.PanelOptions.Controls.Add(this.TitleBarAbout);
            this.PanelOptions.Controls.Add(this.SeparatorLineFour);
            this.PanelOptions.Controls.Add(this.SeparatorLineThree);
            this.PanelOptions.Controls.Add(this.SeparatorLineTwo);
            this.PanelOptions.Controls.Add(this.findTxtbx);
            this.PanelOptions.Controls.Add(this.TitleBarBasicSearch);
            this.PanelOptions.Location = new System.Drawing.Point(33, 12);
            this.PanelOptions.Name = "PanelOptions";
            this.PanelOptions.Size = new System.Drawing.Size(1110, 115);
            this.PanelOptions.TabIndex = 0;
            this.PanelOptions.Click += new System.EventHandler(this.PanelOptions_Click);
            // 
            // RedoChange
            // 
            this.RedoChange.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.RedoChange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.RedoChange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.RedoChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RedoChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.RedoChange.Image = global::StockManagement.Properties.Resources.Redo_16x16;
            this.RedoChange.Location = new System.Drawing.Point(130, 82);
            this.RedoChange.Name = "RedoChange";
            this.RedoChange.Size = new System.Drawing.Size(16, 16);
            this.RedoChange.TabIndex = 47;
            this.RedoChange.TabStop = false;
            this.RedoChange.UseVisualStyleBackColor = false;
            this.RedoChange.Click += new System.EventHandler(this.button8_Click);
            // 
            // UndoChange
            // 
            this.UndoChange.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.UndoChange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.UndoChange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.UndoChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UndoChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.UndoChange.Image = global::StockManagement.Properties.Resources.Undo_16x16;
            this.UndoChange.Location = new System.Drawing.Point(108, 82);
            this.UndoChange.Name = "UndoChange";
            this.UndoChange.Size = new System.Drawing.Size(16, 16);
            this.UndoChange.TabIndex = 46;
            this.UndoChange.TabStop = false;
            this.UndoChange.UseVisualStyleBackColor = false;
            this.UndoChange.Click += new System.EventHandler(this.button7_Click);
            // 
            // TitleBarRootFolder
            // 
            this.TitleBarRootFolder.Location = new System.Drawing.Point(957, 7);
            this.TitleBarRootFolder.Name = "TitleBarRootFolder";
            this.TitleBarRootFolder.Size = new System.Drawing.Size(126, 23);
            this.TitleBarRootFolder.TabIndex = 40;
            this.TitleBarRootFolder.Text = "_applicationDir";
            this.TitleBarRootFolder.UseVisualStyleBackColor = true;
            this.TitleBarRootFolder.Click += new System.EventHandler(this.button1_Click);
            // 
            // SeparatorLineFive
            // 
            this.SeparatorLineFive.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeparatorLineFive.Location = new System.Drawing.Point(950, 7);
            this.SeparatorLineFive.Name = "SeparatorLineFive";
            this.SeparatorLineFive.Size = new System.Drawing.Size(1, 97);
            this.SeparatorLineFive.TabIndex = 39;
            // 
            // TitleBarImportData
            // 
            this.TitleBarImportData.BackColor = System.Drawing.Color.Transparent;
            this.TitleBarImportData.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TitleBarImportData.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TitleBarImportData.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TitleBarImportData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleBarImportData.Image = global::StockManagement.Properties.Resources.ReloadData_32x96;
            this.TitleBarImportData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleBarImportData.Location = new System.Drawing.Point(585, 58);
            this.TitleBarImportData.Name = "TitleBarImportData";
            this.TitleBarImportData.Size = new System.Drawing.Size(142, 40);
            this.TitleBarImportData.TabIndex = 38;
            this.TitleBarImportData.TabStop = false;
            this.TitleBarImportData.Text = "_importData";
            this.TitleBarImportData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TitleBarImportData.UseVisualStyleBackColor = false;
            this.TitleBarImportData.Click += new System.EventHandler(this.TitleBarImportData_Click);
            this.TitleBarImportData.MouseEnter += new System.EventHandler(this.TitleBarImportData_MouseEnter);
            this.TitleBarImportData.MouseLeave += new System.EventHandler(this.TitleBarImportData_MouseLeave);
            // 
            // TitleBarExportData
            // 
            this.TitleBarExportData.BackColor = System.Drawing.Color.Transparent;
            this.TitleBarExportData.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TitleBarExportData.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TitleBarExportData.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TitleBarExportData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleBarExportData.Image = global::StockManagement.Properties.Resources.Search_32x32;
            this.TitleBarExportData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleBarExportData.Location = new System.Drawing.Point(594, 10);
            this.TitleBarExportData.Name = "TitleBarExportData";
            this.TitleBarExportData.Size = new System.Drawing.Size(142, 40);
            this.TitleBarExportData.TabIndex = 37;
            this.TitleBarExportData.TabStop = false;
            this.TitleBarExportData.Text = "_exportTable";
            this.TitleBarExportData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TitleBarExportData.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.TitleBarExportData.UseVisualStyleBackColor = false;
            this.TitleBarExportData.Click += new System.EventHandler(this.TitleBarExportData_Click);
            // 
            // TitleBarSettings
            // 
            this.TitleBarSettings.BackColor = System.Drawing.Color.Transparent;
            this.TitleBarSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TitleBarSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TitleBarSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TitleBarSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleBarSettings.Image = global::StockManagement.Properties.Resources.Settings_32x32;
            this.TitleBarSettings.Location = new System.Drawing.Point(866, 30);
            this.TitleBarSettings.Name = "TitleBarSettings";
            this.TitleBarSettings.Size = new System.Drawing.Size(85, 74);
            this.TitleBarSettings.TabIndex = 36;
            this.TitleBarSettings.TabStop = false;
            this.TitleBarSettings.Text = "_settings";
            this.TitleBarSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TitleBarSettings.UseVisualStyleBackColor = false;
            this.TitleBarSettings.Click += new System.EventHandler(this.flatButton1_Click);
            // 
            // TitleBarAdvancedSearch
            // 
            this.TitleBarAdvancedSearch.BackColor = System.Drawing.Color.Transparent;
            this.TitleBarAdvancedSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TitleBarAdvancedSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TitleBarAdvancedSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TitleBarAdvancedSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleBarAdvancedSearch.Image = global::StockManagement.Properties.Resources.Search_32x32;
            this.TitleBarAdvancedSearch.Location = new System.Drawing.Point(216, 3);
            this.TitleBarAdvancedSearch.Name = "TitleBarAdvancedSearch";
            this.TitleBarAdvancedSearch.Size = new System.Drawing.Size(110, 71);
            this.TitleBarAdvancedSearch.TabIndex = 35;
            this.TitleBarAdvancedSearch.TabStop = false;
            this.TitleBarAdvancedSearch.Text = "_advancedSearch";
            this.TitleBarAdvancedSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TitleBarAdvancedSearch.UseVisualStyleBackColor = false;
            this.TitleBarAdvancedSearch.Click += new System.EventHandler(this.TitleBarAdvancedSearch_Click);
            // 
            // TitleBarSaveChanges
            // 
            this.TitleBarSaveChanges.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TitleBarSaveChanges.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TitleBarSaveChanges.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TitleBarSaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleBarSaveChanges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.TitleBarSaveChanges.Image = global::StockManagement.Properties.Resources.SaveChanges_32x96;
            this.TitleBarSaveChanges.Location = new System.Drawing.Point(12, 7);
            this.TitleBarSaveChanges.Name = "TitleBarSaveChanges";
            this.TitleBarSaveChanges.Size = new System.Drawing.Size(65, 94);
            this.TitleBarSaveChanges.TabIndex = 26;
            this.TitleBarSaveChanges.TabStop = false;
            this.TitleBarSaveChanges.Text = "_save";
            this.TitleBarSaveChanges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TitleBarSaveChanges.UseVisualStyleBackColor = false;
            this.TitleBarSaveChanges.Click += new System.EventHandler(this.TitleBarSaveChanges_Click);
            this.TitleBarSaveChanges.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBarSaveChanges_MouseDown);
            this.TitleBarSaveChanges.MouseEnter += new System.EventHandler(this.TitleBarSaveChanges_MouseEnter);
            this.TitleBarSaveChanges.MouseLeave += new System.EventHandler(this.TitleBarSaveChanges_MouseLeave);
            // 
            // SeparatorLineOne
            // 
            this.SeparatorLineOne.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeparatorLineOne.Location = new System.Drawing.Point(192, 7);
            this.SeparatorLineOne.Name = "SeparatorLineOne";
            this.SeparatorLineOne.Size = new System.Drawing.Size(1, 97);
            this.SeparatorLineOne.TabIndex = 13;
            // 
            // TitleBarDeleteFilters
            // 
            this.TitleBarDeleteFilters.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TitleBarDeleteFilters.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TitleBarDeleteFilters.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TitleBarDeleteFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleBarDeleteFilters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.TitleBarDeleteFilters.Image = global::StockManagement.Properties.Resources.DeleteFilterDisabled_16x16;
            this.TitleBarDeleteFilters.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleBarDeleteFilters.Location = new System.Drawing.Point(85, 13);
            this.TitleBarDeleteFilters.Name = "TitleBarDeleteFilters";
            this.TitleBarDeleteFilters.Size = new System.Drawing.Size(101, 25);
            this.TitleBarDeleteFilters.TabIndex = 27;
            this.TitleBarDeleteFilters.TabStop = false;
            this.TitleBarDeleteFilters.Text = "_deleteFilter";
            this.TitleBarDeleteFilters.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TitleBarDeleteFilters.UseVisualStyleBackColor = false;
            this.TitleBarDeleteFilters.Click += new System.EventHandler(this.TitleBarResetFilters_Click);
            this.TitleBarDeleteFilters.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBarResetFilters_MouseDown);
            this.TitleBarDeleteFilters.MouseEnter += new System.EventHandler(this.TitleBarResetFilters_MouseEnter);
            this.TitleBarDeleteFilters.MouseLeave += new System.EventHandler(this.TitleBarResetFilters_MouseLeave);
            // 
            // TitleBarUpdate
            // 
            this.TitleBarUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TitleBarUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TitleBarUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TitleBarUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleBarUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TitleBarUpdate.Image = global::StockManagement.Properties.Resources.Update_16x16;
            this.TitleBarUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleBarUpdate.Location = new System.Drawing.Point(766, 8);
            this.TitleBarUpdate.Name = "TitleBarUpdate";
            this.TitleBarUpdate.Size = new System.Drawing.Size(105, 25);
            this.TitleBarUpdate.TabIndex = 32;
            this.TitleBarUpdate.TabStop = false;
            this.TitleBarUpdate.Text = "_update";
            this.TitleBarUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TitleBarUpdate.UseVisualStyleBackColor = false;
            this.TitleBarUpdate.Click += new System.EventHandler(this.TitleBarUpdate_Click);
            this.TitleBarUpdate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBarUpdate_MouseDown);
            this.TitleBarUpdate.MouseEnter += new System.EventHandler(this.TitleBarUpdate_MouseEnter);
            this.TitleBarUpdate.MouseLeave += new System.EventHandler(this.TitleBarUpdate_MouseLeave);
            // 
            // TitleBarUnusedValues
            // 
            this.TitleBarUnusedValues.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TitleBarUnusedValues.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TitleBarUnusedValues.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TitleBarUnusedValues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleBarUnusedValues.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TitleBarUnusedValues.Image = global::StockManagement.Properties.Resources.ManageOldCustomer_32x32;
            this.TitleBarUnusedValues.Location = new System.Drawing.Point(470, 10);
            this.TitleBarUnusedValues.Name = "TitleBarUnusedValues";
            this.TitleBarUnusedValues.Size = new System.Drawing.Size(102, 88);
            this.TitleBarUnusedValues.TabIndex = 30;
            this.TitleBarUnusedValues.TabStop = false;
            this.TitleBarUnusedValues.Text = "_unusedValues";
            this.TitleBarUnusedValues.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.TitleBarUnusedValues.UseVisualStyleBackColor = false;
            this.TitleBarUnusedValues.Click += new System.EventHandler(this.TitleBarUnusedValues_Click);
            this.TitleBarUnusedValues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBarUnusedValues_MouseDown);
            this.TitleBarUnusedValues.MouseEnter += new System.EventHandler(this.TitleBarUnusedValues_MouseEnter);
            this.TitleBarUnusedValues.MouseLeave += new System.EventHandler(this.TitleBarUnusedValues_MouseLeave);
            this.TitleBarUnusedValues.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TitleBarUnusedValues_MouseUp);
            // 
            // TitleBarAbout
            // 
            this.TitleBarAbout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.TitleBarAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.TitleBarAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.TitleBarAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TitleBarAbout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TitleBarAbout.Image = global::StockManagement.Properties.Resources.Help_Information_16x16;
            this.TitleBarAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleBarAbout.Location = new System.Drawing.Point(766, 39);
            this.TitleBarAbout.Name = "TitleBarAbout";
            this.TitleBarAbout.Size = new System.Drawing.Size(105, 25);
            this.TitleBarAbout.TabIndex = 29;
            this.TitleBarAbout.TabStop = false;
            this.TitleBarAbout.Text = "_about";
            this.TitleBarAbout.UseVisualStyleBackColor = false;
            this.TitleBarAbout.Click += new System.EventHandler(this.TitleBarAbout_Click);
            this.TitleBarAbout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBarAbout_MouseDown);
            this.TitleBarAbout.MouseEnter += new System.EventHandler(this.TitleBarAbout_MouseEnter);
            this.TitleBarAbout.MouseLeave += new System.EventHandler(this.TitleBarAbout_MouseLeave);
            // 
            // SeparatorLineFour
            // 
            this.SeparatorLineFour.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeparatorLineFour.Location = new System.Drawing.Point(759, 8);
            this.SeparatorLineFour.Name = "SeparatorLineFour";
            this.SeparatorLineFour.Size = new System.Drawing.Size(1, 96);
            this.SeparatorLineFour.TabIndex = 16;
            // 
            // SeparatorLineThree
            // 
            this.SeparatorLineThree.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeparatorLineThree.Location = new System.Drawing.Point(578, 8);
            this.SeparatorLineThree.Name = "SeparatorLineThree";
            this.SeparatorLineThree.Size = new System.Drawing.Size(1, 96);
            this.SeparatorLineThree.TabIndex = 15;
            // 
            // SeparatorLineTwo
            // 
            this.SeparatorLineTwo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SeparatorLineTwo.Location = new System.Drawing.Point(455, 7);
            this.SeparatorLineTwo.Name = "SeparatorLineTwo";
            this.SeparatorLineTwo.Size = new System.Drawing.Size(1, 97);
            this.SeparatorLineTwo.TabIndex = 14;
            // 
            // findTxtbx
            // 
            this.findTxtbx.Location = new System.Drawing.Point(332, 47);
            this.findTxtbx.Name = "findTxtbx";
            this.findTxtbx.Size = new System.Drawing.Size(116, 20);
            this.findTxtbx.TabIndex = 9;
            this.findTxtbx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.findTxtbx_KeyDown);
            // 
            // TitleBarBasicSearch
            // 
            this.TitleBarBasicSearch.Location = new System.Drawing.Point(369, 73);
            this.TitleBarBasicSearch.Name = "TitleBarBasicSearch";
            this.TitleBarBasicSearch.Size = new System.Drawing.Size(79, 23);
            this.TitleBarBasicSearch.TabIndex = 8;
            this.TitleBarBasicSearch.Text = "_search";
            this.TitleBarBasicSearch.UseVisualStyleBackColor = true;
            this.TitleBarBasicSearch.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // DefaultDataGrid
            // 
            this.DefaultDataGrid.AllowUserToAddRows = false;
            this.DefaultDataGrid.BackgroundColor = System.Drawing.Color.CornflowerBlue;
            this.DefaultDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DefaultDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DefaultDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DefaultDataGrid.Cursor = System.Windows.Forms.Cursors.Default;
            this.DefaultDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DefaultDataGrid.Location = new System.Drawing.Point(2, 2);
            this.DefaultDataGrid.Name = "DefaultDataGrid";
            this.DefaultDataGrid.Size = new System.Drawing.Size(902, 230);
            this.DefaultDataGrid.TabIndex = 0;
            this.DefaultDataGrid.Tag = "1";
            this.DefaultDataGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DefaultDataGrid_CellBeginEdit);
            this.DefaultDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DefaultDataGrid_CellEndEdit);
            this.DefaultDataGrid.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DefaultDataGrid_CellMouseMove);
            this.DefaultDataGrid.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DefaultDataGrid_CellPainting);
            this.DefaultDataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DefaultDataGrid_ColumnHeaderMouseClick);
            this.DefaultDataGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DefaultDataGrid_EditingControlShowing);
            this.DefaultDataGrid.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DefaultDataGrid_RowPostPaint);
            this.DefaultDataGrid.SelectionChanged += new System.EventHandler(this.DefaultDataGrid_SelectionChanged);
            this.DefaultDataGrid.Enter += new System.EventHandler(this.DefaultDataGrid_Enter);
            this.DefaultDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DefaultDataGrid_KeyDown);
            this.DefaultDataGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DefaultDataGrid_RightClickMenu);
            // 
            // TextInputBox
            // 
            this.TextInputBox.Location = new System.Drawing.Point(33, 133);
            this.TextInputBox.Name = "TextInputBox";
            this.TextInputBox.Size = new System.Drawing.Size(924, 20);
            this.TextInputBox.TabIndex = 9;
            this.TextInputBox.TextChanged += new System.EventHandler(this.TextInputBox_TextChanged);
            this.TextInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextInputBox_KeyDown);
            // 
            // PanelFilterWindow
            // 
            this.PanelFilterWindow.Controls.Add(this.FilterWindowBorderOne);
            this.PanelFilterWindow.Controls.Add(this.SortColumnDescending);
            this.PanelFilterWindow.Controls.Add(this.SortColumnAscending);
            this.PanelFilterWindow.Controls.Add(this.FilterInputBox);
            this.PanelFilterWindow.Controls.Add(this.AllFilterCheck);
            this.PanelFilterWindow.Controls.Add(this.FilterCancel);
            this.PanelFilterWindow.Controls.Add(this.FilterOk);
            this.PanelFilterWindow.Controls.Add(this.FilterCheckedListbox);
            this.PanelFilterWindow.Location = new System.Drawing.Point(963, 133);
            this.PanelFilterWindow.Name = "PanelFilterWindow";
            this.PanelFilterWindow.Size = new System.Drawing.Size(180, 320);
            this.PanelFilterWindow.TabIndex = 19;
            this.PanelFilterWindow.Leave += new System.EventHandler(this.PanelFilterWindow_Leave);
            // 
            // FilterWindowBorderOne
            // 
            this.FilterWindowBorderOne.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FilterWindowBorderOne.Location = new System.Drawing.Point(3, 62);
            this.FilterWindowBorderOne.Name = "FilterWindowBorderOne";
            this.FilterWindowBorderOne.Size = new System.Drawing.Size(150, 1);
            this.FilterWindowBorderOne.TabIndex = 33;
            // 
            // SortColumnDescending
            // 
            this.SortColumnDescending.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.SortColumnDescending.FlatAppearance.BorderSize = 0;
            this.SortColumnDescending.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SortColumnDescending.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(240)))), ((int)(((byte)(252)))));
            this.SortColumnDescending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SortColumnDescending.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SortColumnDescending.Image = global::StockManagement.Properties.Resources.SortDescending_16x16;
            this.SortColumnDescending.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SortColumnDescending.Location = new System.Drawing.Point(4, 34);
            this.SortColumnDescending.Name = "SortColumnDescending";
            this.SortColumnDescending.Size = new System.Drawing.Size(149, 25);
            this.SortColumnDescending.TabIndex = 0;
            this.SortColumnDescending.TabStop = false;
            this.SortColumnDescending.Text = "_sortDescending";
            this.SortColumnDescending.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SortColumnDescending.UseVisualStyleBackColor = false;
            this.SortColumnDescending.Click += new System.EventHandler(this.SortColumnDescending_Click);
            // 
            // SortColumnAscending
            // 
            this.SortColumnAscending.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.SortColumnAscending.FlatAppearance.BorderSize = 0;
            this.SortColumnAscending.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SortColumnAscending.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(240)))), ((int)(((byte)(252)))));
            this.SortColumnAscending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SortColumnAscending.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SortColumnAscending.Image = global::StockManagement.Properties.Resources.SortAscending_16x16;
            this.SortColumnAscending.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SortColumnAscending.Location = new System.Drawing.Point(4, 3);
            this.SortColumnAscending.Name = "SortColumnAscending";
            this.SortColumnAscending.Size = new System.Drawing.Size(149, 25);
            this.SortColumnAscending.TabIndex = 0;
            this.SortColumnAscending.TabStop = false;
            this.SortColumnAscending.Text = "_sortAscending";
            this.SortColumnAscending.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SortColumnAscending.UseVisualStyleBackColor = false;
            this.SortColumnAscending.Click += new System.EventHandler(this.SortColumnAscending_Click);
            // 
            // FilterInputBox
            // 
            this.FilterInputBox.Location = new System.Drawing.Point(3, 66);
            this.FilterInputBox.Name = "FilterInputBox";
            this.FilterInputBox.Size = new System.Drawing.Size(150, 20);
            this.FilterInputBox.TabIndex = 17;
            this.FilterInputBox.TextChanged += new System.EventHandler(this.FilterInputBox_TextChanged);
            this.FilterInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FilterInputBox_KeyDown);
            this.FilterInputBox.Leave += new System.EventHandler(this.FilterInputBox_Leave);
            // 
            // AllFilterCheck
            // 
            this.AllFilterCheck.AutoSize = true;
            this.AllFilterCheck.Location = new System.Drawing.Point(3, 252);
            this.AllFilterCheck.Name = "AllFilterCheck";
            this.AllFilterCheck.Size = new System.Drawing.Size(71, 17);
            this.AllFilterCheck.TabIndex = 19;
            this.AllFilterCheck.Text = "_selectAll";
            this.AllFilterCheck.UseVisualStyleBackColor = true;
            this.AllFilterCheck.Click += new System.EventHandler(this.AllFilterCheck_Click);
            this.AllFilterCheck.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AllFilterCheck_KeyDown);
            this.AllFilterCheck.Leave += new System.EventHandler(this.AllFilterCheck_Leave);
            // 
            // FilterCancel
            // 
            this.FilterCancel.Location = new System.Drawing.Point(78, 275);
            this.FilterCancel.Name = "FilterCancel";
            this.FilterCancel.Size = new System.Drawing.Size(75, 23);
            this.FilterCancel.TabIndex = 21;
            this.FilterCancel.Text = "_cancel";
            this.FilterCancel.UseVisualStyleBackColor = true;
            this.FilterCancel.Click += new System.EventHandler(this.filterCancelBtn_Click);
            this.FilterCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filterCancelBtn_KeyDown);
            // 
            // FilterOk
            // 
            this.FilterOk.Location = new System.Drawing.Point(3, 275);
            this.FilterOk.Name = "FilterOk";
            this.FilterOk.Size = new System.Drawing.Size(75, 23);
            this.FilterOk.TabIndex = 20;
            this.FilterOk.Text = "_ok";
            this.FilterOk.UseVisualStyleBackColor = true;
            this.FilterOk.Click += new System.EventHandler(this.filterOKBtn_Click);
            this.FilterOk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filterOKBtn_KeyDown);
            this.FilterOk.Leave += new System.EventHandler(this.filterOKBtn_Leave);
            // 
            // FilterCheckedListbox
            // 
            this.FilterCheckedListbox.CheckOnClick = true;
            this.FilterCheckedListbox.FormattingEnabled = true;
            this.FilterCheckedListbox.Location = new System.Drawing.Point(3, 92);
            this.FilterCheckedListbox.Name = "FilterCheckedListbox";
            this.FilterCheckedListbox.Size = new System.Drawing.Size(150, 154);
            this.FilterCheckedListbox.TabIndex = 18;
            this.FilterCheckedListbox.SelectedValueChanged += new System.EventHandler(this.filterCheckedListbox_SelectedValueChanged);
            this.FilterCheckedListbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.filterCheckedListbox_KeyDown);
            this.FilterCheckedListbox.Leave += new System.EventHandler(this.filterCheckedListbox_Leave);
            // 
            // StatusBar
            // 
            this.StatusBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.StatusBar.Controls.Add(this.StatusBarRecordsFound);
            this.StatusBar.Controls.Add(this.StatusBarSelectionMenu);
            this.StatusBar.Location = new System.Drawing.Point(33, 485);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(1113, 20);
            this.StatusBar.TabIndex = 41;
            // 
            // StatusBarRecordsFound
            // 
            this.StatusBarRecordsFound.AutoSize = true;
            this.StatusBarRecordsFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusBarRecordsFound.ForeColor = System.Drawing.Color.White;
            this.StatusBarRecordsFound.Location = new System.Drawing.Point(9, 0);
            this.StatusBarRecordsFound.Name = "StatusBarRecordsFound";
            this.StatusBarRecordsFound.Size = new System.Drawing.Size(78, 13);
            this.StatusBarRecordsFound.TabIndex = 37;
            this.StatusBarRecordsFound.Text = "_recordsFound";
            this.StatusBarRecordsFound.MouseEnter += new System.EventHandler(this.selectionBarTextOne_MouseEnter);
            this.StatusBarRecordsFound.MouseLeave += new System.EventHandler(this.selectionBarTextOne_MouseLeave);
            // 
            // StatusBarSelectionMenu
            // 
            this.StatusBarSelectionMenu.AutoSize = true;
            this.StatusBarSelectionMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusBarSelectionMenu.ForeColor = System.Drawing.Color.White;
            this.StatusBarSelectionMenu.Location = new System.Drawing.Point(206, 0);
            this.StatusBarSelectionMenu.Name = "StatusBarSelectionMenu";
            this.StatusBarSelectionMenu.Size = new System.Drawing.Size(82, 13);
            this.StatusBarSelectionMenu.TabIndex = 36;
            this.StatusBarSelectionMenu.Text = "_selectionMenu";
            this.StatusBarSelectionMenu.MouseEnter += new System.EventHandler(this.selectionBarText_MouseEnter);
            this.StatusBarSelectionMenu.MouseLeave += new System.EventHandler(this.selectionBarText_MouseLeave);
            this.StatusBarSelectionMenu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.selectionBarText_MouseUp);
            // 
            // SelectNextSheet
            // 
            this.SelectNextSheet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.SelectNextSheet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SelectNextSheet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.SelectNextSheet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectNextSheet.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SelectNextSheet.Image = global::StockManagement.Properties.Resources.SelectNext_16x16;
            this.SelectNextSheet.Location = new System.Drawing.Point(63, 432);
            this.SelectNextSheet.Name = "SelectNextSheet";
            this.SelectNextSheet.Size = new System.Drawing.Size(21, 21);
            this.SelectNextSheet.TabIndex = 35;
            this.SelectNextSheet.TabStop = false;
            this.SelectNextSheet.UseVisualStyleBackColor = false;
            this.SelectNextSheet.Click += new System.EventHandler(this.SelectNextSheet_Click);
            // 
            // SelectPreviousSheet
            // 
            this.SelectPreviousSheet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.SelectPreviousSheet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SelectPreviousSheet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.SelectPreviousSheet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectPreviousSheet.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SelectPreviousSheet.Image = global::StockManagement.Properties.Resources.SelectPrevious_16x16;
            this.SelectPreviousSheet.Location = new System.Drawing.Point(36, 432);
            this.SelectPreviousSheet.Name = "SelectPreviousSheet";
            this.SelectPreviousSheet.Size = new System.Drawing.Size(21, 21);
            this.SelectPreviousSheet.TabIndex = 34;
            this.SelectPreviousSheet.TabStop = false;
            this.SelectPreviousSheet.UseVisualStyleBackColor = false;
            this.SelectPreviousSheet.Click += new System.EventHandler(this.SelectPreviousSheet_Click);
            // 
            // AddNewSheet
            // 
            this.AddNewSheet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(222)))), ((int)(((byte)(222)))));
            this.AddNewSheet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.AddNewSheet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.AddNewSheet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddNewSheet.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AddNewSheet.Image = global::StockManagement.Properties.Resources.AddNewSheet_16x16;
            this.AddNewSheet.Location = new System.Drawing.Point(89, 432);
            this.AddNewSheet.Name = "AddNewSheet";
            this.AddNewSheet.Size = new System.Drawing.Size(21, 21);
            this.AddNewSheet.TabIndex = 33;
            this.AddNewSheet.TabStop = false;
            this.AddNewSheet.UseVisualStyleBackColor = false;
            this.AddNewSheet.Click += new System.EventHandler(this.AddNewSheet_Click);
            // 
            // TabSheetHolder
            // 
            this.TabSheetHolder.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.TabSheetHolder.Controls.Add(this.tabSheetOne);
            this.TabSheetHolder.Location = new System.Drawing.Point(33, 155);
            this.TabSheetHolder.Margin = new System.Windows.Forms.Padding(2);
            this.TabSheetHolder.Name = "TabSheetHolder";
            this.TabSheetHolder.SelectedIndex = 0;
            this.TabSheetHolder.Size = new System.Drawing.Size(914, 260);
            this.TabSheetHolder.TabIndex = 42;
            this.TabSheetHolder.SelectedIndexChanged += new System.EventHandler(this.TabSheetHolder_SelectedIndexChanged);
            this.TabSheetHolder.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabSheetHolder_MouseClick);
            this.TabSheetHolder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TabSheetHolder_MouseDoubleClick);
            // 
            // tabSheetOne
            // 
            this.tabSheetOne.Controls.Add(this.DefaultDataGrid);
            this.tabSheetOne.Location = new System.Drawing.Point(4, 4);
            this.tabSheetOne.Margin = new System.Windows.Forms.Padding(2);
            this.tabSheetOne.Name = "tabSheetOne";
            this.tabSheetOne.Padding = new System.Windows.Forms.Padding(2);
            this.tabSheetOne.Size = new System.Drawing.Size(906, 234);
            this.tabSheetOne.TabIndex = 0;
            this.tabSheetOne.Text = "_sheetOne";
            this.tabSheetOne.UseVisualStyleBackColor = true;
            // 
            // StockManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(1164, 621);
            this.Controls.Add(this.TabSheetHolder);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.SelectNextSheet);
            this.Controls.Add(this.PanelOptions);
            this.Controls.Add(this.SelectPreviousSheet);
            this.Controls.Add(this.TextInputBox);
            this.Controls.Add(this.AddNewSheet);
            this.Controls.Add(this.PanelFilterWindow);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(878, 654);
            this.Name = "StockManagement";
            this.Text = "_stockManagement";
            this.PanelOptions.ResumeLayout(false);
            this.PanelOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DefaultDataGrid)).EndInit();
            this.PanelFilterWindow.ResumeLayout(false);
            this.PanelFilterWindow.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.TabSheetHolder.ResumeLayout(false);
            this.tabSheetOne.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelOptions;
        private System.Windows.Forms.Label SeparatorLineFour;
        private System.Windows.Forms.Label SeparatorLineThree;
        private System.Windows.Forms.Label SeparatorLineTwo;
        private System.Windows.Forms.Label SeparatorLineOne;
        private System.Windows.Forms.TextBox findTxtbx;
        private System.Windows.Forms.Button TitleBarBasicSearch;
        private System.Windows.Forms.TextBox TextInputBox;
        private System.Windows.Forms.Panel PanelFilterWindow;
        private System.Windows.Forms.CheckBox AllFilterCheck;
        private System.Windows.Forms.Button FilterCancel;
        private System.Windows.Forms.Button FilterOk;
        private System.Windows.Forms.CheckedListBox FilterCheckedListbox;
        private System.Windows.Forms.TextBox FilterInputBox;
        private System.Windows.Forms.Button TitleBarSaveChanges;
        private System.Windows.Forms.Button TitleBarDeleteFilters;
        private System.Windows.Forms.Button TitleBarAbout;
        private System.Windows.Forms.Button TitleBarUnusedValues;
        private System.Windows.Forms.Button SortColumnDescending;
        private System.Windows.Forms.Button SortColumnAscending;
        private System.Windows.Forms.Label FilterWindowBorderOne;
        private System.Windows.Forms.Button TitleBarUpdate;
        private System.Windows.Forms.DataGridView DefaultDataGrid;
        private System.Windows.Forms.Button AddNewSheet;
        private System.Windows.Forms.Button SelectNextSheet;
        private System.Windows.Forms.Button SelectPreviousSheet;
        private FlatButton TitleBarAdvancedSearch;
        private FlatButton TitleBarSettings;
        private System.Windows.Forms.Panel StatusBar;
        private System.Windows.Forms.Label StatusBarSelectionMenu;
        private System.Windows.Forms.Label StatusBarRecordsFound;
        private FlatButton TitleBarExportData;
        private System.Windows.Forms.Label SeparatorLineFive;
        private System.Windows.Forms.Button TitleBarRootFolder;
        private System.Windows.Forms.Button RedoChange;
        private System.Windows.Forms.Button UndoChange;
        private System.Windows.Forms.TabPage tabSheetOne;
        public System.Windows.Forms.TabControl TabSheetHolder;
        public FlatButton TitleBarImportData;
    }
}