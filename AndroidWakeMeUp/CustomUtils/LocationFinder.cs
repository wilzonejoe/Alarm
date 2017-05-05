using System.Collections.Generic;
using Android.Content;
using Android.Locations;

namespace AndroidWakeMeUp.CustomUtils
{
    public class LocationFinder{
        public static KeyValuePair<double, double> GetLocationData(Context context)
        {
            LocationManager lm = (LocationManager)context.GetSystemService(Context.LocationService);
            Location location;
            bool gpsEnabled = lm.IsProviderEnabled(LocationManager.GpsProvider);
            bool networkEnabled = lm.IsProviderEnabled(LocationManager.NetworkProvider);
            if (gpsEnabled)
                location = lm.GetLastKnownLocation(LocationManager.GpsProvider);
            else if (networkEnabled)
                location = lm.GetLastKnownLocation(LocationManager.NetworkProvider);
            else
                location = null;
            if (location != null)
            {
                KeyValuePair<double, double> locationCoordinate =
                    new KeyValuePair<double, double>(location.Latitude, location.Longitude);
                return locationCoordinate;
            }
            else
            {
                //auckland coordinate
                return new KeyValuePair<double, double>(38.9072, -77.0369);
            }
        }
    }
}