using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_State
    {

        //Abstract base class that also defines the basic functions
        //for Deposit and Withdraw.  These functions are virtual so they
        //can have a body inside the abstract class

        //Also contains an instance of the Context object
        //The state transitions are done inside 'CheckState' which is
        //defined for each state
        public abstract class State
        {
            protected Context context;

            public abstract void showState();

            public virtual void Deposit(int value)
            {
                Console.WriteLine("Depositing: " + value);
                context.Balance += value;
                Console.WriteLine("  Balance: " + context.Balance);
                CheckState();
            }
            public virtual void Withdraw(int value)
            {
                Console.WriteLine("Withdrawing: " + value);
                context.Balance -= value;
                Console.WriteLine("  Balance: " + context.Balance);
                CheckState();
            }
            public abstract void CheckState();

        }

        //RedState indicates the balance is in the negative
        //Context object is passed into the Constructor
        //
        //Deposit and Withdraw are implemented in the base class
        //so no need to implement here unless there are some state
        //specific changes
        //
        //CheckState has the state transitions
        //  If the balance stays < 0, keep in current state
        //  If the balance is between 0 and 999, change to SilverState
        //  If the balance is >= 1000 change to GoldState
        public class RedState : State
        {
            public RedState(Context newContext)
            {
                context = newContext;
                showState();
            }

            public override void showState()
            {
                Console.WriteLine("State = RedState");
            }

            public override void CheckState()
            {
                if (context.Balance < 0)
                    return;
                if (context.Balance >= 0 && context.Balance < 1000)
                    context.State = new SilverState(context);
                if (context.Balance >= 1000)
                    context.State = new GoldState(context);
            }
        }

        public class SilverState : State
        {
            public SilverState(Context newContext)
            {
                context = newContext;
                showState();
            }

            public override void showState()
            {
                Console.WriteLine("State = SilverState");
            }           

            public override void CheckState()
            {
                if (context.Balance >= 0 && context.Balance < 1000)
                    return;
                if (context.Balance < 0)
                    context.State = new RedState(context);
                if (context.Balance >= 1000)
                    context.State = new GoldState(context);
            }
        }

        public class GoldState : State
        {
             public GoldState(Context newContext)
            {
                context = newContext;
                showState();
            }

            public override void showState()
            {
                Console.WriteLine("State = GoldState");
            }

            public override void CheckState()
            {
                if (context.Balance >= 1000)
                    return;
                if (context.Balance < 0)
                    context.State = new RedState(context);
                if (context.Balance >= 0 && context.Balance < 1000)
                    context.State = new SilverState(context);
            }
        }


        public class Context
        {
            public State State { get; set; }
            public int Balance {get;set;}
            string Owner;

            public Context(string Owner)
            {
                this.Owner = Owner;
                Console.WriteLine("Account opened for " + Owner);
                State = new SilverState(this);
            }

            public void Withdraw(int value)
            {
                State.Withdraw(value);
            }

            public void Deposit(int value)
            {
                State.Deposit(value);
            }
        }

        //Create an account and deposit and withdraw values
        public void Run()
        {
            Context Account1 = new Context("Joe");

            Account1.Deposit(100);
            Account1.Deposit(500);
            Account1.Deposit(300);
            Account1.Deposit(400);
            Account1.Deposit(3000);
            Account1.Withdraw(500);
            Account1.Withdraw(500);
            Account1.Withdraw(500);
            Account1.Withdraw(5000);
            Account1.Withdraw(500);
            Account1.Withdraw(500);
            Account1.Withdraw(500);

        }



    }
}
