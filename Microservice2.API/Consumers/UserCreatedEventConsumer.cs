using MassTransit;
using Shared.ServiceBus;

namespace Microservice2.API.Consumers
{
    public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
    {
        public Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            Console.WriteLine($"Gelen Mesaj:{context.Message.Email}");

            return Task.CompletedTask;
        }
    }
}