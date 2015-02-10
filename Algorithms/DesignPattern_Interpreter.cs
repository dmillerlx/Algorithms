using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Algorithms
{
    #region Pattern v1 - not working to interpret roman numerals
    class DesignPattern_Interpreter
    {
        public class Context
        {

            private String input;
            private int output;

            public Context(String input)
            {
                this.input = input;
            }

            public String getInput()
            {
                return input;
            }

            public void setInput(String input)
            {
                this.input = input;
            }

            public int getOutput()
            {
                return output;
            }

            public void setOutput(int output)
            {
                this.output = output;
            }

        }

        public abstract class Expression
        {

            public void interpret(Context context)
            {
                if (context.getInput().Length == 0)
                    return;

                if (context.getInput().StartsWith(nine()))
                {
                    context.setOutput(context.getOutput() + (9 * multiplier()));
                    context.setInput(context.getInput().Substring(2));
                }
                else if (context.getInput().StartsWith(four()))
                {
                    context.setOutput(context.getOutput() + (4 * multiplier()));
                    context.setInput(context.getInput().Substring(2));
                }
                else if (context.getInput().StartsWith(five()))
                {
                    context.setOutput(context.getOutput() + (5 * multiplier()));
                    context.setInput(context.getInput().Substring(1));
                }

                while (context.getInput().StartsWith(one()))
                {
                    context.setOutput(context.getOutput() + (1 * multiplier()));
                    context.setInput(context.getInput().Substring(1));
                }
            }

            public abstract String one();
            public abstract String four();
            public abstract String five();
            public abstract String nine();
            public abstract int multiplier();

        }

        public class ThousandExpression : Expression
        {
            override public string one() { return "M"; }
            override public string four() { return " "; }
            override public string five() { return " "; }
            override public string nine() { return " "; }
            override public int multiplier() { return 1000; }
        }

        public class HundredExpression : Expression
        {
            override public string one() { return "X"; }
            override public string four() { return "XL"; }
            override public string five() { return "L"; }
            override public string nine() { return "XC"; }
            override public int multiplier() { return 10; }
        }
        public class TenExpression : Expression
        {
            override public string one() { return "X"; }
            override public string four() { return "XL"; }
            override public string five() { return "L"; }
            override public string nine() { return "XC"; }
            override public int multiplier() { return 10; }
        }
        public class OneExpression : Expression
        {
            override public string one() { return "I"; }
            override public string four() { return "IV"; }
            override public string five() { return "V"; }
            override public string nine() { return "IX"; }
            override public int multiplier() { return 1; }
        }

        public void Run()
    {
			
	      String roman = "MCMXXVIII";
	      Context context = new Context(roman);

	      // Build the 'parse tree' 
	      List<Expression> tree = new List<Expression>();// ArrayList<Expression>();
	      tree.Add(new ThousandExpression());
	      tree.Add(new HundredExpression());
	      tree.Add(new TenExpression());
	      tree.Add(new OneExpression());

	      // Interpret 
            
	      foreach (Expression it in tree)//= tree.iterator(); it.hasNext();)
	      {
	    	  //Expression exp = (Expression)it.next();
              it.interpret(context);
              //exp.interpret(context);
	      }

	      Console.WriteLine(roman + " = " + context.getOutput().ToString());
	

    }
    }
    #endregion

    #region Pattern v2 - working to interprert Reverse Polish Notiation

    class DesignPattern_Interpreter_v2
    {
        // In this example we have these terminal expressions:
        //      Number, Plus, Minus, Variable
        // There are no NonTerminal expressions

     

        interface Expression
        {
            int interpret(Dictionary<string, Expression> variables);
        }

        class Number : Expression
        {
            private int number;
            public Number(int number) { this.number = number; }
            public int interpret(Dictionary<string, Expression> variables) { return number; }
        }

        class Plus : Expression
        {
            Expression leftOperand;
            Expression rightOperand;
            public Plus(Expression left, Expression right)
            {
                leftOperand = left;
                rightOperand = right;
            }

            public int interpret(Dictionary<string, Expression> variables)
            {
                return leftOperand.interpret(variables) + rightOperand.interpret(variables);
            }
        }

        class Minus : Expression
        {
            Expression leftOperand;
            Expression rightOperand;
            public Minus(Expression left, Expression right)
            {
                leftOperand = left;
                rightOperand = right;
            }

            public int interpret(Dictionary<string, Expression> variables)
            {
                return leftOperand.interpret(variables) - rightOperand.interpret(variables);
            }
        }

        class Variable : Expression
        {
            private string name;
            public Variable(string name) { this.name = name; }
            public int interpret(Dictionary<string, Expression> variables)
            {
                if (null == variables[name]) return 0; //Either return new Number(0).
                return variables[name].interpret(variables);
            }
        }



        class Evaluator : Expression
        {
            private Expression syntaxTree;

            public Evaluator(string expression)
            {
                Stack<Expression> expressionStack = new Stack<Expression>();
                string[] tokens = expression.Split(new string[] { " " }, StringSplitOptions.None);
                foreach (string token in tokens)
                {// (string token : expression.split(" ")) {
                    if (token.Equals("+"))
                    {
                        Expression subExpression = new Plus(expressionStack.Pop(), expressionStack.Pop());
                        expressionStack.Push(subExpression);
                    }
                    else if (token.Equals("-"))
                    {
                        // it's necessary remove first the right operand from the stack
                        Expression right = expressionStack.Pop();
                        // ..and after the left one
                        Expression left = expressionStack.Pop();
                        Expression subExpression = new Minus(left, right);
                        expressionStack.Push(subExpression);
                    }
                    else
                        expressionStack.Push(new Variable(token));
                }
                syntaxTree = expressionStack.Pop();
            }

            public int interpret(Dictionary<string, Expression> context)
            {
                return syntaxTree.interpret(context);
            }
        }


        public void Run()
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Design Pattern - Interpreter");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Example: Interpreting Reverse Polish Notation");
            Console.WriteLine(@"  Ported from the Wiki Java Example: http://en.wikipedia.org/wiki/Interpreter_pattern");
            Console.WriteLine("");

            String expression = "w x z - +";

            Console.WriteLine("Input expression: " + expression);

            Evaluator sentence = new Evaluator(expression);
            Dictionary<string, Expression> variables = new Dictionary<string, Expression>();
            variables.Add("w", new Number(5));
            variables.Add("x", new Number(10));
            variables.Add("z", new Number(42));
            int result = sentence.interpret(variables);
            Console.WriteLine(result);

        }
    }

    #endregion

    #region Pattern v3 - from Derek Banes on Youtube

    class DesignPattern_Interpreter_v3
    {
        //This example interprets questions like:
        // 1 gallon to pints
        //and outputs
        // 1 gallon equals 4 pints
        //
        
        //Context
        //This class parses the input
        public class ConversionContext
        {

            private string conversionQues = "";
            private string conversionResponse = "";
            private string fromConversion = "";
            private string toConversion = "";
            private double quantity;

            string[] partsOfQues;

            public ConversionContext(string input)
            {
                this.conversionQues = input;

                partsOfQues = getInput().Split(new string[] { " " }, StringSplitOptions.None);// getInput().split(" ");

                fromConversion = getCapitalized(partsOfQues[1]);

                toConversion = getLowercase(partsOfQues[3]);

                quantity = double.Parse(partsOfQues[0]);

                conversionResponse = partsOfQues[0] + " " + partsOfQues[1] + " equals ";
            }

            public string getInput() { return conversionQues; }

            public string getFromConversion() { return fromConversion; }

            public string getToConversion() { return toConversion; }

            public string getResponse() { return conversionResponse; }

            public double getQuantity() { return quantity; }

            // Make string lowercase

            public string getLowercase(string wordToLowercase)
            {

                return wordToLowercase.ToLower();

            }

            // Capitalizes the first letter of a word

            public string getCapitalized(string wordToCapitalize)
            {

                // Make characters lower case

                wordToCapitalize = wordToCapitalize.ToLower();

                // Make the first character uppercase

                wordToCapitalize = Char.ToUpper(wordToCapitalize[0]) + wordToCapitalize.Substring(1);

                // Put s on the end if not there

                int lengthOfWord = wordToCapitalize.Length;

                if ((wordToCapitalize[lengthOfWord - 1]) != 's')
                {

                    wordToCapitalize = new StringBuilder(wordToCapitalize).Insert(lengthOfWord, "s").ToString();

                }

                return wordToCapitalize;

            }

        }

        //Base class for Expressions in implement
        public abstract class Expression
        {

            public abstract string gallons(double quantity);
            public abstract string quarts(double quantity);
            public abstract string pints(double quantity);
            public abstract string cups(double quantity);
            public abstract string tablespoons(double quantity);

        }

        //TerminalExpressions implemented for this design
        //No NonTerminal expressions were used
        public class Gallons : Expression
        {

            override public string gallons(double quantity)
            {

                return quantity.ToString(); //double.ToString(quantity);        
            }

            override public string quarts(double quantity)
            {
                return (quantity * 4).ToString();
            }

            override public string pints(double quantity)
            {
                return (quantity * 8).ToString();
            }

            override public string cups(double quantity)
            {
                return (quantity * 16).ToString();
            }

            override public string tablespoons(double quantity)
            {
                return (quantity * 256).ToString();
            }

        }
        public class Quarts : Expression
        {

            override public string gallons(double quantity)
            {

                return (quantity / 4).ToString();
            }

            override public string quarts(double quantity)
            {
                return (quantity).ToString();
            }

            override public string pints(double quantity)
            {
                return (quantity * 2).ToString();
            }

            override public string cups(double quantity)
            {
                return (quantity * 4).ToString();
            }

            override public string tablespoons(double quantity)
            {
                return (quantity * 64).ToString();
            }

        }
        public class Pints : Expression
        {

            override public string gallons(double quantity)
            {

                return (quantity / 8).ToString();
            }

            override public string quarts(double quantity)
            {
                return (quantity / 2).ToString();
            }

            override public string pints(double quantity)
            {
                return (quantity).ToString();
            }

            override public string cups(double quantity)
            {
                return (quantity * 2).ToString();
            }

            override public string tablespoons(double quantity)
            {
                return (quantity * 32).ToString();
            }

        }
        public class Cups : Expression
        {

            override public string gallons(double quantity)
            {

                return (quantity / 16).ToString();
            }

            override public string quarts(double quantity)
            {
                return (quantity / 4).ToString();
            }

            override public string pints(double quantity)
            {
                return (quantity / 2).ToString();
            }

            override public string cups(double quantity)
            {
                return (quantity).ToString();
            }

            override public string tablespoons(double quantity)
            {
                return (quantity * 16).ToString();
            }

        }
        public class Tablespoons : Expression
        {

            override public string gallons(double quantity)
            {

                return (quantity / 256).ToString();
            }

            override public string quarts(double quantity)
            {
                return (quantity / 64).ToString();
            }

            override public string pints(double quantity)
            {
                return (quantity / 32).ToString();
            }

            override public string cups(double quantity)
            {
                return (quantity / 16).ToString();
            }

            override public string tablespoons(double quantity)
            {
                return (quantity).ToString();
            }

        }

        //This factory is not part of the design pattern
        //This selected the correct TerminalExpression based on the input parsed int the Context
        public Expression ExpressionFactory(string name)
        {
            switch (name.ToUpper())
            {
                case "GALLONS": return new Gallons();
                case "PINTS": return new Pints();
                case "CUPS": return new Cups();
                case "TABLESPOONS": return new Tablespoons();
                default: return null;
            }
        }

        //Run the test
        public void Run()
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Design Pattern - Interpreter");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Example: Interpreting and converting between Gallons, Pints, Cups, and Teaspoons");
            Console.WriteLine(@"  Ported from the Derek Banes youtube example: https://www.youtube.com/watch?v=6CVymSJQuJE http://www.newthinktank.com/2012/10/interpreter-design-pattern-tutorial/");
            Console.WriteLine("   Derek Banes example uses Java Reflection.  I used a factory pattern and switches instead");
            Console.WriteLine("");


            // Add the question to the context for analysis
            string questionAsked = "1 gallon to pints";

            Console.WriteLine("Question: " + questionAsked);

            ConversionContext question = new ConversionContext(questionAsked);

            string fromConversion = question.getFromConversion();

            string toConversion = question.getToConversion();

            double quantity = question.getQuantity();

            try
            {
                //Get the Expression based on the fromConversion parsing
                Expression tempClass = ExpressionFactory(fromConversion);

                string toQuantity = string.Empty;

                //Do the conversion using the correct method
                switch (toConversion.ToUpper())
                {
                    case "GALLONS": toQuantity = tempClass.gallons(quantity); break;
                    case "PINTS": toQuantity = tempClass.pints(quantity); break;
                    case "CUPS": toQuantity = tempClass.cups(quantity); break;
                    case "TABLESPOONS": toQuantity = tempClass.tablespoons(quantity); break;
                }

               
                //Answer the question
                string answerToQues = question.getResponse() +
                        toQuantity + " " + toConversion;


                Console.WriteLine("Answer: " + answerToQues);
               

            }
            catch (Exception ex)
            { }



        }






    }//end DesignPattern_v3


#endregion




}
