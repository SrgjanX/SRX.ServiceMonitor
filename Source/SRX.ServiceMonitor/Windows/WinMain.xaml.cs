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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await servicesPanel.Load();
            configPanel.Load();
            aboutPanel.Load();
        }
    }
}