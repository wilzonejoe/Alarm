﻿namespace CoreWakeMeUp.Configurations
{
    public class Content
    {
        public static int MaxHour = 24;
        public static int MaxMinuteAndSecond = 60;
        public static string waetherApiKey = "f7d399f1d2367da583899c754b238b38";
        public static string weatherIconUrl = "http://openweathermap.org/img/w/{0}.png";
        public static string weatherApiUrl = "http://api.openweathermap.org/data/2.5/weather?id=2193733&appid=f7d399f1d2367da583899c754b238b38";
        public static string weatherApiUrlCoordinate = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid=f7d399f1d2367da583899c754b238b38";
    }
}
