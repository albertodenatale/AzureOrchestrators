using Flurl.Http;
using Models;
using System;
using System.Fabric;
using System.Fabric.Query;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Data
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }

    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly FabricClient fabricClient;
        private readonly IServiceIPResolver serviceIpResolver;

        public WeatherForecastService(IServiceIPResolver serviceIPResolver, FabricClient fabricClient)
        {
            this.fabricClient = fabricClient;
            this.serviceIpResolver = serviceIPResolver;
        }

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var serviceName = serviceIpResolver.GetServiceName("Backend");
            var serviceUri = serviceIpResolver.GetServiceUri("Backend");

            Partition partition = (await this.fabricClient.QueryManager.GetPartitionListAsync(serviceName)).FirstOrDefault();

            if (partition != null)
            {
                return await $"{serviceUri}/api/Weather?PartitionKey={((Int64RangePartitionInformation)partition.PartitionInformation).LowKey}&PartitionKind=Int64Range".GetJsonAsync<WeatherForecast[]>();
            }

            return null;
        }
    }
}
