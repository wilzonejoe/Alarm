using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Widget;
using SupportActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;

namespace AndroidWakeMeUp.CustomUtils.ViewRelated
{
    public class MyActionBarDrawerToggle : SupportActionBarDrawerToggle
    {
        private readonly AppCompatActivity _mHostActivity;
        private readonly int _mOpenedResource;
        private readonly int _mClosedResource;
        private TextView activityTitle;

        public MyActionBarDrawerToggle(AppCompatActivity host, DrawerLayout drawerLayout, int openedResource, int closedResource)
            : base(host, drawerLayout, openedResource, closedResource)
        {
            _mHostActivity = host;
            _mOpenedResource = openedResource;
            _mClosedResource = closedResource;
            activityTitle = (TextView)_mHostActivity.FindViewById(Resource.Id.activity_title);
        }

        public override void OnDrawerOpened(Android.Views.View drawerView)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                base.OnDrawerOpened(drawerView);

                activityTitle.Text = _mHostActivity.GetString(_mOpenedResource);
            }
        }

        public override void OnDrawerClosed(Android.Views.View drawerView)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                base.OnDrawerClosed(drawerView);
                activityTitle.Text = _mHostActivity.GetString(_mClosedResource);
            }
        }

        public override void OnDrawerSlide(Android.Views.View drawerView, float slideOffset)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                base.OnDrawerSlide(drawerView, slideOffset);
            }
        }
    }
}

