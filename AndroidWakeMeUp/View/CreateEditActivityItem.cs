using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using CoreWakeMeUp.Enumeration;
using Java.Lang;

namespace AndroidWakeMeUp.View
{
    [Activity(Label = "CreateEditActivityItem", Theme = "@style/MyTheme")]
    public class CreateEditActivityItem : Activity
    {
        private Spinner _activityTypeSpinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateEditActivity);
            // Create your application here
            SetSpinnerItem();
        }

        private async void SetSpinnerItem()
        {
            _activityTypeSpinner = FindViewById<Spinner>(Resource.Id.activity_type_spinner);
            var activityTypes = typeof(ActivityType).GetEnumNames();
            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, activityTypes);
            _activityTypeSpinner.Adapter = adapter;
        }
    }
}