﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CoreWakeMeUp.Configurations;
using CoreWakeMeUp.database;
using CoreWakeMeUp.Endpoint;
using CoreWakeMeUp.Entity;
using Foundation;
using Newtonsoft.Json;
using SQLite;
using UIKit;

namespace IosWakeMeUp
{
    public partial class MainViewController : UIViewController
    {
        private TimeController _db;
        public MainViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            UpdateClock();
            StartDb();
            DateTime dateTime = DateTime.Now;
            Time time = new Time()
            {
                Hour = dateTime.Hour,
                Minute = dateTime.Minute,
                Second = dateTime.Second
            };
            _db.InsertItemIntoTable(time);
            FillInTable();
            GetWeatherInfo();
        }

        public async void UpdateClock()
        {
            await Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);
                    DateTime nowDateTime = DateTime.Now;
                    InvokeOnMainThread(() =>
                    {
                        string currentTime = nowDateTime.ToString("hh:mm:ss");
                        string currentAmpm = nowDateTime.ToString("tt");
                        string currentDate = nowDateTime.ToString("MMMM dd, yyyy");
                        current_time_info.Text = currentTime;
                        current_am_pm_info.Text = currentAmpm;
                        current_date_info.Text = currentDate;
                    });
                }
            });
        }
        private void StartDb()
        {
            _db = TimeController.Instance(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            _db.CreateDataBase<Time>();
        }

        public void FillInTable()
        {
            UITableView table;
            table = new UITableView()
            {
                Frame = new CoreGraphics.CGRect(0,100,View.Bounds.Width,View.Bounds.Height)
            };
            View.AddSubview(table);
            var times = _db.SelectAllTime();
            List<string>timeString  = new List<string>();
            foreach (var time in times)
            {
                timeString.Add(time.ToString());
            }
            table.Source =  new TableSource(timeString.ToArray());

        }

        private async void GetWeatherInfo()
        {
            string url = Content.weatherApiUrl;
            var response = ConnectPoint.HttpGetData(Content.weatherApiUrl);
            var result = await response;
            if (result.Key == HttpStatusCode.OK)
            {
                OpenWeather openWeather = JsonConvert.DeserializeObject<OpenWeather>(result.Value);
                GetImageBitmapFromUrl(openWeather.Weather[0].Icon);
                current_weather_info.Text = openWeather.Weather[0].Description;
                current_city.Text = openWeather.Name;
                current_temperature.Text = (openWeather.Main.Temp - 273.15) + "°C";
            }
        }

        private async void GetImageBitmapFromUrl(string icon)
        {
            var response = ConnectPoint.HttpGetImage(string.Format(Content.weatherIconUrl, icon));
            var result = await response;
            if (result.Key == HttpStatusCode.OK)
            {
                var data = NSData.FromArray(result.Value);
                var uiimage = UIImage.LoadFromData(data);
                weather_icon.Image = uiimage;
            }
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}