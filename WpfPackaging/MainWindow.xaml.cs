using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
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

            try
            {

                if (Package.Current != null)
                {
                    var version = $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
                    versionLabel.Text = version;
                }
            }
            catch (InvalidOperationException exception)
            {
                Crashes.TrackError(exception);
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
            Analytics.TrackEvent("Checking for Updates");

            try
            {
                var currentPackage = Package.Current;
                var result = await currentPackage.CheckUpdateAvailabilityAsync();
                if (result?.Availability == PackageUpdateAvailability.Available)
                {
                    MessageBox.Show("There's a new update! Restart your app to install it");
                }
                else
                {
                    Analytics.TrackEvent("No updates available");
                }
            }
            catch (InvalidOperationException exception)
            {
                Crashes.TrackError(exception);
            }
        }
    }
}
