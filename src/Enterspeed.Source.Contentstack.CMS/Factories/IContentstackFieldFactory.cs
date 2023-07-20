using System.Collections.Generic;
using Enterspeed.Source.Contentstack.CMS.Models;

namespace Enterspeed.Source.Contentstack.CMS.Factories;

public interface IContentstackFieldFactory
{
    ContentstackField Create(KeyValuePair<string, object> field);
}