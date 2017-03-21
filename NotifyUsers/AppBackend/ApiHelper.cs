using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AppBackend
{
    public class ApiHelper
    {
        public static readonly string ApiEndpoint = "https://spatial.virtualearth.net/REST/v1/data/e571c65f66de488ea9dde02ff57585b0/geoFences/TestBoundaries?spatialFilter=intersects(%27POINT%20({0}%20{1})%27)&$format=json&key={2}";
        public static readonly string ApiKey = "bneAWOJdktXwP5i4cgWe~cf4jAcJUeD9X3nQLXEHjXQ~As-yWK2_R7Wxaehn8I3JIaTSkORIrlIQP66prVtVodG5MzrfC9p38sUaoasx25ih";

        public static bool IsPointWithinBounds(string longitude, string latitude)
        {
            var json = new WebClient().DownloadString(string.Format(ApiEndpoint, longitude, latitude, ApiKey));
            var result = JsonConvert.DeserializeObject<Rootobject>(json);
            if (result.d.results != null && result.d.results.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}