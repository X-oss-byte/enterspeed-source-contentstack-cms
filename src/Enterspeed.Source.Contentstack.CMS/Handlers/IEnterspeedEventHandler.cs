using Enterspeed.Source.Contentstack.CMS.Models;

namespace Enterspeed.Source.Contentstack.CMS.Handlers
{
    internal interface IEnterspeedEventHandler
    {
        bool CanHandle(ContentStackResource resource);
        void Handle(ContentStackResource resource);
    }
}
