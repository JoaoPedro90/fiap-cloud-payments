using PaymentsService.Domain.Events;

namespace PaymentsService.Models
{
    public class Transacao
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UsuarioId { get; set; }
        public string JogoId { get; set; }
        public decimal Valor { get; set; }
        public string MetodoPagamento { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }

        private List<EventBase> _eventosPendentes = new();

        public static Transacao Criar(string id, decimal valor)
        {
            var transacao = new Transacao();
            transacao.AplicarEvento(new PagamentoCriado { AggregateId = id, Valor = valor });
            return transacao;
        }

        private void AplicarEvento(EventBase evento)
        {
            switch (evento)
            {
                case PagamentoCriado e:
                    Id = e.Id;
                    Valor = e.Valor;
                    Status = "Criado";
                    break;
            }

            _eventosPendentes.Add(evento);
        }

        public IEnumerable<EventBase> EventosPendentes => _eventosPendentes;
    }
}
