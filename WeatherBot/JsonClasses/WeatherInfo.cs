namespace WeatherBot
{
    class WeatherInfo
    {
        public TemperatureInfo Main { get; set; }
        public CloudsInfo Clouds { get; set; }
        public WindInfo Wind { get; set; }
        public string Dt_txt { get; set; }
    }
}
