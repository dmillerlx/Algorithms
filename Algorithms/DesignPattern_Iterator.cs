using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Iterator
    {
        //
        //Iterator is used to transverse a collection
		//
		//Common interface to access different collection of objects
        //		
		//Define a common interface so different collection types can be iterated
        
        
        ////////////////////
        public interface ISocialNetworking
        {
            IIterator CreateIterator();
        }

        public class Facebook : ISocialNetworking
        {
            private LinkedList<string> Users;

            public Facebook()
            {
                Users = new LinkedList<string>();
                Users.AddLast("Mike");
                Users.AddLast("Tom");
                Users.AddLast("Jake");
            }

            public IIterator CreateIterator()
            {
                return new FacebookIterator(Users);
            }
        }

        public class Twitter : ISocialNetworking
        {
            private string[] Users;

            public Twitter()
            {
                Users = new[] { "Kathy", "Lori", "Jamie" };
            }

            public IIterator CreateIterator()
            {
                return new TwitterIterator(Users);
            }
        }

        public class FacebookIterator : IIterator
        {
            private LinkedList<string> Users;

            private int position;

            public FacebookIterator(LinkedList<string> users)
            {
                this.Users = users;
                position = 0;
            }

            public void First()
            {
                position = 0;
            }

            public string Next()
            {
                return Users.ElementAt(position++);
            }

            public bool IsDone()
            {
                return position >= Users.Count;
            }

            public string CurrentItem()
            {
                return Users.ElementAt(position);
            }
        }

        public class TwitterIterator : IIterator
        {
            private string[] Users;
            private int position;

            public TwitterIterator(string[] users)
            {
                this.Users = users;
                position = 0;
            }

            public void First()
            {
                position = 0;
            }

            public string Next()
            {
                return Users[position++];
            }

            public bool IsDone()
            {
                return position >= Users.Length;
            }

            public string CurrentItem()
            {
                return Users[position++];
            }
        }


        ////////////////////////

        //Requrired elements for iterator
        //		First, Next, IsDone, CurrentItem
        public interface IIterator
        {
            void First();
            string Next();
            bool IsDone();
            string CurrentItem();
        }

        //////////////////////

        public void Run()
        {
            ISocialNetworking fb = new Facebook();
            ISocialNetworking tw = new Twitter();

            IIterator fbIterator = fb.CreateIterator();
            IIterator twIterator = tw.CreateIterator();

            Console.WriteLine("Facebook:");
            PrintUsers(fbIterator);

            Console.WriteLine("Twitter:");
            PrintUsers(twIterator);


        }

        void PrintUsers(IIterator iterate)
        {
            while (!iterate.IsDone())
            {
                Console.WriteLine(iterate.Next());
            }
        }



    }
}
