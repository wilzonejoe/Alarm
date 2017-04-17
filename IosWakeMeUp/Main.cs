using UIKit;
using System;
using CoreWakeMeUp;


namespace IosWakeMeUp
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			//try
			//{
				UIApplication.Main(args, null, "AppDelegate");
			//}
			//catch (Exception e)
			//{
			//	Console.WriteLine("ERROR at Main.cs " + e);
			//}
			////adhoc code to test alarm, will implement in better place
			//AlarmController ac = new AlarmController();
        }
    }
}