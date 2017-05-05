using System;
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
                                
            }
        }
    }
}