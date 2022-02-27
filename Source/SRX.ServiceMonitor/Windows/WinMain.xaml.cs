//srgjanx

using System.Windows;

namespace SRX.ServiceMonitor.Windows
{
    public partial class WinMain : Window
    {
        public WinMain()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            servicesPanel.Load();
            configPanel.Load();
            aboutPanel.Load();
        }
    }
}