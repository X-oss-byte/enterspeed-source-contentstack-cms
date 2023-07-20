using System.Collections.Generic;

namespace Enterspeed.Source.Contentstack.CMS.Models;

public class ContentstackObjectField : ContentstackField
{
    public ContentstackObjectField(KeyValuePair<string, object> field)
    {
        Name = field.Key;
        Type = field.Value?.GetType();
        Value = field.Value;
    }
}