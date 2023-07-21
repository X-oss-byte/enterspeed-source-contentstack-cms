using System.Collections.Generic;
using System.Runtime.InteropServices;
using Enterspeed.Source.Contentstack.CMS.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;
using Newtonsoft.Json.Linq;

namespace Enterspeed.Source.Contentstack.CMS.Services.FieldValueConverters;

public class ArrayFieldValueConverter : IEnterspeedFieldValueConverter
{
    private readonly IEntityIdentityService _entityIdentityService;

    public ArrayFieldValueConverter(IEntityIdentityService entityIdentityService)
    {
        _entityIdentityService = entityIdentityService;
    }

    public bool IsConverter(ContentstackField field)
    {
        return field.Type == typeof(JArray);
    }

    public IEnterspeedProperty Convert(ContentstackField field)
    {
        //if (field.Type == typeof(string[]))
        //{
        //    var value = ((ContentstackObjectField)field).GetValue<string[]>();

        //    var arrayItems = new List<IEnterspeedProperty>();
        //    foreach (var stringValue in value)
        //    {
        //        arrayItems.Add(new StringEnterspeedProperty(stringValue));
        //    }

        //    return new ArrayEnterspeedProperty(field.Name, arrayItems.ToArray());
        //}
        //if (field.Type == typeof(ContentfulResource[]))
        //{
        //    var value = ((ContentfulArrayField)field).GetValue<ContentfulResource[]>();

        //    var arrayItems = new List<IEnterspeedProperty>();
        //    foreach (var contentfulResource in value)
        //    {
        //        arrayItems.Add(new ObjectEnterspeedProperty(new Dictionary<string, IEnterspeedProperty>
        //        {
        //            ["id"] = new StringEnterspeedProperty("id", _entityIdentityService.GetId(contentfulResource.SystemProperties.Id, locale)),
        //            ["type"] = new StringEnterspeedProperty("type", contentfulResource.SystemProperties.Type),
        //            ["linkType"] = new StringEnterspeedProperty("linkType", contentfulResource.SystemProperties.LinkType)
        //        }));
        //    }

        //    return new ArrayEnterspeedProperty(field.Name, arrayItems.ToArray());
        //}'
        // TODO: Fix. This is only for testing purposes. 
        var arrayItems = new List<IEnterspeedProperty> { new StringEnterspeedProperty("test") };
        return new ArrayEnterspeedProperty(field.Name, arrayItems.ToArray());
    }
}