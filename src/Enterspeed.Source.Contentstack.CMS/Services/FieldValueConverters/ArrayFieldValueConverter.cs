using System.Collections.Generic;
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

        var arrayItems = new List<IEnterspeedProperty> { new StringEnterspeedProperty("test") };
        return new ArrayEnterspeedProperty(field.Name, arrayItems.ToArray());
    }
}