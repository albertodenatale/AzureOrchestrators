using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Data
{
    public class WeatherForecastService 
    {    
        private string apiUrl;

        public WeatherForecastService(IConfiguration configuration)
        {
            this.apiUrl = configuration["Configuration:BackendUrl"];
        }

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            return await $"{apiUrl}/weather".GetJsonAsync<WeatherForecast[]>();
        }
    }
}
