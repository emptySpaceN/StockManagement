namespace StockManagement
{
    partial class SearchWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchWindow));
            this.TextBoxSearchString = new System.Windows.Forms.TextBox();
            this.SearchNext = new System.Windows.Forms.Button();
            this.CheckUpperLowerCase = new System.Windows.Forms.CheckBox();
            this.LabelSearchText = new System.Windows.Forms.Label();
            this.PanelOptions = new System.Windows.Forms.Panel();
            this.AvailableSearchSheetsHolder = new System.Windows.Forms.ComboBox();
            this.AvailableSearchSheetsTitle = new System.Windows.Forms.Label();
            this.SearchAll = new System.Windows.Forms.Button();
            this.CloseForm = new System.Windows.Forms.Button();
            this.PanelOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxSearchString
            // 
            this.TextBoxSearchString.Location = new System.Drawing.Point(163, 29);
            this.TextBoxSearchString.Name = "TextBoxSearchString";
            this.TextBoxSearchString.Size = new System.Drawing.Size(250, 20);
            this.TextBoxSearchString.TabIndex = 0;
            this.TextBoxSearchString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxSearchString_KeyDown);
            // 
            // SearchNext
            // 
            this.SearchNext.Location = new System.Drawing.Point(228, 154);
            this.SearchNext.Name = "SearchNext";
            this.SearchNext.Size = new System.Drawing.Size(116, 23);
            this.SearchNext.TabIndex = 3;
            this.SearchNext.Text = "_searchNext";
            this.SearchNext.UseVisualStyleBackColor = true;
            this.SearchNext.Click += new System.EventHandler(this.ButtonSearchNext_Click);
            // 
            // CheckUpperLowerCase
            // 
            this.CheckUpperLowerCase.AutoSize = true;
            this.CheckUpperLowerCase.Location = new System.Drawing.Point(163, 55);
            this.CheckUpperLowerCase.Name = "CheckUpperLowerCase";
            this.CheckUpperLowerCase.Size = new System.Drawing.Size(112, 17);
            this.CheckUpperLowerCase.TabIndex = 1;
            this.CheckUpperLowerCase.Text = "_upperLowerCase";
            this.CheckUpperLowerCase.UseVisualStyleBackColor = true;
            // 
            // LabelSearchText
            // 
            this.LabelSearchText.AutoSize = true;
            this.LabelSearchText.Location = new System.Drawing.Point(38, 32);
            this.LabelSearchText.Name = "LabelSearchText";
            this.LabelSearchText.Size = new System.Drawing.Size(77, 13);
            this.LabelSearchText.TabIndex = 11;
            this.LabelSearchText.Text = "_text to search";
            // 
            // PanelOptions
            // 
            this.PanelOptions.BackColor = System.Drawing.Color.White;
            this.PanelOptions.Controls.Add(this.AvailableSearchSheetsHolder);
            this.PanelOptions.Controls.Add(this.AvailableSearchSheetsTitle);
            this.PanelOptions.Controls.Add(this.CheckUpperLowerCase);
            this.PanelOptions.Controls.Add(this.LabelSearchText);
            this.PanelOptions.Controls.Add(this.TextBoxSearchString);
            this.PanelOptions.Location = new System.Drawing.Point(12, 12);
            this.PanelOptions.Name = "PanelOptions";
            this.PanelOptions.Size = new System.Drawing.Size(454, 136);
            this.PanelOptions.TabIndex = 5;
            // 
            // AvailableSearchSheetsHolder
            // 
            this.AvailableSearchSheetsHolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AvailableSearchSheetsHolder.FormattingEnabled = true;
            this.AvailableSearchSheetsHolder.Location = new System.Drawing.Point(82, 108);
            this.AvailableSearchSheetsHolder.Name = "AvailableSearchSheetsHolder";
            this.AvailableSearchSheetsHolder.Size = new System.Drawing.Size(121, 21);
            this.AvailableSearchSheetsHolder.TabIndex = 13;
            this.AvailableSearchSheetsHolder.SelectedValueChanged += new System.EventHandler(this.AvailableSearchSheetsHolder_SelectedValueChanged);
            // 
            // AvailableSearchSheetsTitle
            // 
            this.AvailableSearchSheetsTitle.AutoSize = true;
            this.AvailableSearchSheetsTitle.Location = new System.Drawing.Point(12, 111);
            this.AvailableSearchSheetsTitle.Name = "AvailableSearchSheetsTitle";
            this.AvailableSearchSheetsTitle.Size = new System.Drawing.Size(65, 13);
            this.AvailableSearchSheetsTitle.TabIndex = 12;
            this.AvailableSearchSheetsTitle.Text = "_currentGrid";
            // 
            // SearchAll
            // 
            this.SearchAll.Location = new System.Drawing.Point(143, 154);
            this.SearchAll.Name = "SearchAll";
            this.SearchAll.Size = new System.Drawing.Size(79, 23);
            this.SearchAll.TabIndex = 2;
            this.SearchAll.Text = "_searchAll";
            this.SearchAll.UseVisualStyleBackColor = true;
            // 
            // CloseForm
            // 
            this.CloseForm.Location = new System.Drawing.Point(350, 154);
            this.CloseForm.Name = "CloseForm";
            this.CloseForm.Size = new System.Drawing.Size(116, 23);
            this.CloseForm.TabIndex = 4;
            this.CloseForm.Text = "_close";
            this.CloseForm.UseVisualStyleBackColor = true;
            this.CloseForm.Click += new System.EventHandler(this.ButtonCloseForm_Click);
            // 
            // SearchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 183);
            this.Controls.Add(this.CloseForm);
            this.Controls.Add(this.SearchAll);
            this.Controls.Add(this.PanelOptions);
            this.Controls.Add(this.SearchNext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "_searchWindow";
            this.PanelOptions.ResumeLayout(false);
            this.PanelOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxSearchString;
        private System.Windows.Forms.Button SearchNext;
        private System.Windows.Forms.CheckBox CheckUpperLowerCase;
        private System.Windows.Forms.Label LabelSearchText;
        private System.Windows.Forms.Panel PanelOptions;
        private System.Windows.Forms.Button SearchAll;
        private System.Windows.Forms.Button CloseForm;
        private System.Windows.Forms.Label AvailableSearchSheetsTitle;
        private System.Windows.Forms.ComboBox AvailableSearchSheetsHolder;
    }
}