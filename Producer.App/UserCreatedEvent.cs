using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer.App
{
    internal class UserCreatedEvent5
    {
        public int Id { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }

        public UserCreatedEvent5()
        {
            var a = new UserCreatedEvent5() { Email = "a" };
            var b = new UserCreatedEvent3();
        }

        //create constructor with parameters
    }


    internal class UserCreatedEvent
    {
        public int Id { get; init; }
        public string UserName { get; init; }
        public int Email { get; init; }


        //create constructor with parameters
        public UserCreatedEvent(int id, string userName, int email)
        {
            Id = id;
            UserName = userName;
            Email = email;
        }
    }

    internal class UserCreatedEvent3
    {
        public int Id { get; }
        public string UserName { get; }
        public int Email { get; }


        //create constructor with parameters
        //public UserCreatedEvent3(int id, string userName, int email)
        //{
        //    Id = id;
        //    UserName = userName;
        //    Email = email;
        //}
    }

    internal record UserCreatedEvent2(int Id, string UserName, string Email);
}