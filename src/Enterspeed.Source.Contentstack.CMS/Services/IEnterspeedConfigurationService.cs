using Enterspeed.Source.Contentstack.CMS.Models.Configuration;

namespace Enterspeed.Source.Contentstack.CMS.Services;

public interface IEnterspeedConfigurationService
{
    EnterspeedContentstackConfiguration GetConfiguration();
}