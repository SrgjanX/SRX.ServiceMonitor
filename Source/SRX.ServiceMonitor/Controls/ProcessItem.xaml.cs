//srgjanx

using SRX.ServiceMonitor.Models;
using System.Diagnostics;
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

        public string DisplayName
        {
            get => btnProcess.Content as string;
            set => btnProcess.Content = value;
        }

        public string ProcessName
        {
            get => btnProcess.Name;
            set => btnProcess.Name = value;
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

        private void btnProcessName_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process p1 = new Process();
            p1.StartInfo.FileName =  ProcessName + ".exe";
            p1.StartInfo.CreateNoWindow = true;
            p1.Start();

           
        }
    }
}
