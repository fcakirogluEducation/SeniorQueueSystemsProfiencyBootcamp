using TechCareerBroker.ObserverDP.Observer;

namespace TechCareerBroker.ObserverDP
{
    internal class UserService
    {
        private Subject _subject;

        public UserService(Subject subject)
        {
            _subject = subject;
        }

        public void Create(User user)
        {
            // db insert

            // notify
            _subject.UserNotify(user);
        }
    }
}