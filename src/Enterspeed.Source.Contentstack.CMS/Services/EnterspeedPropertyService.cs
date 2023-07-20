using System.Collections.Generic;
using Contentstack.Core.Models;
using Enterspeed.Source.Contentstack.CMS.Factories;
using Enterspeed.Source.Contentstack.CMS.Services.FieldValueConverters;
using Enterspeed.Source.Sdk.Api.Models.Properties;

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
                
        }

        return new Dictionary<string, IEnterspeedProperty>();
    }
}