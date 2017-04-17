using System;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Widget;
using NotificationCompat = Android.Support.V7.App.NotificationCompat;

namespace AndroidWakeMeUp.CustomObject
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class DeviceBootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == Intent.ActionBootCompleted)
            {
                /* Setting the alarm here */
//                Intent alarmIntent = new Intent(context, typeof(AlarmReceiver));
//                PendingIntent pendingIntent = PendingIntent.GetBroadcast(context, 0, alarmIntent, 0);
//
//                DateTime nowDateTime = DateTime.Now;
//                //add 1 minutes
//                DateTime newDate = nowDateTime.AddSeconds(10);
//
//                AlarmManager manager = (AlarmManager) context.GetSystemService(Context.AlarmService);
//                int interval = 8000;
//                manager.SetInexactRepeating(AlarmType.RtcWakeup, nowDateTime.Millisecond , interval,
//                    pendingIntent);


            }
        }
    }
}