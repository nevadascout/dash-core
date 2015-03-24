using Dash.Properties;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dash
{
    public partial class Main : Form
    {
        private AutocompleteMenu ArmaSense { get; set; }
        
        private FileSystemWatcher watcher = new FileSystemWatcher();

        private static bool allowSyntaxHighlighting = false;
        private static bool formLoaded = false;

        // Styles
        private readonly MarkerStyle sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        public static string Lang { get; set; }

        public DashGlobal DashGlobal { get; set; }


        public Main()
        {
            this.DashGlobal = new DashGlobal(
                textArea_TextChanged,
                textArea_TextChangedDelayed,
                textArea_KeyUp,
                textArea_SelectionChangedDelayed,
                textArea_DragDrop,
                textArea_DragEnter,
                mainTabControl,
                ArmaSense,
                this);

            InitializeComponent();

            this.DashGlobal.EditorHelper.MainTabControl = mainTabControl;
            this.DashGlobal.TabsHelper.MainTabControl = mainTabControl;
            this.DashGlobal.SettingsHelper.MainTabControl = mainTabControl;
            this.DashGlobal.EditorHelper.ArmaSenseImageList = armaSenseImageList;
        }
        
        private void Main_Load(object sender, EventArgs e)
        {
            // Populate Treeview
            DashGlobal.FilesHelper.SetTreeviewDirectory(directoryTreeView, Settings.Default.TreeviewDir);

            // TODO - Optimise this
            DashGlobal.EditorHelper.UserVariablesCurrentFile = new List<UserVariable>();

            // Set window size from memory
            if (Settings.Default.WindowMaximised)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.Size = new Size(
                    Convert.ToInt32(Settings.Default.Window_Width),
                    Convert.ToInt32(Settings.Default.Window_Height)
                );
            }

            // Clear tabs + add default tab
            foreach (TabPage tab in mainTabControl.TabPages)
            {
                mainTabControl.TabPages.Remove(tab);
            }

            // Allow the textArea_TextChanged event to fire so
            // we get syntax highlighting in open files on form load
            allowSyntaxHighlighting = true;

            if (Settings.Default.OpenTabs != null)
            {
                if (Settings.Default.OpenTabs.Count == 0)
                {
                    DashGlobal.TabsHelper.CreateBlankTab(FileType.Other);
                    ArmaSense = DashGlobal.EditorHelper.CreateArmaSense();
                }
                else
                {
                    foreach (var file in Settings.Default.OpenTabs)
                    {
                        try
                        {
                            if (File.Exists(file)) DashGlobal.TabsHelper.CreateTabOpenFile(file);
                            else throw new Exception("File not found!");
                        }
                        catch
                        {
                            MessageBox.Show(string.Format("Unable to open file:\n\n{0}", file));
                        }
                    }

                    if (Settings.Default.SelectedTab < mainTabControl.TabCount)
                    {
                        mainTabControl.SelectTab(Settings.Default.SelectedTab);
                    }
                }
            }
            else
            {
                Settings.Default.OpenTabs = new List<string>();
            }

            if (mainTabControl.TabPages.Count == 0)
            {
                DashGlobal.TabsHelper.CreateBlankTab();
                DashGlobal.SetWindowTitle("{new file}");
            }

            if ((bool)Settings.Default.FirstLoad)
            {
                var tutorialFile = AppDomain.CurrentDomain.BaseDirectory + "\\tutorial.txt";
                if (File.Exists(tutorialFile))
                {
                    DashGlobal.TabsHelper.CreateBlankTab(FileType.Other, "tutorial.txt");
                    DashGlobal.EditorHelper.GetActiveEditor().Text = File.ReadAllText(tutorialFile);
                    DashGlobal.TabsHelper.SetSelectedTabClean();
                }
            }

            // Add file watcher to update treeview on file change
            //watcher.Path = Settings.Default.TreeviewDir;

            //watcher.NotifyFilter = NotifyFilters.DirectoryName | 
            //                       NotifyFilters.FileName | 
            //                       NotifyFilters.LastAccess |
            //                       NotifyFilters.LastWrite;

            //watcher.Created += FileSystemChanged;
            //watcher.Changed += FileSystemChanged;
            //watcher.Deleted += FileSystemChanged;
            //watcher.Renamed += FileSystemChanged;

            //watcher.EnableRaisingEvents = true;

            formLoaded = true;

            // Apply stored settings
            mainSplitContainer.SplitterDistance = Convert.ToInt32(Settings.Default.SplitterWidth);

            DashGlobal.EditorHelper.ActiveEditor.GoHome();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DashGlobal.SettingsHelper.UpdateOpenTabs();
            Settings.Default.SelectedTab = mainTabControl.SelectedIndex;
            Settings.Default.Save();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            Control window = (Control) sender;

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

        public void InitStylesPriority()
        {
            textArea.AddStyle(sameWordsStyle);
        }

        private void FileSystemChanged(object source, FileSystemEventArgs e)
        {
            DashGlobal.FilesHelper.SetTreeviewDirectory(directoryTreeView, Settings.Default.TreeviewDir);
        }


        private void mainSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (formLoaded)
            {
                Settings.Default.SplitterWidth = mainSplitContainer.SplitterDistance;
                Settings.Default.Save();
            }
        }

        private void directoryTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // If not a folder, load into the editor
            if (e.Node.ImageIndex != 0 && e.Node.ImageIndex != 4)
            {
                var filename = e.Node.Tag.ToString();

                bool tabOpen = mainTabControl.TabPages.Cast<TabPage>().Any(tagPage => tagPage.Name == filename);

                // Don't create new tab if the file is already open
                if (tabOpen)
                {
                    TabPage tab = DashGlobal.TabsHelper.GetTabByFilename(mainTabControl, filename);
                    mainTabControl.SelectTab(tab);

                    return;
                }

                if (!File.Exists(filename))
                {
                    MessageBox.Show(string.Format("Unable to open file:\n\n'{0}'\n\nFile does not exist!", filename));
                }
                else
                {
                    if (mainTabControl.SelectedTab != null && (mainTabControl.SelectedTab.Text == "New File" && DashGlobal.EditorHelper.GetActiveEditor().Text == string.Empty))
                    {
                        // Close the current tab
                        mainTabControl.TabPages.Remove(mainTabControl.SelectedTab);
                    }

                    DashGlobal.TabsHelper.CreateTabOpenFile(filename);
                }
            }
        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs closee)
        {
            TabControl tabControl = (TabControl)sender;

            if (tabControl.SelectedTab != null)
            {
                tabControl.SelectedTab.Controls[0].Focus();
                if (tabControl.SelectedTab.Controls[0].Tag.ToString() == "")
                {
                    DashGlobal.SetWindowTitle("{new file}");
                }
                else
                {
                    DashGlobal.SetWindowTitle(tabControl.SelectedTab.Controls[0].Tag.ToString());

                    Lang = DashGlobal.FilesHelper.GetLangFromFile(tabControl.SelectedTab.Controls[0].Tag.ToString());
                }

                // TODO - Optimise this
                ArmaSense = DashGlobal.EditorHelper.CreateArmaSense();
            }

            // Break if the form isn't loaded
            if (!formLoaded) return;
            
            // TODO - Optimise this
            DashGlobal.EditorHelper.UserVariablesCurrentFile = new List<UserVariable>();

            DashGlobal.SettingsHelper.UpdateOpenTabs();
            Settings.Default.Save();
        }



        #region Background Worker for building file userVars / rebuilding ArmaSense on update

        private void loadUserVarsBw_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerObject workerObject = e.Argument as WorkerObject;

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

                        var varType = DashGlobal.EditorHelper.GetVarTypeFromString(tooltip);

                        workerObject.UserVariables.Add(new UserVariable { VarName = variable, TooltipTitle = varType, TooltipText = tooltip });
                    }

                    // Attempt using regex -> regex times out if lines are longer than 20 chars
                    //foreach (Match m in Regex.Matches(cleanLine, "(\\w+.)+="))
                    //{
                    //    var parts = m.Value.Split('=');
                    //    var variable = parts[0].Trim();
                    //    workerObject.UserVariables.Add(variable);
                    //}
                }

                e.Result = workerObject;
            }
        }

        private void loadUserVarsBw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = e.Result as WorkerObject;
            
            try
            {
                if (result == null) return;

                DashGlobal.EditorHelper.UserVariablesCurrentFile = result.UserVariables;

                DashGlobal.EditorHelper.BuildAutocompleteMenu(ArmaSense, result.ForceUpdate);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
        }

        #endregion

        
        #region TextArea (Editor) Event Handlers

        public void textArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Break out if the form isn't loaded
            if (!allowSyntaxHighlighting) return;

            // Set markers for folding
            e.ChangedRange.ClearFoldingMarkers();

            e.ChangedRange.SetFoldingMarkers("{", "}");

            e.ChangedRange.SetFoldingMarkers(@"//region\b", @"//endregion\b");
            e.ChangedRange.SetFoldingMarkers(@"// region\b", @"// endregion\b");
            e.ChangedRange.SetFoldingMarkers(@"//#region\b", @"//#endregion\b");
            e.ChangedRange.SetFoldingMarkers(@"// #region\b", @"// #endregion\b");

            DashGlobal.EditorHelper.PerformSyntaxHighlighting(e, Lang);


            // Todo - switch this out for a check against the file CRC hash
            //FilesHelper.CheckFileDirtyState();


            // Make file dirty
            FileInfo tag = mainTabControl.SelectedTab.Tag as FileInfo;
            if (tag != null) tag.Dirty = true;
            mainTabControl.SelectedTab.Tag = tag;
        }

        private void textArea_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            WorkerObject worker = new WorkerObject
            {
                Editor = DashGlobal.EditorHelper.ActiveEditor,
                UserVariables = new List<UserVariable>()
            };

            if (!loadUserVarsBw.IsBusy)
            {
                loadUserVarsBw.RunWorkerAsync(worker);
            }
        }

        public void textArea_SelectionChangedDelayed(object sender, EventArgs e)
        {
            var editor = DashGlobal.EditorHelper.GetActiveEditor();

            editor.Range.ClearStyle(sameWordsStyle);

            if (editor.Selection.IsEmpty)
            {
                // Highlight word matches

                // Get fragment around caret
                var fragment = editor.Selection.GetFragment(@"\w");
                string text = fragment.Text;

                if (text.Length == 0)
                    return;

                var ranges = editor.Range.GetRanges("\\b" + Regex.Escape(text) + "\\b").ToArray();
                if (ranges.Length > 1)
                {
                    foreach (var r in ranges)
                    {
                        r.SetStyle(sameWordsStyle);
                    }
                }
            }
            else
            {
                // Highlight exact selection matches

                // Get fragment around caret
                //var fragment = editor.Selection.GetFragment(@"\w");
                string text = editor.Selection.Text;

                if (text.Length == 0)
                    return;

                // Limit selection to 100 chars
                if (text.Length > 100)
                    return;

                // Don't highlight spaces
                if (text.Trim() == string.Empty)
                    return;

                // Escape regex chars
                text = text.Replace("[", "\\[");
                text = text.Replace("]", "\\]");
                text = text.Replace("$", "\\$");
                text = text.Replace(".", "\\.");

                //var ranges = editor.Range.GetRanges("\\b" + text + "\\b").ToArray();
                var ranges = editor.Range.GetRanges(Regex.Escape(text)).ToArray();
                if (ranges.Length > 1)
                {
                    foreach (var r in ranges)
                    {
                        r.SetStyle(sameWordsStyle);
                    }
                }
            }
        }

        private void textArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemSemicolon)
            {
                WorkerObject worker = new WorkerObject
                {
                    Editor = DashGlobal.EditorHelper.ActiveEditor,
                    UserVariables = new List<UserVariable>(),
                    ForceUpdate = true
                };

                if (!loadUserVarsBw.IsBusy)
                {
                    loadUserVarsBw.RunWorkerAsync(worker);
                }
                //EditorHelper.BuildAutocompleteMenu(ArmaSense, true);
            }
        }

        private void textArea_DragDrop(object sender, DragEventArgs e)
        {
            object filename = e.Data.GetData("FileDrop");

            if (filename != null)
            {
                var list = filename as string[];

                if (list != null && !string.IsNullOrWhiteSpace(list[0]))
                {
                    foreach (var fileName in list)
                    {
                        // Check we're opening a file
                        FileAttributes fileAttributes = File.GetAttributes(fileName);
                        if((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory) break;

                        try
                        {
                            if (mainTabControl.SelectedTab != null && (mainTabControl.SelectedTab.Text == "New File" && DashGlobal.EditorHelper.GetActiveEditor().Text == string.Empty))
                            {
                                // Close the current tab
                                mainTabControl.TabPages.Remove(mainTabControl.SelectedTab);
                            }

                            DashGlobal.TabsHelper.CreateTabOpenFile(fileName);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(string.Format("Unable to load file:\n\n{0}", fileName));
                        }
                    }
                }
            }
        }

        private void textArea_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        #endregion


        #region Click Event Handlers

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void rebuildArmaSenseCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkerObject worker = new WorkerObject
            {
                Editor = DashGlobal.EditorHelper.ActiveEditor,
                UserVariables = DashGlobal.EditorHelper.UserVariablesCurrentFile,
                ForceUpdate = true
            };

            if (!loadUserVarsBw.IsBusy)
            {
                loadUserVarsBw.RunWorkerAsync(worker);
            }
            //EditorHelper.BuildAutocompleteMenu(ArmaSense, true);
        }

        private void saveTabBarContextStripItem_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem.PerformClick();
        }

        private void closeTabBarContextMenuItem_Click(object sender, EventArgs e)
        {
            DashGlobal.TabsHelper.CloseTab(mainTabControl.SelectedTab);
        }

        private void closeAllButThisTabBarContextMenuItem_Click(object sender, EventArgs e)
        {
            DashGlobal.TabsHelper.CloseAllTabsExcept(mainTabControl.SelectedTab);

            // Force a save of the current open tab pages
            DashGlobal.SettingsHelper.UpdateOpenTabs();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editor = DashGlobal.EditorHelper.GetActiveEditor();

            var filePath = editor.Tag.ToString();
            var contents = editor.Text;

            if (filePath == String.Empty)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "SQF File|*.sqf|C++ File|*.cpp|SQM File|*.sqm|All Files|*.*"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, DashGlobal.EditorHelper.GetActiveEditor().Text);
                    DashGlobal.TabsHelper.SetSelectedTabClean();
                }
            }
            else
            {
                File.WriteAllText(filePath, contents);
                DashGlobal.TabsHelper.SetSelectedTabClean();
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult openFile = openFileDialog.ShowDialog();

            if (openFile == DialogResult.OK)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    DashGlobal.TabsHelper.CreateTabOpenFile(file);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "SQF File|*.sqf|C++ File|*.cpp|SQM File|*.sqm|All Files|*.*"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, DashGlobal.EditorHelper.GetActiveEditor().Text);

                // Close current tab
                DashGlobal.TabsHelper.CloseTab(mainTabControl.SelectedTab);

                // Reopen from file
                DashGlobal.TabsHelper.CreateTabOpenFile(sfd.FileName);
            }
        }

        private void commentToolStripButton_Click(object sender, EventArgs e)
        {
            DashGlobal.EditorHelper.ActiveEditor.CommentSelected();
        }

        private void commentCurrentLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DashGlobal.EditorHelper.ActiveEditor.CommentSelected();
        }

        private void openFileStripButton_Click(object sender, EventArgs e)
        {
            openFileToolStripMenuItem.PerformClick();
        }

        private void saveAllToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tabPage in mainTabControl.TabPages)
            {
                var editor = tabPage.Controls[0];

                var filePath = editor.Tag.ToString();
                var contents = editor.Text;

                if (filePath == String.Empty)
                {
                    SaveFileDialog sfd = new SaveFileDialog
                    {
                        Filter = "SQF File|*.sqf|C++ File|*.cpp|SQM File|*.sqm|All Files|*.*"
                    };

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, DashGlobal.EditorHelper.GetActiveEditor().Text);

                        // Close current tab
                        DashGlobal.TabsHelper.CloseTab(tabPage);

                        // Reopen from file
                        DashGlobal.TabsHelper.CreateTabOpenFile(sfd.FileName);
                    }
                }
                else
                {
                    File.WriteAllText(filePath, contents);
                }
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem.PerformClick();
        }

        private void lookupDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editor = DashGlobal.EditorHelper.GetActiveEditor();

            var fragment = editor.Selection.GetFragment(@"\w");

            var lookupWord = fragment.Text;

            Process.Start("https://community.bistudio.com/wiki?search=" + lookupWord);
        }

        private void bohemiaInteractiveWikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://community.bistudio.com/wiki/Category:Scripting_Commands");
        }

        private void openFolderToolStripButton_Click(object sender, EventArgs e)
        {
            openFolderToolStripMenuItem.PerformClick();
        }

        private void showQuickHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tutorialFile = AppDomain.CurrentDomain.BaseDirectory + "\\tutorial.txt";
            if (File.Exists(tutorialFile))
            {
                DashGlobal.TabsHelper.CreateBlankTab(FileType.Other, "tutorial.txt");
                DashGlobal.EditorHelper.GetActiveEditor().Text = File.ReadAllText(tutorialFile);
            }
            else
            {
                MessageBox.Show("Unable to find tutorial.txt in the Dash install directory\n\nPlease go to the Dash website to view the documentation there instead.");
            }
        }

        private void closeTabToolStripButton_Click(object sender, EventArgs e)
        {
            closeToolStripMenuItem.PerformClick();
        }

        private void btnRefreshFileBrowser_Click(object sender, EventArgs e)
        {
            DashGlobal.FilesHelper.SetTreeviewDirectory(directoryTreeView, Settings.Default.TreeviewDir);
        }

        private void newSqfFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DashGlobal.TabsHelper.CreateBlankTab();
        }

        private void newCppFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DashGlobal.TabsHelper.CreateBlankTab(FileType.Cpp);
        }

        private void newSqfFileToolStripButton_Click(object sender, EventArgs e)
        {
            newSqfFileToolStripMenuItem.PerformClick();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DashGlobal.TabsHelper.CloseTab(mainTabControl.SelectedTab);
            DashGlobal.SettingsHelper.UpdateOpenTabs();
            Settings.Default.Save();
        }

        private void dashWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://dash.nevadascout.com/");
        }

        private void dashDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://dash.nevadascout.com/docs/");
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFolderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                DashGlobal.FilesHelper.SetTreeviewDirectory(directoryTreeView, openFolderDialog.SelectedPath);
            }

            Settings.Default.TreeviewDir = openFolderDialog.SelectedPath;
            Settings.Default.Save();
        }

        private void logStackTraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Not going to work as the stack trace only includes user clicking "dump stack trace"
            //Logger.Log("User Requested Stack Trace:");

            Process.Start("https://github.com/nevadascout/Dash/issues/new");
        }

        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://dash.nevadascout.com/feedback");
        }

        #endregion



        private void mainTabControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                DashGlobal.TabsHelper.CloseTab(DashGlobal.TabsHelper.GetClickedTab(e));
                DashGlobal.SettingsHelper.UpdateOpenTabs();
            }

            if (e.Button == MouseButtons.Right)
            {
                var clickedTab = DashGlobal.TabsHelper.GetClickedTab(e);
                var pos = new Point((e.Location.X - 4), (e.Location.Y - 24));
                mainTabControl.SelectTab(clickedTab);
                tabBarContextMenu.Show(clickedTab, pos);
            }
        }

    }


    class WorkerObject
    {
        public FastColoredTextBox Editor { get; set; }
        public List<UserVariable> UserVariables { get; set; }
        public bool ForceUpdate { get; set; }
    }

    public class UserVariable
    {
        public string VarName { get; set; }
        public string TooltipTitle { get; set; }
        public string TooltipText { get; set; }
    }
}