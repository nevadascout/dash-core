namespace Dash
{
    using System;
    using System.Windows.Forms;

    using Dash.Properties;

    using FastColoredTextBoxNS;

    /// <summary>
    /// The dash global class
    /// </summary>
    public class DashGlobal
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DashGlobal"/> class.
        /// </summary>
        /// <param name="textAreaTextChanged">
        /// The text area text changed.
        /// </param>
        /// <param name="textAreaTextChangedDelayed">
        /// The text area text changed delayed.
        /// </param>
        /// <param name="textAreaKeyUp">
        /// The text area key up.
        /// </param>
        /// <param name="textAreaSelectionChangedDelayed">
        /// The text area selection changed delayed.
        /// </param>
        /// <param name="textAreaDragDrop">
        /// The text area drag drop.
        /// </param>
        /// <param name="textAreaDragEnter">
        /// The text area drag enter.
        /// </param>
        /// <param name="mainTabControl">
        /// The main tab control.
        /// </param>
        /// <param name="armaSense">
        /// The ArmaSense
        /// </param>
        /// <param name="mainWindow">
        /// The main window.
        /// </param>
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

        /// <summary>
        /// Gets the editor helper.
        /// </summary>
        public EditorHelper EditorHelper { get; private set; }

        /// <summary>
        /// Gets or sets the files helper.
        /// </summary>
        public FilesHelper FilesHelper { get; set; }

        /// <summary>
        /// Gets or sets the settings helper.
        /// </summary>
        public SettingsHelper SettingsHelper { get; set; }

        /// <summary>
        /// Gets or sets the tabs helper.
        /// </summary>
        public TabsHelper TabsHelper { get; set; }

        /// <summary>
        /// Gets or sets the main window.
        /// </summary>
        public Main MainWindow { get; set; }

        /// <summary>
        /// The set window title.
        /// </summary>
        /// <param name="titleText">
        /// The title text.
        /// </param>
        public void SetWindowTitle(string titleText)
        {
            this.MainWindow.Text = titleText + " - " + Settings.Default.AppName;
        }
    }
}