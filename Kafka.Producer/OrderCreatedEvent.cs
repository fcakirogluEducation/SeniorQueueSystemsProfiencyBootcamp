using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Producer
{
    internal class OrderCreatedEvent
    {
        public string Code { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}