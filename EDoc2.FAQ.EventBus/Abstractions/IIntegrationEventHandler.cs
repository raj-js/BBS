using EDoc2.FAQ.EventBus.Events;
using System.Threading.Tasks;

namespace EDoc2.FAQ.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent @event);
    }
}
