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
            RemoteViews customNotifView = new RemoteViews(context.PackageName,
            Resource.Layout.NotificationLayout);
//            customNotifView.SetTextViewText(Resource.Id.text, "Hello World!");

            NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
                .SetSmallIcon(Android.Resource.Drawable.AlertLightFrame);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =(NotificationManager)
                context.GetSystemService(Context.NotificationService);

            // Publish the notification:
            const int notificationId = 0;
            notification.ContentView = customNotifView;
            notificationManager.Notify(notificationId, notification);
        }
    }
}