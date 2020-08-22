using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Display;
using Windows.Devices.Enumeration;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MonitorInfoTool
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async Task TestAsync()
        {
            var selector = DisplayMonitor.GetDeviceSelector();
            var infos = await DeviceInformation.FindAllAsync(selector);

            var monitors = new List<DisplayMonitor>();
            foreach (var info in infos)
            {
                var monitor = await DisplayMonitor.FromInterfaceIdAsync(info.Id);
                monitors.Add(monitor);
            }

            var output = new List<string>();
            foreach (var monitor in monitors)
            {
                output.Add($"RawDpiX: {monitor.RawDpiX}    RawDpiY: {monitor.RawDpiY}    Width: {monitor.NativeResolutionInRawPixels.Width}    Height: {monitor.NativeResolutionInRawPixels.Height}");
            }
            MainListView.ItemsSource = output;
        }

        private void ClearAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            MainListView.ItemsSource = null;
        }

        private void RunAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            var ignored = TestAsync();
        }
    }
}
