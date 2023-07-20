using System.Text.Json;
using System.Threading.Tasks;
using Contentstack.Core;
using Contentstack.Core.Models;
using Enterspeed.Source.Contentstack.CMS.Constants;
using Enterspeed.Source.Contentstack.CMS.Models;
using Enterspeed.Source.Contentstack.CMS.Services;

namespace Enterspeed.Source.Contentstack.CMS.Handlers;

internal class EntryPublishEventHandler : IEnterspeedEventHandler
{
    private readonly ContentstackClient _contentstackClient;
    private readonly IEntityIdentityService _entityIdentityService;
    private readonly IEnterspeedPropertyService _enterspeedPropertyService;

    public EntryPublishEventHandler(
        ContentstackClient contentstackClient,
        IEntityIdentityService entityIdentityService,
        IEnterspeedPropertyService enterspeedPropertyService
        )
    {
        _contentstackClient = contentstackClient;
        _entityIdentityService = entityIdentityService;
        _enterspeedPropertyService = enterspeedPropertyService;
    }

    public bool CanHandle(ContentstackResource resource)
    {
        return resource?.Module == WebhooksConstants.Types.Entry
               && resource?.Event == WebhooksConstants.Events.Publish;
    }

    public async Task Handle(ContentstackResource resource)
    {
        var entry = JsonSerializer.Deserialize<EntryDataResource>(resource.Data.ToString() ?? string.Empty);
        if (entry != null)
        {
            var entryData = await _contentstackClient.ContentType(entry.ContentType?.Uid).Entry(entry.EntryResource.Uid).Fetch<Entry>();
            var entity = new EnterspeedEntity(entryData, entry.Locale, _entityIdentityService, _enterspeedPropertyService);
        }
    }
}