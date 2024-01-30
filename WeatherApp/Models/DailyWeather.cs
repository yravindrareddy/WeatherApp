namespace WeatherApp.Models
{
    public class DailyWeather
    {
        public string Condition { get; set; }
        public string Description { get; set; }
        public int CurrentTemp { get; set; }
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }        
        public int FeelTemp { get; set; }
        public int Humidity { get; set; }
        public float WindSpeed { get; set; }
        public string IconUrl { get; set; }
        public int AtmosphericPressure { get; set; }
        public DateTime ForecastDateTime { get; set; }        
    }
}
