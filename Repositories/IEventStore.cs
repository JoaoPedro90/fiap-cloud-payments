using PaymentsService.Domain.Events;

namespace PaymentsService.Repositories
{
    public interface IEventStore
    {
        void SalvarEvento(EventBase evento);
        IEnumerable<EventBase> ObterEventos(string aggregateId);
    }

}
