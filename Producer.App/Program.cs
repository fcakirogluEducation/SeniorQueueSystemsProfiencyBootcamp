// See https://aka.ms/new-console-template for more information

using System.Text;
using RabbitMQ.Client;

Console.WriteLine("Producer");

var connectionFactory = new ConnectionFactory()
{
    Uri = new Uri("amqps://loyxqdcn:qyjCkzpTkMWAXPCrpj4CyyD9dWeulTRB@woodpecker.rmq.cloudamqp.com/loyxqdcn")
};

using var connection = connectionFactory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
channel.ConfirmSelect();

try
{
    for (int i = 1; i < 11; i++)
    {
        var message = $"Message {i}";


        var messageAsByte = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish("", "demo-queue", null, messageAsByte);
        channel.WaitForConfirms(TimeSpan.FromSeconds(5));
        Console.WriteLine("Mesaj gönderilmiştir.");
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
    Console.WriteLine("Mesaj gönderilirken bir hata meydana geldi.");
}