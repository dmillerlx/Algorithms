using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Facade
    {
        //////////////////////////
		//Class encapsulates complicated functions
		
        public enum Dough {Wheat, White, Rye};
        public enum Sauce {Red, SpicyRed};
        public enum Topping {Pepporoni, Mushrooms};
        public enum Cheese {Mozerlla, None};

		public class PizzaFacade
		{
			private Dough dough;
			private Sauce sauce;
			private Topping top;
			private Cheese cheese;

            public string Pizza {get;set;}

			public PizzaFacade(Dough dough, Sauce sauce, Topping top, Cheese cheese)
			{
				this.dough = dough;
				this.sauce = sauce;
				this.top = top;
				this.cheese = cheese;
			}


            public void MakePizza()
            {
                List<string> pizza = new List<string>();

                pizza.Add(dough.ToString());
                pizza.Add(sauce.ToString()); 
                pizza.Add(top.ToString()); 
                pizza.Add(cheese.ToString());

                Pizza = string.Empty;
                foreach (string s in pizza)
                {
                    if (Pizza.Length > 0)
                        Pizza += " ";
                    Pizza += s;
                }
            }
		}
		
		/////////////////Main
		public void Run()
		{
            client1();
            client2();
			
		}
		
		void client1()
		{
			PizzaFacade pf = new PizzaFacade(Dough.Wheat, Sauce.Red, Topping.Mushrooms, Cheese.Mozerlla);
			
			pf.MakePizza();

            Console.WriteLine(pf.Pizza);
        }

		void client2()
		{
            PizzaFacade pf = new PizzaFacade(Dough.White, Sauce.Red, Topping.Pepporoni, Cheese.None);
			
			pf.MakePizza();

            Console.WriteLine(pf.Pizza);
		}


    }
}
