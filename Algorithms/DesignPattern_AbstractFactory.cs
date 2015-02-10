using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace Algorithms
{
    //Abstract Factory is a factory of factories
    //So it is used to really create or factories that return the objects

    //Implementation from here:
    //http://www.tutorialspoint.com/design_pattern/abstract_factory_pattern.htm


    public class DesignPattern_AbstractFactory
    {

        #region Shape Interface + Shape Concrete Classes

        //Shape interface
        public interface Shape
        {
            void draw();
        }

        //Concrete shape classes
        public class Rectangle : Shape
        {
            void Shape.draw()
            {
                Debug.WriteLine("Draw Rectangle");
            }
        }
        public class Square : Shape
        {
            void Shape.draw()
            {
                Debug.WriteLine("Draw Square");
            }
        }
        public class Circle : Shape
        {
            void Shape.draw()
            {
                Debug.WriteLine("Draw Circle");
            }
        }

        #endregion

        #region Color Interface + Color Concrete Classes
        //Color interface
        public interface Color
        {
            void fill();
        }

        //Color concrete classes
        public class Red : Color
        {
            public void fill()
            {
                Debug.WriteLine("Fill Red");
            }
        }
        public class Green : Color
        {
            public void fill()
            {
                Debug.WriteLine("Fill Green");
            }
        }
        public class Blue : Color
        {
            public void fill()
            {
                Debug.WriteLine("Fill Blue");
            }
        }

        #endregion

        #region Shape and Color factories implement AbstractFactory so they can both be used from the Factory Producer

        //
        //This is what makes it an abstract factory
        //
        //A normal factory (non-abstract) would just create the Shape or Color (as below in ShapeFactory and ColorFactory)
        //But an abstract factory extends an abstract class that has all the types of factories that can be created
        //

        //Abstract class to get factories for Color and Shape Objects
        public abstract class AbstractFactory
        {
            abstract public Color getColor(string color);
            abstract public Shape getShape(string shape);
        }


        //
        //Concrete Factory classes that create Shape and Color classes
        //
        
        //Create factories by extending the AbstractFactory
        public class ShapeFactory : AbstractFactory
        {
            //Only implementing the getShape, so getColor can return null
            public override Color getColor(string color)
            {
                return null;
            }

            public override Shape getShape(string shape)
            {
                if (string.IsNullOrEmpty(shape))
                    return null;

                switch (shape.ToUpper())
                {
                    case "CIRCLE": return new Circle();
                    case "RECTANGLE": return new Rectangle();
                    case "SQUARE": return new Square();
                }

                return null;
            }
        }

        //Create color factory, implementing the Abstract Factory
        public class ColorFactory : AbstractFactory
        {
            public override Color getColor(string color)
            {
                if (string.IsNullOrEmpty(color))
                    return null;

                switch (color.ToUpper())
                {
                    case "RED": return new Red();
                    case "BLUE": return new Blue();
                    case "GREEN": return new Green();
                }

                return null;
            }

            public override Shape getShape(string shape)
            {
                return null;
            }
        }

        #endregion


        //Finally create generator/producer class to get the factories by passing in the type of factory requested
        //This class creates the factory for the type of object requested
        //
        //
        //                  *** This is a factory that creates factories ***
        //
        //
        public class FactoryProducer
        {
            public static AbstractFactory getFactory(string choice)
            {
                if (string.IsNullOrEmpty(choice))
                    return null;
                switch (choice.ToUpper())
                {
                    case "SHAPE": return new ShapeFactory();
                    case "COLOR": return new ColorFactory();
                }

                return null;
            }
        }


        public void RunDemo()
        {
            Debug.WriteLine("Abstract Factory Demo");
            Debug.WriteLine("--------------------------------------------");

            //Get shape factory
            AbstractFactory shapeFactory = FactoryProducer.getFactory("shape");

            //Now use factory to get shapes and do draw
            Shape s1 = shapeFactory.getShape("circle");
            s1.draw();

            Shape s2 = shapeFactory.getShape("rectangle");
            s2.draw();

            Shape s3 = shapeFactory.getShape("square");
            s3.draw();



            //Get color factory
            AbstractFactory colorFactory = FactoryProducer.getFactory("color");

            //Now use factory to get colors and fill
            Color c1 = colorFactory.getColor("red");
            c1.fill();

            Color c2 = colorFactory.getColor("green");
            c2.fill();

            Color c3 = colorFactory.getColor("blue");
            c3.fill();


        }



    }
}
