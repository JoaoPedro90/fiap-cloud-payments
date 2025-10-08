using PaymentsService.Domain.Events;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PaymentsService.Domain.Events;

namespace PaymentsService.Repositories
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options) { }

        public DbSet<EventEntity> Eventos { get; set; }
    }

    public class EventEntity
    {
        public Guid Id { get; set; }
        public string AggregateId { get; set; }
        public string TipoEvento { get; set; }
        public string Dados { get; set; } // JSON do evento
        public DateTime Timestamp { get; set; }
    }

    public class EventStoreMySql : IEventStore
    {
        private readonly EventStoreContext _context;

        public EventStoreMySql(EventStoreContext context)
        {
            _context = context;
        }

        public void SalvarEvento(EventBase evento)
        {
            var entity = new EventEntity
            {
                Id = evento.Id,
                AggregateId = evento.AggregateId,
                TipoEvento = evento.GetType().Name,
                Dados = System.Text.Json.JsonSerializer.Serialize(evento),
                Timestamp = evento.Timestamp
            };

            _context.Eventos.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<EventBase> ObterEventos(string aggregateId)
        {
            var entities = _context.Eventos
                .Where(e => e.AggregateId == aggregateId)
                .OrderBy(e => e.Timestamp)
                .ToList();

            var eventos = new List<EventBase>();

            foreach (var entity in entities)
            {
                var tipo = Type.GetType($"PaymentsService.Domain.Events.{entity.TipoEvento}");
                if (tipo != null)
                {
                    var evento = (EventBase?)System.Text.Json.JsonSerializer.Deserialize(entity.Dados, tipo);
                    if (evento != null)
                        eventos.Add(evento);
                }
            }

            return eventos;
        }
    }
}
