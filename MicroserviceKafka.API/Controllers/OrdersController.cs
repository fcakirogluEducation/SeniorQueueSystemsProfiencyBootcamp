using MicroserviceKafka.API.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceKafka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(KafkaBus kafkabus) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var orderCreatedEvent = new OrderCreatedEvent() { Code = "10", TotalPrice = 300.00m };


            var result = await kafkabus.Send(orderCreatedEvent);

            return Ok(result);
        }
    }
}