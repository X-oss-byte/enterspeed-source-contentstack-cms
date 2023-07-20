using System.Threading.Tasks;
using Contentstack.Core;
using Enterspeed.Source.Contentstack.CMS.Constants;
using Enterspeed.Source.Contentstack.CMS.Models;

namespace Enterspeed.Source.Contentstack.CMS.Handlers;

internal class AssetPublishEventHandler : IEnterspeedEventHandler
{
    private readonly ContentstackClient _contentstackClient;

    public AssetPublishEventHandler(ContentstackClient contentstackClient)
    {
        _contentstackClient = contentstackClient;
    }

    public bool CanHandle(ContentStackResource resource)
    {
        return resource?.Module == WebhooksConstants.Types.Asset
               && resource?.Event == WebhooksConstants.Events.Publish;
    }

    public async Task Handle(ContentStackResource resource)
    {
    }
}