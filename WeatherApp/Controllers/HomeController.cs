using Microsoft.AspNetCore.Mvc;
using System.Collections;
using WeatherApp.Mappers;
using WeatherApp.Models;
using WeatherApp.Repositories;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherRepository _weatherRepository;

        public HomeController(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }
        public ActionResult Index()
        {
            ViewBag.InitialLoad = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string cityName)
        {
            var location = await _weatherRepository.GetLocation(cityName);
            IEnumerable<DailyWeather> weatherInfo;
            if (location == null)
            {
                weatherInfo = null;
            }
            else
            {
                var weatherForecast = await _weatherRepository.GetWeatherForecast(location.Latitude, location.Longitude);
                var weatherCollection = weatherForecast.list.Select(w => DailyWeatherMapper.Map(w));
                var weatherGroup = weatherCollection.GroupBy(w => w.ForecastDateTime.Date).Select(g => g.First()).ToList();
                weatherInfo = weatherGroup;
            }
            ViewBag.InitialLoad = false;
            return View(weatherInfo);
        }
    }
}
