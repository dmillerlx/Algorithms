using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Mediator
    {

        //Common interface for communicating between objects
        public interface Mediator
        {
            void Register(Colleague col);
            void Send(string message, string from, string to);
        }

        //Concrete implementation that maintains a list of colleagues
        //and routes the messages
        public class MediatorConcrete : Mediator
        {
            List<Colleague> users = new List<Colleague>();
            public void Register(Colleague col)
            {
                users.Add(col);
            }

            public void Send(string message, string from, string to)
            {
                foreach (Colleague c in users)
                {
                    if (c.name == to)
                        c.Recieve(message, from);
                }
            }
        }

        //Base abstract class to implement for all Colleague's
        public abstract class Colleague        
        {
            public string name { get; set; }
            abstract public void Send(string value, string to);
            abstract public void Recieve(string value, string from);
        }

        //Concrete class for colleague
        public class HPColleague : Colleague
        {
            Mediator mediator;

            public HPColleague(string newName, Mediator newMediator)
            {
                name = newName;
                mediator = newMediator;
            }

            override public void Send(string value, string to)
            {
                mediator.Send(value, name, to);
            }

            override public void Recieve(string value, string from)
            {
                Console.WriteLine("HPColleague " + name + " Recieved from " + from + "> " + value);
            }
        }

        //Concrete class for colleague
        public class EDSColleague : Colleague
        {
            Mediator mediator;

            public EDSColleague(string newName, Mediator newMediator)
            {
                name = newName;
                mediator = newMediator;
            }

            override public void Send(string value, string to)
            {
                mediator.Send(value, name, to);
            }

            override public void Recieve(string value, string from)
            {
                Console.WriteLine("EDSColleague " + name + " Recieved from " + from + "> " + value);
            }
        }

        public void Run()
        {
            //Create concrete mediator object
            MediatorConcrete mediator = new MediatorConcrete();

            //Create colleague's and pass in mediator
            HPColleague col1 = new HPColleague("Dave", mediator);
            HPColleague col2 = new HPColleague("John", mediator);
            HPColleague col3 = new HPColleague("Jimmy", mediator);

            EDSColleague col4 = new EDSColleague("Alex", mediator);
            EDSColleague col5 = new EDSColleague("Carolyn", mediator);
            EDSColleague col6 = new EDSColleague("Jeff", mediator);

            //Register colleague's with mediator.  This could be done
            //in the Collegague constructor also
            mediator.Register(col1);
            mediator.Register(col2);
            mediator.Register(col3);
            mediator.Register(col4);
            mediator.Register(col5);
            mediator.Register(col6);

            //Finally send message's from one Colleague to another
            //and let the Mediator do the routing
            col1.Send("Hello dude", "John");
            col2.Send("Hi, how are you?", "Alex");
            col5.Send("I am doing great!", "Dave");

        }


    }
}
