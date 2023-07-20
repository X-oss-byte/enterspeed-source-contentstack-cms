using System;
using Enterspeed.Source.Contentstack.CMS.Models;
using Newtonsoft.Json.Linq;

namespace Enterspeed.Source.Contentstack.CMS.Factories;

public class ContentstackFieldFactory : IContentstackFieldFactory
{
    public ContentstackField Create(object field)
    {
        //if (field.Value.GetType() == typeof(JObject))
        //    return new ContentStackObjectField(field);

        //if (field.Value.GetType() == typeof(JArray))
        //    return new ContentfulArrayField(field);

        //return new ContentfulSimpleField(field);
        throw new NotImplementedException("");
    }
}