using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CoreWakeMeUp.Enumeration;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Spinner = Android.Support.V7.Widget.AppCompatSpinner;

namespace AndroidWakeMeUp.View
{
    [Activity(Label = "", Theme = "@style/MyTheme")]
    public class CreateEditActivityItem : AppCompatActivity
    {
        private Spinner _activityTypeSpinner;
        private Toolbar _mToolbar;
        private NumberPicker numberpicker;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateEditActivity);
            _mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_mToolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            numberpicker = (NumberPicker)FindViewById(Resource.Id.numberPicker1);

            numberpicker.MinValue = 0;

            numberpicker.MaxValue = 100;

            SetSpinnerItem();
        }

        private void SetSpinnerItem()
        {
            _activityTypeSpinner = FindViewById<Spinner>(Resource.Id.activity_type_spinner);
            var activityTypes = typeof(ActivityType).GetEnumNames();
            ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, activityTypes);
            _activityTypeSpinner.Adapter = adapter;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    Finish();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}