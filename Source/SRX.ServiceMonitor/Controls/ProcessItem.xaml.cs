//srgjanx

using SRX.ServiceMonitor.Models;
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

        public string DispalyName
        {
            get => lblProcessName.Content as string;
            set => lblProcessName.Content = value;
        }

        public void SetStatus(ProcessStatus status)
        {
            switch (status)
            {
                case ProcessStatus.Running:
                    lblProcessName.BorderBrush = Brushes.Green;
                    lblProcessName.Background = Brushes.LightGreen;
                    break;
                case ProcessStatus.Stopped:
                    lblProcessName.BorderBrush = Brushes.DarkRed;
                    lblProcessName.Background = Brushes.LightCoral;
                    break;
            }
        }
    }
}
