namespace PaymentsService.Domain.Events
{
    public abstract class EventBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string AggregateId { get; set; }
    }
}
