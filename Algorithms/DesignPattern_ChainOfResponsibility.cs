using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_ChainOfResponsibility
    {
        public interface Chain
        {
            void setNextChain(Chain nextChain);
            void calculate(Numbers numbers);
        }

        public class Numbers
        {
            public int number1 { get; set; }
            public int number2 { get; set; }
            public string calcWanted { get; set; }

            public Numbers(int num1, int num2, string calcWanted)
            {
                number1 = num1;
                number2 = num2;
                this.calcWanted = calcWanted;
            }

            public int result { get; set; }
            public bool error = false;
            public string errorMessage = string.Empty;
        }

        public class addNumbers : Chain
        {
            Chain nextChain;

            public void setNextChain(Chain nextChain)
            {
                this.nextChain = nextChain;
            }

            public void calculate(Numbers numbers)
            {
                if (numbers.calcWanted == "add")
                {
                    numbers.result = numbers.number1 + numbers.number2;
                }
                else
                {
                    if (nextChain != null)
                        nextChain.calculate(numbers);
                    else
                    {
                        numbers.error = true;
                        numbers.errorMessage = "Unsupported operation: " + numbers.calcWanted;
                    }
                }
            }
        }
        
        public class subNumbers : Chain
        {
            Chain nextChain;

            public void setNextChain(Chain nextChain)
            {
                this.nextChain = nextChain;
            }

            public void calculate(Numbers numbers)
            {
                if (numbers.calcWanted == "sub")
                {
                    numbers.result = numbers.number1 - numbers.number2;
                }
                else
                {
                    if (nextChain != null)
                        nextChain.calculate(numbers);
                    else
                    {
                        numbers.error = true;
                        numbers.errorMessage = "Unsupported operation: " + numbers.calcWanted;
                    }
                }
            }
        }

        public class multNumbers : Chain
        {
            Chain nextChain;

            public void setNextChain(Chain nextChain)
            {
                this.nextChain = nextChain;
            }

            public void calculate(Numbers numbers)
            {
                if (numbers.calcWanted == "mult")
                {
                    numbers.result = numbers.number1 * numbers.number2;
                }
                else
                {
                    if (nextChain != null)
                        nextChain.calculate(numbers);
                    else
                    {
                        numbers.error = true;
                        numbers.errorMessage = "Unsupported operation: " + numbers.calcWanted;
                    }
                }
            }
        }

        public class divNumbers : Chain
        {
            Chain nextChain;

            public void setNextChain(Chain nextChain)
            {
                this.nextChain = nextChain;
            }

            public void calculate(Numbers numbers)
            {
                if (numbers.calcWanted == "div")
                {
                    if (numbers.number2 == 0)
                    {
                        numbers.error = true;
                        numbers.errorMessage = "Divide by zero";
                    }
                    else
                    {
                        numbers.result = numbers.number1 / numbers.number2;
                    }
                }
                else
                {
                    if (nextChain != null)
                        nextChain.calculate(numbers);
                    else
                    {
                        numbers.error = true;
                        numbers.errorMessage = "Unsupported operation: " + numbers.calcWanted;
                    }
                }
            }
        }


        public void Run()
        {
            Chain chain1 = new addNumbers();
            Chain chain2 = new subNumbers();
            Chain chain3 = new multNumbers();
            Chain chain4 = new divNumbers();

            chain1.setNextChain(chain2);
            chain2.setNextChain(chain3);
            chain3.setNextChain(chain4);

            Numbers operation = new Numbers(4, 2, "add");

            Console.Write(operation.number1 + " " + operation.calcWanted + " " + operation.number2 + " = ");

            chain1.calculate(operation);
            if (operation.error)
            {
                Console.WriteLine("Failed: " + operation.errorMessage);
            }
            else
            {
                 Console.WriteLine(operation.result);

            }


        }

    }
}
