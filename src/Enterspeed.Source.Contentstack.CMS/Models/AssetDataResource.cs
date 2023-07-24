using System.Text.Json.Serialization;

namespace Enterspeed.Source.Contentstack.CMS.Models;

public class AssetDataResource
{
    [JsonPropertyName("asset")]
    public AssetResource EntryResource { get; set; }

    [JsonPropertyName("environment")]
    public Environment Environment { get; set; }
}