using System;
using System.Threading.Tasks;
using Frontend.Data;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherForecastService weatherForecastService;

        public HomeController(IWeatherForecastService weatherForecastService)
        {
            this.weatherForecastService = weatherForecastService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.weatherForecastService.GetForecastAsync(DateTime.Now));
        }
    }
}