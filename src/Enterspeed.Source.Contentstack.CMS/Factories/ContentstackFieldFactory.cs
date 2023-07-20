using System.Collections.Generic;
using Enterspeed.Source.Contentstack.CMS.Models;

namespace Enterspeed.Source.Contentstack.CMS.Factories;

public class ContentstackFieldFactory : IContentstackFieldFactory
{
    public ContentstackField Create(KeyValuePair<string, object> field)
    {
        return new ContentstackObjectField(field);
    }
}