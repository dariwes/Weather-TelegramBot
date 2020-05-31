namespace WeatherBot
{
    class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }
        public WindInfo Wind { get; set; }
        public CloudsInfo Clouds { get; set; }
        public string Name { get; set; }
    }
}
