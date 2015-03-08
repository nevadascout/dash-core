using System.Windows.Forms;
using Dash.Properties;

namespace Dash
{
    public class SettingsHelper
    {
        public TabControl MainTabControl { get; set; }

        public void UpdateOpenTabs()
        {
            // Empty the open tabs list
            if (Settings.Default.OpenTabs != null)
            {
                Settings.Default.OpenTabs.Clear();
            }

            // Save open tabs to settings so we can load them again when we reopen the app
            foreach (TabPage tab in MainTabControl.TabPages)
            {
                // Ensure the tab has been saved somewhere
                if (!string.IsNullOrEmpty(tab.Name) && tab.Name.Contains("\\"))
                {
                    Settings.Default.OpenTabs.Add(tab.Name);
                }
            }
        }
    }
}
