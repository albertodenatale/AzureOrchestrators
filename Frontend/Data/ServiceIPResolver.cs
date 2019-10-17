using Microsoft.Extensions.Configuration;
using System;
using System.Fabric;

namespace Frontend.Data
{
    public interface IServiceIPResolver
    {
        Uri GetServiceUri(string serviceName);
        Uri GetServiceName(string serviceName);
    }

    public class ServiceIPResolver: IServiceIPResolver
    {
        private readonly StatelessServiceContext context;
        private readonly string reverseProxyBaseUri;

        public ServiceIPResolver(IConfiguration configuration, StatelessServiceContext context)
        {
            this.context = context;
            this.reverseProxyBaseUri = configuration["Configuration:ReverseProxyUri"];
        }

        /// <summary>
        /// Constructs a service name for a specific poll.
        /// Example: fabric:/VotingApplication/polls/name-of-poll
        /// </summary>
        /// <param name="poll"></param>
        /// <returns></returns>
        public Uri GetServiceUri(string serviceName)
        {
            var uri = GetServiceName(serviceName);

            return GetProxyAddress(uri);
        }

        public Uri GetServiceName(string serviceName)
        {
            return new Uri($"{context.CodePackageActivationContext.ApplicationName}/{serviceName}");
        }

        /// <summary>
        /// Constructs a reverse proxy URL for a given service.
        /// Example: http://localhost:19081/VotingApplication/VotingData/
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private Uri GetProxyAddress(Uri serviceName)
        {
            return new Uri($"{this.reverseProxyBaseUri}{serviceName.AbsolutePath}");
        }
    }
}
