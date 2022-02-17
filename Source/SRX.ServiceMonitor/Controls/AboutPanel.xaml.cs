//srgjanx

using SRX.ServiceMonitor.Properties;
using System;
using System.Reflection;
using System.Windows.Controls;

namespace SRX.ServiceMonitor.Controls
{
    public partial class AboutPanel : UserControl
    {
        public AboutPanel()
        {
            InitializeComponent();
        }

        public void Load()
        {
            lblAppName.Content = Settings.Default.About_AppName;
            lblAppVersion.Content = $"Version :: {GetVersion}";
            lblAppURL.Content = Settings.Default.About_Link;
        }

        private string GetVersion
        {
            get
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }
    }
}