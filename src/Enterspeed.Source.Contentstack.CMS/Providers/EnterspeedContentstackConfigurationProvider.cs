using Enterspeed.Source.Contentstack.CMS.Services;
using Enterspeed.Source.Sdk.Api.Providers;
using Enterspeed.Source.Sdk.Configuration;

namespace Enterspeed.Source.Contentstack.CMS.Providers;

public class EnterspeedContentstackConfigurationProvider : IEnterspeedConfigurationProvider
{
    private readonly IEnterspeedConfigurationService _enterspeedConfigurationService;

    public EnterspeedContentstackConfigurationProvider(IEnterspeedConfigurationService enterspeedConfigurationService)
    {
        _enterspeedConfigurationService = enterspeedConfigurationService;
    }

    public EnterspeedConfiguration Configuration => _enterspeedConfigurationService.GetConfiguration();
}