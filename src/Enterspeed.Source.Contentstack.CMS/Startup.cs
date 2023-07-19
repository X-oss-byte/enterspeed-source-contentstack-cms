using Contentstack.Core;
using Contentstack.Core.Configuration;
using Enterspeed.Source.Contentstack.CMS.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Enterspeed.Source.Contentstack.CMS
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configurationService = new EnterspeedConfigurationService();

            var configuration = configurationService.GetConfiguration();
            var options = new ContentstackOptions()
            {
                ApiKey = configuration.ContentstackApiKey,
                DeliveryToken = configuration.ContentStackDeliveryToken,
                Environment = configuration.Environment
            };

            builder.Services.AddSingleton(new ContentstackClient(options));
            builder.Services.AddSingleton<IEnterspeedConfigurationService>(configurationService);
        }
    }
}
