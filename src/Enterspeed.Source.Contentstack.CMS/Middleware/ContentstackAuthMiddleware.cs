using System.Net;
using System.Threading.Tasks;
using Enterspeed.Source.Contentstack.CMS.Models.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace Enterspeed.Source.Contentstack.CMS.Middleware;

public class ContentstackAuthMiddleware : IFunctionsWorkerMiddleware
{
    private readonly EnterspeedContentstackConfiguration _contentstackConfiguration;

    public ContentstackAuthMiddleware(EnterspeedContentstackConfiguration contentstackConfiguration)
    {
        _contentstackConfiguration = contentstackConfiguration;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if (context != null)
        {
            if (context.BindingContext.BindingData.TryGetValue("api_key", out var apiKey))
            {
                if (apiKey != null && _contentstackConfiguration.ContentstackApiKey.Equals(apiKey.ToString()))
                {
                    await next(context);
                }
            }
            else
            {
                var httpRequestData = await context.GetHttpRequestDataAsync();
                if (httpRequestData != null)
                {
                    var response = httpRequestData.CreateResponse();
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    await response.WriteStringAsync("You are not authorized");

                    context.GetInvocationResult().Value = response;
                }
            }
        }
    }
}
