using System.Threading.Tasks;
using Enterspeed.Source.Contentstack.CMS.Models;

namespace Enterspeed.Source.Contentstack.CMS.Handlers;

public interface IEnterspeedEventHandler
{
    bool CanHandle(ContentStackResource resource);
    Task Handle(ContentStackResource resource);
}