using System;
using Android.App;
using Android.Content;

namespace AndroidWakeMeUp.CustomUtils.BroadcastReceiver
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class AlarmReceiver : Android.Content.BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Notification.Builder builder = new Notification.Builder(context)
                .SetContentTitle("Alarm notification")
                .SetContentText("Alarm rings at " + DateTime.Now)
                .SetSmallIcon(Resource.Drawable.Icon);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                context.GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}