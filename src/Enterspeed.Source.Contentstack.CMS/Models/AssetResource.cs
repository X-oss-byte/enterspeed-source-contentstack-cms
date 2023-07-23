using System.Text.Json.Serialization;

namespace Enterspeed.Source.Contentstack.CMS.Models;

public class AssetResource
{
    [JsonPropertyName("uid")]
    public string Uid { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("isDIr")]
    public string IsDir { get; set; }

    [JsonPropertyName("content_type")]
    public string ContentType { get; set; }

    [JsonPropertyName("_version")]
    public int Version { get; set; }
}