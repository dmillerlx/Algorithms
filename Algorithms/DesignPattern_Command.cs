using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Command
    {

        #region Example 1 - game buttons
        public interface Command
        {
            void execute();
        }

        public class JumpCommand : Command
        {
            public void execute()
            {
                System.Console.WriteLine("Jump!");
            }
        }

        public class FireCommand : Command
        {
            public void execute()
            {
                System.Console.WriteLine("Fire!");
            }
        }

        public class LurchCommand : Command
        {
            public void execute()
            {
                System.Console.WriteLine("Lurch!");
            }
        }

        public class SwapCommand : Command
        {
            public void execute()
            {
                System.Console.WriteLine("Swap!");
            }
        }


        public class InputHandler
        {
            Command _buttonA;
            Command _buttonB;
            Command _buttonC;
            Command _buttonD;

            public void HandleInput(string input)
            {
                _buttonA = new JumpCommand();
                _buttonB = new FireCommand();
                _buttonC = new LurchCommand();
                _buttonD = new SwapCommand();


                switch (input.ToUpper())
                {
                    case "A": _buttonA.execute(); break;
                    case "B": _buttonB.execute(); break;
                    case "C": _buttonC.execute(); break;
                    case "D": _buttonD.execute(); break;
                }
            }
        }

        public void Run()
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Command Pattern - Game Buttons");

            InputHandler handler = new InputHandler();

            handler.HandleInput("A");
            handler.HandleInput("B");
            handler.HandleInput("C");
            handler.HandleInput("D");

        }
        #endregion

        #region Example 2 - Light Switch

        public interface ICommand
        {
            void Execute();
        }

        //Invoker class
        public class Switch
        {
            ICommand _closedCommand;
            ICommand _openCommand;

            public Switch(ICommand closedCommand, ICommand openCommand)
            {
                _closedCommand = closedCommand;
                _openCommand = openCommand;
            }

            public void Close()
            {
                _closedCommand.Execute();
            }

            public void Open()
            {
                _openCommand.Execute();
            }            
        }

        //Interface for reciever to implement
        public interface ISwitchable
        {
            void PowerOn();
            void PowerOff();
        }

        //Reciever class - this is the class that knows how to implement the function
        public class Light : ISwitchable
        {
            public void PowerOn()
            {
                Console.WriteLine("Light On!");
            }

            public void PowerOff()
            {
                Console.WriteLine("Light Off!");
            }
        }

        //Command for turning on the device - ConcreteCommand
        public class CloseSwitchCommand: ICommand
        {
            ISwitchable _switchable;
            public CloseSwitchCommand(ISwitchable switchable)
            {
                _switchable = switchable;
            }

            public void Execute()
            {
                _switchable.PowerOn();
            }
        }

        //Command for turning off the device - ConcreteCommand
        public class OpenSwitchCommand : ICommand
        {
            ISwitchable _switchable;
            public OpenSwitchCommand(ISwitchable switchable)
            {
                _switchable = switchable;
            }

            public void Execute()
            {
                _switchable.PowerOff();
            }
        }


        public void Run2()
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Command Pattern - Light Switch");

            //Create object
            ISwitchable lamp = new Light();

            //Create commands and pass reference to object
            ICommand switchClose = new CloseSwitchCommand(lamp);
            ICommand switchOpen = new OpenSwitchCommand(lamp);

            //Create switch and pass reference to command objects
            Switch s = new Switch(switchClose, switchOpen);

            //Switch (the Invoker) will invoke Execute() (the command) on the command object - _closedCommand.Execute();
            s.Close();

            //Switch (the Invoker) will invoke Execute() (the command) on the command object - _openCommand.Execute();
            s.Open();

        }


        #endregion

    }
}
