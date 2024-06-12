using MassTransit;
using Microservice2.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserCreatedEventConsumer>();
    x.AddConsumer<UserCreatedEventConsumer2>();
    x.UsingRabbitMq((context, config) =>
    {
        config.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(4)));


        config.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15),
            TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(60)));
        config.UseInMemoryOutbox(context);

        config.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.ReceiveEndpoint("microservice2.user.created.event.queue",
            e => { e.ConfigureConsumer<UserCreatedEventConsumer>(context); });

        config.ReceiveEndpoint("microservice2.user.created.event.queue2",
            e => { e.ConfigureConsumer<UserCreatedEventConsumer2>(context); });
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();