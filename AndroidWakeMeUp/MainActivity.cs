using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.Widget;
using Android.OS;
using AndroidWakeMeUp.Resources.database;
using CoreWakeMeUp.Configurations;
using CoreWakeMeUp.Endpoint;
using CoreWakeMeUp.Entity;
using Newtonsoft.Json;

namespace AndroidWakeMeUp
{
    [Activity(Label = "AndroidWakeMeUp", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class MainActivity : Activity
    {
        private TextView _currentTimeInfoTextView;
        private TextView _currentTimeAmpmInfoTextView;
        private TextView _currentDateInfoTextView;
        private TextView _currentWeatherInfoTextView;
        private TextView _currentTempInfoTextView;
        private TextView _currentCityInfoTextView;
        private ImageView _currentWeatherInfoImageView;
        private ListView _listData;
        private List<Time> _listSource;
        private DataBase _db;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
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
            _listData = FindViewById<ListView>(Resource.Id.activityList);
            _listSource = new List<Time>();
            UpdateTime();
            GetWeatherInfo();
            startDB();
            DateTime nowTime = DateTime.Now;
            Time time = new Time()
            {
                   Hour = nowTime.Hour,
                   Minute = nowTime.Minute,
                   Second = nowTime.Second
            };

            _db.insertIntoTableTime(time);
            LoadData();
        }

        private void startDB()
        {
            _db = new DataBase();
            _db.createDataBase();
        }

        private async void GetWeatherInfo()
        {
            var response = ConnectPoint.HttpGetData(Content.waetherApiUrl);
            var result = await response;
            if (result.Key == HttpStatusCode.OK)
            {
                OpenWeather openWeather = JsonConvert.DeserializeObject<OpenWeather>(result.Value);
                GetImageBitmapFromUrl(openWeather.weather[0].icon);
                _currentWeatherInfoTextView.Text = openWeather.weather[0].description;
                _currentCityInfoTextView.Text = openWeather.name;
                _currentTempInfoTextView.Text = (openWeather.main.temp - 273.15) + "°C";
            }
        }

        private async void GetImageBitmapFromUrl(string icon)
        {
            Bitmap bitmap = null;
            var response = ConnectPoint.HttpGetImage(string.Format(Content.weatherIconUrl, icon));
            var result = await response;
            if (result.Key == HttpStatusCode.OK)
            {
                bitmap = BitmapFactory.DecodeByteArray(result.Value, 0, result.Value.Length);
                _currentWeatherInfoImageView.SetImageBitmap(bitmap);
            }
        }

        private async void UpdateTime()
        {
            while (true)
            {
                await Task.Delay(1000);
                DateTime nowDateTime = DateTime.Now;

                RunOnUiThread(() => updateInfoText(DateTime.Now));
            }
        }

        private void updateInfoText(DateTime nowDateTime)
        {
            string currentTime = nowDateTime.ToString("hh:mm:ss");
            string currentAmpm = nowDateTime.ToString("tt");
            string currentDate = nowDateTime.ToString("MMMM dd, yyyy");
            _currentDateInfoTextView.Text = currentDate;
            _currentTimeAmpmInfoTextView.Text = currentAmpm;
            _currentTimeInfoTextView.Text = currentTime;
        }

        private void LoadData()
        {
            _listSource = _db.selectTableTime();
            _listData.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, _listSource);
        }
    }
}