namespace Dash
{
    using System;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    using Dash.Properties;

    using FastColoredTextBoxNS;

    /// <summary>
    /// The settings form.
    /// </summary>
    public partial class SettingsForm : Form
    {
        /// <summary>
        /// The auto change style.
        /// </summary>
        private readonly TextStyle autoChangeStyle = new TextStyle(Brushes.SlateGray, null, FontStyle.Regular);

        // FCTB Syntax Highlighting

        /// <summary>
        /// Comment styling
        /// </summary>
        private readonly TextStyle commentStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);

        /// <summary>
        /// User change style
        /// </summary>
        private readonly TextStyle userChangeStyle = new TextStyle(Brushes.Black, Brushes.Moccasin, FontStyle.Regular);

        /// <summary>
        /// Initialises a new instance of the <see cref="SettingsForm"/> class.
        /// </summary>
        public SettingsForm()
        {
            this.InitializeComponent();

            var settings = Settings.Default;

            this.chkShowRuler.Checked = settings.ShowRuler;
            this.txtRulerWidth.Text = settings.RulerWidth.ToString();

            this.chkEnableArmaSense.Checked = settings.EnableArmaSense;
            this.chkEnableAutoIndentation.Checked = settings.EnableAutoIndentation;
            this.chkHighlightSyntaxErrors.Checked = settings.EnableSyntaxErrorHighlighting;

            this.chkAutoAddFileCopyrightComment.Checked = settings.EnableFileHeaderComment;
            this.commentEditor.Text = settings.FileHeaderCommentText;

            this.chkEnableLineWrapping.Checked = settings.EnableLineWrapping;

            this.chkCustomSyntaxHighlighter.Checked = settings.EnableCustomSyntaxTheme;
            this.chkCustomThemes.Checked = settings.EnableCustomDashTheme;

            if (!settings.EnableFileHeaderComment)
            {
                this.commentEditor.Enabled = false;
            }

            if (!settings.ShowRuler)
            {
                this.txtRulerWidth.Enabled = false;
            }
        }

        /// <summary>
        /// The sqf highlight.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SqfHighlight(TextChangedEventArgs e)
        {
            var range = this.commentEditor.Range;

            // Clear editor styling
            range.ClearStyle(this.commentStyle, this.autoChangeStyle, this.userChangeStyle);

            // Re-apply styles to editor
            range.SetStyle(this.autoChangeStyle, @"{filename}|{year}");
            range.SetStyle(this.userChangeStyle, @"<.*>");

            range.SetStyle(this.commentStyle, @"//.*$", RegexOptions.Multiline);
            range.SetStyle(this.commentStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            range.SetStyle(
                this.commentStyle, 
                @"(/\*.*?\*/)|(.*\*/)", 
                RegexOptions.Singleline | RegexOptions.RightToLeft);
        }

        /// <summary>
        /// The comment editor_ text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void commentEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.SqfHighlight(e);
        }

        /// <summary>
        /// The btn save_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You must restart Dash for your setting changes to take affect.");

            Settings.Default.Save();
            this.Close();
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
            Settings.Default.Reload();
        }

        /// <summary>
        /// The chk show ruler_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void chkShowRuler_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowRuler = this.chkShowRuler.Checked;

            if (this.chkShowRuler.Checked)
            {
                this.txtRulerWidth.Enabled = true;
            }
            else
            {
                this.txtRulerWidth.Enabled = false;
            }
        }

        /// <summary>
        /// The chk auto add file copyright comment_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void chkAutoAddFileCopyrightComment_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableFileHeaderComment = this.chkAutoAddFileCopyrightComment.Checked;

            if (this.chkAutoAddFileCopyrightComment.Checked)
            {
                this.commentEditor.Enabled = true;
            }
            else
            {
                this.commentEditor.Enabled = false;
            }
        }

        /// <summary>
        /// The chk enable arma sense_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void chkEnableArmaSense_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableArmaSense = this.chkEnableArmaSense.Checked;
        }

        /// <summary>
        /// The chk highlight syntax errors_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void chkHighlightSyntaxErrors_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableSyntaxErrorHighlighting = this.chkHighlightSyntaxErrors.Checked;
        }

        /// <summary>
        /// The chk enable auto indentation_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void chkEnableAutoIndentation_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableAutoIndentation = this.chkEnableAutoIndentation.Checked;
        }

        /// <summary>
        /// The chk custom syntax highlighter_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void chkCustomSyntaxHighlighter_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableCustomSyntaxTheme = this.chkCustomSyntaxHighlighter.Checked;
        }

        /// <summary>
        /// The chk custom themes_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void chkCustomThemes_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableCustomDashTheme = this.chkCustomThemes.Checked;
        }

        /// <summary>
        /// The settings form_ form closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Reload();
        }

        /// <summary>
        /// The lnk reset to default_ link clicked.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void lnkResetToDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var resetDialogResult =
                MessageBox.Show(
                    "Do you want to reset your Dash settings?\n\nWarning: This is irreversible", 
                    "Reset Settings?", 
                    MessageBoxButtons.YesNo);
            if (resetDialogResult == DialogResult.Yes)
            {
                Settings.Default.Reset();
                Settings.Default.Save();
                MessageBox.Show(
                    "Your Dash settings have been reset to their defaults.\nPlease restart Dash for this to take effect.");
                this.Close();
            }
        }

        /// <summary>
        /// The chk enable line wrapping_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void chkEnableLineWrapping_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableLineWrapping = this.chkEnableLineWrapping.Checked;
        }
    }
}