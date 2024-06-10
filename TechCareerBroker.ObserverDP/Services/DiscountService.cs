using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerBroker.ObserverDP.Observer;

namespace TechCareerBroker.ObserverDP.Services
{
    internal class DiscountService : IUserObserver
    {
        public void UserCreatedEvent(User user)
        {
            Console.WriteLine($"İndirim tanımlandı.({user.Email})");
        }
    }
}