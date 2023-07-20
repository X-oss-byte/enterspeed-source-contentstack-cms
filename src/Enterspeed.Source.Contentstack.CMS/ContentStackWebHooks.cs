using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Enterspeed.Source.Contentstack.CMS.Exceptions;
using Enterspeed.Source.Contentstack.CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Enterspeed.Source.Contentstack.CMS.Handlers;

namespace Enterspeed.Source.ContentStack.CMS;

public class ContentStackWebHooks
{
    private readonly IEnumerable<IEnterspeedEventHandler> _enterspeedEventHandlers;

    public ContentStackWebHooks(
        IEnumerable<IEnterspeedEventHandler> enterspeedEventHandlers
        )
    {
        _enterspeedEventHandlers = enterspeedEventHandlers;
    }

    [FunctionName("ContentStackWebHooks")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var requestData = JsonSerializer.Deserialize<ContentStackResource>(requestBody);

        var enterspeedEventHandler = _enterspeedEventHandlers.FirstOrDefault(x => x.CanHandle(requestData));

        if (enterspeedEventHandler == null)
        {
            return new BadRequestObjectResult($"no handler found for event '{requestData.Event}'");
        }

        try
        {
            await enterspeedEventHandler.Handle(requestData);
        }
        catch (EventHandlerException exception)
        {
            return new UnprocessableEntityObjectResult(exception.Message);
        }

        return new OkObjectResult("");
    }
}