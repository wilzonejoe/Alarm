﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Locations;
using Android.Widget;
using Android.OS;
using CoreWakeMeUp.Configurations;
using CoreWakeMeUp.database;
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
            StartDb();
            //testing database
            DateTime nowTime = DateTime.Now;
            Time time = new Time()
            {
                Hour = nowTime.Hour,
                Minute = nowTime.Minute,
                Second = nowTime.Second
            };

            _db.insertIntoTableTime(time);
            RefreshListData();
        }

        private void StartDb()
        {
            _db = new DataBase(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            _db.createDataBase();
        }

        private async void GetWeatherInfo()
        {
            KeyValuePair<double, double> locationCoordinate = GetLocationData();
            string url = string.Format(Content.weatherApiUrlCoordinate, locationCoordinate.Key,
                locationCoordinate.Value);
            var response = ConnectPoint.HttpGetData(Content.weatherApiUrl);
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
            _listSource = _db.selectTableTime();
            _listData.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, _listSource);
        }

        private KeyValuePair<double, double> GetLocationData()
        {
            LocationManager lm = (LocationManager) GetSystemService(Context.LocationService);
            Location location;
            bool gpsEnabled = lm.IsProviderEnabled(LocationManager.GpsProvider);
            bool networkEnabled = lm.IsProviderEnabled(LocationManager.NetworkProvider);
            if (gpsEnabled)
                location = lm.GetLastKnownLocation(LocationManager.GpsProvider);
            else if (networkEnabled)
                location = lm.GetLastKnownLocation(LocationManager.NetworkProvider);
            else
                location = null;
            if (location != null)
            {
                KeyValuePair<double, double> locationCoordinate =
                    new KeyValuePair<double, double>(location.Latitude, location.Longitude);
                return locationCoordinate;
            }
            else
            {
                return new KeyValuePair<double, double>();
            }
        }
    }
}
