//srgjanx

using SRX.ServiceMonitor.Properties;
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
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(Settings.Default.ProcessNamesFilepath, txtConfig.Text);
        }

        private void OnConfigFileTextChanged_Event(object source, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                txtConfig.Text = new ProcessManager().GetText();
            });
        }

        public void Load()
        {
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
            OnConfigFileTextChanged_Event(null, null);
        }
    }
}