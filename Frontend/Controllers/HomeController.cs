using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Data;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherForecastService weatherForecastService;

        public HomeController(WeatherForecastService weatherForecastService)
        {
            this.weatherForecastService = weatherForecastService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this.weatherForecastService.GetForecastAsync(DateTime.Now));
        }
    }
}