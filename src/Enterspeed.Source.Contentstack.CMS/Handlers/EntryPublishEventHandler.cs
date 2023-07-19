using Contentstack.Core;
using Enterspeed.Source.Contentstack.CMS.Constants;
using Enterspeed.Source.Contentstack.CMS.Models;
using Enterspeed.Source.Contentstack.CMS.Services;

namespace Enterspeed.Source.Contentstack.CMS.Handlers
{
    internal class EntryPublishEventHandler : IEnterspeedEventHandler
    {
        private readonly ContentstackClient _contentstackClient;
        private readonly IEntityIdentityService _entityIdentityService;

        public EntryPublishEventHandler(ContentstackClient contentstackClient,
            IEntityIdentityService entityIdentityService)
        {
            _contentstackClient = contentstackClient;
            _entityIdentityService = entityIdentityService;
        }

        public bool CanHandle(ContentStackResource resource)
        {
            return resource?.Module == WebhooksConstants.Types.Asset
                   && resource?.Event == WebhooksConstants.Events.Publish;
        }

        public void Handle(ContentStackResource resource)
        {
            if (resource.Data is EntryDataResource data)
            {
                var entryData = _contentstackClient.ContentType(data.ContentType?.Title).Entry(data.EntryResource?.Uid);
                var entity = new EnterspeedEntity(entryData, data.Locale, _entityIdentityService);
            }
        }
    }
}
