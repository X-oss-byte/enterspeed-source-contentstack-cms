using Enterspeed.Source.Contentstack.CMS.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentstack.CMS.Services.FieldValueConverters;

public class NumberFieldValueConverter : IEnterspeedFieldValueConverter
{
    public bool IsConverter(ContentstackField field)
    {
        return field.Type == typeof(double) || field.Type == typeof(long);
    }

    public IEnterspeedProperty Convert(ContentstackField field)
    {
        var number = 0d;
        if (double.TryParse(field.Value.ToString(), out var n))
        {
            number = n;
        }

        return new NumberEnterspeedProperty(field.Name, number);
    }
}