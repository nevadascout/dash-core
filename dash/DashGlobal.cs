namespace Dash
{
    using System;
    using System.Windows.Forms;

    using Dash.Properties;

    using FastColoredTextBoxNS;

    public class DashGlobal
    {       
        public DashGlobal(
            EventHandler<TextChangedEventArgs> textAreaTextChanged, 
            EventHandler<TextChangedEventArgs> textAreaTextChangedDelayed, 
            KeyEventHandler textAreaKeyUp, 
            EventHandler textAreaSelectionChangedDelayed, 
            DragEventHandler textAreaDragDrop, 
            DragEventHandler textAreaDragEnter, 
            TabControl mainTabControl, 
            AutocompleteMenu armaSense, 
            Main mainWindow)
        {
            this.EditorHelper = new EditorHelper(
                textAreaTextChanged, 
                textAreaTextChangedDelayed, 
                textAreaKeyUp, 
                textAreaSelectionChangedDelayed, 
                textAreaDragDrop, 
                textAreaDragEnter, 
                mainTabControl, 
                armaSense, 
                this);

            this.TabsHelper = new TabsHelper(
                textAreaTextChanged, 
                textAreaSelectionChangedDelayed, 
                mainTabControl, 
                this);

            this.FilesHelper = new FilesHelper(this);
            this.SettingsHelper = new SettingsHelper();

            this.MainWindow = mainWindow;
        }

        public EditorHelper EditorHelper { get; private set; }

        public FilesHelper FilesHelper { get; set; }

        public SettingsHelper SettingsHelper { get; set; }

        public TabsHelper TabsHelper { get; set; }

        public Main MainWindow { get; set; }

        public void SetWindowTitle(string titleText)
        {
            this.MainWindow.Text = titleText + " - " + Settings.Default.AppName;
        }
    }
}
