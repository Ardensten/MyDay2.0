using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDay2._0.Models
{
    internal class WeatherInfo
    {
        public class Coord
        {
            public float lon { get; set; }
            public float lat { get; set; }
        }

        public class Weather
        {
            public string icon { get; set; }
            public string description { get; set; }
        }

        public class Main
        {
            public float temp { get; set; }
            public float feels_like { get; set; }
        }



        public class Sys
        {
            public int type { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Root
        { 
            public Coord coord { get; set; }
            public List<Weather> weather { get; set;}
            public Main main { get; set; }
            public Sys sys { get; set; }

        }

    }
}
