using CommunityToolkit.Mvvm.ComponentModel;
using MyDay2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace MyDay2._0.ViewModels
{
    internal partial class LoggedInViewModel : ObservableObject
    {
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string message;

        [ObservableProperty]
        string weatherIcon;
        [ObservableProperty]
        float temp;
        [ObservableProperty]
        float feelsLikeTemp;
        [ObservableProperty]
        string weatherDescription;

        public LoggedInViewModel()
        {
            Name = Singleton.GetInstance().loggedInName;
            Message = GreetingStringBuilder();
        }

        public string GreetingStringBuilder()
        {
            var culture = new System.Globalization.CultureInfo("sv-SE");
            var day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            int hour = Convert.ToInt32(DateTime.Now.Hour);
            string greeting;

            switch (hour)
            {
                case int n when n < 10:
                    greeting = "God morgon";
                    break;
                case int n when n > 17:
                    greeting = "God kväll";
                    break;
                case int n when n > 12:
                    greeting = "God eftermiddag";
                    break;
                case int n when n >= 10:
                    greeting = "God förmiddag";
                    break;
                default:
                    greeting = "Välkommen";
                    break;
            }

            GetWeather();
            Message = $"{greeting} {Name}! Idag är det {day}. Det är {Temp} grader ute, känns som {FeelsLikeTemp} grader och är {WeatherDescription}.";
            return Message;
        }
        void GetWeather()
        {
            string APIKey = "d880f634a9c683c787a8ac74425fb095";
            string city = "nykoping";

            using (HttpClient client = new HttpClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&lang=se", city, APIKey);
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    WeatherInfo.Root Info = JsonConvert.DeserializeObject<WeatherInfo.Root>(json);
                    WeatherIcon = "https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                    Temp = Info.main.temp;
                    FeelsLikeTemp = Info.main.feels_like;
                    WeatherDescription = Info.weather[0].description;
                }
            }
        }
    }
}
