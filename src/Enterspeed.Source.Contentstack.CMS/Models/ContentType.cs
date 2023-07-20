using System.Text.Json.Serialization;

namespace Enterspeed.Source.Contentstack.CMS.Models;

public class ContentType
{
    [JsonPropertyName("uid")]
    public string Uid { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}