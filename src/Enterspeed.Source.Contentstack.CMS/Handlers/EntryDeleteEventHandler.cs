using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Enterspeed.Source.Contentstack.CMS.Constants;
using Enterspeed.Source.Contentstack.CMS.Exceptions;
using Enterspeed.Source.Contentstack.CMS.Models;
using Enterspeed.Source.Contentstack.CMS.Services;
using Enterspeed.Source.Sdk.Api.Services;

namespace Enterspeed.Source.Contentstack.CMS.Handlers
{
    public class EntryDeleteEventHandler : IEnterspeedEventHandler
    {
        private readonly IEnterspeedIngestService _enterspeedIngestService;
        private readonly IEntityIdentityService _entityIdentityService;

        public EntryDeleteEventHandler(
            IEnterspeedIngestService enterspeedIngestService,
            IEntityIdentityService entityIdentityService
            )
        {
            _enterspeedIngestService = enterspeedIngestService;
            _entityIdentityService = entityIdentityService;
        }

        public bool CanHandle(ContentstackResource resource)
        {
            return resource.Event.Equals(WebHookConstants.Events.UnPublish) ||
                   resource.Event.Equals(WebHookConstants.Events.Delete);
        }

        public Task Handle(ContentstackResource resource)
        {
            var data = JsonSerializer.Deserialize<EntryDataResource>(resource.Data.ToString() ?? string.Empty);
            var id = _entityIdentityService.GetId(data.EntryResource.Uid, data.Locale);

            var deleteResponse = _enterspeedIngestService.Delete(id);
            if (!deleteResponse.Success && deleteResponse.Status != HttpStatusCode.NotFound)
            {
                throw new EventHandlerException($"Failed deleting entity ({id}). Message: {deleteResponse.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
