//srgjanx

using System.Windows;

namespace SRX.ServiceMonitor.Windows
{
    public partial class WinMain : Window
    {
        public WinMain()
        {
            InitializeComponent();
            servicesPanel.Load();
            configPanel.Load();
            aboutPanel.Load();
        }
    }
}