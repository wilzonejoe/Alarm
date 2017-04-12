using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWakeMeUp.database;
using CoreWakeMeUp.Entity;
using SQLite;
using UIKit;

namespace IosWakeMeUp
{
    public partial class MainViewController : UIViewController
    {
        private DataBase _db;
        public MainViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            UpdateClock();
            startDB();
            DateTime dateTime = DateTime.Now;
            Time time = new Time()
            {
                Hour = dateTime.Hour,
                Minute = dateTime.Minute,
                Second = dateTime.Second
            };
            _db.insertIntoTableTime(time);
            FillInTable();
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
            UITableView table;
            table = new UITableView()
            {
                Frame = new CoreGraphics.CGRect(0,100,View.Bounds.Width,View.Bounds.Height)
            };
            View.AddSubview(table);
            var times = _db.selectTableTime();
            List<string>timeString  = new List<string>();
            foreach (var time in times)
            {
                timeString.Add(time.ToString());
            }
            table.Source =  new TableSource(timeString.ToArray());
            
        }
        

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}