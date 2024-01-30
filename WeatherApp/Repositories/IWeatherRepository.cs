using WeatherApp.Models;

namespace WeatherApp.Repositories
{
    public interface IWeatherRepository
    {
        public Task<LocationDto?> GetLocation(string city);

        public Task<WeatherForecastResponse> GetWeatherForecast(decimal latitude, decimal longitude);
    }
}
