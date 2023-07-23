using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Enterspeed.Source.Contentstack.CMS.Exceptions;
using Enterspeed.Source.Contentstack.CMS.Handlers;
using Enterspeed.Source.Contentstack.CMS.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Enterspeed.Source.ContentStack.CMS;

public class ContentstackWebHooks
{
    private readonly IEnumerable<IEnterspeedEventHandler> _enterspeedEventHandlers;

    public ContentstackWebHooks(
        IEnumerable<IEnterspeedEventHandler> enterspeedEventHandlers)
    {
        _enterspeedEventHandlers = enterspeedEventHandlers;
    }

    [Function("ContentstackWebHooks")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var requestData = JsonSerializer.Deserialize<ContentstackResource>(requestBody);

        var enterspeedEventHandler = _enterspeedEventHandlers.FirstOrDefault(x => x.CanHandle(requestData));
        if (enterspeedEventHandler == null)
        {
            var response = req.CreateResponse(HttpStatusCode.BadRequest);
            await response.WriteStringAsync($"no handler found for event '{requestData.Event}'");

            return response;
        }

        try
        {
            await enterspeedEventHandler.Handle(requestData);
        }
        catch (EventHandlerException exception)
        {
            var response = req.CreateResponse(HttpStatusCode.UnprocessableEntity);
            await response.WriteStringAsync(exception.Message);

            return response;
        }

        return req.CreateResponse(HttpStatusCode.OK);
    }
}