namespace StockManagement
{
    partial class OldCustomers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OldCustomers));
            this.CloseForm = new System.Windows.Forms.Button();
            this.ReloadContent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CloseForm
            // 
            this.CloseForm.Location = new System.Drawing.Point(656, 526);
            this.CloseForm.Name = "CloseForm";
            this.CloseForm.Size = new System.Drawing.Size(116, 23);
            this.CloseForm.TabIndex = 15;
            this.CloseForm.Text = "_close";
            this.CloseForm.UseVisualStyleBackColor = true;
            this.CloseForm.Click += new System.EventHandler(this.CloseForm_Click);
            // 
            // ReloadContent
            // 
            this.ReloadContent.Location = new System.Drawing.Point(534, 526);
            this.ReloadContent.Name = "ReloadContent";
            this.ReloadContent.Size = new System.Drawing.Size(116, 23);
            this.ReloadContent.TabIndex = 16;
            this.ReloadContent.Text = "_reloadData";
            this.ReloadContent.UseVisualStyleBackColor = true;
            this.ReloadContent.Click += new System.EventHandler(this.ReloadContent_Click);
            // 
            // OldCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.ReloadContent);
            this.Controls.Add(this.CloseForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OldCustomers";
            this.Text = "_oldCustomer";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button CloseForm;
        private System.Windows.Forms.Button ReloadContent;
    }
}