using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace Dash.Installer
{
    public partial class Main : Form
    {
        public bool Installing { get; set; }
        public bool InstallDone { get; set; }

        private const string DashZipName = "dash_v1.2.zip";
        private const string TempDir = @"C:\temp\";

        public Main()
        {
            InitializeComponent();
            this.Installing = false;
            this.InstallDone = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            pnlLoading.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.Installing)
            {
                // Cancel download
                pnlLoading.Hide();
                btnInstall.Enabled = true;
                this.Installing = false;
            }
            else
            {
                this.Close();
            }

        }

        private void lnkNevadaScout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://nevadascout.com");
        }

        private void lnkPavelTorgashov_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/PavelTorgashov/FastColoredTextBox");
        }

        private void lnkYannickLung_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://hawcons.com/");
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            var result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.txtPath.Text = folderDialog.SelectedPath;
            }
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (this.InstallDone)
            {
                // Add additional backslashes if necessary
                if (!txtPath.Text.EndsWith("\\"))
                {
                    txtPath.Text += "\\";
                }

                Process.Start(txtPath.Text + "dash.exe");
                this.Close();
                return;
            }

            pnlLoading.Show();
            btnInstall.Enabled = false;
            btnCancel.Enabled = false;
            this.Installing = true;

            Install();
        }


        private void Install()
        {
            if (!Directory.Exists(txtPath.Text))
            {
                // Create path if it doesn't exist
                Directory.CreateDirectory(txtPath.Text);
            }

            progress.Value = 0;

            // Download files to C:\temp
            using (WebClient client = new WebClient())
            {
                // Update progress bar + amount downloaded
                client.DownloadProgressChanged += client_DownloadProgressChanged;

                // Start next step of install -> copy files
                client.DownloadFileCompleted += client_DownloadFileCompleted;

                if (!Directory.Exists(TempDir))
                {
                    // Create path if it doesn't exist
                    Directory.CreateDirectory(TempDir);
                }

                client.DownloadFileAsync(new Uri("http://dl.nevadascout.com/dash/v1.2.zip"), TempDir + DashZipName);
            }
        }


        #region File Download

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            lblStatus.Text = "Copying Files...";

            // Extract zip + copy files
            if (!copyFilesBw.IsBusy)
            {
                copyFilesBw.RunWorkerAsync();
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            progress.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        #endregion


        #region Copy Files

        private void copyFilesBw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Extract zip to chosen path
                ZipFile.ExtractToDirectory(TempDir + DashZipName, txtPath.Text);

                // Clean up
                File.Delete(TempDir + DashZipName);
            }
            catch
            {
                // ignored
            }
        }

        private void copyFilesBw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblStatus.Text = "Done!";
            progress.Value = 100;

            btnCancel.Enabled = false;
            btnInstall.Enabled = true;
            btnInstall.Text = "Launch Dash!";
            btnInstall.Font = new Font(btnInstall.Font, FontStyle.Bold);
            this.InstallDone = true;

            // Run dash on close
        }

        #endregion

    }
}
