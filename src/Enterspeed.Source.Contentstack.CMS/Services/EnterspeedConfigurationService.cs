using System;
using Enterspeed.Source.Contentstack.CMS.Models.Configuration;

namespace Enterspeed.Source.Contentstack.CMS.Services
{
    public class EnterspeedConfigurationService : IEnterspeedConfigurationService
    {
        public EnterspeedContentstackConfiguration GetConfiguration()
        {
            return new EnterspeedContentstackConfiguration()
            {
                ApiKey = Environment.GetEnvironmentVariable("Enterspeed.ApiKey"),
                BaseUrl = Environment.GetEnvironmentVariable("Enterspeed.BaseUrl"),
                ContentstackApiKey = Environment.GetEnvironmentVariable("ContentStack.ApiKey"),
                ContentStackDeliveryToken = Environment.GetEnvironmentVariable("ContentStack.DeliveryToken"),
            };
        }
    }
}
