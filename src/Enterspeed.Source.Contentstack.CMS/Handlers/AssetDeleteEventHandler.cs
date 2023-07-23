using System;
using System.Threading.Tasks;
using Enterspeed.Source.Contentstack.CMS.Constants;
using Enterspeed.Source.Contentstack.CMS.Models;

namespace Enterspeed.Source.Contentstack.CMS.Handlers
{
    public class AssetDeleteEventHandler : IEnterspeedEventHandler
    {
        public bool CanHandle(ContentstackResource resource)
        {
            return (resource.Event.Equals(WebHookConstants.Events.UnPublish) ||
                   resource.Event.Equals(WebHookConstants.Events.Delete)) &&
                   resource.Module.Equals(WebHookConstants.Types.Asset);
        }

        public async Task Handle(ContentstackResource resource)
        {
        }
    }
}
