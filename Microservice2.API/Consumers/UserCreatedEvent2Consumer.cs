using MassTransit;
using Shared.ServiceBus;

namespace Microservice2.API.Consumers
{
    public class UserCreatedEventConsumer2(IPublishEndpoint publishEndpoint) : IConsumer<UserCreatedEvent>
    {
        public Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            // db
            // send event

            // db

            // send event
            // db


            //stock

            //


            Console.WriteLine("UserCreatedEventConsumer2 çalıştı");

            throw new Exception("hata var");
            Console.WriteLine($"Gelen Mesaj:{context.Message.Email}");

            return Task.CompletedTask;
        }
    }
}