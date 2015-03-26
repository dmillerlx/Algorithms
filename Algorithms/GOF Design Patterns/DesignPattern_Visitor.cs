using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Visitor
    {
        //Interface that defines an interface to allow the class to be visitable
        //Common function name: Accept
        public interface Visitable
        {
            double accept(Visitor visitor);
        }

        //Implements the Vistable interface and implements the Accept function
        public class Liquor : Visitable
        {
            private double price;

            public Liquor(double item)
            {
                price = item;
            }

            public double getPrice() { return price; }

            public double accept(Visitor visitor)
            {
                return visitor.visit(this);
            }
        }

        public class Tobacco : Visitable
        {
            private double price;

            public Tobacco(double item)
            {
                price = item;
            }

            public double getPrice() { return price; }

            public double accept(Visitor visitor)
            {
                return visitor.visit(this);
            }

        }

        public class Necessity : Visitable
        {
            private double price;

            public Necessity(double item)
            {
                price = item;
            }

            public double getPrice() { return price; }

            public double accept(Visitor visitor)
            {
                return visitor.visit(this);
            }

        }

        //Interface that defines the type objects that can be visited
        public interface Visitor
        {
            double visit(Liquor liquorItem);
            double visit(Tobacco tabaccoItem);
            double visit(Necessity necessityItem);
        }

        //Implements the Visitor interface for each type of class to visit
        public class TaxVisitor : Visitor
        {
            public TaxVisitor() { }

            public double visit(Liquor liquorItem)
            {
                Console.WriteLine("Liquor Item: Price with Tax");
                return liquorItem.getPrice() * 0.18 + liquorItem.getPrice();
            }

            public double visit(Tobacco tabaccoItem)
            {
                Console.WriteLine("Tobacco Item: Price with Tax");
                return tabaccoItem.getPrice() * 0.45 + tabaccoItem.getPrice();
            }

            public double visit(Necessity necessityItem)
            {
                Console.WriteLine("Necessity Item: Price with Tax");
                return necessityItem.getPrice() * 0 + necessityItem.getPrice();
            }
        }

        public class TaxHolidayVisitor : Visitor
        {
            public TaxHolidayVisitor() { }

            public double visit(Liquor liquorItem)
            {
                Console.WriteLine("Liquor Item: Price with Tax");
                return liquorItem.getPrice() * 0.30 + liquorItem.getPrice();
            }

            public double visit(Tobacco tabaccoItem)
            {
                Console.WriteLine("Tobacco Item: Price with Tax");
                return tabaccoItem.getPrice() * 0.1 + tabaccoItem.getPrice();
            }

            public double visit(Necessity necessityItem)
            {
                Console.WriteLine("Necessity Item: Price with Tax");
                return necessityItem.getPrice() * 0 + necessityItem.getPrice();
            }
        }

        public void Run()
        {
            //Create visitor objects.  For this example we are making
            //two types of tax visitors.  Each will apply a different amount
            //of tax on products
            TaxVisitor taxcalc = new TaxVisitor();
            TaxHolidayVisitor taxHolidayCalc = new TaxHolidayVisitor();

            Console.WriteLine("*******************************");
            Console.WriteLine("Visitor Pattern");
            Console.WriteLine("*******************************");
            Console.WriteLine();
            Console.WriteLine("Creating items");

            //Create the items we are working with
            Necessity milk = new Necessity(3.47);
            Liquor vodka = new Liquor(11.99);
            Tobacco cigars = new Tobacco(19.99);

            //Apply the Tax visitor
            Console.WriteLine();
            Console.WriteLine("Tax Prices");
            Console.WriteLine(milk.accept(taxcalc));
            Console.WriteLine(vodka.accept(taxcalc));
            Console.WriteLine(cigars.accept(taxcalc));

            //Apply the Tax Holiday visitor
            Console.WriteLine();
            Console.WriteLine("Tax Holiday Prices");
            Console.WriteLine(milk.accept(taxHolidayCalc));
            Console.WriteLine(vodka.accept(taxHolidayCalc));
            Console.WriteLine(cigars.accept(taxHolidayCalc));
        
        }

    }
}
