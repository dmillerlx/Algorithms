using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Decorator
    {

        public abstract class IceCream
        {
            public abstract double Cost();
        }

        //Concrete classes
        public class Chocolate : IceCream
        {
            public override double Cost()
            {
                return 1.50;
            }
        }

        public class Vanilla : IceCream
        {
            public override double Cost()
            {
                return 1.0;
            }
        }

        //
        //Decorator  (toppings) - inherit IceCream		
        public abstract class Topping : IceCream
        {
            //Topping holds an instance to the next item in
            //so each concrete decoration will walk up the stack
            protected IceCream IceCream { get; set; }

            protected Topping(IceCream s)
            {
                IceCream = s;
            }
        }

        //
        //Concrete examples of toppings		
        public class Fudge : Topping
        {
            public Fudge(IceCream s)
                : base(s)
            {
            }

            public override double Cost()
            {
                return 0.50 + IceCream.Cost();
            }
        }

        public class Sprinkle : Topping
        {
            public Sprinkle(IceCream s)
                : base(s)
            {
            }

            public override double Cost()
            {
                return 0.25 + IceCream.Cost();
            }
        }


        /////////////Main
        public void Run()
		{
			//Create IceCream
			IceCream iceCream = new Chocolate();
			
			//Now decorate it
			
			iceCream = new Sprinkle(iceCream);
			iceCream = new Fudge(iceCream);
			
			//Print cost			
			Console.WriteLine(iceCream.Cost());
		
			
            //When you call cost on iceCream it calls
            //Fudge.Cost() which calls Sprinkle.Cost()
            //which calls Chocolate.Cost()
			
            //-------------------------------------
            //|Fudge								|
            //|									|
            //|	-------------------------		|	
            //|	|Sprinkle 				|		|
            //|	|		-------------	|		|			
            //|	|	    |Chocolate  |	|		|
            //|	|		-------------	|		|			
            //|	|						|		|
            //|	|-----------------------|		|
            //|									|
            //------------------------------------|
		
		}


    }
}
