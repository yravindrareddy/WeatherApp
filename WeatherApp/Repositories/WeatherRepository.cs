using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherApp.Models;

namespace WeatherApp.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public WeatherRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration;
        }
        public async Task<LocationDto?> GetLocation(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["OpenWeatherApi:BaseAddress"]);

            var url = $"/geo/1.0/direct?q={city}&limit=5&appid={_configuration["AppId"]}";

            var resp = await httpClient.GetAsync(url);
            if (resp.IsSuccessStatusCode)
            {
                var content = await resp.Content.ReadAsStringAsync();
                var locations = JsonSerializer.Deserialize<List<LocationDto>>(content);
                return locations.FirstOrDefault();
            }
            return null;
        }

        public async Task<WeatherForecastResponse> GetWeatherForecast(decimal latitude, decimal longitude)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_configuration["OpenWeatherApi:BaseAddress"]);

            var url = $"/data/2.5/forecast?lat={latitude}&lon={longitude}&appid={_configuration["AppId"]}";

            var resp = await httpClient.GetAsync(url);
            var content = await resp.Content.ReadAsStringAsync();
            var weatherForecastResponse = JsonSerializer.Deserialize<WeatherForecastResponse>(content);
            return weatherForecastResponse;
        }
    }
}
