namespace StockManagement
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.DataFileTitle = new System.Windows.Forms.Label();
            this.DataFileText = new System.Windows.Forms.TextBox();
            this.BrowseDataFile = new System.Windows.Forms.Button();
            this.AdditionalSettingsGroup = new System.Windows.Forms.GroupBox();
            this.DelimiterTitle = new System.Windows.Forms.Label();
            this.LanguageTitle = new System.Windows.Forms.Label();
            this.DelimiterText = new System.Windows.Forms.ComboBox();
            this.LanguageHolder = new System.Windows.Forms.ComboBox();
            this.BackgroundPanel = new System.Windows.Forms.Panel();
            this.DataPreviewTitle = new System.Windows.Forms.Label();
            this.DataPreview = new System.Windows.Forms.DataGridView();
            this.AdditionalSettingsGroup.SuspendLayout();
            this.BackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // DataFileTitle
            // 
            this.DataFileTitle.AutoSize = true;
            this.DataFileTitle.Location = new System.Drawing.Point(411, 14);
            this.DataFileTitle.Name = "DataFileTitle";
            this.DataFileTitle.Size = new System.Drawing.Size(96, 13);
            this.DataFileTitle.TabIndex = 0;
            this.DataFileTitle.Text = "_customer file path";
            // 
            // DataFileText
            // 
            this.DataFileText.Location = new System.Drawing.Point(414, 30);
            this.DataFileText.Name = "DataFileText";
            this.DataFileText.Size = new System.Drawing.Size(320, 20);
            this.DataFileText.TabIndex = 2;
            // 
            // BrowseDataFile
            // 
            this.BrowseDataFile.Location = new System.Drawing.Point(738, 27);
            this.BrowseDataFile.Name = "BrowseDataFile";
            this.BrowseDataFile.Size = new System.Drawing.Size(75, 23);
            this.BrowseDataFile.TabIndex = 3;
            this.BrowseDataFile.Text = "_browse";
            this.BrowseDataFile.UseVisualStyleBackColor = true;
            this.BrowseDataFile.Click += new System.EventHandler(this.BrowseDataFile_Click);
            // 
            // AdditionalSettingsGroup
            // 
            this.AdditionalSettingsGroup.Controls.Add(this.DelimiterTitle);
            this.AdditionalSettingsGroup.Controls.Add(this.LanguageTitle);
            this.AdditionalSettingsGroup.Controls.Add(this.DelimiterText);
            this.AdditionalSettingsGroup.Controls.Add(this.LanguageHolder);
            this.AdditionalSettingsGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AdditionalSettingsGroup.Location = new System.Drawing.Point(15, 14);
            this.AdditionalSettingsGroup.Name = "AdditionalSettingsGroup";
            this.AdditionalSettingsGroup.Size = new System.Drawing.Size(383, 105);
            this.AdditionalSettingsGroup.TabIndex = 11;
            this.AdditionalSettingsGroup.TabStop = false;
            this.AdditionalSettingsGroup.Text = "_additionalSettings";
            // 
            // DelimiterTitle
            // 
            this.DelimiterTitle.AutoSize = true;
            this.DelimiterTitle.Location = new System.Drawing.Point(5, 62);
            this.DelimiterTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DelimiterTitle.Name = "DelimiterTitle";
            this.DelimiterTitle.Size = new System.Drawing.Size(51, 13);
            this.DelimiterTitle.TabIndex = 10;
            this.DelimiterTitle.Text = "_delimiter";
            // 
            // LanguageTitle
            // 
            this.LanguageTitle.AutoSize = true;
            this.LanguageTitle.Location = new System.Drawing.Point(5, 16);
            this.LanguageTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LanguageTitle.Name = "LanguageTitle";
            this.LanguageTitle.Size = new System.Drawing.Size(57, 13);
            this.LanguageTitle.TabIndex = 9;
            this.LanguageTitle.Text = "_language";
            // 
            // DelimiterText
            // 
            this.DelimiterText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DelimiterText.FormattingEnabled = true;
            this.DelimiterText.Location = new System.Drawing.Point(7, 78);
            this.DelimiterText.Name = "DelimiterText";
            this.DelimiterText.Size = new System.Drawing.Size(121, 21);
            this.DelimiterText.TabIndex = 8;
            this.DelimiterText.SelectionChangeCommitted += new System.EventHandler(this.DelimiterText_SelectionChangeCommitted);
            // 
            // LanguageHolder
            // 
            this.LanguageHolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageHolder.FormattingEnabled = true;
            this.LanguageHolder.Location = new System.Drawing.Point(5, 32);
            this.LanguageHolder.Name = "LanguageHolder";
            this.LanguageHolder.Size = new System.Drawing.Size(121, 21);
            this.LanguageHolder.TabIndex = 5;
            this.LanguageHolder.SelectionChangeCommitted += new System.EventHandler(this.LanguageHolder_SelectionChangeCommitted);
            // 
            // BackgroundPanel
            // 
            this.BackgroundPanel.BackColor = System.Drawing.Color.White;
            this.BackgroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BackgroundPanel.Controls.Add(this.DataPreviewTitle);
            this.BackgroundPanel.Controls.Add(this.DataPreview);
            this.BackgroundPanel.Controls.Add(this.AdditionalSettingsGroup);
            this.BackgroundPanel.Controls.Add(this.BrowseDataFile);
            this.BackgroundPanel.Controls.Add(this.DataFileTitle);
            this.BackgroundPanel.Controls.Add(this.DataFileText);
            this.BackgroundPanel.Location = new System.Drawing.Point(12, 12);
            this.BackgroundPanel.Name = "BackgroundPanel";
            this.BackgroundPanel.Size = new System.Drawing.Size(831, 353);
            this.BackgroundPanel.TabIndex = 12;
            // 
            // DataPreviewTitle
            // 
            this.DataPreviewTitle.AutoSize = true;
            this.DataPreviewTitle.Location = new System.Drawing.Point(12, 122);
            this.DataPreviewTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DataPreviewTitle.Name = "DataPreviewTitle";
            this.DataPreviewTitle.Size = new System.Drawing.Size(72, 13);
            this.DataPreviewTitle.TabIndex = 13;
            this.DataPreviewTitle.Text = "_dataPreview";
            // 
            // DataPreview
            // 
            this.DataPreview.AllowUserToAddRows = false;
            this.DataPreview.BackgroundColor = System.Drawing.Color.CornflowerBlue;
            this.DataPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataPreview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataPreview.Cursor = System.Windows.Forms.Cursors.Default;
            this.DataPreview.Location = new System.Drawing.Point(15, 138);
            this.DataPreview.Name = "DataPreview";
            this.DataPreview.ReadOnly = true;
            this.DataPreview.Size = new System.Drawing.Size(801, 198);
            this.DataPreview.TabIndex = 12;
            this.DataPreview.Tag = "1";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 373);
            this.Controls.Add(this.BackgroundPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "_settings";
            this.AdditionalSettingsGroup.ResumeLayout(false);
            this.AdditionalSettingsGroup.PerformLayout();
            this.BackgroundPanel.ResumeLayout(false);
            this.BackgroundPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DataFileTitle;
        private System.Windows.Forms.TextBox DataFileText;
        private System.Windows.Forms.Button BrowseDataFile;
        private System.Windows.Forms.GroupBox AdditionalSettingsGroup;
        private System.Windows.Forms.ComboBox LanguageHolder;
        private System.Windows.Forms.Panel BackgroundPanel;
        private System.Windows.Forms.Label DataPreviewTitle;
        private System.Windows.Forms.DataGridView DataPreview;
        private System.Windows.Forms.ComboBox DelimiterText;
        private System.Windows.Forms.Label LanguageTitle;
        private System.Windows.Forms.Label DelimiterTitle;
    }
}