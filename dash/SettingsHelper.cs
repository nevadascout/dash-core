namespace Dash
{
    using System.Windows.Forms;

    using Dash.Properties;

    /// <summary>
    /// The settings helper.
    /// </summary>
    public class SettingsHelper
    {
        /// <summary>
        /// Gets or sets the main tab control.
        /// </summary>
        public TabControl MainTabControl { get; set; }

        /// <summary>
        /// The update open tabs.
        /// </summary>
        public void UpdateOpenTabs()
        {
            // Empty the open tabs list
            if (Settings.Default.OpenTabs != null)
            {
                Settings.Default.OpenTabs.Clear();
            }

            // Save open tabs to settings so we can load them again when we reopen the app
            foreach (TabPage tab in this.MainTabControl.TabPages)
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