// See https://aka.ms/new-console-template for more information

using Confluent.Kafka;
using Confluent.Kafka.Admin;

Console.WriteLine("Producer");

await SendToTopic();
//await CreateTopic();

// create kafka topic


async Task CreateTopic()
{
    var adminClient = new AdminClientBuilder(new AdminClientConfig()
    {
        BootstrapServers = "localhost:9094"
    }).Build();

    await adminClient.CreateTopicsAsync(new[]
    {
        new TopicSpecification()
        {
            Name = "mytopic", NumPartitions = 3, ReplicationFactor = 1
        }
    });

    Console.WriteLine("topic oluştu");
}

async Task SendToTopic()
{
    var config = new ProducerConfig() { BootstrapServers = "localhost:9094" };


    using var producer = new ProducerBuilder<Null, string>(config).Build();


    var dr = await producer.ProduceAsync("mytopic", new Message<Null, string> { Value = "test" });


    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
}