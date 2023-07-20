using Enterspeed.Source.Contentstack.CMS.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentstack.CMS.Services.FieldValueConverters;

public class StringFieldValueConverter : IEnterspeedFieldValueConverter
{
    public bool IsConverter(ContentstackField field)
    {
        return field.Type == typeof(string);
    }

    public IEnterspeedProperty Convert(ContentstackField field)
    {
        return new StringEnterspeedProperty(field.Name, field.Value.ToString());
    }
}