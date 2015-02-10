using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Algorithms
{
    abstract public class Animal
    {
        public abstract int weight {get;set;}               
    }

    public class Mamal : Animal
    {
        public override int weight {get;set;}
    }

    interface ISound
    {
        //public static readonly int octive = 1;
        void makeSound();
    }


    class Dog : Mamal, ISound
    {        
        public void makeSound()
        {
            Debug.WriteLine("Bark weight:" + weight);// octive: "+octive);
        }
    }

    class Cat : Mamal, ISound
    {

        public void makeSound()
        {
            Debug.WriteLine("Meow weight:" + weight);
        }
    }

    class Bird : Animal, ISound
    {
        public override int weight { get; set; }

        public void makeSound()
        {
            Debug.WriteLine("Chirp weight:"+weight);
        }
    }

    public class InterfaceTest
    {
        public InterfaceTest() { }

        public void Run()
        {

            List<ISound> soundList = new List<ISound>();

            soundList.Add(new Bird());
            soundList.Add(new Dog());
            soundList.Add(new Cat());

            foreach (ISound s in soundList)
            {
                s.makeSound();
            }          

        }

    }

}
