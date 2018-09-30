namespace StockManagement
{
    partial class AboutWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.CreatorLink = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.SourceWebsiteLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ApplicationName = new System.Windows.Forms.Label();
            this.ApplicationVersion = new System.Windows.Forms.Label();
            this.AuthorE_MailTitle = new System.Windows.Forms.Label();
            this.AuthorE_MailLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // CreatorLink
            // 
            this.CreatorLink.AutoSize = true;
            this.CreatorLink.Location = new System.Drawing.Point(283, 87);
            this.CreatorLink.Name = "CreatorLink";
            this.CreatorLink.Size = new System.Drawing.Size(42, 13);
            this.CreatorLink.TabIndex = 0;
            this.CreatorLink.TabStop = true;
            this.CreatorLink.Text = "Freepik";
            this.CreatorLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CreatorLink_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "erstellt und von";
            // 
            // SourceWebsiteLink
            // 
            this.SourceWebsiteLink.AutoSize = true;
            this.SourceWebsiteLink.Location = new System.Drawing.Point(87, 100);
            this.SourceWebsiteLink.Name = "SourceWebsiteLink";
            this.SourceWebsiteLink.Size = new System.Drawing.Size(91, 13);
            this.SourceWebsiteLink.TabIndex = 3;
            this.SourceWebsiteLink.TabStop = true;
            this.SourceWebsiteLink.Text = "www.flaticon.com";
            this.SourceWebsiteLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SourceWebsiteLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Das in diesem Programm verwendete Icon wurden durch";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(175, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "herunterlegaden.";
            // 
            // ApplicationName
            // 
            this.ApplicationName.AutoSize = true;
            this.ApplicationName.Location = new System.Drawing.Point(12, 20);
            this.ApplicationName.Name = "ApplicationName";
            this.ApplicationName.Size = new System.Drawing.Size(101, 13);
            this.ApplicationName.TabIndex = 6;
            this.ApplicationName.Text = "_stockManagement";
            // 
            // ApplicationVersion
            // 
            this.ApplicationVersion.AutoSize = true;
            this.ApplicationVersion.Location = new System.Drawing.Point(12, 33);
            this.ApplicationVersion.Name = "ApplicationVersion";
            this.ApplicationVersion.Size = new System.Drawing.Size(84, 13);
            this.ApplicationVersion.TabIndex = 7;
            this.ApplicationVersion.Text = "_versionNumber";
            // 
            // AuthorE_MailTitle
            // 
            this.AuthorE_MailTitle.AutoSize = true;
            this.AuthorE_MailTitle.Location = new System.Drawing.Point(12, 46);
            this.AuthorE_MailTitle.Name = "AuthorE_MailTitle";
            this.AuthorE_MailTitle.Size = new System.Drawing.Size(43, 13);
            this.AuthorE_MailTitle.TabIndex = 8;
            this.AuthorE_MailTitle.Text = "_author";
            // 
            // AuthorE_MailLink
            // 
            this.AuthorE_MailLink.AutoSize = true;
            this.AuthorE_MailLink.Location = new System.Drawing.Point(61, 46);
            this.AuthorE_MailLink.Name = "AuthorE_MailLink";
            this.AuthorE_MailLink.Size = new System.Drawing.Size(37, 13);
            this.AuthorE_MailLink.TabIndex = 9;
            this.AuthorE_MailLink.TabStop = true;
            this.AuthorE_MailLink.Text = "_email";
            this.AuthorE_MailLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EMailLink_LinkClicked);
            // 
            // AboutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 166);
            this.Controls.Add(this.AuthorE_MailLink);
            this.Controls.Add(this.AuthorE_MailTitle);
            this.Controls.Add(this.ApplicationVersion);
            this.Controls.Add(this.ApplicationName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SourceWebsiteLink);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CreatorLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "AboutWindow";
            this.Text = "_about";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel CreatorLink;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel SourceWebsiteLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ApplicationName;
        private System.Windows.Forms.Label ApplicationVersion;
        private System.Windows.Forms.Label AuthorE_MailTitle;
        private System.Windows.Forms.LinkLabel AuthorE_MailLink;
    }
}