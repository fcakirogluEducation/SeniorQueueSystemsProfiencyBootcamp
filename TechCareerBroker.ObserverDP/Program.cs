// See https://aka.ms/new-console-template for more information

using TechCareerBroker.ObserverDP;
using TechCareerBroker.ObserverDP.Observer;
using TechCareerBroker.ObserverDP.Services;

Console.WriteLine("Observer Design Pattern");

var newUser = new User() { Email = "ahmet@outlook.com", Id = 1, Phone = "555 555 55 55" };


var subject = new Subject();

var userType = "manager";
if (userType == "manager")
{
    subject.Register(new EmailService());
    subject.Register(new SmsService());
    subject.Register(new DiscountService());
}
else
{
    subject.Register(new EmailService());
    subject.Register(new SmsService());
}


var userService = new UserService(subject);

userService.Create(newUser);