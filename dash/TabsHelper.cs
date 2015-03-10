using System;
using System.Linq;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace Dash
{
    public class TabsHelper
    {
        private EventHandler<TextChangedEventArgs> TextAreaTextChanged { get; set; }
        private EventHandler TextAreaSelectionChangedDelayed { get; set; }
        public TabControl MainTabControl { get; set; }

        public EditorHelper EditorHelper { get; set; }

        public TabsHelper(EventHandler<TextChangedEventArgs> textAreaTextChanged, EventHandler textAreaSelectionChangedDelayed, TabControl mainTabControl, EditorHelper editorHelper)
        {
            MainTabControl = mainTabControl;
            TextAreaSelectionChangedDelayed = textAreaSelectionChangedDelayed;
            TextAreaTextChanged = textAreaTextChanged;
            EditorHelper = editorHelper;
        }


        public void CreateBlankTab(FileType fileType = FileType.Sqf, string filename = "New File")
        {
            var cleanName = filename + MainTabControl.TabPages.Count;

            MainTabControl.TabPages.Add(new TabPage(filename) { Name = cleanName });
            MainTabControl.SuspendLayout();
            MainTabControl.TabPages[cleanName].Controls.Add(EditorHelper.CreateEditor());
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
            MainTabControl.TabPages[fileToOpen].Controls.Add(EditorHelper.CreateEditor(fileToOpen));
            MainTabControl.TabPages[fileToOpen].Tag = new FileInfo() { Dirty = true, CrcHash = "TODO" };
            MainTabControl.ResumeLayout();
            MainTabControl.SelectTab(fileToOpen);

            EditorHelper.PerformSyntaxHighlighting(null, FilesHelper.GetLangFromFile(fileToOpen), true);
        }


        public void CloseTab(TabPage tab)
        {
            var tabCount = MainTabControl.TabPages.Count;
            bool closingCurrentTab = (MainTabControl.SelectedTab == tab);

            // Break out if no tab selected
            if (tab == null) return;

            // Change tab first to stop title bar flashing up with tab index 0
            if (!closingCurrentTab && tabCount > 1)
            {
                if (MainTabControl.SelectedIndex == (tabCount - 1))
                {
                    // Select 
                    MainTabControl.SelectTab(MainTabControl.TabPages[MainTabControl.SelectedIndex]);
                }
                else
                {
                    // Select the tab to the right
                    MainTabControl.SelectTab(MainTabControl.TabPages[MainTabControl.SelectedIndex + 1]);
                }
            }

            var tag = MainTabControl.SelectedTab.Tag as FileInfo;

            if (tag.Dirty)
            {
                DialogResult  message = MessageBox.Show("This file has been changed. Close without saving?", "Close without saving?", MessageBoxButtons.YesNo);

                if (message == DialogResult.Yes)
                {
                    MainTabControl.TabPages.Remove(tab);

                    if (MainTabControl.TabPages.Count == 0)
                    {
                        CreateBlankTab(FileType.Other);
                    }
                }

                // Break out
                return;
            }

            MainTabControl.TabPages.Remove(tab);

            if (MainTabControl.TabPages.Count == 0)
            {
                CreateBlankTab(FileType.Other);
            }

            EditorHelper.ActiveEditor.Focus();
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