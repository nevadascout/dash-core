using FastColoredTextBoxNS;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Dash
{
    public class TabsHelper
    {
        private EventHandler<TextChangedEventArgs> TextAreaTextChanged { get; set; }

        private EventHandler TextAreaSelectionChangedDelayed { get; set; }

        public TabControl MainTabControl { get; set; }

        public DashGlobal DashGlobal { get; set; }

        public TabsHelper(EventHandler<TextChangedEventArgs> textAreaTextChanged, EventHandler textAreaSelectionChangedDelayed, TabControl mainTabControl, DashGlobal dashGlobal)
        {
            MainTabControl = mainTabControl;
            TextAreaSelectionChangedDelayed = textAreaSelectionChangedDelayed;
            TextAreaTextChanged = textAreaTextChanged;
            DashGlobal = dashGlobal;
        }

        public void CreateBlankTab(FileType fileType = FileType.Sqf, string filename = "New File")
        {
            var cleanName = filename + MainTabControl.TabPages.Count;

            MainTabControl.TabPages.Add(new TabPage(filename) { Name = cleanName });
            MainTabControl.SuspendLayout();
            MainTabControl.TabPages[cleanName].Controls.Add(DashGlobal.EditorHelper.CreateEditor());
            MainTabControl.TabPages[cleanName].Tag = new FileInfo() { Dirty = false };
            MainTabControl.ResumeLayout();
            MainTabControl.SelectTab(cleanName);
        }

        public void CreateTabOpenFile(string fileToOpen)
        {
            var fileParts = fileToOpen.Split('\\');
            var tabText = fileParts[fileParts.Count() - 1];

            MainTabControl.TabPages.Add(new TabPage(tabText) { Name = fileToOpen });
            MainTabControl.SuspendLayout();
            MainTabControl.TabPages[fileToOpen].Controls.Add(DashGlobal.EditorHelper.CreateEditor(fileToOpen));
            MainTabControl.ResumeLayout();
            MainTabControl.SelectTab(fileToOpen);

            DashGlobal.SetWindowTitle(fileToOpen);
            Main.Lang = DashGlobal.FilesHelper.GetLangFromFile(fileToOpen);

            DashGlobal.EditorHelper.ActiveEditor.OpenFile(fileToOpen);
            MainTabControl.TabPages[fileToOpen].Tag = new FileInfo() { Dirty = false, CrcHash = CRC.CRC32.ComputeHash(fileToOpen), filePath = fileToOpen };
            DashGlobal.EditorHelper.PerformSyntaxHighlighting(null, Main.Lang, true);
        }

        public void CloseTab(TabPage tab)
        {
            var tabCount = MainTabControl.TabPages.Count;
            bool closingCurrentTab = (MainTabControl.SelectedTab == tab);
            var closingTabId = MainTabControl.SelectedIndex;

            // Break out if no tab selected
            if (tab == null) return;
            if (!closingCurrentTab) return;

            // Don't close if the file hasn't been changed from default
            if (tabCount == 1 && tab.Controls[0].Text == string.Empty) return;

            var tag = MainTabControl.SelectedTab.Tag as FileInfo;

            if (tag.Dirty)
            {
                DialogResult message = MessageBox.Show("This file has been modified. Do you want to save it?", "Save file?", MessageBoxButtons.YesNoCancel);
                if (message == DialogResult.Yes)
                {
                    // TODO -- Add call to filesHelper.SaveFile() to save the file or save as if it hasn't yet been saved
                    return;
                }
            }

            if (closingTabId == (tabCount - 1))
            {
                if (MainTabControl.TabPages.Count == 1)
                {
                    MainTabControl.TabPages.Remove(tab);
                    CreateBlankTab(FileType.Other);
                    DashGlobal.SetWindowTitle("{new file}");
                    DashGlobal.EditorHelper.ActiveEditor.Focus();
                    return;
                }

                // If we're closing the last tab in the list, select the tab to the left
                MainTabControl.SelectTab(MainTabControl.TabPages[closingTabId - 1]);
            }
            else
            {
                // Select the right-most tab
                MainTabControl.SelectTab(MainTabControl.TabPages[closingTabId + 1]);
            }

            // Close the tab
            MainTabControl.TabPages.Remove(tab);

            if (MainTabControl.TabPages.Count == 0)
            {
                CreateBlankTab(FileType.Other);
                DashGlobal.SetWindowTitle("{new file}");
            }

            DashGlobal.EditorHelper.ActiveEditor.Focus();
        }

        public void CloseAllTabsExcept(TabPage tab)
        {
            foreach (TabPage tabPage in MainTabControl.TabPages)
            {
                if (tabPage != tab)
                {
                    this.CloseTab(tabPage);
                }
            }
        }

        public TabPage GetTabByFilename(TabControl mainTabControl, string filename)
        {
            return mainTabControl.TabPages.Cast<TabPage>().FirstOrDefault(tab => tab.Name == filename);
        }

        public TabPage GetClickedTab(MouseEventArgs e)
        {
            TabPage page = null;

            try
            {
                page = MainTabControl.TabPages.Cast<TabPage>()
                                     .Where((t, i) =>
                                         MainTabControl.GetTabRect(i)
                                                       .Contains(e.Location)).First();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }

            return page;
        }

        public void SetSelectedTabDirty()
        {
            FileInfo info = MainTabControl.SelectedTab.Tag as FileInfo;
            info.Dirty = true;
            MainTabControl.SelectedTab.Tag = info;
        }

        public void SetSelectedTabClean()
        {
            FileInfo info = MainTabControl.SelectedTab.Tag as FileInfo;
            info.Dirty = false;
            MainTabControl.SelectedTab.Tag = info;
        }

        /// <summary>
        /// Function to check if the File was changed outside of the editor.
        /// </summary>
        public void CheckTabDirtyState()
        {
            FileInfo info = MainTabControl.SelectedTab.Tag as FileInfo;
            if (info != null)
            {
                if (System.IO.File.Exists(info.filePath))
                {
                    UInt32 CrcCheck = CRC.CRC32.ComputeHash(info.filePath);
                    if (CrcCheck != info.CrcHash)
                    {
                        var result = System.Windows.Forms.MessageBox.Show((info.filePath + "\r\n\r\nThis file was changed by another program!\r\nDo you want to reload it?"),
                            "Reload", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            CloseTab(MainTabControl.SelectedTab);
                            CreateTabOpenFile(info.filePath);
                            info.CrcHash = CrcCheck;
                        }
                        else
                            info.Dirty = true;
                    }
                }
                else
                {
                    var result = System.Windows.Forms.MessageBox.Show((info.filePath + "\r\n\r\nThis file has been deleted!\r\nDo you want to keep it?"),
                    "File deleted", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                        CloseTab(MainTabControl.SelectedTab);
                }
            }
            // TODO -- Add more crc checks. Now it is only checked if you change the tab
        }
    }

    public class FileInfo
    {
        public bool Dirty { get; set; }

        public UInt32 CrcHash { get; set; }

        public string filePath { get; set; }
    }
}