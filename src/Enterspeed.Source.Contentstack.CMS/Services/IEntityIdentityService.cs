using Contentstack.Core.Models;

namespace Enterspeed.Source.Contentstack.CMS.Services;

public interface IEntityIdentityService
{
    string GetId(Entry entry, string locale);
    string GetId(string id, string locale);
}