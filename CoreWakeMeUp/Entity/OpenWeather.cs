using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreWakeMeUp.Entity
{
    //json example
    //{"coord":{"lon":139,"lat":35},
    //"sys":{"country":"JP","sunrise":1369769524,"sunset":1369821049},
    //"weather":[{"id":804,"main":"clouds","description":"overcast clouds","icon":"04n"}],
    //"main":{"temp":289.5,"humidity":89,"pressure":1013,"temp_min":287.04,"temp_max":292.04},
    //"wind":{"speed":7.31,"deg":187.002},
    //"rain":{"3h":0},
    //"clouds":{"all":92},
    //"dt":1369824698,
    //"id":1851632,
    //"name":"Shuzenji",
    //"cod":200}
    public class Coord
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public double SeaLevel { get; set; }
        public double GrndLevel { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
        public double Deg { get; set; }
    }

    public class Clouds
    {
        public double All { get; set; }
    }

    public class Sys
    {
        public double Message { get; set; }
        public string Country { get; set; }
        public double Sunrise { get; set; }
        public double Sunset { get; set; }
    }

    public class OpenWeather
    {
        public Coord Coord { get; set; }
        public List<Weather> Weather { get; set; }
        public string Base { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public int Dt { get; set; }
        public Sys Sys { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }

}
