using System.Threading.Tasks;
using Contentstack.Core.Configuration;
using Contentstack.Core;
using Enterspeed.Source.Contentstack.CMS.Factories;
using Enterspeed.Source.Contentstack.CMS.Handlers;
using Enterspeed.Source.Contentstack.CMS.Middleware;
using Enterspeed.Source.Contentstack.CMS.Providers;
using Enterspeed.Source.Contentstack.CMS.Services.FieldValueConverters;
using Enterspeed.Source.Contentstack.CMS.Services;
using Enterspeed.Source.Sdk.Api.Connection;
using Enterspeed.Source.Sdk.Api.Providers;
using Enterspeed.Source.Sdk.Api.Services;
using Enterspeed.Source.Sdk.Domain.Connection;
using Enterspeed.Source.Sdk.Domain.Services;
using Enterspeed.Source.Sdk.Domain.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Enterspeed.Source.Contentstack.CMS;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults(builder =>
            {
                builder.UseMiddleware<ContentstackAuthMiddleware>();

            }).ConfigureServices(services =>
            {
                var configurationService = new EnterspeedConfigurationService();

                var configuration = configurationService.GetConfiguration();
                var options = new ContentstackOptions()
                {
                    ApiKey = configuration.ContentstackApiKey,
                    DeliveryToken = configuration.ContentStackDeliveryToken,
                    Environment = configuration.ContentstackEnvironment,
                    Region = configuration.ContentStackRegion
                };
                services.AddSingleton(new ContentstackClient(options));
                services.AddSingleton(configuration);

                services.AddSingleton<IEnterspeedConfigurationProvider, EnterspeedContentstackConfigurationProvider>();
                services.AddSingleton<IEnterspeedConfigurationService>(configurationService);
                services.AddSingleton<IEnterspeedConnection, EnterspeedConnection>();
                services.AddSingleton<IEnterspeedIngestService, EnterspeedIngestService>();
                services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
                services.AddSingleton<IEntityIdentityService, EntityIdentityService>();
                services.AddSingleton<IEnterspeedPropertyService, EnterspeedPropertyService>();
                services.AddSingleton<IContentstackFieldFactory, ContentstackFieldFactory>();

                // Field value converters
                services.AddSingleton<IEnterspeedFieldValueConverter, StringFieldValueConverter>();
                services.AddSingleton<IEnterspeedFieldValueConverter, NumberFieldValueConverter>();
                services.AddSingleton<IEnterspeedFieldValueConverter, BooleanFieldValueConverter>();
                services.AddSingleton<IEnterspeedFieldValueConverter, ObjectFieldValueConverter>();
                services.AddSingleton<IEnterspeedFieldValueConverter, ArrayFieldValueConverter>();

                // Event handlers
                services.AddSingleton<IEnterspeedEventHandler, EntryPublishEventHandler>();
                services.AddSingleton<IEnterspeedEventHandler, EntryDeleteEventHandler>();
                services.AddSingleton<IEnterspeedEventHandler, AssetPublishEventHandler>();
                services.AddSingleton<IEnterspeedEventHandler, AssetDeleteEventHandler>();
            })
            .Build();
        await host.RunAsync();
    }
}