namespace PaymentsService.Domain.Events
{
    public class PagamentoCriado : EventBase
    {
        public decimal Valor { get; set; }
        public string Metodo { get; set; }
    }
}
