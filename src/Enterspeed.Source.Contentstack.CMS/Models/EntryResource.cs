using System.Text.Json.Serialization;

namespace Enterspeed.Source.Contentstack.CMS.Models
{
    internal class EntryResource
    {
        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("_version")]
        public int Version { get; set; }
    }
}
