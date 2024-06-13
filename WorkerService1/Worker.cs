using System.Security.AccessControl;
using System.Text;
using Confluent.Kafka;
using WorkerService1.Kafka;
using static Confluent.Kafka.ConfigPropertyNames;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IConsumer<int, OrderCreatedEvent> _consumer;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9094",
                GroupId = "test-consumer-group-1",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<int, OrderCreatedEvent>(config)
                .SetValueDeserializer(new CustomValueDeserializer<OrderCreatedEvent>()).Build();

            _consumer.Subscribe("api_topic");


            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumerResult = _consumer.Consume(5000);

                if (consumerResult == null)
                {
                    Console.WriteLine("kuyrukta mesaj yok");
                    continue;
                }

                Console.WriteLine(
                    $"gelen mesaj:{consumerResult.Message.Value.Code} - {consumerResult.Message.Value.TotalPrice} ");
            }
        }
    }
}