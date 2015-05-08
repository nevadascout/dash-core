namespace Dash
{
    using System;
    using System.Linq;
    using System.Windows.Forms;

    using FastColoredTextBoxNS;

    public class TabsHelper
    {
        public TabsHelper(
            EventHandler<TextChangedEventArgs> textAreaTextChanged, 
            EventHandler textAreaSelectionChangedDelayed, 
            TabControl mainTabControl, 
            DashGlobal dashGlobal)
        {
            this.MainTabControl = mainTabControl;
            this.TextAreaSelectionChangedDelayed = textAreaSelectionChangedDelayed;
            this.TextAreaTextChanged = textAreaTextChanged;
            this.DashGlobal = dashGlobal;
        }

        public TabControl MainTabControl { get; set; }

        public DashGlobal DashGlobal { get; set; }

        private EventHandler<TextChangedEventArgs> TextAreaTextChanged { get; set; }

        private EventHandler TextAreaSelectionChangedDelayed { get; set; }

        public void CreateBlankTab(FileType fileType = FileType.Sqf, string filename = "New File")
        {
            var cleanName = filename + this.MainTabControl.TabPages.Count;

            this.MainTabControl.TabPages.Add(new TabPage(filename) { Name = cleanName });
            this.MainTabControl.SuspendLayout();
            this.MainTabControl.TabPages[cleanName].Controls.Add(this.DashGlobal.EditorHelper.CreateEditor());
            this.MainTabControl.TabPages[cleanName].Tag = new FileInfo { Dirty = false };
            this.MainTabControl.ResumeLayout();
            this.MainTabControl.SelectTab(cleanName);
        }

        public void CreateTabOpenFile(string fileToOpen)
        {
            var fileParts = fileToOpen.Split('\\');
            var tabText = fileParts[fileParts.Count() - 1];

            this.MainTabControl.TabPages.Add(new TabPage(tabText) { Name = fileToOpen });
            this.MainTabControl.SuspendLayout();
            this.MainTabControl.TabPages[fileToOpen].Controls.Add(this.DashGlobal.EditorHelper.CreateEditor(fileToOpen));
            this.MainTabControl.ResumeLayout();
            this.MainTabControl.SelectTab(fileToOpen);

            this.DashGlobal.SetWindowTitle(fileToOpen);
            Main.Lang = this.DashGlobal.FilesHelper.GetLangFromFile(fileToOpen);

            this.DashGlobal.EditorHelper.ActiveEditor.OpenFile(fileToOpen);
            this.MainTabControl.TabPages[fileToOpen].Tag = new FileInfo { Dirty = false, CrcHash = "TODO" };

            this.DashGlobal.EditorHelper.PerformSyntaxHighlighting(null, Main.Lang, true);
        }

        public void CloseTab(TabPage tab)
        {
            var tabCount = this.MainTabControl.TabPages.Count;
            var closingCurrentTab = this.MainTabControl.SelectedTab == tab;
            var closingTabId = this.MainTabControl.SelectedIndex;

            // Break out if no tab selected
            if (tab == null)
            {
                return;
            }

            if (!closingCurrentTab)
            {
                return;
            }

            // Don't close if the file hasn't been changed from default
            if (tabCount == 1 && tab.Controls[0].Text == string.Empty)
            {
                return;
            }

            var tag = this.MainTabControl.SelectedTab.Tag as FileInfo;

            if (tag.Dirty)
            {
                var message = MessageBox.Show(
                    "This file has been modified. Do you want to save it?", 
                    "Save file?", 
                    MessageBoxButtons.YesNo);
                if (message == DialogResult.Yes)
                {
                    // TODO -- Add call to filesHelper.SaveFile() to save the file or save as if it hasn't yet been saved
                    return;
                }
            }

            if (closingTabId == (tabCount - 1))
            {
                if (this.MainTabControl.TabPages.Count == 1)
                {
                    this.MainTabControl.TabPages.Remove(tab);
                    this.CreateBlankTab(FileType.Other);
                    this.DashGlobal.SetWindowTitle("{new file}");
                    this.DashGlobal.EditorHelper.ActiveEditor.Focus();
                    return;
                }

                // If we're closing the last tab in the list, select the tab to the left
                this.MainTabControl.SelectTab(this.MainTabControl.TabPages[closingTabId - 1]);
            }
            else
            {
                // Select the right-most tab
                this.MainTabControl.SelectTab(this.MainTabControl.TabPages[closingTabId + 1]);
            }

            // Close the tab
            this.MainTabControl.TabPages.Remove(tab);

            if (this.MainTabControl.TabPages.Count == 0)
            {
                this.CreateBlankTab(FileType.Other);
                this.DashGlobal.SetWindowTitle("{new file}");
            }

            this.DashGlobal.EditorHelper.ActiveEditor.Focus();
        }

        public void CloseAllTabsExcept(TabPage tab)
        {
            foreach (TabPage tabPage in this.MainTabControl.TabPages)
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
                page =
                    this.MainTabControl.TabPages.Cast<TabPage>()
                        .Where((t, i) => this.MainTabControl.GetTabRect(i).Contains(e.Location))
                        .First();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }

            return page;
        }

        public void SetSelectedTabDirty()
        {
            var info = this.MainTabControl.SelectedTab.Tag as FileInfo;
            info.Dirty = true;
            this.MainTabControl.SelectedTab.Tag = info;
        }

        public void SetSelectedTabClean()
        {
            var info = this.MainTabControl.SelectedTab.Tag as FileInfo;
            info.Dirty = false;
            this.MainTabControl.SelectedTab.Tag = info;
        }

        public void CheckTabDirtyState()
        {
            // TODO -- Add CRC hash checking in here
        }
    }

    public class FileInfo
    {
        public bool Dirty { get; set; }

        public string CrcHash { get; set; }
    }
}