namespace MicroserviceKafka.API.Kafka
{
    public class OrderCreatedEvent
    {
        public string Code { get; init; } = null!;
        public decimal TotalPrice { get; init; }
    }
}