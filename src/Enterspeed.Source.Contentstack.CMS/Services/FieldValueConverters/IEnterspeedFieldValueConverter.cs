using Enterspeed.Source.Contentstack.CMS.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentstack.CMS.Services.FieldValueConverters;

public interface IEnterspeedFieldValueConverter
{
    bool IsConverter(ContentstackField value);
    IEnterspeedProperty Convert(ContentstackField contentstackField);
}