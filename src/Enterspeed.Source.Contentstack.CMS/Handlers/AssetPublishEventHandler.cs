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

    public bool CanHandle(ContentstackResource resource)
    {
        return resource?.Module == WebHookConstants.Types.Asset
               && resource?.Event == WebHookConstants.Events.Publish;
    }

    public async Task Handle(ContentstackResource resource)
    {
    }
}