using Contentstack.Core;
using Contentstack.Core.Configuration;
using Contentstack.Core.Internals;
using Enterspeed.Source.Contentstack.CMS;
using Enterspeed.Source.Contentstack.CMS.Handlers;
using Enterspeed.Source.Contentstack.CMS.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Enterspeed.Source.Contentstack.CMS;

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
            Environment = configuration.ContentstackEnvironment,
            Region = ContentstackRegion.EU
        };

        builder.Services.AddSingleton(new ContentstackClient(options));
        builder.Services.AddSingleton<IEnterspeedConfigurationService>(configurationService);

        // Event handlers
        builder.Services.AddSingleton<IEnterspeedEventHandler, EntryPublishEventHandler>();

        // Services 
        builder.Services.AddSingleton<IEntityIdentityService, EntityIdentityService>();
    }
}