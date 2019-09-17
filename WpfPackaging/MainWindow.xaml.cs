//using Microsoft.AppCenter.Analytics;
//using Microsoft.AppCenter.Crashes;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel;

namespace WpfPackaging
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Package.Current != null)
            {
                var version = $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
                versionLabel.Text = version;
            }
            else
            {
                versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            }
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await CheckForUpdates();
        }

        private async Task CheckForUpdates()
        {
            //Analytics.TrackEvent("Checking for Updates");
            if (Package.Current == null)
                return;
            var currentPackage = Package.Current;
            var result = await currentPackage.CheckUpdateAvailabilityAsync();
            if (result?.Availability == PackageUpdateAvailability.Available)
            {
                MessageBox.Show("There's a new update! Restart your app to install it");
            }
        }
    }
}
