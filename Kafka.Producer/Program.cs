// See https://aka.ms/new-console-template for more information

using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Kafka.Producer;

Console.WriteLine("Producer");
string topicName = "mytopic13";

await CreateTopic();

await SendToTopic();
//await CreateTopic();

// create kafka topic


async Task CreateTopic()
{
    try
    {
        var adminClient = new AdminClientBuilder(new AdminClientConfig()
        {
            BootstrapServers = "localhost:9094",
        }).Build();


        var config = new Dictionary<string, string>();

        //config.Add("retention.ms", TimeSpan.FromDays(30).TotalMicroseconds.ToString());
        config.Add("retention.ms", "-1");
        config.Add("min.insync.replicas", "3");
        //config.Add("retention.bytes", "1024");
        await adminClient.CreateTopicsAsync(new[]
        {
            new TopicSpecification()
            {
                Name = topicName, NumPartitions = 5, ReplicationFactor = 1, Configs = config
            }
        });
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }


    Console.WriteLine("topic oluştu");
}

async Task SendToTopic()
{
    var config = new ProducerConfig()
    {
        BootstrapServers = "localhost:9094",
        Acks = Acks.Leader,
        MessageTimeoutMs = 10000,
        RetryBackoffMs = 2000
    };


    using var producer = new ProducerBuilder<int, OrderCreatedEvent>(config)
        .SetValueSerializer(new CustomValueSerializer<OrderCreatedEvent>()).Build();


    for (int i = 0; i < 10; i++)
    {
        var orderCreatedEvent = new OrderCreatedEvent() { Code = i.ToString(), TotalPrice = i * 100 };

        var topicPartition = new TopicPartition(topicName, new Partition(3));


        var dr = await producer.ProduceAsync(topicPartition, new Message<int, OrderCreatedEvent>
        {
            Headers = new Headers
            {
                { "correlation.id", Encoding.UTF8.GetBytes("correlation.value") },
                { "version", Encoding.UTF8.GetBytes("v1") }
            },
            Key = int.Parse(orderCreatedEvent.Code),
            Value = orderCreatedEvent
        });


        Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
    }
}