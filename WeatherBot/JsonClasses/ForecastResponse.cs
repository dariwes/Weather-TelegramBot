using System.Collections.Generic;

namespace WeatherBot
{
    class ForecastResponse
    {
        public List<WeatherInfo> List { get; set; }
        public CityInfo City { get; set; }
    }
}
