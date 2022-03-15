using System.Windows;

namespace SRX.ServiceMonitor.Views
{
    public partial class MainView : Window
    {
        public MainView()
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