using Android.App;
using Android.Content;

namespace AndroidWakeMeUp.CustomUtils.BroadcastReceiver
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class DeviceBootReceiver : Android.Content.BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == Intent.ActionBootCompleted)
            {
                //                Intent alarmIntent = new Intent(this, typeof(AlarmReceiver));
                //                PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, alarmIntent, 0);
                //                    DateTime dateTime = //new date;
                //                    DateTimeOffset triggerOffset = new DateTimeOffset(dateTime);
                //                AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
                //                manager.SetAlarmClock(new AlarmManager.AlarmClockInfo(triggerOffset.ToUnixTimeMilliseconds(), pendingIntent),pendingIntent);
                //                Console.WriteLine(manager.NextAlarmClock.TriggerTime);
            }
        }
    }
}