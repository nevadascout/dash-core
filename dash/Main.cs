// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Dash
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    using Dash.Properties;

    using FastColoredTextBoxNS;

    /// <summary>
    /// The main.
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        /// The allow syntax highlighting.
        /// </summary>
        private static bool allowSyntaxHighlighting;

        /// <summary>
        /// The form loaded.
        /// </summary>
        private static bool formLoaded;

        /// <summary>
        /// Word styling
        /// </summary>
        private readonly MarkerStyle sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        /// <summary>
        /// The watcher.
        /// </summary>
        private FileSystemWatcher watcher = new FileSystemWatcher();

        // TODO -- Move all of these into app.config file
        private readonly double version = 1.5; // App version 

        // TODO -- Change this to the nevadascout domain
        private readonly string site = "http://pastebin.com/raw.php?i=kEfPgtGQ";

        // TODO -- Remove this when reworked to use ClickOnce library
        private readonly string downloadFileLocation = "https://mega.co.nz/#!uJ9FXA7L!v7mKjV5DTKs5dp7lRfvNkwBP2NZJjqT681nIHlKWs7w";

        // Updater vars ~ trdwll

        /// <summary>
        /// Initialises a new instance of the <see cref="Main"/> class.
        /// </summary>
        public Main()
        {
            this.DashGlobal = new DashGlobal(
                this.textArea_TextChanged, 
                this.textArea_TextChangedDelayed, 
                this.textArea_KeyUp, 
                this.textArea_SelectionChangedDelayed, 
                this.textArea_DragDrop, 
                this.textArea_DragEnter, 
                this.mainTabControl, 
                this.ArmaSense, 
                this);

            this.InitializeComponent();

            this.DashGlobal.EditorHelper.MainTabControl = this.mainTabControl;
            this.DashGlobal.TabsHelper.MainTabControl = this.mainTabControl;
            this.DashGlobal.SettingsHelper.MainTabControl = this.mainTabControl;
            this.DashGlobal.EditorHelper.ArmaSenseImageList = this.armaSenseImageList;
        }

        /// <summary>
        /// Gets or sets the lang.
        /// </summary>
        public static string Lang { get; set; }

        /// <summary>
        /// Gets or sets the dash global.
        /// </summary>
        public DashGlobal DashGlobal { get; set; }

        /// <summary>
        /// Gets or sets the arma sense.
        /// </summary>
        private AutocompleteMenu ArmaSense { get; set; }

        /// <summary>
        /// The main_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <exception cref="FileNotFoundException">
        /// Exception thrown if the file is not found
        /// </exception>
        private void Main_Load(object sender, EventArgs e)
        {
            // Populate Treeview
            this.DashGlobal.FilesHelper.SetTreeviewDirectory(this.directoryTreeView, Settings.Default.TreeviewDir);

            // TODO - Optimise this
            this.DashGlobal.EditorHelper.UserVariablesCurrentFile = new List<UserVariable>();

            // Set window size from memory
            if (Settings.Default.WindowMaximised)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.Size = new Size(
                    Convert.ToInt32(Settings.Default.Window_Width), 
                    Convert.ToInt32(Settings.Default.Window_Height));
            }

            // Clear tabs + add default tab
            foreach (TabPage tab in this.mainTabControl.TabPages)
            {
                this.mainTabControl.TabPages.Remove(tab);
            }

            // Allow the textArea_TextChanged event to fire so
            // we get syntax highlighting in open files on form load
            allowSyntaxHighlighting = true;

            if (Settings.Default.OpenTabs != null)
            {
                if (Settings.Default.OpenTabs.Count == 0)
                {
                    this.DashGlobal.TabsHelper.CreateBlankTab(FileType.Other);
                    this.ArmaSense = this.DashGlobal.EditorHelper.CreateArmaSense();
                }
                else
                {
                    foreach (var file in Settings.Default.OpenTabs)
                    {
                        try
                        {
                            if (File.Exists(file))
                            {
                                this.DashGlobal.TabsHelper.CreateTabOpenFile(file);
                            }
                            else
                            {
                                throw new FileNotFoundException("The file was not found!");
                            }
                        }
                        catch
                        {
                            MessageBox.Show(string.Format("Unable to open file:\n\n{0}", file));
                        }
                    }

                    if (Settings.Default.SelectedTab < this.mainTabControl.TabCount)
                    {
                        this.mainTabControl.SelectTab(Settings.Default.SelectedTab);
                    }
                }
            }
            else
            {
                Settings.Default.OpenTabs = new List<string>();
            }

            if (this.mainTabControl.TabPages.Count == 0)
            {
                this.DashGlobal.TabsHelper.CreateBlankTab();
                this.DashGlobal.SetWindowTitle("{new file}");
            }

            if (Settings.Default.FirstLoad)
            {
                var tutorialFile = AppDomain.CurrentDomain.BaseDirectory + "\\tutorial.txt";
                if (File.Exists(tutorialFile))
                {
                    this.DashGlobal.TabsHelper.CreateBlankTab(FileType.Other, "tutorial.txt");
                    this.DashGlobal.EditorHelper.GetActiveEditor().Text = File.ReadAllText(tutorialFile);
                    this.DashGlobal.TabsHelper.SetSelectedTabClean();
                }
            }

            // Add file watcher to update treeview on file change
            // watcher.Path = Settings.Default.TreeviewDir;

            // watcher.NotifyFilter = NotifyFilters.DirectoryName | 
            // NotifyFilters.FileName | 
            // NotifyFilters.LastAccess |
            // NotifyFilters.LastWrite;

            // watcher.Created += FileSystemChanged;
            // watcher.Changed += FileSystemChanged;
            // watcher.Deleted += FileSystemChanged;
            // watcher.Renamed += FileSystemChanged;

            // watcher.EnableRaisingEvents = true;
            formLoaded = true;

            // Apply stored settings
            this.mainSplitContainer.SplitterDistance = Convert.ToInt32(Settings.Default.SplitterWidth);

            this.DashGlobal.EditorHelper.ActiveEditor.GoHome();
        }

        /// <summary>
        /// The main_ form closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DashGlobal.SettingsHelper.UpdateOpenTabs();
            Settings.Default.SelectedTab = this.mainTabControl.SelectedIndex;
            Settings.Default.Save();
        }

        /// <summary>
        /// The main_ resize.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Main_Resize(object sender, EventArgs e)
        {
            var window = (Control)sender;

            if (this.WindowState == FormWindowState.Maximized)
            {
                Settings.Default.WindowMaximised = true;
            }
            else
            {
                Settings.Default.WindowMaximised = false;
                Settings.Default.Window_Width = window.Size.Width;
                Settings.Default.Window_Height = window.Size.Height;
            }

            Settings.Default.Save();
        }

        /// <summary>
        /// The init styles priority.
        /// </summary>
        public void InitStylesPriority()
        {
            this.textArea.AddStyle(this.sameWordsStyle);
        }

        /// <summary>
        /// The file system changed.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void FileSystemChanged(object source, FileSystemEventArgs e)
        {
            this.DashGlobal.FilesHelper.SetTreeviewDirectory(this.directoryTreeView, Settings.Default.TreeviewDir);
        }

        /// <summary>
        /// The main split container_ splitter moved.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void mainSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (formLoaded)
            {
                Settings.Default.SplitterWidth = this.mainSplitContainer.SplitterDistance;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// The directory tree view_ node mouse double click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void directoryTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // If not a folder, load into the editor
            if (e.Node.ImageIndex != 0 && e.Node.ImageIndex != 4)
            {
                var filename = e.Node.Tag.ToString();

                var tabOpen = this.mainTabControl.TabPages.Cast<TabPage>().Any(tagPage => tagPage.Name == filename);

                // Don't create new tab if the file is already open
                if (tabOpen)
                {
                    var tab = this.DashGlobal.TabsHelper.GetTabByFilename(this.mainTabControl, filename);
                    this.mainTabControl.SelectTab(tab);

                    return;
                }

                if (!File.Exists(filename))
                {
                    MessageBox.Show(string.Format("Unable to open file:\n\n'{0}'\n\nFile does not exist!", filename));
                }
                else
                {
                    if (this.mainTabControl.SelectedTab != null
                        && (this.mainTabControl.SelectedTab.Text == "New File"
                            && this.DashGlobal.EditorHelper.GetActiveEditor().Text == string.Empty))
                    {
                        // Close the current tab
                        this.mainTabControl.TabPages.Remove(this.mainTabControl.SelectedTab);
                    }

                    this.DashGlobal.TabsHelper.CreateTabOpenFile(filename);
                }
            }
        }

        /// <summary>
        /// The main tab control_ selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="closee">
        /// The closee.
        /// </param>
        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs closee)
        {
            var tabControl = (TabControl)sender;

            if (tabControl.SelectedTab != null)
            {
                tabControl.SelectedTab.Controls[0].Focus();
                if (tabControl.SelectedTab.Controls[0].Tag.ToString() == string.Empty)
                {
                    this.DashGlobal.SetWindowTitle("{new file}");
                }
                else
                {
                    this.DashGlobal.SetWindowTitle(tabControl.SelectedTab.Controls[0].Tag.ToString());

                    Lang = this.DashGlobal.FilesHelper.GetLangFromFile(
                        tabControl.SelectedTab.Controls[0].Tag.ToString());
                }

                // TODO - Optimise this
                this.ArmaSense = this.DashGlobal.EditorHelper.CreateArmaSense();
            }

            // Break if the form isn't loaded
            if (!formLoaded)
            {
                return;
            }

            // TODO - Optimise this
            this.DashGlobal.EditorHelper.UserVariablesCurrentFile = new List<UserVariable>();

            this.DashGlobal.SettingsHelper.UpdateOpenTabs();
            Settings.Default.Save();
        }

        /// <summary>
        /// The main tab control_ mouse up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void mainTabControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.DashGlobal.TabsHelper.CloseTab(this.DashGlobal.TabsHelper.GetClickedTab(e));
                this.DashGlobal.SettingsHelper.UpdateOpenTabs();
            }

            if (e.Button == MouseButtons.Right)
            {
                var clickedTab = this.DashGlobal.TabsHelper.GetClickedTab(e);
                var pos = new Point(e.Location.X - 4, e.Location.Y - 24);
                this.mainTabControl.SelectTab(clickedTab);
                this.tabBarContextMenu.Show(clickedTab, pos);
            }
        }

        /// <summary>
        /// The about tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #region Background Worker for building file userVars / rebuilding ArmaSense on update

        /// <summary>
        /// The load user vars bw_ do work.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void loadUserVarsBw_DoWork(object sender, DoWorkEventArgs e)
        {
            var workerObject = e.Argument as WorkerObject;

            // Build user variables in current file
            if (workerObject != null)
            {
                var lines = workerObject.Editor.Lines;
                workerObject.UserVariables.Clear();

                foreach (var line in lines)
                {
                    // Loop through each occurange of string to left of "=" -> won't work if multiple variables are declared on one line
                    var parts = line.Split('=');
                    if (parts.Length > 1)
                    {
                        var variable = parts[0].Trim();
                        var tooltipParts = parts[1].Split(';');
                        var tooltip = tooltipParts[0].Trim();

                        var varType = this.DashGlobal.EditorHelper.GetVarTypeFromString(tooltip);

                        workerObject.UserVariables.Add(
                            new UserVariable { VarName = variable, TooltipTitle = varType, TooltipText = tooltip });
                    }

                    // Attempt using regex -> regex times out if lines are longer than 20 chars
                    // foreach (Match m in Regex.Matches(cleanLine, "(\\w+.)+="))
                    // {
                    // var parts = m.Value.Split('=');
                    // var variable = parts[0].Trim();
                    // workerObject.UserVariables.Add(variable);
                    // }
                }

                e.Result = workerObject;
            }
        }

        /// <summary>
        /// The load user vars bw_ run worker completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void loadUserVarsBw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = e.Result as WorkerObject;

            try
            {
                if (result == null)
                {
                    return;
                }

                this.DashGlobal.EditorHelper.UserVariablesCurrentFile = result.UserVariables;

                this.DashGlobal.EditorHelper.BuildAutocompleteMenu(this.ArmaSense, result.ForceUpdate);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
        }

        #endregion

        #region TextArea (Editor) Event Handlers

        /// <summary>
        /// The text area_ text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void textArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Break out if the form isn't loaded
            if (!allowSyntaxHighlighting)
            {
                return;
            }

            // Set markers for folding
            e.ChangedRange.ClearFoldingMarkers();

            e.ChangedRange.SetFoldingMarkers("{", "}");

            e.ChangedRange.SetFoldingMarkers(@"//region\b", @"//endregion\b");
            e.ChangedRange.SetFoldingMarkers(@"// region\b", @"// endregion\b");
            e.ChangedRange.SetFoldingMarkers(@"//#region\b", @"//#endregion\b");
            e.ChangedRange.SetFoldingMarkers(@"// #region\b", @"// #endregion\b");

            this.DashGlobal.EditorHelper.PerformSyntaxHighlighting(e, Lang);

            // Todo - switch this out for a check against the file CRC hash
            // FilesHelper.CheckFileDirtyState();

            // Make file dirty
            var tag = this.mainTabControl.SelectedTab.Tag as FileInfo;
            if (tag != null)
            {
                tag.Dirty = true;
            }

            this.mainTabControl.SelectedTab.Tag = tag;
        }

        /// <summary>
        /// The text area_ text changed delayed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void textArea_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            var worker = new WorkerObject
                             {
                                 Editor = this.DashGlobal.EditorHelper.ActiveEditor, 
                                 UserVariables = new List<UserVariable>()
                             };

            if (!this.loadUserVarsBw.IsBusy)
            {
                this.loadUserVarsBw.RunWorkerAsync(worker);
            }
        }

        /// <summary>
        /// The text area_ selection changed delayed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void textArea_SelectionChangedDelayed(object sender, EventArgs e)
        {
            var editor = this.DashGlobal.EditorHelper.GetActiveEditor();

            editor.Range.ClearStyle(this.sameWordsStyle);

            if (editor.Selection.IsEmpty)
            {
                // Highlight word matches

                // Get fragment around caret
                var fragment = editor.Selection.GetFragment(@"\w");
                var text = fragment.Text;

                if (text.Length == 0)
                {
                    return;
                }

                var ranges = editor.Range.GetRanges("\\b" + Regex.Escape(text) + "\\b").ToArray();
                if (ranges.Length > 1)
                {
                    foreach (var r in ranges)
                    {
                        r.SetStyle(this.sameWordsStyle);
                    }
                }
            }
            else
            {
                // Highlight exact selection matches

                // Get fragment around caret
                // var fragment = editor.Selection.GetFragment(@"\w");
                var text = editor.Selection.Text;

                if (text.Length == 0)
                {
                    return;
                }

                // Limit selection to 100 chars
                if (text.Length > 100)
                {
                    return;
                }

                // Don't highlight spaces
                if (text.Trim() == string.Empty)
                {
                    return;
                }

                // Escape regex chars
                text = text.Replace("[", "\\[");
                text = text.Replace("]", "\\]");
                text = text.Replace("$", "\\$");
                text = text.Replace(".", "\\.");

                // var ranges = editor.Range.GetRanges("\\b" + text + "\\b").ToArray();
                var ranges = editor.Range.GetRanges(Regex.Escape(text)).ToArray();
                if (ranges.Length > 1)
                {
                    foreach (var r in ranges)
                    {
                        r.SetStyle(this.sameWordsStyle);
                    }
                }
            }
        }

        /// <summary>
        /// The text area_ key up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void textArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemSemicolon)
            {
                var worker = new WorkerObject
                                 {
                                     Editor = this.DashGlobal.EditorHelper.ActiveEditor, 
                                     UserVariables = new List<UserVariable>(), 
                                     ForceUpdate = true
                                 };

                if (!this.loadUserVarsBw.IsBusy)
                {
                    this.loadUserVarsBw.RunWorkerAsync(worker);
                }

                // EditorHelper.BuildAutocompleteMenu(ArmaSense, true);
            }
        }

        /// <summary>
        /// The text area_ drag drop.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void textArea_DragDrop(object sender, DragEventArgs e)
        {
            var filename = e.Data.GetData("FileDrop");

            if (filename != null)
            {
                var list = filename as string[];

                if (list != null && !string.IsNullOrWhiteSpace(list[0]))
                {
                    foreach (var fileName in list)
                    {
                        // Check we're opening a file
                        var fileAttributes = File.GetAttributes(fileName);
                        if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            break;
                        }

                        try
                        {
                            if (this.mainTabControl.SelectedTab != null
                                && (this.mainTabControl.SelectedTab.Text == "New File"
                                    && this.DashGlobal.EditorHelper.GetActiveEditor().Text == string.Empty))
                            {
                                // Close the current tab
                                this.mainTabControl.TabPages.Remove(this.mainTabControl.SelectedTab);
                            }

                            this.DashGlobal.TabsHelper.CreateTabOpenFile(fileName);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(string.Format("Unable to load file:\n\n{0}", fileName));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The text area_ drag enter.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void textArea_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        #endregion

        #region Click Event Handlers

        /// <summary>
        /// The settings tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        /// <summary>
        /// The rebuild arma sense cache tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void rebuildArmaSenseCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var worker = new WorkerObject
                             {
                                 Editor = this.DashGlobal.EditorHelper.ActiveEditor, 
                                 UserVariables = this.DashGlobal.EditorHelper.UserVariablesCurrentFile, 
                                 ForceUpdate = true
                             };

            if (!this.loadUserVarsBw.IsBusy)
            {
                this.loadUserVarsBw.RunWorkerAsync(worker);
            }

            // EditorHelper.BuildAutocompleteMenu(ArmaSense, true);
        }

        /// <summary>
        /// The save tab bar context strip item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void saveTabBarContextStripItem_Click(object sender, EventArgs e)
        {
            this.saveToolStripMenuItem.PerformClick();
        }

        /// <summary>
        /// The close tab bar context menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void closeTabBarContextMenuItem_Click(object sender, EventArgs e)
        {
            this.DashGlobal.TabsHelper.CloseTab(this.mainTabControl.SelectedTab);
        }

        /// <summary>
        /// The close all but this tab bar context menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void closeAllButThisTabBarContextMenuItem_Click(object sender, EventArgs e)
        {
            this.DashGlobal.TabsHelper.CloseAllTabsExcept(this.mainTabControl.SelectedTab);

            // Force a save of the current open tab pages
            this.DashGlobal.SettingsHelper.UpdateOpenTabs();
        }

        /// <summary>
        /// The save tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsTabAlive())
            {
                var editor = this.DashGlobal.EditorHelper.GetActiveEditor();

                var filePath = editor.Tag.ToString();
                var fileContents = editor.Text;

                if (filePath == string.Empty)
                {
                    var sfd = new SaveFileDialog
                                  {
                                      Filter = "SQF File|*.sqf|C++ File|*.cpp|SQM File|*.sqm|All Files|*.*"
                                  };

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, this.DashGlobal.EditorHelper.GetActiveEditor().Text);
                        this.DashGlobal.TabsHelper.SetSelectedTabClean();
                    }
                }
                else
                {
                    File.WriteAllText(filePath, fileContents);
                    this.DashGlobal.TabsHelper.SetSelectedTabClean();
                }
            }
            else
            {
                MessageBox.Show("Failed to save file!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The open file tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFile = this.openFileDialog.ShowDialog();

            if (openFile == DialogResult.OK)
            {
                foreach (var file in this.openFileDialog.FileNames)
                {
                    this.DashGlobal.TabsHelper.CreateTabOpenFile(file);
                }
            }
        }

        /// <summary>
        /// The save as tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.IsTabAlive())
            {
                var sfd = new SaveFileDialog { Filter = "SQF File|*.sqf|C++ File|*.cpp|SQM File|*.sqm|All Files|*.*" };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, this.DashGlobal.EditorHelper.GetActiveEditor().Text);

                    // Close current tab
                    this.DashGlobal.TabsHelper.CloseTab(this.mainTabControl.SelectedTab);

                    // Reopen from file
                    this.DashGlobal.TabsHelper.CreateTabOpenFile(sfd.FileName);
                }
            }
            else
            {
                MessageBox.Show("Failed to save file!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The comment tool strip button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void commentToolStripButton_Click(object sender, EventArgs e)
        {
            this.DashGlobal.EditorHelper.ActiveEditor.CommentSelected();
        }

        /// <summary>
        /// The comment current line tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void commentCurrentLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DashGlobal.EditorHelper.ActiveEditor.CommentSelected();
        }

        /// <summary>
        /// The open file strip button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void openFileStripButton_Click(object sender, EventArgs e)
        {
            this.openFileToolStripMenuItem.PerformClick();
        }

        /// <summary>
        /// The save all tool strip button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void saveAllToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tabPage in this.mainTabControl.TabPages)
            {
                var editor = tabPage.Controls[0];

                var filePath = editor.Tag.ToString();
                var contents = editor.Text;

                if (filePath == string.Empty)
                {
                    var sfd = new SaveFileDialog
                                  {
                                      Filter = "SQF File|*.sqf|C++ File|*.cpp|SQM File|*.sqm|All Files|*.*"
                                  };

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, this.DashGlobal.EditorHelper.GetActiveEditor().Text);

                        // Close current tab
                        this.DashGlobal.TabsHelper.CloseTab(tabPage);

                        // Reopen from file
                        this.DashGlobal.TabsHelper.CreateTabOpenFile(sfd.FileName);
                    }
                }
                else
                {
                    File.WriteAllText(filePath, contents);
                }
            }
        }

        /// <summary>
        /// The save tool strip button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            this.saveToolStripMenuItem.PerformClick();
        }

        /// <summary>
        /// The lookup definition tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void lookupDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editor = this.DashGlobal.EditorHelper.GetActiveEditor();

            var fragment = editor.Selection.GetFragment(@"\w");

            var lookupWord = fragment.Text;

            Process.Start("https://community.bistudio.com/wiki?search=" + lookupWord);
        }

        /// <summary>
        /// The bohemia interactive wiki tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void bohemiaInteractiveWikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://community.bistudio.com/wiki/Category:Scripting_Commands");
        }

        /// <summary>
        /// The open folder tool strip button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void openFolderToolStripButton_Click(object sender, EventArgs e)
        {
            this.openFolderToolStripMenuItem.PerformClick();
        }

        /// <summary>
        /// The show quick help tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void showQuickHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tutorialFile = AppDomain.CurrentDomain.BaseDirectory + "\\tutorial.txt";
            if (File.Exists(tutorialFile))
            {
                this.DashGlobal.TabsHelper.CreateBlankTab(FileType.Other, "tutorial.txt");
                this.DashGlobal.EditorHelper.GetActiveEditor().Text = File.ReadAllText(tutorialFile);
            }
            else
            {
                MessageBox.Show(
                    "Unable to find tutorial.txt in the Dash install directory\n\nPlease go to the Dash website to view the documentation there instead.");
            }
        }

        /// <summary>
        /// The close tab tool strip button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void closeTabToolStripButton_Click(object sender, EventArgs e)
        {
            this.closeToolStripMenuItem.PerformClick();
        }

        /// <summary>
        /// The btn refresh file browser_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnRefreshFileBrowser_Click(object sender, EventArgs e)
        {
            this.DashGlobal.FilesHelper.SetTreeviewDirectory(this.directoryTreeView, Settings.Default.TreeviewDir);
        }

        /// <summary>
        /// The new sqf file tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void newSqfFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DashGlobal.TabsHelper.CreateBlankTab();
        }

        /// <summary>
        /// The new cpp file tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void newCppFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DashGlobal.TabsHelper.CreateBlankTab(FileType.Cpp);
        }

        /// <summary>
        /// The new sqf file tool strip button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void newSqfFileToolStripButton_Click(object sender, EventArgs e)
        {
            this.newSqfFileToolStripMenuItem.PerformClick();
        }

        /// <summary>
        /// The close tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DashGlobal.TabsHelper.CloseTab(this.mainTabControl.SelectedTab);
            this.DashGlobal.SettingsHelper.UpdateOpenTabs();
            Settings.Default.Save();
        }

        /// <summary>
        /// The dash website tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void dashWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://dash.nevadascout.com/");
        }

        /// <summary>
        /// The dash documentation tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void dashDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://dash.nevadascout.com/docs/");
        }

        /// <summary>
        /// The open folder tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = this.openFolderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.DashGlobal.FilesHelper.SetTreeviewDirectory(
                    this.directoryTreeView, 
                    this.openFolderDialog.SelectedPath);
            }

            Settings.Default.TreeviewDir = this.openFolderDialog.SelectedPath;
            Settings.Default.Save();
        }

        /// <summary>
        /// The log stack trace tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void logStackTraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Not going to work as the stack trace only includes user clicking "dump stack trace"
            // Logger.Log("User Requested Stack Trace:");
            Process.Start("https://github.com/nevadascout/Dash/issues");
        }

        /// <summary>
        /// The send feedback tool strip menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:travis@impactmod.com");
        }

        #endregion

        private void closeAllTabsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Disabled as this doesn't do any file "dirty" status checking before closing tabs
            this.mainTabControl.TabPages.Clear();
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Simple method of clearing project ~ trdwll
            this.directoryTreeView.Nodes.Clear();
            Settings.Default.TreeviewDir = null;
            Settings.Default.Save();
        }

        /// <summary>
        /// Check if zero or more tabs are active
        /// </summary>
        /// <returns>Returns true or false</returns>
        private bool IsTabAlive()
        {
            if (this.mainTabControl.TabCount <= 0)
            {
                return false;
            }

            return true;
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check for updates ~ trdwll
            this.CheckForUpdate(true);
        }

        /// <summary>
        /// Check for updates
        /// 
        /// Update code to download the file in the background rather than
        /// via Process.Start(url);
        /// 
        /// ~ trdwll
        /// </summary>
        /// <param name="showNotification">
        /// True if you want to display a notification in the notification area
        /// </param>
        private void CheckForUpdate(bool showNotification = false)
        {
            try
            {
                var client = new WebClient();
                var stream = client.OpenRead(this.site);

                if (stream != null)
                {
                    var reader = new StreamReader(stream);
                    var webVersion = Convert.ToDouble(reader.ReadToEnd());

                    if (webVersion > this.version)
                    {
                        if (showNotification)
                        {
                            var icoPath = Application.StartupPath + @"\dash-code.ico";

                            if (File.Exists(icoPath))
                            {
                                this.updateIcon.Icon = new Icon(icoPath);
                            }
                            else
                            {
                                this.updateIcon.Icon = SystemIcons.Information;
                            }

                            this.updateIcon.BalloonTipText = "Dash Update " + webVersion + " Available!";
                            this.updateIcon.BalloonTipTitle = "Dash";
                            this.updateIcon.ShowBalloonTip(3000);
                        }

                        var dialogResult = MessageBox.Show("Would you like to download the newest update?", "Update " + webVersion + " Available", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            Process.Start(this.downloadFileLocation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You are running: " + this.version + "\nThis is the current version.");
                    }

                    stream.Close();
                    stream.Dispose();
                }

                client.Dispose();
            }
            catch
            {
                MessageBox.Show("Unable to update. Check your internet connection!");
            }
        }

        #region Drag & Drop

        // ~ trdwll
        private void directoryTreeView_DragDrop(object sender, DragEventArgs e)
        {
            foreach (var filePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                this.DashGlobal.FilesHelper.SetTreeviewDirectory(this.directoryTreeView, filePath);
                Settings.Default.TreeviewDir = filePath;
                Settings.Default.Save();
            }
        }

        private void directoryTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        #endregion Drag & Drop
    }

    /// <summary>
    /// The user variable.
    /// </summary>
    public class UserVariable
    {
        /// <summary>
        /// Gets or sets the var name.
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// Gets or sets the tooltip title.
        /// </summary>
        public string TooltipTitle { get; set; }

        /// <summary>
        /// Gets or sets the tooltip text.
        /// </summary>
        public string TooltipText { get; set; }
    }

    /// <summary>
    /// The worker object.
    /// </summary>
    internal class WorkerObject
    {
        /// <summary>
        /// Gets or sets the editor.
        /// </summary>
        public FastColoredTextBox Editor { get; set; }

        /// <summary>
        /// Gets or sets the user variables.
        /// </summary>
        public List<UserVariable> UserVariables { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether force update.
        /// </summary>
        public bool ForceUpdate { get; set; }
    }
}