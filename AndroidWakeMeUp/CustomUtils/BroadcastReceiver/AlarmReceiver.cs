using System;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Widget;
using AndroidWakeMeUp.View;

namespace AndroidWakeMeUp.CustomUtils.BroadcastReceiver
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class AlarmReceiver : Android.Content.BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Notification notification = new Notification.Builder(context)
                .SetContentTitle("Alarm is ringing")
                .SetContentText("RING RING RING")
                .SetSmallIcon(Resource.Drawable.Icon)
                .Build();

            // Get the notification manager:
            NotificationManager notificationManager =(NotificationManager)
                context.GetSystemService(Context.NotificationService);

            // Publish the notification:
            const int notificationId = 0;

            notificationManager.Notify(notificationId, notification);
        }
    }
}