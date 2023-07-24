using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Enterspeed.Source.Contentstack.CMS.Constants;
using Enterspeed.Source.Contentstack.CMS.Exceptions;
using Enterspeed.Source.Contentstack.CMS.Models;
using Enterspeed.Source.Sdk.Api.Services;

namespace Enterspeed.Source.Contentstack.CMS.Handlers;

public class AssetDeleteEventHandler : IEnterspeedEventHandler
{
    private readonly IEnterspeedIngestService _enterspeedIngestService;

    public AssetDeleteEventHandler(IEnterspeedIngestService enterspeedIngestService)
    {
        _enterspeedIngestService = enterspeedIngestService;
    }

    public bool CanHandle(ContentstackResource resource)
    {
        return (resource.Event.Equals(WebHookConstants.Events.UnPublish) ||
                resource.Event.Equals(WebHookConstants.Events.Delete)) &&
               resource.Module.Equals(WebHookConstants.Types.Asset);
    }

    public Task Handle(ContentstackResource resource)
    {
        var data = JsonSerializer.Deserialize<AssetDataResource>(resource.Data.ToString() ?? string.Empty);
        if (data != null)
        {
            var id = data.EntryResource.Uid;

            var deleteResponse = _enterspeedIngestService.Delete(id);
            if (!deleteResponse.Success && deleteResponse.Status != HttpStatusCode.NotFound)
            {
                throw new EventHandlerException($"Failed deleting entity ({id}). Message: {deleteResponse.Message}");
            }
        }

        return Task.CompletedTask;
    }
}