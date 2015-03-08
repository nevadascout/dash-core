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
            MainTabControl.ResumeLayout();
            MainTabControl.SelectTab(cleanName);
        }

        public void CloseTab(TabPage tab)
        {
            var tabCount = MainTabControl.TabPages.Count;
            bool closingCurrentTab = (MainTabControl.SelectedTab == tab);

            // Break out if no tab selected
            if (tab == null) return;

            // Change tab first to stop title bar flashing up with tab index 0
            if (closingCurrentTab && tabCount > 1)
            {
                if (MainTabControl.SelectedIndex == (tabCount - 1))
                {
                    // Select 
                    MainTabControl.SelectTab(MainTabControl.TabPages[MainTabControl.SelectedIndex - 1]);
                }
                else
                {
                    // Select the tab to the left
                    MainTabControl.SelectTab(MainTabControl.TabPages[MainTabControl.SelectedIndex + 1]);
                }
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

        public void CreateTabOpenFile(string fileToOpen)
        {
            var fileParts = fileToOpen.Split('\\');
            var tabText = fileParts[fileParts.Count() - 1];

            MainTabControl.TabPages.Add(new TabPage(tabText) { Name = fileToOpen });
            MainTabControl.SuspendLayout();
            MainTabControl.TabPages[fileToOpen].Controls.Add(EditorHelper.CreateEditor(fileToOpen));
            MainTabControl.ResumeLayout();
            MainTabControl.SelectTab(fileToOpen);

            EditorHelper.PerformSyntaxHighlighting(null, FilesHelper.GetLangFromFile(fileToOpen), true);
        }

        public TabPage GetTabByFilename(TabControl mainTabControl, string filename)
        {
            return mainTabControl.TabPages.Cast<TabPage>().FirstOrDefault(tab => tab.Name == filename);
        }

        public TabPage GetClickedTab(MouseEventArgs e)
        {
            return MainTabControl.TabPages.Cast<TabPage>()
                                           .Where((t, i) =>
                                               MainTabControl.GetTabRect(i)
                                                             .Contains(e.Location)).First();
        }
    }
}