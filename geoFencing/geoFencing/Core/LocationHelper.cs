using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Devices.Geolocation;

namespace geoFencing.Core
{
    public class LocationHelper
    {
        private static readonly uint AppDesiredAccuracyInMeters = 10;
        
        private static async void Geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            await LocationHelper.SendLocationToBackend("wns", "TEST_USER", "TEST", "37.7746", "-122.3858");


            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Debug.WriteLine(string.Concat(args.Position.Coordinate.Longitude, " ", args.Position.Coordinate.Latitude));
            });
        }


        public async static Task<Geoposition> GetCurrentLocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    {
                        Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = AppDesiredAccuracyInMeters };
                        geolocator.PositionChanged += Geolocator_PositionChanged;
                        return await geolocator.GetGeopositionAsync();
                    }
                default:
                    {
                        return null;
                    }
            }
        }
        public static async Task SendLocationToBackend(string pns, string userTag, string message, string latitude, string longitude)
        {
            var POST_URL = "http://localhost:8741/api/notifications?pns=" +
                pns + "&to_tag=" + userTag + "&latitude=" + latitude + "&longitude=" + longitude;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    await httpClient.PostAsync(POST_URL, new StringContent("\"" + message + "\"",
                        System.Text.Encoding.UTF8, "application/json"));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

    }
}
