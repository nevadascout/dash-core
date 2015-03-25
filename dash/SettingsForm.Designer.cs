namespace Dash
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkShowRuler = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkEnableLineWrapping = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkEnableAutoIndentation = new System.Windows.Forms.CheckBox();
            this.chkHighlightSyntaxErrors = new System.Windows.Forms.CheckBox();
            this.chkEnableArmaSense = new System.Windows.Forms.CheckBox();
            this.txtRulerWidth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.commentEditor = new FastColoredTextBoxNS.FastColoredTextBox();
            this.chkAutoAddFileCopyrightComment = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkCustomThemes = new System.Windows.Forms.CheckBox();
            this.chkCustomSyntaxHighlighter = new System.Windows.Forms.CheckBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.lnkResetToDefault = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commentEditor)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(311, 472);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(193, 37);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Settings";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(215, 472);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 37);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkShowRuler
            // 
            this.chkShowRuler.AutoSize = true;
            this.chkShowRuler.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkShowRuler.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowRuler.Location = new System.Drawing.Point(15, 22);
            this.chkShowRuler.Name = "chkShowRuler";
            this.chkShowRuler.Size = new System.Drawing.Size(88, 20);
            this.chkShowRuler.TabIndex = 3;
            this.chkShowRuler.Text = "Show ruler";
            this.chkShowRuler.UseVisualStyleBackColor = true;
            this.chkShowRuler.CheckedChanged += new System.EventHandler(this.chkShowRuler_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEnableLineWrapping);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chkEnableAutoIndentation);
            this.groupBox1.Controls.Add(this.chkHighlightSyntaxErrors);
            this.groupBox1.Controls.Add(this.chkEnableArmaSense);
            this.groupBox1.Controls.Add(this.txtRulerWidth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkShowRuler);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(492, 130);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Editor Options";
            // 
            // chkEnableLineWrapping
            // 
            this.chkEnableLineWrapping.AutoSize = true;
            this.chkEnableLineWrapping.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkEnableLineWrapping.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableLineWrapping.Location = new System.Drawing.Point(344, 100);
            this.chkEnableLineWrapping.Name = "chkEnableLineWrapping";
            this.chkEnableLineWrapping.Size = new System.Drawing.Size(142, 20);
            this.chkEnableLineWrapping.TabIndex = 10;
            this.chkEnableLineWrapping.Text = "Enable line wrapping";
            this.chkEnableLineWrapping.UseVisualStyleBackColor = true;
            this.chkEnableLineWrapping.CheckedChanged += new System.EventHandler(this.chkEnableLineWrapping_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(237, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "chars";
            // 
            // chkEnableAutoIndentation
            // 
            this.chkEnableAutoIndentation.AutoSize = true;
            this.chkEnableAutoIndentation.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkEnableAutoIndentation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableAutoIndentation.Location = new System.Drawing.Point(15, 100);
            this.chkEnableAutoIndentation.Name = "chkEnableAutoIndentation";
            this.chkEnableAutoIndentation.Size = new System.Drawing.Size(160, 20);
            this.chkEnableAutoIndentation.TabIndex = 8;
            this.chkEnableAutoIndentation.Text = "Enable auto-indentation";
            this.chkEnableAutoIndentation.UseVisualStyleBackColor = true;
            this.chkEnableAutoIndentation.CheckedChanged += new System.EventHandler(this.chkEnableAutoIndentation_CheckedChanged);
            // 
            // chkHighlightSyntaxErrors
            // 
            this.chkHighlightSyntaxErrors.AutoSize = true;
            this.chkHighlightSyntaxErrors.Enabled = false;
            this.chkHighlightSyntaxErrors.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkHighlightSyntaxErrors.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHighlightSyntaxErrors.Location = new System.Drawing.Point(15, 74);
            this.chkHighlightSyntaxErrors.Name = "chkHighlightSyntaxErrors";
            this.chkHighlightSyntaxErrors.Size = new System.Drawing.Size(175, 20);
            this.chkHighlightSyntaxErrors.TabIndex = 7;
            this.chkHighlightSyntaxErrors.Text = "Highlight SQF syntax errors";
            this.chkHighlightSyntaxErrors.UseVisualStyleBackColor = true;
            this.chkHighlightSyntaxErrors.CheckedChanged += new System.EventHandler(this.chkHighlightSyntaxErrors_CheckedChanged);
            // 
            // chkEnableArmaSense
            // 
            this.chkEnableArmaSense.AutoSize = true;
            this.chkEnableArmaSense.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkEnableArmaSense.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableArmaSense.Location = new System.Drawing.Point(15, 48);
            this.chkEnableArmaSense.Name = "chkEnableArmaSense";
            this.chkEnableArmaSense.Size = new System.Drawing.Size(222, 20);
            this.chkEnableArmaSense.TabIndex = 6;
            this.chkEnableArmaSense.Text = "Enable ArmaSense code completion";
            this.chkEnableArmaSense.UseVisualStyleBackColor = true;
            this.chkEnableArmaSense.CheckedChanged += new System.EventHandler(this.chkEnableArmaSense_CheckedChanged);
            // 
            // txtRulerWidth
            // 
            this.txtRulerWidth.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRulerWidth.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRulerWidth.Location = new System.Drawing.Point(203, 22);
            this.txtRulerWidth.Name = "txtRulerWidth";
            this.txtRulerWidth.Size = new System.Drawing.Size(34, 22);
            this.txtRulerWidth.TabIndex = 5;
            this.txtRulerWidth.Text = "80";
            this.txtRulerWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(131, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ruler Width:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.commentEditor);
            this.groupBox2.Controls.Add(this.chkAutoAddFileCopyrightComment);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(492, 190);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Editing";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(15, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 30);
            this.label2.TabIndex = 9;
            this.label2.Text = "{filename} and {year} will be set automatically.\r\nYou need to set the project nam" +
    "e and your website manually.";
            // 
            // commentEditor
            // 
            this.commentEditor.AllowDrop = false;
            this.commentEditor.AllowMacroRecording = false;
            this.commentEditor.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.commentEditor.AutoIndent = false;
            this.commentEditor.AutoIndentChars = false;
            this.commentEditor.AutoIndentExistingLines = false;
            this.commentEditor.AutoScrollMinSize = new System.Drawing.Size(291, 28);
            this.commentEditor.BackBrush = null;
            this.commentEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.commentEditor.CharHeight = 14;
            this.commentEditor.CharWidth = 8;
            this.commentEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.commentEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.commentEditor.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.commentEditor.HighlightFoldingIndicator = false;
            this.commentEditor.Hotkeys = resources.GetString("commentEditor.Hotkeys");
            this.commentEditor.IsReplaceMode = false;
            this.commentEditor.Location = new System.Drawing.Point(15, 48);
            this.commentEditor.Name = "commentEditor";
            this.commentEditor.Paddings = new System.Windows.Forms.Padding(0);
            this.commentEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.commentEditor.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("commentEditor.ServiceColors")));
            this.commentEditor.Size = new System.Drawing.Size(460, 95);
            this.commentEditor.TabIndex = 8;
            this.commentEditor.Text = "// <project name> {filename}\r\n// Copyright (c) {year} <website>";
            this.commentEditor.Zoom = 100;
            this.commentEditor.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.commentEditor_TextChanged);
            // 
            // chkAutoAddFileCopyrightComment
            // 
            this.chkAutoAddFileCopyrightComment.AutoSize = true;
            this.chkAutoAddFileCopyrightComment.Enabled = false;
            this.chkAutoAddFileCopyrightComment.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkAutoAddFileCopyrightComment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoAddFileCopyrightComment.Location = new System.Drawing.Point(15, 22);
            this.chkAutoAddFileCopyrightComment.Name = "chkAutoAddFileCopyrightComment";
            this.chkAutoAddFileCopyrightComment.Size = new System.Drawing.Size(313, 20);
            this.chkAutoAddFileCopyrightComment.TabIndex = 7;
            this.chkAutoAddFileCopyrightComment.Text = "Automatically add the following to the top of all files:";
            this.chkAutoAddFileCopyrightComment.UseVisualStyleBackColor = true;
            this.chkAutoAddFileCopyrightComment.CheckedChanged += new System.EventHandler(this.chkAutoAddFileCopyrightComment_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkCustomThemes);
            this.groupBox3.Controls.Add(this.chkCustomSyntaxHighlighter);
            this.groupBox3.Controls.Add(this.linkLabel2);
            this.groupBox3.Controls.Add(this.linkLabel1);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 344);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(492, 78);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Customisation";
            // 
            // chkCustomThemes
            // 
            this.chkCustomThemes.AutoSize = true;
            this.chkCustomThemes.Enabled = false;
            this.chkCustomThemes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkCustomThemes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustomThemes.Location = new System.Drawing.Point(15, 48);
            this.chkCustomThemes.Name = "chkCustomThemes";
            this.chkCustomThemes.Size = new System.Drawing.Size(181, 20);
            this.chkCustomThemes.TabIndex = 11;
            this.chkCustomThemes.Text = "Enable custom Dash themes";
            this.chkCustomThemes.UseVisualStyleBackColor = true;
            this.chkCustomThemes.CheckedChanged += new System.EventHandler(this.chkCustomThemes_CheckedChanged);
            // 
            // chkCustomSyntaxHighlighter
            // 
            this.chkCustomSyntaxHighlighter.AutoSize = true;
            this.chkCustomSyntaxHighlighter.Enabled = false;
            this.chkCustomSyntaxHighlighter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkCustomSyntaxHighlighter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustomSyntaxHighlighter.Location = new System.Drawing.Point(15, 22);
            this.chkCustomSyntaxHighlighter.Name = "chkCustomSyntaxHighlighter";
            this.chkCustomSyntaxHighlighter.Size = new System.Drawing.Size(214, 20);
            this.chkCustomSyntaxHighlighter.TabIndex = 9;
            this.chkCustomSyntaxHighlighter.Text = "Enable custom syntax highlighting";
            this.chkCustomSyntaxHighlighter.UseVisualStyleBackColor = true;
            this.chkCustomSyntaxHighlighter.CheckedChanged += new System.EventHandler(this.chkCustomSyntaxHighlighter_CheckedChanged);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(280, 50);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(206, 15);
            this.linkLabel2.TabIndex = 10;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "How-to: Customise Dash main theme";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(237, 24);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(249, 15);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "How-to: Customise syntax highlighting colors";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(-1, 456);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(20, 25, 0, 0);
            this.label3.Size = new System.Drawing.Size(518, 66);
            this.label3.TabIndex = 7;
            this.label3.Text = "Restart Dash to view changes";
            // 
            // lnkResetToDefault
            // 
            this.lnkResetToDefault.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
            this.lnkResetToDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkResetToDefault.AutoSize = true;
            this.lnkResetToDefault.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkResetToDefault.LinkColor = System.Drawing.Color.DimGray;
            this.lnkResetToDefault.Location = new System.Drawing.Point(9, 431);
            this.lnkResetToDefault.Name = "lnkResetToDefault";
            this.lnkResetToDefault.Size = new System.Drawing.Size(90, 15);
            this.lnkResetToDefault.TabIndex = 12;
            this.lnkResetToDefault.TabStop = true;
            this.lnkResetToDefault.Text = "Reset to Default";
            this.lnkResetToDefault.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkResetToDefault_LinkClicked);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(516, 521);
            this.Controls.Add(this.lnkResetToDefault);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings - Dash";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commentEditor)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkShowRuler;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRulerWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkEnableArmaSense;
        private System.Windows.Forms.GroupBox groupBox2;
        private FastColoredTextBoxNS.FastColoredTextBox commentEditor;
        private System.Windows.Forms.CheckBox chkAutoAddFileCopyrightComment;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox chkEnableAutoIndentation;
        private System.Windows.Forms.CheckBox chkHighlightSyntaxErrors;
        private System.Windows.Forms.CheckBox chkCustomThemes;
        private System.Windows.Forms.CheckBox chkCustomSyntaxHighlighter;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel lnkResetToDefault;
        private System.Windows.Forms.CheckBox chkEnableLineWrapping;
    }
}