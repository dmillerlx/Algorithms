using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Template
    {

        //Abstract class defines the basic algorithm
		//Hooks or place holders are inserted to allow subclasses to alter
		//the algorithm behaviour.
			
		//For this example, a Hoagie shop is making sandwiches
		//The basic algorithm is defined in makeSandwich()
		//Subclasses can
		//		- Enable/disable certain features of the algorithm, like adding meat or cheese
		//			Subclass overrides CustomerWantsMeat()
		//		- Define the behaviour of each algorithm function, like AddMeat
		//
		//The test app will call makeSandwich(), which will run the algorithm and
		//the subclasses will define what goes onto the sandwich, but will not change
		//the structure, so it will always have a bun (CutBun()) and be wrapped (WrapTheHoagie()).


        public abstract class Hoagie
        {

            public virtual bool CustomerWantsMeat() { return true; }
            public virtual bool CustomerWantsVeggies() { return true; }
            public virtual bool CustomerWantsCheese() { return true; }
            public virtual bool CustomerWantsCondiments() { return true; }
            

            public void makeSandwich()
            {
                CutBun();

                if (CustomerWantsMeat())
                    AddMeat();

                if (CustomerWantsCheese())
                    AddCheese();

                if (CustomerWantsVeggies())
                    AddVeggies();

                if (CustomerWantsCondiments())
                    AddCondiments();

                WrapTheHoagie();
            }

            public abstract void AddMeat();
            public abstract void AddCheese();
            public abstract void AddVeggies();
            public abstract void AddCondiments();

            public void CutBun()
            {
                Console.WriteLine("Bun is cut");
            }

            public void WrapTheHoagie()
            {
                Console.WriteLine("Hoagie is wrapped");
            }

        }


        public class ItalianHoagie : Hoagie
        {
            string[] Meat = { "Peperoni", "Salami" };
            string[] Cheese = { "Provolone", "Swiss" };
            string[] Veggies = { "Mushrooms", "Bell Peppers" };
            string[] Condiments = { "Salt", "Pepper", "Oil", "Vinegar" };
            
            public override void AddMeat()
            {
                Console.WriteLine("Adding meat:");

                foreach(string s in Meat)
                {
                    Console.WriteLine("  " + s);
                }
            }

            public override void AddCheese()
            {
                Console.WriteLine("Adding Cheese:");

                foreach (string s in Cheese)
                {
                    Console.WriteLine("  " + s);
                }
            }

            public override void AddVeggies()
            {
                Console.WriteLine("Adding Veggies:");

                foreach (string s in Veggies)
                {
                    Console.WriteLine("  " + s);
                }
            }

            public override void AddCondiments()
            {
                Console.WriteLine("Adding Condiments:");

                foreach (string s in Condiments)
                {
                    Console.WriteLine("  " + s);
                }
            }
        }

        //Concrete classes implement the features of the template
        //This class overrides the algorithm functions for
        //	AddVeggies() and AddCondiments() - Adds those items
        //	AddMeat() and AddCheese() - these do nothing since it is a veggie hoagie

        //This class also changes the algorithm using the hooks to disable
        //the adding of Cheese and Meat.  This is done by overriding
        //CustomerWantsCheese() and CustomerWantsMeat()
        public class VeggieHoagie : Hoagie
        {
            string[] Veggies = { "Mushrooms", "Bell Peppers" };
            string[] Condiments = { "Salt", "Pepper", "Oil", "Vinegar" };

            public override bool CustomerWantsCheese() { return false; }
            public override bool CustomerWantsMeat() { return false; }

            public override void AddMeat()
            {
                //Do Nothing
            }

            public override void AddCheese()
            {
                //Do Nothing
            }

            public override void AddVeggies()
            {
                Console.WriteLine("Adding Veggies:");

                foreach (string s in Veggies)
                {
                    Console.WriteLine("  " + s);
                }
            }

            public override void AddCondiments()
            {
                Console.WriteLine("Adding Condiments:");

                foreach (string s in Condiments)
                {
                    Console.WriteLine("  " + s);
                }
            }

           
        }


        public void Run()
        {
            Hoagie customer12 = new ItalianHoagie();
            customer12.makeSandwich();

            Hoagie customer13 = new VeggieHoagie();
            customer13.makeSandwich();

        }


    }
}
