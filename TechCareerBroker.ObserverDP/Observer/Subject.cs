namespace TechCareerBroker.ObserverDP.Observer
{
    internal class Subject
    {
        private List<IUserObserver> observerList;

        public Subject()
        {
            observerList = new List<IUserObserver>();
        }


        public void Register(IUserObserver observer)
        {
            observerList.Add(observer);
        }

        public void Unregister(IUserObserver observer)
        {
            observerList.Remove(observer);
        }

        public void UserNotify(User user)
        {
            foreach (var userObserver in observerList)
            {
                userObserver.UserCreatedEvent(user);
            }
        }
    }
}