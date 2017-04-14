// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace IosWakeMeUp
{
    [Register ("MainViewController")]
    partial class MainViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel current_am_pm_info { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel current_city { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel current_date_info { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel current_temperature { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel current_time_info { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel current_weather_info { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView weather_icon { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (current_am_pm_info != null) {
                current_am_pm_info.Dispose ();
                current_am_pm_info = null;
            }

            if (current_city != null) {
                current_city.Dispose ();
                current_city = null;
            }

            if (current_date_info != null) {
                current_date_info.Dispose ();
                current_date_info = null;
            }

            if (current_temperature != null) {
                current_temperature.Dispose ();
                current_temperature = null;
            }

            if (current_time_info != null) {
                current_time_info.Dispose ();
                current_time_info = null;
            }

            if (current_weather_info != null) {
                current_weather_info.Dispose ();
                current_weather_info = null;
            }

            if (weather_icon != null) {
                weather_icon.Dispose ();
                weather_icon = null;
            }
        }
    }
}