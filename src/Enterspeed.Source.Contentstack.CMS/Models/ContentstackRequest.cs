using System.Text.Json.Serialization;

namespace Enterspeed.Source.Contentstack.CMS.Models;

public class ContentstackRequest
{
    [JsonPropertyName("api_key")]
    public string ApiKey { get; set; }

}