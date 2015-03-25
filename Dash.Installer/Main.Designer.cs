namespace Dash.Installer
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lnkNevadaScout = new System.Windows.Forms.LinkLabel();
            this.lnkPavelTorgashov = new System.Windows.Forms.LinkLabel();
            this.lnkYannickLung = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.copyFilesBw = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnInstall);
            this.panel1.Location = new System.Drawing.Point(-2, 297);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 76);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(24, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 37);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(272, 19);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(245, 37);
            this.btnInstall.TabIndex = 3;
            this.btnInstall.Text = "Download + Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dash Installer";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(23, 103);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(401, 23);
            this.txtPath.TabIndex = 2;
            this.txtPath.Text = "C:\\Program Files (x86)\\Dash";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 34);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Install Dash to path:";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(430, 102);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(86, 25);
            this.btnSelectFolder.TabIndex = 5;
            this.btnSelectFolder.Text = "Select Folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Credits";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Main Code:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "FastColoredTextBox:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(27, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Icon:";
            // 
            // lnkNevadaScout
            // 
            this.lnkNevadaScout.AutoSize = true;
            this.lnkNevadaScout.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkNevadaScout.Location = new System.Drawing.Point(161, 193);
            this.lnkNevadaScout.Name = "lnkNevadaScout";
            this.lnkNevadaScout.Size = new System.Drawing.Size(79, 15);
            this.lnkNevadaScout.TabIndex = 10;
            this.lnkNevadaScout.TabStop = true;
            this.lnkNevadaScout.Text = "nevada_scout";
            this.lnkNevadaScout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNevadaScout_LinkClicked);
            // 
            // lnkPavelTorgashov
            // 
            this.lnkPavelTorgashov.AutoSize = true;
            this.lnkPavelTorgashov.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkPavelTorgashov.Location = new System.Drawing.Point(161, 218);
            this.lnkPavelTorgashov.Name = "lnkPavelTorgashov";
            this.lnkPavelTorgashov.Size = new System.Drawing.Size(94, 15);
            this.lnkPavelTorgashov.TabIndex = 11;
            this.lnkPavelTorgashov.TabStop = true;
            this.lnkPavelTorgashov.Text = "Pavel Torgashov";
            this.lnkPavelTorgashov.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPavelTorgashov_LinkClicked);
            // 
            // lnkYannickLung
            // 
            this.lnkYannickLung.AutoSize = true;
            this.lnkYannickLung.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkYannickLung.Location = new System.Drawing.Point(161, 244);
            this.lnkYannickLung.Name = "lnkYannickLung";
            this.lnkYannickLung.Size = new System.Drawing.Size(79, 15);
            this.lnkYannickLung.TabIndex = 12;
            this.lnkYannickLung.TabStop = true;
            this.lnkYannickLung.Text = "Yannick Lung";
            this.lnkYannickLung.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkYannickLung_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(155, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "Version: 1.2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(256, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(146, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Released: March 24th 2015";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(304, 193);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 15);
            this.label9.TabIndex = 15;
            this.label9.Text = "Thanks To:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(392, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 15);
            this.label10.TabIndex = 16;
            this.label10.Text = "SirBlasteLot";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(392, 218);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "Charlie Hadden";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(392, 244);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 15);
            this.label12.TabIndex = 18;
            this.label12.Text = "+ Others!";
            // 
            // folderDialog
            // 
            this.folderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderDialog.SelectedPath = "C:\\Program Files (x86)\\";
            // 
            // pnlLoading
            // 
            this.pnlLoading.Controls.Add(this.progress);
            this.pnlLoading.Controls.Add(this.lblStatus);
            this.pnlLoading.Location = new System.Drawing.Point(12, 12);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(510, 279);
            this.pnlLoading.TabIndex = 19;
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(82, 129);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(339, 23);
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progress.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(79, 111);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(89, 15);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Downloading...";
            // 
            // copyFilesBw
            // 
            this.copyFilesBw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.copyFilesBw_DoWork);
            this.copyFilesBw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.copyFilesBw_RunWorkerCompleted);
            // 
            // Main
            // 
            this.AcceptButton = this.btnInstall;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(534, 372);
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lnkYannickLung);
            this.Controls.Add(this.lnkPavelTorgashov);
            this.Controls.Add(this.lnkNevadaScout);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Install Dash v1.2";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lnkNevadaScout;
        private System.Windows.Forms.LinkLabel lnkPavelTorgashov;
        private System.Windows.Forms.LinkLabel lnkYannickLung;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker copyFilesBw;
    }
}

