namespace WorkerService1.Kafka
{
    internal class OrderCreatedEvent
    {
        public string Code { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}