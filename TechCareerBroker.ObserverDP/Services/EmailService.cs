using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareerBroker.ObserverDP.Observer;

namespace TechCareerBroker.ObserverDP.Services
{
    internal class EmailService : IUserObserver
    {
        public void UserCreatedEvent(User user)
        {
            Console.WriteLine($"Email gönderildi.{user.Email}");
        }
    }
}