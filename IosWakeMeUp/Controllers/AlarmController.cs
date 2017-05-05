using System;
using System.Linq;
using System.Collections.Generic;
using UIKit;
using Foundation;
using EventKit;
using EventKitUI;

namespace IosWakeMeUp
{
	public static class AlarmController
	{
		
		public enum days
		{
			Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
		}

		public static void CreateAlarm(days day, DateTime time)
		{
			// add day and time
			// create the notification
			var notification = new UILocalNotification();

			// set the fire date (the date time in which it will fire)
			notification.FireDate = NSDate.FromTimeIntervalSinceNow(10);

			// configure the alert
			notification.AlertAction = "View Alert";
			notification.AlertBody = "Your 10 second alert has fired!";

			// modify the badge
			notification.ApplicationIconBadgeNumber = 1;

			// set the sound to be the default sound
			notification.SoundName = UILocalNotification.DefaultSoundName;

			// schedule it
			UIApplication.SharedApplication.ScheduleLocalNotification(notification);
			Console.WriteLine("Scheduled...");
		}

		public static void setAlarmInactive(days day)
		{
			
		}

		public static void EditAlarm(days day)
		{
			switch (day)
			{
				case days.Monday:
					break;
				case days.Tuesday:
					break;
				case days.Wednesday:
					break;
				case days.Thursday:
					break;
				case days.Friday:
					break;
				case days.Saturday:
					break;
				case days.Sunday:
					break;
		
			}
		}
	}

}
