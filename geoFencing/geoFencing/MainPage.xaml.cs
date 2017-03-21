using geoFencing.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Windows.ApplicationModel.Core;
using Windows.Networking.PushNotifications;
using Microsoft.WindowsAzure.Messaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace geoFencing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;

        }
        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            var location = await LocationHelper.GetCurrentLocation();

            if (location != null)
            {
                Debug.WriteLine(string.Concat(location.Coordinate.Longitude,
                    " ", location.Coordinate.Latitude));
            }
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            var hub = new NotificationHub("geoFencing", "Endpoint=sb://geofencing.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=zFPOv+C3ToFt4PB9QVTKZDL7bmDatvn3S0wWqtIXPjc=");
            var result = await hub.RegisterNativeAsync(channel.Uri);

            // Displays the registration ID so you know it was successful
            if (result.RegistrationId != null)
            {
                Debug.WriteLine("Reg successful.");
            }

        }

    }
}
