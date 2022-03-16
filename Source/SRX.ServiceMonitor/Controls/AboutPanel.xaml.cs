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

        public string AppName
        {
            get => (string)lblAppName.Content;
            set => lblAppName.Content = value;
        }

        public string AppVersion
        {
            get => (string)lblAppVersion.Content;
            set => lblAppVersion.Content = value;
        }

        public string AppURL
        {
            get => (string)lblAppURL.Content;
            set => lblAppURL.Content = value;
        }

        public void Load()
        {
            AppName = Settings.Default.About_AppName;
            AppVersion = $"Version :: {GetVersion}";
            AppURL = Settings.Default.About_Link;
        }

        private string GetVersion
        {
            get
            {
                Version? version = Assembly.GetExecutingAssembly()?.GetName()?.Version;
                return $"{version?.Major}.{version?.Minor}.{version?.Build}";
            }
        }
    }
}