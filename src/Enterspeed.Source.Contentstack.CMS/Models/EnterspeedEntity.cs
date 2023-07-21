using System.Collections.Generic;
using Contentstack.Core.Models;
using Enterspeed.Source.Contentstack.CMS.Services;
using Enterspeed.Source.Sdk.Api.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentstack.CMS.Models;

public class EnterspeedEntity : IEnterspeedEntity
{
    public EnterspeedEntity(Entry entry, string locale, IEntityIdentityService entityIdentityService, IEnterspeedPropertyService enterspeedPropertyService)
    {
        Id = entityIdentityService.GetId(entry, locale);
        Type = entry.GetContentType();
        Properties = enterspeedPropertyService.GetProperties(entry, locale);
    }

    public string Id { get; }
    public string Type { get; }
    public string Url => null;
    public string[] Redirects => null;
    public string ParentId => null;
    public IDictionary<string, IEnterspeedProperty> Properties { get; }
}