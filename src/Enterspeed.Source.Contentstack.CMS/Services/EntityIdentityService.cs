using Contentstack.Core.Models;

namespace Enterspeed.Source.Contentstack.CMS.Services;

public class EntityIdentityService : IEntityIdentityService
{
    public string GetId(Entry entry, string locale)
    {
        return GetId(entry.Uid, locale);
    }

    public string GetId(string id, string locale)
    {
        return $"{id}-{locale}".ToLower();
    }
}