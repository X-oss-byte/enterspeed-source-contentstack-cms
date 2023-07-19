using System.Text.Json.Serialization;

namespace Enterspeed.Source.Contentstack.CMS.Models
{
    public class Url
    {
        [JsonPropertyName("url")]
        public string Value { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }
    }
}
