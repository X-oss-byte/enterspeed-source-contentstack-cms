using System.Text.Json;
using System.Threading.Tasks;
using Contentstack.Core;
using Enterspeed.Source.Contentstack.CMS.Constants;
using Enterspeed.Source.Contentstack.CMS.Exceptions;
using Enterspeed.Source.Contentstack.CMS.Models;
using Enterspeed.Source.Contentstack.CMS.Services;
using Enterspeed.Source.Sdk.Api.Services;

namespace Enterspeed.Source.Contentstack.CMS.Handlers;

public class AssetPublishEventHandler : IEnterspeedEventHandler
{
    private readonly ContentstackClient _contentstackClient;
    private readonly IEnterspeedPropertyService _enterspeedPropertyService;
    private readonly IEnterspeedIngestService _enterspeedIngestService;

    public AssetPublishEventHandler(ContentstackClient contentstackClient,
        IEnterspeedPropertyService enterspeedPropertyService, IEnterspeedIngestService enterspeedIngestService)
    {
        _contentstackClient = contentstackClient;
        _enterspeedPropertyService = enterspeedPropertyService;
        _enterspeedIngestService = enterspeedIngestService;
    }

    public bool CanHandle(ContentstackResource resource)
    {
        return resource?.Module == WebHookConstants.Types.Asset
               && resource?.Event == WebHookConstants.Events.Publish;
    }

    public async Task Handle(ContentstackResource resource)
    {
        var asset = JsonSerializer.Deserialize<AssetDataResource>(resource.Data.ToString() ?? string.Empty);
        if (asset != null)
        {
            var assetData = await _contentstackClient.Asset(asset.EntryResource.Uid).Fetch();
            var entity = new EnterspeedEntity(assetData, asset.EntryResource.ContentType, _enterspeedPropertyService);

            var saveResponse = _enterspeedIngestService.Save(entity);
            if (!saveResponse.Success)
            {
                var message = saveResponse.Exception != null
                    ? saveResponse.Exception.Message
                    : saveResponse.Message;
                throw new EventHandlerException($"Failed ingesting entity ({entity.Id}). Message: {message}");
            }
        }
    }
}