namespace WeatherBot
{
    class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float? Latitude { get; set; } = null;
        public float? Longitude { get; set; } = null;
    }
}
