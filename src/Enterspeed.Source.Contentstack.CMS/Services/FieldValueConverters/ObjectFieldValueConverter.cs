using System.Collections.Generic;
using Enterspeed.Source.Contentstack.CMS.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;
using Newtonsoft.Json.Linq;

namespace Enterspeed.Source.Contentstack.CMS.Services.FieldValueConverters;

public class ObjectFieldValueConverter : IEnterspeedFieldValueConverter
{
    private readonly IEntityIdentityService _entityIdentityService;

    public ObjectFieldValueConverter(IEntityIdentityService entityIdentityService)
    {
        _entityIdentityService = entityIdentityService;
    }

    public bool IsConverter(ContentstackField field)
    {
        return field.Type == typeof(JObject);
    }

    public IEnterspeedProperty Convert(ContentstackField field)
    {
        var value = (JObject)field.Value;
        var properties = new Dictionary<string, IEnterspeedProperty>();
        //{
        //    ["id"] = new StringEnterspeedProperty("id", _entityIdentityService.GetId(value..Id, locale)),
        //    ["type"] = new StringEnterspeedProperty("type", value.SystemProperties.Type),
        //    ["linkType"] = new StringEnterspeedProperty("linkType", value.SystemProperties.LinkType)
        //};

        return new ObjectEnterspeedProperty(field.Name, properties);
    }
}