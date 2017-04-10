using System;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Graphics;
using Android.Widget;
using Android.OS;
using CoreWakeMeUp.Configurations;
using CoreWakeMeUp.Entity;
using Java.IO;
using Java.Lang;
using Java.Net;
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

        public TextView CurrentTimeInfoTextView
        {
            get { return _currentTimeInfoTextView; }
            set { _currentTimeInfoTextView = value; }
        }

        public TextView CurrentTimeAmpmInfoTextView
        {
            get { return _currentTimeAmpmInfoTextView; }
            set { _currentTimeAmpmInfoTextView = value; }
        }

        public TextView CurrentDateInfoTextView
        {
            get { return _currentDateInfoTextView; }
            set { _currentDateInfoTextView = value; }
        }

        public TextView CurrentWeatherInfoTextView
        {
            get { return _currentWeatherInfoTextView; }
            set { _currentWeatherInfoTextView = value; }
        }

        public TextView CurrentCityInfoTextView
        {
            get { return _currentCityInfoTextView; }
            set { _currentCityInfoTextView = value; }
        }

        public ImageView CurrentWeatherInfoImageView
        {
            get { return _currentWeatherInfoImageView; }
            set { _currentWeatherInfoImageView = value; }
        }

        public TextView CurrentTempInfoTextView
        {
            get { return _currentTempInfoTextView; }
            set { _currentTempInfoTextView = value; }
        }

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
            UpdateTime();
            GetWeather getWeather = new GetWeather(this);
            getWeather.Execute();
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

        private class GetWeather : AsyncTask<string, Java.Lang.Void, OpenWeather>
        {
            private ProgressDialog pd;
            private TextView _currentWeatherInfoTextView;
            private TextView _currentCityInfoTextView;
            private TextView _currentTempInfoTextView;
            private ImageView _currentWeatherInfoImageView;

            public GetWeather(MainActivity activity)
            {
                pd = new ProgressDialog(activity);
                _currentWeatherInfoTextView = activity.CurrentWeatherInfoTextView;
                _currentCityInfoTextView = activity._currentCityInfoTextView;
                _currentWeatherInfoImageView = activity._currentWeatherInfoImageView;
                _currentTempInfoTextView = activity._currentTempInfoTextView;
            }

            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                pd.SetTitle("Loading weather info .....");
                pd.Show();
            }

            protected override OpenWeather RunInBackground(params string[] @params)
            {
                string result = null;
                try
                {
                    URL url = new URL(Content.waetherApiUrl);
                    using (var urlConnection = (HttpURLConnection) url.OpenConnection())
                    {
                        if (urlConnection.ResponseCode == HttpStatus.Ok)
                        {
                            BufferedReader r = new BufferedReader(new InputStreamReader(urlConnection.InputStream));
                            StringBuilder sb = new StringBuilder();
                            string line;
                            while ((line = r.ReadLine()) != null)
                                sb.Append(line);
                            result = sb.ToString();
                            urlConnection.Disconnect();
                        }
                        else
                        {
                            result = null;
                        }
                    }
                }
                catch (Java.Lang.Exception e)
                {
                    result = null;
                }
                OpenWeather openWeather = JsonConvert.DeserializeObject<OpenWeather>((string)result);
                GetImageBitmapFromUrl($"http://openweathermap.org/img/w/{openWeather.weather[0].icon}.png");
                return openWeather;
            }

            protected override void OnPostExecute(OpenWeather openWeather)
            {
                base.OnPostExecute(openWeather);
                if (openWeather != null)
                {
                    _currentWeatherInfoTextView.Text = openWeather.weather[0].description;
                    _currentCityInfoTextView.Text = openWeather.name;
                    _currentTempInfoTextView.Text = (openWeather.main.temp - 273.15) + "°C";
                }
                else
                {
                    _currentWeatherInfoTextView.Text = "Can't retrieve weather";
                    _currentCityInfoTextView.Text = "----";
                }

                pd.Dismiss();
            }

            private async Task<Bitmap> GetImageBitmapFromUrl(string url)
            {
                WebClient webClient = new WebClient();
                byte[] bytes = null;

                try
                {
                    bytes = await webClient.DownloadDataTaskAsync(url);
                }
                catch (TaskCanceledException)
                {
                    // Exception
                    return null;
                }
                catch (System.Exception e)
                {
                    // Exception
                    return null;
                }

                Bitmap bitmap = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                _currentWeatherInfoImageView.SetImageBitmap(bitmap);
                return bitmap;
            }
        }
    }
}