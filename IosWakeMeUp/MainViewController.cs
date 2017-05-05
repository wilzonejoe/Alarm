using System;
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
		partial void UIButton137_TouchUpInside(UIButton sender)
		{
			Console.WriteLine("Touched button");
		}

        private DataBase _db;
        public MainViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
		{
            base.ViewDidLoad();
			AlarmController.CreateAlarm(AlarmController.days.Monday, DateTime.Now);

            //// Perform any additional setup after loading the view, typically from a nib.
            //UpdateClock();
            //startDB();
            //DateTime dateTime = DateTime.Now;
            //Time time = new Time()
            //{
            //    Hour = dateTime.Hour,
            //    Minute = dateTime.Minute,
            //    Second = dateTime.Second
            //};
            //_db.insertIntoTableTime(time);
            //FillInTable();
            //GetWeatherInfo();
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
        private void startDB()
        {
            _db = new DataBase(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            _db.createDataBase();
        }

        public void FillInTable()
        {
            //UITableView table;
            //table = new UITableView()
            //{
            //    Frame = new CoreGraphics.CGRect(0,100,View.Bounds.Width,View.Bounds.Height)
            //};
            //View.AddSubview(table);
            //var times = _db.selectTableTime();
            //List<string>timeString  = new List<string>();
            //foreach (var time in times)
            //{
            //    timeString.Add(time.ToString());
            //}
            //table.Source =  new TableSource(timeString.ToArray());

        }

        private async void GetWeatherInfo()
        {
            string url = Content.weatherApiUrl;
            var response = ConnectPoint.HttpGetData(Content.weatherApiUrl);
            var result = await response;
            if (result.Key == HttpStatusCode.OK)
            {
                OpenWeather openWeather = JsonConvert.DeserializeObject<OpenWeather>(result.Value);
                GetImageBitmapFromUrl(openWeather.weather[0].icon);
                current_weather_info.Text = openWeather.weather[0].description;
                current_city.Text = openWeather.name;
                current_temperature.Text = (openWeather.main.temp - 273.15) + "°C";
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