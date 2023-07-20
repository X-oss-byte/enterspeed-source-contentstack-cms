using System.Threading.Tasks;
using Enterspeed.Source.Contentstack.CMS.Models;

namespace Enterspeed.Source.Contentstack.CMS.Handlers;

public interface IEnterspeedEventHandler
{
    bool CanHandle(ContentstackResource resource);
    Task Handle(ContentstackResource resource);
}