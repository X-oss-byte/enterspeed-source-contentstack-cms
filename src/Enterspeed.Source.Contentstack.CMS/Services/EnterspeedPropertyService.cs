using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Contentstack.Core.Models;
using Enterspeed.Source.Contentstack.CMS.Factories;
using Enterspeed.Source.Contentstack.CMS.Services.FieldValueConverters;
using Enterspeed.Source.Sdk.Api.Models.Properties;
using Newtonsoft.Json.Linq;

namespace Enterspeed.Source.Contentstack.CMS.Services;

public class EnterspeedPropertyService : IEnterspeedPropertyService
{
    private const string MetaData = "metaData";
    private const string Title = "title";
    private const string Description = "description";
    private const string File = "file";

    private readonly IEnumerable<IEnterspeedFieldValueConverter> _fieldValueConverters;
    private readonly IContentstackFieldFactory _contentstackFieldFactory;

    public EnterspeedPropertyService(
        IEnumerable<IEnterspeedFieldValueConverter> fieldValueConverters,
        IContentstackFieldFactory contentstackFieldFactory)
    {
        _fieldValueConverters = fieldValueConverters;
        _contentstackFieldFactory = contentstackFieldFactory;
    }

    public IDictionary<string, IEnterspeedProperty> GetProperties(Entry entry, string locale)
    {
        var properties = new Dictionary<string, IEnterspeedProperty>();
        foreach (var field in entry.Object)
        {
            var contentStackField = _contentstackFieldFactory.Create(field);
            var converter = _fieldValueConverters.FirstOrDefault(x => x.IsConverter(contentStackField));
            var value = converter?.Convert(contentStackField);

            if (value == null)
            {
                continue;
            }
            properties.Add(value.Name, value);
        }

        properties.Add(MetaData, CreateMetaData(entry));

        return properties;
    }

    private static IEnterspeedProperty CreateMetaData(Entry entry)
    {
        var publishDetails = (entry.Object.FirstOrDefault(p => p.Key == "publish_details").Value as JObject);

        var metaData = new Dictionary<string, IEnterspeedProperty>()
        {
            ["locale"] = new StringEnterspeedProperty("locale", publishDetails?.GetValue("locale")?.ToString()),
            ["type"] = new StringEnterspeedProperty("type", entry.GetContentType()),
            ["environment"] = new StringEnterspeedProperty("environment", publishDetails?.GetValue("environment")?.ToString()),
            ["createDate"] = new StringEnterspeedProperty("createDate", entry.GetCreateAt().ToString("yyyy-MM-ddTHH:mm:ss")),
            ["updateDate"] = new StringEnterspeedProperty("updateDate", entry.GetUpdateAt().ToString("yyyy-MM-ddTHH:mm:ss"))
        };

        return new ObjectEnterspeedProperty(MetaData, metaData);
    }
}