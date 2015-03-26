using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Observer
    {
        //Observer part
        public interface ISubscriber
        {
            void Notify();
        }

        public class User : ISubscriber
        {
            public void Notify()
            {
                Console.WriteLine("User has been updated about new video");
            }
        }

        public class Moderator : ISubscriber
        {
            public void Notify()
            {
                Console.WriteLine("Moderator needs to review new video");
            }
        }


        //Subject part
        //3 methods required
        //  Method to subscribe
        //  Method to unsubscribe
        //  Method to notify
        public class YouTubeChannel
        {
            private List<ISubscriber> subscribers = new List<ISubscriber>();

            public void Subscribe(ISubscriber s)
            {
                subscribers.Add(s);
            }

            public void UnSubscribe(ISubscriber s)
            {
                subscribers.Remove(s);
            }

            public void NotifySubscribers()
            {
                foreach (ISubscriber s in subscribers)
                {
                    s.Notify();
                }
            }
        }

        public void Run()
        {
            //Create instance of object
            YouTubeChannel test = new YouTubeChannel();

            //Create users
            ISubscriber Joey = new User();
            ISubscriber Gai = new Moderator();

            //Subscribe
            test.Subscribe(Joey);
            test.Subscribe(Gai);

            //Notify
            test.NotifySubscribers();

            //UnSubscribe
            test.UnSubscribe(Gai);

            //Notify
            test.NotifySubscribers();

        }


    }
}
