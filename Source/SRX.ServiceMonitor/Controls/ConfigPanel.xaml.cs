//srgjanx

using SRX.ServiceMonitor.Utils;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace SRX.ServiceMonitor.Controls
{
    public partial class ConfigPanel : UserControl
    {
        public ConfigPanel()
        {
            InitializeComponent();
            LostFocus += ConfigPanel_LostFocus;
        }

        public string ConfigContent
        {
            get => txtConfig.Text;
            set => txtConfig.Text = value;
        }

        private void SetConfigContentFromFile()
        {
            ConfigContent = new ProcessManager().GetText();
        }

        private void ConfigPanel_LostFocus(object sender, RoutedEventArgs e)
        {
            new ProcessManager().SetText(ConfigContent);
        }

        public void Load()
        {
            //Monitor for external changes to the config file:
            Assembly assembly = Assembly.GetExecutingAssembly();
            string path = Path.GetDirectoryName(assembly.Location);
            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher
            {
                Path = path,
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.txt"
            };
            fileSystemWatcher.Changed += new FileSystemEventHandler(OnConfigFileTextChanged_Event);
            fileSystemWatcher.EnableRaisingEvents = true;
            //Fill the text of the config textbox:
            SetConfigContentFromFile();
        }

        private void OnConfigFileTextChanged_Event(object source, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                SetConfigContentFromFile();
            });
        }
    }
}