using Enterspeed.Source.Sdk.Configuration;

namespace Enterspeed.Source.Contentstack.CMS.Models.Configuration;

public class EnterspeedContentstackConfiguration : EnterspeedConfiguration
{
    public string ContentstackApiKey { get; set; }
    public string ContentStackDeliveryToken { get; set; }
    public string ContentstackEnvironment { get; set; }
}