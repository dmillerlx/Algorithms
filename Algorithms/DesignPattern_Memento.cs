using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Memento
    {
        //Originator stores the current state        
        //It also can create a memento object of the current state
        public class Originator
        {
            public string state;

            public Originator(string initalState)
            {
                state = initalState;
                Console.WriteLine("Initial state: " + state);
            }

            public Memento getMemento()
            {
                Console.WriteLine("Creating memento: " + state);
                Memento m = new Memento(state);
                return m;
            }

            public void restoreFromMemento(Memento m)
            {
                state = m.GetState();
                Console.WriteLine("Restored from memento: " + state);
            }

            public void SetState(string val)
            {
                state = val;
                Console.WriteLine("Set state: " + state);
            }            
            
        }
        
        //Memento is an object that stores the state of another object        
        public class Memento
        {
            public string value;
            public Memento(string newValue)
            {
                value = newValue;
            }

            public string GetState()
            {
                return value;
            }
        }

        //CareTaker stores the mementos and allows for retrieval of the mementos at a later time
        public class CareTaker
        {
            List<Memento> mementos = new List<Memento>();

            public int Count { get { return mementos.Count(); } }
            

            public CareTaker()
            {

            }

            public void AddMemento(Memento m)
            {
                mementos.Add(m);
                Console.WriteLine("Caretaker - Saving Memento.  Items saved: " + mementos.Count);
            }

            public Memento GetMemento(int index)
            {
                if (index > Count || Count <= 0)
                {
                    return null;
                }

                return mementos[index];
            }
        }


        public void Run()
        {
            int currentIndex = 0;
            CareTaker careTaker = new CareTaker();

            //Original string: I walked
            Originator originator = new Originator("I walked");

            //Store memento of object
            careTaker.AddMemento(originator.getMemento());

            //Change to: I walked to the store
            originator.SetState("I walked to the store");
            careTaker.AddMemento(originator.getMemento());

            //Change to: I walked to the store to get some food
            originator.SetState("I walked to the store to get some food");
            careTaker.AddMemento(originator.getMemento());

            //Now lets store off the current position
            currentIndex = careTaker.Count - 1;

            //Now lets undo
            //Change to: I walked to the store
            originator.restoreFromMemento(careTaker.GetMemento(currentIndex - 1));

            //Now lets redo
            //Change to: I walked to the store to get some food
            originator.restoreFromMemento(careTaker.GetMemento(currentIndex));

        }
    }
}
