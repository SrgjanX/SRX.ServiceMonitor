//srgjanx

using SRX.ServiceMonitor.Models;
using SRX.ServiceMonitor.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SRX.ServiceMonitor.Controls
{
    public partial class ProcessItem : UserControl
    {
        private string filePath;

        public ProcessItem()
        {
            InitializeComponent();
        }

        public ProcessItem(ProcessInfo processInfo)
        {
            InitializeComponent();
            ProcessName = processInfo.ProcessName;
            DisplayName = processInfo.DisplayName;
            FilePath = processInfo.FilePath;
            SetStatus(processInfo.Status);
        }

        public string ProcessName { get; set; }

        public string DisplayName
        {
            get => btnProcess.Content as string;
            set => btnProcess.Content = value;
        }

        public string FilePath
        {
            get => filePath;
            set
            {
                filePath = value;
                ToolTip = value;
            }
        }

        public void SetStatus(ProcessStatus status)
        {
            switch (status)
            {
                case ProcessStatus.Running:
                    btnProcess.BorderBrush = Brushes.Green;
                    btnProcess.Background = Brushes.LightGreen;
                    break;
                case ProcessStatus.Stopped:
                    btnProcess.BorderBrush = Brushes.DarkRed;
                    btnProcess.Background = Brushes.LightCoral;
                    break;
            }
        }

        private void btnProcessName_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePath ?? (ProcessName.EndsWith(".exe") ? ProcessName : $"{ProcessName}.exe");
            new ProcessManager().OpenProcess(filePath);
        }
    }
}