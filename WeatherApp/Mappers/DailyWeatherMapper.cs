using WeatherApp.Models;

namespace WeatherApp.Mappers
{
    public class DailyWeatherMapper
    {
        public static DailyWeather Map(WeatherDto weatherDto)
        {
            return new DailyWeather()
            {
                Condition = weatherDto.weather[0].main,
                Description = weatherDto.weather[0].description,
                CurrentTemp = ConvertKelvinToCelsius(weatherDto.main.temp),
                MaxTemp = ConvertKelvinToCelsius(weatherDto.main.temp_max),
                MinTemp = ConvertKelvinToCelsius(weatherDto.main.temp_min),
                FeelTemp = ConvertKelvinToCelsius(weatherDto.main.feels_like),
                Humidity = weatherDto.main.humidity,
                WindSpeed = weatherDto.wind.speed * (3.6f),
                AtmosphericPressure = weatherDto.main.grnd_level,
                ForecastDateTime = Convert.ToDateTime(weatherDto.dt_txt),
                IconUrl = $"https://openweathermap.org/img/wn/{weatherDto.weather[0].icon}.png"
            };
        }

        public static int ConvertKelvinToCelsius(float kelvin)
        {         
            return Convert.ToInt32(kelvin - 273.15);
        }     
        
    }
}
