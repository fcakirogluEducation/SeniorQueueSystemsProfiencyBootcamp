// See https://aka.ms/new-console-template for more information

using System.Text;
using Confluent.Kafka;
using Kafka.Consumer;

Console.WriteLine("Hello, World!");

await Consume();

Task Consume()
{
    var config = new ConsumerConfig()
    {
        BootstrapServers = "localhost:9094",
        GroupId = "test-consumer-group-5",
        AutoOffsetReset = AutoOffsetReset.Earliest
    };

    using var consumer = new ConsumerBuilder<int, OrderCreatedEvent>(config)
        .SetValueDeserializer(new CustomValuDeserializer<OrderCreatedEvent>()).Build();

    consumer.Subscribe("mytopic12");

    while (true)
    {
        var consumerResult = consumer.Consume(5000);

        if (consumerResult == null)
        {
            continue;
        }


        if (consumerResult.Message.Headers != null &&
            consumerResult.Message.Headers!.TryGetLastBytes("correlation.id", out byte[] value))
        {
            string correlationId = Encoding.UTF8.GetString(value);

            Console.WriteLine($"CorrelationId:{correlationId}");
        }


        Console.WriteLine(
            $"gelen mesaj:{consumerResult.Message.Value.Code} - {consumerResult.Message.Value.TotalPrice} ");
    }


    return Task.CompletedTask;
}

Console.ReadLine();