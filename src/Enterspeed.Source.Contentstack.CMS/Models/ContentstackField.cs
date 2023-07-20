using System;

namespace Enterspeed.Source.Contentstack.CMS.Models;

public abstract class ContentstackField
{
    public string Name { get; set; }
    public Type Type { get; set; }
    public object Value { get; set; }

    public T GetValue<T>()
    {
        return (T)Value;
    }
}