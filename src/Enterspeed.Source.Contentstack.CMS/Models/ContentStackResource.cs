using System;
using System.Text.Json.Serialization;

namespace Enterspeed.Source.Contentstack.CMS.Models;

public class ContentstackResource

{
    [JsonPropertyName("module")]
    public string Module { get; set; }

    [JsonPropertyName("api_key")]
    public string ApiKey { get; set; }

    [JsonPropertyName("event")]
    public string Event { get; set; }

    [JsonPropertyName("triggered_at")]
    public DateTime TriggeredAt { get; set; }

    [JsonPropertyName("data")]
    public object Data { get; set; }
}