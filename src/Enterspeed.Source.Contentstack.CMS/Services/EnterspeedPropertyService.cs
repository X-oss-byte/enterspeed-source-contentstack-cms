using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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

    public IDictionary<string, IEnterspeedProperty> GetProperties(Entry entry)
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

    public IDictionary<string, IEnterspeedProperty> GetProperties(Asset asset)
    {
        var properties = new Dictionary<string, IEnterspeedProperty>
            {
                { Title, new StringEnterspeedProperty(Title, asset.FileName) },
                { Description, new StringEnterspeedProperty(Description, asset.Description ?? string.Empty) },
                { File, CreateFileData(asset) },
                { MetaData, CreateMetaData(asset) }
            };

        return properties;
    }

    private static IEnterspeedProperty CreateFileData(Asset asset)
    {
        var publishDetails = asset.Get("publish_details");
        var fileData = new Dictionary<string, IEnterspeedProperty>()
        {
            ["fileName"] = new StringEnterspeedProperty("fileName", asset.FileName),
            ["url"] = new StringEnterspeedProperty("url", asset.Url),
            ["contentType"] = new StringEnterspeedProperty("contentType", asset.Get("content_type")?.ToString()),
            ["size"] = new StringEnterspeedProperty("size", asset.FileSize)
        };

        return new ObjectEnterspeedProperty("file", fileData);
    }

    private static IEnterspeedProperty CreateMetaData(Asset asset)
    {
        var publishDetails = asset.Get("publish_details") as JObject;
        var metaData = new Dictionary<string, IEnterspeedProperty>
        {
            ["environment"] = new StringEnterspeedProperty("environment", publishDetails?.GetValue("environment")?.ToString()),
            ["createDate"] = new StringEnterspeedProperty("createDate", asset.GetCreateAt().ToString("yyyy-MM-ddTHH:mm:ss")),
            ["updateDate"] = new StringEnterspeedProperty("updateDate", asset.GetUpdateAt().ToString("yyyy-MM-ddTHH:mm:ss"))
        };

        return new ObjectEnterspeedProperty(MetaData, metaData);
    }
}