//srgjanx

using SRX.ServiceMonitor.Models;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SRX.ServiceMonitor.Controls
{
    public partial class ProcessItem : UserControl
    {
        public ProcessItem()
        {
            InitializeComponent();
        }

        public string ProcessName { get; set; }

        public string DisplayName
        {
            get => btnProcess.Content as string;
            set => btnProcess.Content = value;
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
            string filePath = ProcessName.EndsWith(".exe") ? ProcessName : $"{ProcessName}.exe";
            try
            {
                Process p1 = new Process();
                p1.StartInfo.FileName = filePath;
                p1.StartInfo.CreateNoWindow = true;
                p1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\r\nFile path: {filePath}", "Could not open process", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
