using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Factory
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
                Console.WriteLine("Draw Rectangle");
            }
        }
        public class Square : Shape
        {
            void Shape.draw()
            {
                Console.WriteLine("Draw Square");
            }
        }
        public class Circle : Shape
        {
            void Shape.draw()
            {
                Console.WriteLine("Draw Circle");
            }
        }

        #endregion

        //Shape factory returns requested object
        public class ShapeFactory
        {
            public Shape getShape(string shape)
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


        public void Run()
        {
            ShapeFactory shapeFactory = new ShapeFactory();

            Shape s1 = shapeFactory.getShape("square");
            s1.draw();

            Shape s2 = shapeFactory.getShape("circle");
            s2.draw();

        }

    }
}
