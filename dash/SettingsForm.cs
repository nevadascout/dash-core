using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace Dash
{
    public partial class SettingsForm : Form
    {
        // FCTB Syntax Highlighting
        private readonly TextStyle commentStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        private readonly TextStyle autoChangeStyle = new TextStyle(Brushes.SlateGray, null, FontStyle.Regular);
        private readonly TextStyle userChangeStyle = new TextStyle(Brushes.Black, Brushes.Moccasin, FontStyle.Regular);

        public SettingsForm()
        {
            InitializeComponent();

            var settings = Properties.Settings.Default;

            chkShowRuler.Checked = settings.ShowRuler;
            txtRulerWidth.Text = settings.RulerWidth.ToString();

            chkEnableArmaSense.Checked = settings.EnableArmaSense;
            chkEnableAutoIndentation.Checked = settings.EnableAutoIndentation;
            chkHighlightSyntaxErrors.Checked = settings.EnableSyntaxErrorHighlighting;
            
            chkAutoAddFileCopyrightComment.Checked = settings.EnableFileHeaderComment;
            commentEditor.Text = settings.FileHeaderCommentText;

            chkEnableLineWrapping.Checked = settings.EnableLineWrapping;

            chkCustomSyntaxHighlighter.Checked = settings.EnableCustomSyntaxTheme;
            chkCustomThemes.Checked = settings.EnableCustomDashTheme;

            if (!settings.EnableFileHeaderComment)
                commentEditor.Enabled = false;

            if (!settings.ShowRuler)
                txtRulerWidth.Enabled = false;
        }
        
        private void SqfHighlight(TextChangedEventArgs e)
        {
            var range = commentEditor.Range;

            // Clear editor styling
            range.ClearStyle(commentStyle, autoChangeStyle, userChangeStyle);

            // Re-apply styles to editor
            range.SetStyle(autoChangeStyle, @"{filename}|{year}");
            range.SetStyle(userChangeStyle, @"<.*>");

            range.SetStyle(commentStyle, @"//.*$", RegexOptions.Multiline);
            range.SetStyle(commentStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            range.SetStyle(commentStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
        }

        private void commentEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            SqfHighlight(e);
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.Reload();
        }

        private void chkShowRuler_CheckedChanged(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.ShowRuler = chkShowRuler.Checked;

            if (chkShowRuler.Checked)
            {
                txtRulerWidth.Enabled = true;
            }
            else
            {
                txtRulerWidth.Enabled = false;
            }
        }

        private void chkAutoAddFileCopyrightComment_CheckedChanged(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.EnableFileHeaderComment = chkAutoAddFileCopyrightComment.Checked;

            if (chkAutoAddFileCopyrightComment.Checked)
            {
                commentEditor.Enabled = true;
            }
            else
            {
                commentEditor.Enabled = false;
            }
        }

        private void chkEnableArmaSense_CheckedChanged(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.EnableArmaSense = chkEnableArmaSense.Checked;

        }

        private void chkHighlightSyntaxErrors_CheckedChanged(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.EnableSyntaxErrorHighlighting = chkHighlightSyntaxErrors.Checked;
        }

        private void chkEnableAutoIndentation_CheckedChanged(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.EnableAutoIndentation = chkEnableAutoIndentation.Checked;
        }

        private void chkCustomSyntaxHighlighter_CheckedChanged(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.EnableCustomSyntaxTheme = chkCustomSyntaxHighlighter.Checked;
        }

        private void chkCustomThemes_CheckedChanged(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.EnableCustomDashTheme = chkCustomThemes.Checked;
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Reload();
        }

        private void lnkResetToDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult resetDialogResult = MessageBox.Show("Do you want to reset your Dash settings?\n\nWarning: This is irreversible", "Reset Settings?", MessageBoxButtons.YesNo);
            if (resetDialogResult == DialogResult.Yes)
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();
                MessageBox.Show("Your Dash settings have been reset to their defaults.\nPlease restart Dash for this to take effect.");
                this.Close();
            }
        }

        private void chkEnableLineWrapping_CheckedChanged(object sender, System.EventArgs e)
        {
            Properties.Settings.Default.EnableLineWrapping = chkEnableLineWrapping.Checked;
        }
    }
}