using System;
using System.Threading.Tasks;
using UIKit;

namespace IosWakeMeUp
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            UpdateClock();
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

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}