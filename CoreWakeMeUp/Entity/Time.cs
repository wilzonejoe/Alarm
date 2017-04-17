using System.Text;
using CoreWakeMeUp.Configurations;
using CoreWakeMeUp.Enumeration;

namespace CoreWakeMeUp.Entity
{
    public class Time : SaveAbleObject
    {
        public Time()
        {
        }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public int Second { get; set; }

        public void Add(Time time)
        {
            Hour = TimeCalculation(TimeType.HOUR, Hour + time.Hour);
            Minute = TimeCalculation(TimeType.MINUTE, Minute + time.Minute);
            Second = TimeCalculation(TimeType.SECOND, Second + time.Second);
        }

        public void Subtract(Time time)
        {
            Hour = TimeCalculation(TimeType.HOUR, Hour - time.Hour);
            Minute = TimeCalculation(TimeType.MINUTE, Minute - time.Minute);
            Second = TimeCalculation(TimeType.SECOND, Second - time.Second);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Hour);
            builder.Append(":");
            if (Minute < 10)
                builder.Append("0");
            builder.Append(Minute);
            builder.Append(":");
            if (Second < 10)
                builder.Append("0");
            builder.Append(Second);

            return builder.ToString();
        }

        public void adjustment()
        {
            Hour = TimeCalculation(TimeType.HOUR, Hour);
            Minute = TimeCalculation(TimeType.MINUTE, Minute);
            Second = TimeCalculation(TimeType.SECOND, Second);
        }

        private static int TimeCalculation(TimeType type, int newHour)
        {
            int Max;
            switch (type)
            {
                case TimeType.HOUR:
                    Max = Content.MaxHour;
                    break;
                default:
                    Max = Content.MaxMinuteAndSecond;
                    break;
            }
            int newTemp = -1;
            if (0 <= newHour && newHour < Max)
                newTemp = newHour;
            else if (newHour < 0)
            {
                int tempHour = newHour;
                while (tempHour < 0)
                {
                    tempHour = Max + tempHour;
                }
                newTemp = tempHour;
            }
            else
            {
                while (newHour > Max)
                    newHour -= Max;
                newTemp = newHour;
            }

            return newTemp;
        }
    }
}