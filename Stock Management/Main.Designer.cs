namespace StockManagement
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.CloseForm = new System.Windows.Forms.PictureBox();
            this.MinimizeForm = new System.Windows.Forms.PictureBox();
            this.SettingsButton = new CustomButton();
            this.StockManagementButton = new CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.CloseForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeForm)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseForm
            // 
            this.CloseForm.Image = global::StockManagement.Properties.Resources.CloseButtonNormal_29x22;
            this.CloseForm.Location = new System.Drawing.Point(1078, 13);
            this.CloseForm.Margin = new System.Windows.Forms.Padding(4);
            this.CloseForm.Name = "CloseForm";
            this.CloseForm.Size = new System.Drawing.Size(29, 22);
            this.CloseForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.CloseForm.TabIndex = 1;
            this.CloseForm.TabStop = false;
            this.CloseForm.Click += new System.EventHandler(this.CloseForm_Click);
            this.CloseForm.MouseEnter += new System.EventHandler(this.CloseForm_MouseEnter);
            this.CloseForm.MouseLeave += new System.EventHandler(this.CloseForm_MouseLeave);
            // 
            // MinimizeForm
            // 
            this.MinimizeForm.Image = global::StockManagement.Properties.Resources.MinimizeButtonNormal_29x22;
            this.MinimizeForm.Location = new System.Drawing.Point(1041, 13);
            this.MinimizeForm.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeForm.Name = "MinimizeForm";
            this.MinimizeForm.Size = new System.Drawing.Size(29, 22);
            this.MinimizeForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.MinimizeForm.TabIndex = 2;
            this.MinimizeForm.TabStop = false;
            this.MinimizeForm.Click += new System.EventHandler(this.MinimizeForm_Click);
            this.MinimizeForm.MouseEnter += new System.EventHandler(this.MinimizeForm_MouseEnter);
            this.MinimizeForm.MouseLeave += new System.EventHandler(this.MinimizeForm_MouseLeave);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Location = new System.Drawing.Point(324, 413);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(350, 123);
            this.SettingsButton.TabIndex = 8;
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.TextPosition = CustomButton.ButtonTextAlign.MiddleCenter;
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.Settings_Click);
            // 
            // StockManagementButton
            // 
            this.StockManagementButton.FlatAppearance.BorderSize = 0;
            this.StockManagementButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StockManagementButton.Location = new System.Drawing.Point(324, 141);
            this.StockManagementButton.Margin = new System.Windows.Forms.Padding(4);
            this.StockManagementButton.Name = "StockManagementButton";
            this.StockManagementButton.Size = new System.Drawing.Size(359, 119);
            this.StockManagementButton.TabIndex = 5;
            this.StockManagementButton.Text = "Stock Management";
            this.StockManagementButton.TextPosition = CustomButton.ButtonTextAlign.MiddleCenter;
            this.StockManagementButton.Click += new System.EventHandler(this.StockManagement_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(1120, 646);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.StockManagementButton);
            this.Controls.Add(this.MinimizeForm);
            this.Controls.Add(this.CloseForm);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hauptmenü";
            ((System.ComponentModel.ISupportInitialize)(this.CloseForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox CloseForm;
        private System.Windows.Forms.PictureBox MinimizeForm;
        private CustomButton StockManagementButton;
        private CustomButton SettingsButton;
    }
}

