using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ServiceBus;

namespace Microservice1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IPublishEndpoint _publishEndpoint;
        private ISendEndpointProvider _sendEndpointProvider;

        public UsersController(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
        {
            _publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
        }


        [HttpPost]
        public async Task<IActionResult> Create()
        {
            // db operations
            var userCreatedEvent = new UserCreatedEvent()
                { Id = 10, Email = "ahmet@outlook.com", UserName = "ahmet16" };

            CancellationTokenSource cts = new CancellationTokenSource(30000);

            await _publishEndpoint.Publish(userCreatedEvent, pipe => { pipe.SetAwaitAck(true); }, cts.Token);

            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Create2()
        {
            // db operations
            var userCreatedEvent = new UserCreatedEvent()
                { Id = 10, Email = "mehmet@outlook.com", UserName = "mehmet16" };


            var sendEndpoint =
                await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:microservice2.user.created.event.queue2"));


            CancellationTokenSource cts = new CancellationTokenSource(30000);

            await sendEndpoint.Send(userCreatedEvent, pipe => { pipe.SetAwaitAck(true); }, cts.Token);

            return Ok();
        }
    }
}