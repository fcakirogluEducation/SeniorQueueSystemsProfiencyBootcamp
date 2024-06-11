// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using Producer.App;
using RabbitMQ.Client;

Console.WriteLine("Producer");

var connectionFactory = new ConnectionFactory()
{
    //Uri = new Uri("amqps://loyxqdcn:qyjCkzpTkMWAXPCrpj4CyyD9dWeulTRB@woodpecker.rmq.cloudamqp.com/loyxqdcn")
    HostName = "localhost",
};

using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();


channel.BasicReturn += (sender, e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());

    Console.WriteLine($"Mesaj ilgili kuyruklara gönderilemedi.{message}");
};


//fanout exchange
channel.ExchangeDeclare("topic-exchange", ExchangeType.Topic, true, false, null);


for (int i = 1; i < 11; i++)
{
    var message = $"Message {i}";


    var messageAsByte = Encoding.UTF8.GetBytes(message);


    channel.BasicPublish("topic-exchange", "info.error.warning", mandatory: true, null, messageAsByte);

    Console.WriteLine("Mesaj gönderilmiştir.");
}

for (int i = 1; i < 11; i++)
{
    var message = new UserCreatedEvent2(Id: 1, UserName: "ahmet15", Email: "ahmet@outlook.com");


    var messageAsByte = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));


    var messageProperties = channel.CreateBasicProperties();


    messageProperties.Headers = new Dictionary<string, object>()
    {
        { "version", "v1" },
        { "created", DateTime.Now }
    };
    messageProperties.Persistent = true;

    channel.BasicPublish("topic-exchange", "info.error.critical", mandatory: true, messageProperties, messageAsByte);

    Console.WriteLine("Mesaj gönderilmiştir.");
}