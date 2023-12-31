﻿using System.Text.Json;
using System.Threading.Tasks;
using Contentstack.Core;
using Contentstack.Core.Models;
using Enterspeed.Source.Contentstack.CMS.Constants;
using Enterspeed.Source.Contentstack.CMS.Exceptions;
using Enterspeed.Source.Contentstack.CMS.Models;
using Enterspeed.Source.Contentstack.CMS.Services;
using Enterspeed.Source.Sdk.Api.Services;

namespace Enterspeed.Source.Contentstack.CMS.Handlers;

public class EntryPublishEventHandler : IEnterspeedEventHandler
{
    private readonly ContentstackClient _contentstackClient;
    private readonly IEntityIdentityService _entityIdentityService;
    private readonly IEnterspeedPropertyService _enterspeedPropertyService;
    private readonly IEnterspeedIngestService _enterspeedIngestService;

    public EntryPublishEventHandler(
        ContentstackClient contentstackClient,
        IEntityIdentityService entityIdentityService,
        IEnterspeedPropertyService enterspeedPropertyService,
        IEnterspeedIngestService enterspeedIngestService)
    {
        _contentstackClient = contentstackClient;
        _entityIdentityService = entityIdentityService;
        _enterspeedPropertyService = enterspeedPropertyService;
        _enterspeedIngestService = enterspeedIngestService;
    }

    public bool CanHandle(ContentstackResource resource)
    {
        return resource?.Module == WebHookConstants.Types.Entry
               && resource?.Event == WebHookConstants.Events.Publish;
    }

    public async Task Handle(ContentstackResource resource)
    {
        var data = JsonSerializer.Deserialize<EntryDataResource>(resource.Data.ToString() ?? string.Empty);
        if (data != null)
        {
            var entry = await _contentstackClient.ContentType(data.ContentType?.Uid).Entry(data.EntryResource.Uid).Fetch<Entry>();
            var entity = new EnterspeedEntity(entry, data.Locale, _entityIdentityService, _enterspeedPropertyService);

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