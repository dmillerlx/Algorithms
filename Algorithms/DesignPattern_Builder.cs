using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Algorithms
{
    class DesignPattern_Builder
    {
        public class BuilderDemo01
        {

            //Builder pattern

            //This pattern hides the process of construction
            //It creates a complex object step by step

            //In this example, we have a complicated set of classes
            //that is created using the MealBuilder


            #region Complicated Object to create

            //1 - Packing and food item interfaces
            public interface Item
            {
                string name();
                Packing packing();
                float price();
            }

            public interface Packing
            {
                string pack();
            }

            //2 - Concrete packaging classes
            public class Wrapper : Packing
            {
                public string pack()
                {
                    return "Wrapper";
                }
            }

            public class Bottle : Packing
            {
                public string pack()
                {
                    return "Bottle";
                }
            }

            //3 - Abstract classes (Burger and ColdDrink) implementing item interface to provide default functionality
            public abstract class Burger : Item
            {
                public Packing packing()
                {
                    return new Wrapper();
                }

                public abstract string name();

                public abstract float price();
            }

            public abstract class ColdDrink : Item
            {
                public Packing packing()
                {
                    return new Bottle();
                }

                public abstract string name();

                public abstract float price();
            }


            //Step 4 - create concrete classes extending burger and coldDrink
            public class VegBurger : Burger
            {
                override public float price()
                {
                    return 25;
                }

                override public string name()
                {
                    return "Veg Burger";
                }
            }

            public class ChickenBurger : Burger
            {
                override public float price()
                {
                    return (float)50.5;
                }

                override public string name()
                {
                    return "Checken Burger";
                }
            }

            public class Coke : ColdDrink
            {
                override public float price()
                {
                    return 30;
                }

                override public string name()
                {
                    return "Coke";
                }
            }

            public class Pepsi : ColdDrink
            {
                override public float price()
                {
                    return 35;
                }

                override public string name()
                {
                    return "Pepsi";
                }

            }


            //Step 5 - create meal class having item objects

            public class Meal
            {
                private List<Item> items = new List<Item>();

                public void addItem(Item item)
                {
                    items.Add(item);
                }

                public float getCost()
                {
                    float cost = 0;
                    foreach (Item i in items)
                    {
                        cost += i.price();
                    }

                    return cost;
                }

                public void showItems()
                {
                    foreach (Item i in items)
                    {
                        Debug.Write("Item: " + i.name());
                        Debug.Write(", Packing: " + i.packing().pack());
                        Debug.WriteLine(", Price: " + i.price());
                    }
                }

            }

            #endregion

            //
            //Step 6 - create MealBuilder class
            //
            //  This is the actual builder class which creates a complex object in a specific order
            //  using a specific algorithm

            public class MealBuilder
            {
                public Meal prepareVegMeal()
                {
                    Meal meal = new Meal();
                    meal.addItem(new VegBurger());
                    meal.addItem(new Coke());
                    return meal;
                }

                public Meal prepareNonVegMeal()
                {
                    Meal meal = new Meal();
                    meal.addItem(new ChickenBurger());
                    meal.addItem(new Pepsi());
                    return meal;
                }

            }


            //Step 7 - Demo

            public void RunDemo()
            {
                MealBuilder mealBuilder = new MealBuilder();

                Meal vegMeal = mealBuilder.prepareVegMeal();
                Debug.WriteLine("Veg Meal");
                vegMeal.showItems();
                Debug.WriteLine("Total Cost: " + vegMeal.getCost());

                Meal nonVegMeal = mealBuilder.prepareNonVegMeal();
                Debug.WriteLine("\n\nNon-Veg Meal");
                nonVegMeal.showItems();
                Debug.WriteLine("Total Cost: " + nonVegMeal.getCost());
            }

        }

        public class BuilderDemo02
        {
            //Product we want to create
            class RobotProduct
            {
                public string RobotData { get; set; }
            }

            //
            //Abstract class or interface for all builders
            public abstract class RobotBuilder
            {
                public abstract void CreateArm();
                public abstract void CreateHead();
                public abstract void CreateLegs();
                public abstract void CreateTorso();
            }

            //Concrete classes that implement the interface
            //These classes have the small steps to create the robot but do not call them
            //They are called by the director

            class SmallRobotBuilder : RobotBuilder
            {
                public RobotProduct completedRobot = new RobotProduct();
                public RobotProduct GetRobot() { return completedRobot; }
                
                public override void CreateArm() { completedRobot.RobotData += "SmallArm "; }
                public override void CreateHead() { completedRobot.RobotData += "SmallHead "; }
                public override void CreateLegs() { completedRobot.RobotData += "SmallLegs "; }
                public override void CreateTorso() { completedRobot.RobotData += "SmallTorso "; }
            }
            class LargeRobotBuilder : RobotBuilder
            {
                public RobotProduct completedRobot;
                public RobotProduct GetRobot() { return completedRobot; }

                public override void CreateArm() { completedRobot.RobotData += "LargeArm "; }
                public override void CreateHead() { completedRobot.RobotData += "LargeHead "; }
                public override void CreateLegs() { completedRobot.RobotData += "LargeLegs "; }
                public override void CreateTorso() { completedRobot.RobotData += "LargeTorso "; }
            }



            //Director receives the builder from the client via the constructor
            //Director calls the builder methods
            class RobotDirector
            {
                RobotBuilder myBuilder;

                public RobotDirector(RobotBuilder robotBuilder)
				{
                    myBuilder = robotBuilder;
				}

                public void MakeRobot()
				{
					myBuilder.CreateArm();
					myBuilder.CreateLegs();
					myBuilder.CreateHead();
					myBuilder.CreateTorso();
				}
            }

            //Client creates the builder, passes to the director, requests the object, then gets the object from the builder
            public void Run()
            {
                //Create the builder
                SmallRobotBuilder smallBuilder = new SmallRobotBuilder();

                //Pass the builder to the director via the constructor
                RobotDirector director = new RobotDirector(smallBuilder);

                //Request to make the object
                director.MakeRobot();

                //Finally get the finished product from the builder
                RobotProduct myRobot = smallBuilder.GetRobot();

                Console.WriteLine(myRobot.RobotData);

            }

        }

    }
}
