using System.Text.Json.Serialization;

namespace Enterspeed.Source.Contentstack.CMS.Models;

public class EntryDataResource
{
    [JsonPropertyName("entry")]
    public EntryResource EntryResource { get; set; }

    [JsonPropertyName("content_type")]
    public ContentType ContentType { get; set; }

    [JsonPropertyName("environment")]
    public Environment Environment { get; set; }

    [JsonPropertyName("action")]
    public string Action { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("locale")]
    public string Locale { get; set; }
}
