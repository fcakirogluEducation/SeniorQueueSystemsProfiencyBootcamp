// See https://aka.ms/new-console-template for more information

using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Consumer");


var connectionFactory = new ConnectionFactory()
{
    //Uri = new Uri("amqps://loyxqdcn:qyjCkzpTkMWAXPCrpj4CyyD9dWeulTRB@woodpecker.rmq.cloudamqp.com/loyxqdcn")

    HostName = "localhost"
};

using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();


channel.BasicQos(0, 5, true);

channel.QueueDeclare("topic-queue", true, false, false, null);

channel.QueueBind("topic-queue", "topic-exchange", "info.error.warning", null);


var consumer = new EventingBasicConsumer(channel);


consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    try
    {
        var properties = e.BasicProperties;

        //read version header
        var version = properties.Headers["version"];

        var message = Encoding.UTF8.GetString(e.Body.ToArray());

        Console.WriteLine($"Gelen Mesaj: {message}");

        channel.BasicAck(e.DeliveryTag, true);
    }
    catch (Exception exception)
    {
        Console.WriteLine(exception);
        throw;
    }
}

channel.BasicConsume("topic-queue", autoAck: false, consumer);

var x = Console.ReadLine();