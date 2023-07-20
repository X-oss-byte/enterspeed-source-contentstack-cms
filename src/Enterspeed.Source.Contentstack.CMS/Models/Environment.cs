using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Enterspeed.Source.Contentstack.CMS.Models;

public class Environment
{
    [JsonPropertyName("uid")]
    public string Uid { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("urls")]
    public List<Url> Urls { get; set; }
}