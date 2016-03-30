namespace Dash.Installer
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Windows.Forms;

    using IWshRuntimeLibrary;

    using File = System.IO.File;

    /// <summary>
    /// The main.
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// The dash zip name.
        /// </summary>
        private const string DashZipName = "dash_v1.2.zip";

        /// <summary>
        /// The temp dir.
        /// </summary>
        private const string TempDir = @"C:\temp\";

        /// <summary>
        /// Initialises a new instance of the <see cref="Main"/> class.
        /// </summary>
        public Main()
        {
            this.InitializeComponent();
            this.Installing = false;
            this.InstallDone = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether installing.
        /// </summary>
        public bool Installing { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether install done.
        /// </summary>
        public bool InstallDone { get; set; }

        /// <summary>
        /// The main_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Main_Load(object sender, EventArgs e)
        {
            this.pnlLoading.Hide();
        }

        /// <summary>
        /// The btn cancel_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.Installing)
            {
                // Cancel download
                this.pnlLoading.Hide();
                this.btnInstall.Enabled = true;
                this.Installing = false;
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// The lnk nevada scout_ link clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void lnkNevadaScout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://nevadascout.com");
        }

        /// <summary>
        /// The lnk pavel torgashov_ link clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void lnkPavelTorgashov_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/PavelTorgashov/FastColoredTextBox");
        }

        /// <summary>
        /// The lnk yannick lung_ link clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void lnkYannickLung_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://hawcons.com/");
        }

        /// <summary>
        /// The btn select folder_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            var result = this.folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.txtPath.Text = this.folderDialog.SelectedPath;
            }
        }

        /// <summary>
        /// The btn install_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (this.InstallDone)
            {
                // Add additional backslashes if necessary
                if (!this.txtPath.Text.EndsWith("\\"))
                {
                    this.txtPath.Text += "\\";
                }

                Process.Start(this.txtPath.Text + "dash.exe");
                this.Close();
                return;
            }

            this.pnlLoading.Show();
            this.btnInstall.Enabled = false;
            this.btnCancel.Enabled = false;
            this.Installing = true;

            this.Install();
        }

        /// <summary>
        /// The install.
        /// </summary>
        private void Install()
        {
            if (!Directory.Exists(this.txtPath.Text))
            {
                // Create path if it doesn't exist
                Directory.CreateDirectory(this.txtPath.Text);
            }

            this.progress.Value = 0;

            // Download files to C:\temp
            using (var client = new WebClient())
            {
                // Update progress bar + amount downloaded
                client.DownloadProgressChanged += this.client_DownloadProgressChanged;

                // Start next step of install -> copy files
                client.DownloadFileCompleted += this.client_DownloadFileCompleted;

                if (!Directory.Exists(TempDir))
                {
                    // Create path if it doesn't exist
                    Directory.CreateDirectory(TempDir);
                }

                client.DownloadFileAsync(new Uri("http://dl.nevadascout.com/dash/v1.2.zip"), TempDir + DashZipName);
            }
        }

        /// <summary>
        /// The create shortcut.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        private void CreateShortcut()
        {
            var app = string.Empty;

            // Add additional backslashes if necessary
            if (!this.txtPath.Text.EndsWith("\\"))
            {
                app += this.txtPath.Text + "\\";
            }

            app += "dash.exe";

            object shDesktop = "Desktop";
            var shell = new WshShell();
            var shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\Dash SQF Editor.lnk";
            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Dash SQF Editor";
            shortcut.TargetPath = app;
            shortcut.Save();
        }

        #region File Download

        /// <summary>
        /// The client_ download file completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.lblStatus.Text = "Copying Files...";

            // Extract zip + copy files
            if (!this.copyFilesBw.IsBusy)
            {
                this.copyFilesBw.RunWorkerAsync();
            }
        }

        /// <summary>
        /// The client_ download progress changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var bytesIn = double.Parse(e.BytesReceived.ToString());
            var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            var percentage = bytesIn / totalBytes * 100;

            this.progress.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        #endregion

        #region Copy Files

        /// <summary>
        /// The copy files bw_ do work.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void copyFilesBw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Extract zip to chosen path
                ZipFile.ExtractToDirectory(TempDir + DashZipName, this.txtPath.Text);

                // Clean up
                File.Delete(TempDir + DashZipName);
            }
            catch
            {
                // ignored
            }

            // Create desktop shortcut
            this.CreateShortcut();
        }

        /// <summary>
        /// The copy files bw_ run worker completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void copyFilesBw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.lblStatus.Text = "Done!";
            this.progress.Value = 100;

            this.btnCancel.Enabled = false;
            this.btnInstall.Enabled = true;
            this.btnInstall.Text = "Launch Dash!";
            this.btnInstall.Font = new Font(this.btnInstall.Font, FontStyle.Bold);
            this.InstallDone = true;

            // Run dash on close
        }

        #endregion
    }
}