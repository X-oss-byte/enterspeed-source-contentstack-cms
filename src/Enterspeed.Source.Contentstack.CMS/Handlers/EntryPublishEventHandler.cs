using Contentstack.Core;
using Enterspeed.Source.Contentstack.CMS.Constants;
using Enterspeed.Source.Contentstack.CMS.Models;

namespace Enterspeed.Source.Contentstack.CMS.Handlers
{
    internal class EntryPublishEventHandler : IEnterspeedEventHandler
    {
        private readonly ContentstackClient _contentstackClient;

        public EntryPublishEventHandler(ContentstackClient contentstackClient)
        {
            _contentstackClient = contentstackClient;
        }

        public bool CanHandle(ContentStackResource resource)
        {
            return resource?.Module == WebhooksConstants.Types.Asset
                   && resource?.Event == WebhooksConstants.Events.Publish;
        }

        public void Handle(ContentStackResource resource)
        {
            if (resource.Data is EntryResource data)
            {
                var assetData = _contentstackClient.ContentType(WebhooksConstants.Types.Asset).Entry(data.Uid);
            }
        }
    }
}
