using System.IO;
using System.Threading.Tasks;
using Enterspeed.Source.Contentstack.CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Enterspeed.Source.ContentStack.CMS
{
    public static class ContentStackWebHooks
    {
        [FunctionName("ContentStackWebHooks")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<ContentStackResource>(requestBody);
            
            
            return new OkObjectResult("");
        }
    }
}
