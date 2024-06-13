using System.Text;
using Confluent.Kafka;
using static Confluent.Kafka.ConfigPropertyNames;

namespace MicroserviceKafka.API.Kafka
{
    public class KafkaBus
    {
        private readonly IProducer<int, OrderCreatedEvent> _producer;


        public KafkaBus()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9094",
                Acks = Acks.Leader,
                MessageTimeoutMs = 10000,
                RetryBackoffMs = 2000,
            };


            _producer = new ProducerBuilder<int, OrderCreatedEvent>(config)
                .SetValueSerializer(new CustomValueSerializer<OrderCreatedEvent>()).Build();
        }

        public async Task<bool> Send(OrderCreatedEvent orderCreatedEvent)
        {
            var dr = await _producer.ProduceAsync("api_topic", new Message<int, OrderCreatedEvent>
            {
                Key = int.Parse(orderCreatedEvent.Code),
                Value = orderCreatedEvent
            });

            return dr.Status == PersistenceStatus.Persisted;
        }
    }
}