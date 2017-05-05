using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using AndroidWakeMeUp.CustomUtils;
using AndroidWakeMeUp.CustomUtils.BroadcastReceiver;
using AndroidWakeMeUp.CustomUtils.ViewRelated;
using CoreWakeMeUp.Configurations;
using CoreWakeMeUp.database;
using CoreWakeMeUp.Endpoint;
using CoreWakeMeUp.Entity;
using Newtonsoft.Json;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace AndroidWakeMeUp.View
{
    [Activity(Label = "", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]

    public class MainActivity : AppCompatActivity
    {
        private TextView _currentTimeInfoTextView;
        private TextView _currentTimeAmpmInfoTextView;
        private TextView _currentDateInfoTextView;
        private TextView _currentWeatherInfoTextView;
        private TextView _currentTempInfoTextView;
        private TextView _currentCityInfoTextView;
        private ImageView _currentWeatherInfoImageView;
        private List<Time> _listSource;
        private TimeController _db;

        private RecyclerView _mRecyclerView;
        private RecyclerView.LayoutManager _mLayoutManager;

        //drawer elements
        private Toolbar _mToolbar;

        private MyActionBarDrawerToggle _mDrawerToggle;
        private DrawerLayout _mDrawerLayout;
        private ListView _mLeftDrawer;
        private ArrayAdapter _mLeftAdapter;
        private List<string> _mLeftDataSet;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            _mToolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            _mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);

            _mLeftDrawer.Tag = 0;

            SetSupportActionBar(_mToolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            _mLeftDataSet = new List<string> {"Left Item 1", "Left Item 2"};
            _mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _mLeftDataSet);
            _mLeftDrawer.Adapter = _mLeftAdapter;

            _mDrawerToggle = new MyActionBarDrawerToggle(
                this, //Host Activity
                _mDrawerLayout, //DrawerLayout
                Resource.String.openDrawer, //Opened Message
                Resource.String.ApplicationName //Closed Message
            );

            _mDrawerLayout.AddDrawerListener(_mDrawerToggle);

            _mDrawerToggle.SyncState();

            Init();
        }

        private void Init()
        {
            _currentTimeInfoTextView = FindViewById<TextView>(Resource.Id.current_time_info);
            _currentTimeAmpmInfoTextView = FindViewById<TextView>(Resource.Id.current_time_am_pm_info);
            _currentDateInfoTextView = FindViewById<TextView>(Resource.Id.current_date_info);
            _currentWeatherInfoTextView = FindViewById<TextView>(Resource.Id.current_weather_info);
            _currentCityInfoTextView = FindViewById<TextView>(Resource.Id.current_city_name);
            _currentTempInfoTextView = FindViewById<TextView>(Resource.Id.current_temp_info);
            _currentWeatherInfoImageView = FindViewById<ImageView>(Resource.Id.current_weather_img);
            _mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            _mLayoutManager = new LinearLayoutManager(this);
            _mRecyclerView.SetLayoutManager(_mLayoutManager);
           
            //            _listData = FindViewById<ListView>(Resource.Id.activityList);
                        _listSource = new List<Time>();
            UpdateTime();
            GetWeatherInfo();
            StartDb();
            //testing database
            DateTime nowTime = DateTime.Now;
            Time time = new Time()
            {
                Hour = nowTime.Hour,
                Minute = nowTime.Minute,
                Second = nowTime.Second
            };

            _db.InsertItemIntoTable(time);
            RefreshListData();

            Intent alarmIntent = new Intent(this, typeof(AlarmReceiver));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, 0, alarmIntent, 0);
            DateTime dateTime = DateTime.Now.AddSeconds(5);
            DateTimeOffset triggerOffset = new DateTimeOffset(dateTime);
            AlarmManager manager = (AlarmManager)GetSystemService(Context.AlarmService);
            manager.SetAlarmClock(new AlarmManager.AlarmClockInfo(triggerOffset.ToUnixTimeMilliseconds(), pendingIntent), pendingIntent);
            Console.WriteLine(manager.NextAlarmClock.TriggerTime);
        }

        private void StartDb()
        {
            _db = TimeController.Instance(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            _db.CreateDataBase<Time>();
        }

        private async void GetWeatherInfo()
        {
            KeyValuePair<double, double> locationCoordinate = LocationFinder.GetLocationData(this);
            string url = string.Format(Content.weatherApiUrlCoordinate, locationCoordinate.Key,
                locationCoordinate.Value);
            Console.WriteLine(url);
            var response = ConnectPoint.HttpGetData(url);
            var result = await response;
            if (result.Key == HttpStatusCode.OK)
            {
                OpenWeather openWeather = JsonConvert.DeserializeObject<OpenWeather>(result.Value);
                GetImageBitmapFromUrl(openWeather.Weather[0].Icon);
                _currentWeatherInfoTextView.Text = openWeather.Weather[0].Description;
                _currentCityInfoTextView.Text = openWeather.Name;
                _currentTempInfoTextView.Text = Convert.ToInt32(openWeather.Main.Temp - 273.15) + "°C";
            }
        }

        private async void GetImageBitmapFromUrl(string icon)
        {
            var response = ConnectPoint.HttpGetImage(string.Format(Content.weatherIconUrl, icon));
            var result = await response;
            if (result.Key == HttpStatusCode.OK)
            {
                var bitmap = BitmapFactory.DecodeByteArray(result.Value, 0, result.Value.Length);
                _currentWeatherInfoImageView.SetImageBitmap(bitmap);
            }
        }

        private async void UpdateTime()
        {
            while (true)
            {
                await Task.Delay(1000);

                RunOnUiThread(() => UpdateInfoText(DateTime.Now));
            }
        }

        private void UpdateInfoText(DateTime nowDateTime)
        {
            string currentTime = nowDateTime.ToString("hh:mm:ss");
            string currentAmpm = nowDateTime.ToString("tt");
            string currentDate = nowDateTime.ToString("MMMM dd, yyyy");
            _currentDateInfoTextView.Text = currentDate;
            _currentTimeAmpmInfoTextView.Text = currentAmpm;
            _currentTimeInfoTextView.Text = currentTime;
        }

        private void RefreshListData()
        {
            _listSource = _db.SelectAllTime();
            //            _listData.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, _listSource);
            ActivityItemAdapter mAdapter = new ActivityItemAdapter(_listSource);
            _mRecyclerView.SetAdapter(mAdapter);

        }



        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    _mDrawerToggle.OnOptionsItemSelected(item);
                    return true;
                case Resource.Id.create_edit_activity:
                    Intent intent = new Intent(this, typeof(CreateEditActivityItem));
                    StartActivity(intent);
//                    Finish();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            _mDrawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            _mDrawerToggle.OnConfigurationChanged(newConfig);
        }
    }
}