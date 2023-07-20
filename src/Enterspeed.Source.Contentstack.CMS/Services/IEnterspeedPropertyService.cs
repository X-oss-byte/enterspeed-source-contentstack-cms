using Contentstack.Core.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;
using System.Collections.Generic;

namespace Enterspeed.Source.Contentstack.CMS.Services;

public interface IEnterspeedPropertyService
{
    IDictionary<string, IEnterspeedProperty> GetProperties(Entry entry, string locale);
}