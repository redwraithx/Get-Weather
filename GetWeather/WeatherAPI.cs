using System;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;


// This App Gets its information from OpenWeatherMap.org using the free "api" they offer.


namespace GetWeather
{
    class WeatherData
    {
        private string city;
        private string country;

        public WeatherData(string City)
        {
            city = City;
            country = Country;
        }

        public string City { get; set; }
        public string Country { get; set; }


        public void CheckWeather()
        {
            WeatherAPI dataAPI = new WeatherAPI(city, country);
        }
    }


    class WeatherAPI : GetWeatherForm
    {
        public WeatherAPI(string city, string country)
        {

            SetCurrentURL(city, country);
            jsonDocObj = GetJsonData(CurrentURL);

        }

        private const string APIKEY = "e2749de9d7c8ea283bb0b49788a84345"; // key used to get weather data... each is unique
        private string CurrentURL;
        private WeatherTransferObject jsonDocObj;

        private void SetCurrentURL(string setCity, string setCountry)
        {
            CurrentURL = "http://api.openweathermap.org/data/2.5/weather?q=" + setCity + "," + setCountry + "$mode=xml&units=metric&APPID=" + APIKEY;
        }

        private WeatherTransferObject GetJsonData(string URL)
        {
            if (URL == null)
                return null;


            WeatherTransferObject weatherData = new WeatherTransferObject();
            string jsonData = "";

            using (WebClient client = new WebClient())
            {
                try
                {
                    jsonData = client.DownloadString(URL);

                    weatherData = JsonConvert.DeserializeObject<WeatherTransferObject>(jsonData);

                    // set icon                    
                    instance.SetIcon(weatherData.weather[0].icon);

                    // set all Data
                    instance.UpdateWeatherData(weatherData);
                }
                catch (Exception e)
                {

                    MessageBox.Show("Error: " + e.Message);
                    
                    throw;
                }
            }
  
            return weatherData;
        }
    }
}
