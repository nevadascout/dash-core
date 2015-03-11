using System;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace Dash
{
    public class DashGlobal
    {
        public EditorHelper EditorHelper { get; private set; }
        public FilesHelper FilesHelper { get; set; }
        public Logger Logger { get; set; }
        public SettingsHelper SettingsHelper { get; set; }
        public TabsHelper TabsHelper { get; set; }

        public DashGlobal(
            EventHandler<TextChangedEventArgs> textAreaTextChanged,
            EventHandler<TextChangedEventArgs> textAreaTextChangedDelayed,
            KeyEventHandler textAreaKeyUp,
            EventHandler textAreaSelectionChangedDelayed,
            DragEventHandler textAreaDragDrop,
            DragEventHandler textAreaDragEnter,
            TabControl mainTabControl,
            AutocompleteMenu armaSense)
        {
            EditorHelper = new EditorHelper(
                textAreaTextChanged,
                textAreaTextChangedDelayed,
                textAreaKeyUp,
                textAreaSelectionChangedDelayed,
                textAreaDragDrop,
                textAreaDragEnter,
                mainTabControl,
                armaSense,
                this);

            TabsHelper = new TabsHelper(
                textAreaTextChanged,
                textAreaSelectionChangedDelayed,
                mainTabControl,
                this);

            FilesHelper = new FilesHelper(this);

            SettingsHelper = new SettingsHelper();
            Logger = new Logger();
        }
    }
}