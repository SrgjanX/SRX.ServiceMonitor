//srgjanx

using System.Windows;

namespace SRX.ServiceMonitor.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            RootNavigation.Loaded += RootNavigation_Loaded;
        }

        private void RootNavigation_Loaded(object sender, RoutedEventArgs e)
        {
            RootNavigation.Navigate("dashboard", true);
        }
    }
}