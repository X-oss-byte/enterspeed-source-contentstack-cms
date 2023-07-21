using Contentstack.Core;
using Contentstack.Core.Configuration;
using Enterspeed.Source.Contentstack.CMS;
using Enterspeed.Source.Contentstack.CMS.Factories;
using Enterspeed.Source.Contentstack.CMS.Handlers;
using Enterspeed.Source.Contentstack.CMS.Providers;
using Enterspeed.Source.Contentstack.CMS.Services;
using Enterspeed.Source.Contentstack.CMS.Services.FieldValueConverters;
using Enterspeed.Source.Sdk.Api.Connection;
using Enterspeed.Source.Sdk.Api.Providers;
using Enterspeed.Source.Sdk.Api.Services;
using Enterspeed.Source.Sdk.Domain.Connection;
using Enterspeed.Source.Sdk.Domain.Services;
using Enterspeed.Source.Sdk.Domain.SystemTextJson;
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
            Region = configuration.ContentStackRegion
        };
        builder.Services.AddSingleton<IEnterspeedConfigurationProvider, EnterspeedContentstackConfigurationProvider>();

        builder.Services.AddSingleton<IEnterspeedConfigurationService>(configurationService);
        builder.Services.AddSingleton<IEnterspeedConnection, EnterspeedConnection>();
        builder.Services.AddSingleton<IEnterspeedIngestService, EnterspeedIngestService>();
        builder.Services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
        builder.Services.AddSingleton<IEntityIdentityService, EntityIdentityService>();
        builder.Services.AddSingleton<IEnterspeedPropertyService, EnterspeedPropertyService>();
        builder.Services.AddSingleton(new ContentstackClient(options));
        builder.Services.AddSingleton<IContentstackFieldFactory, ContentstackFieldFactory>();

        // Field value converters
        builder.Services.AddSingleton<IEnterspeedFieldValueConverter, StringFieldValueConverter>();
        builder.Services.AddSingleton<IEnterspeedFieldValueConverter, NumberFieldValueConverter>();
        builder.Services.AddSingleton<IEnterspeedFieldValueConverter, BooleanFieldValueConverter>();
        builder.Services.AddSingleton<IEnterspeedFieldValueConverter, ObjectFieldValueConverter>();
        builder.Services.AddSingleton<IEnterspeedFieldValueConverter, ArrayFieldValueConverter>();

        // Event handlers
        builder.Services.AddSingleton<IEnterspeedEventHandler, EntryPublishEventHandler>();
        builder.Services.AddSingleton<IEnterspeedEventHandler, EntryDeleteEventHandler>();
    }
}