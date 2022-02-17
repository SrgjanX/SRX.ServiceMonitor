//srgjanx

using SRX.ServiceMonitor.Utils;
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
                    border.BorderBrush = Brushes.Green;
                    border.Background = Brushes.LightGreen;
                    break;
                case ProcessStatus.Stopped:
                    border.BorderBrush = Brushes.DarkRed;
                    border.Background = Brushes.LightCoral;
                    break;
            }
            
        }
    }
}
