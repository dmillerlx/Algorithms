using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Composite_with_Decorator
    {

        //
        // Composite and Decorator Design Pattern's
        // 
        // This design pattern is used to organize items in a hericheral structure
        // where by the leaf and nodes both implement simular functions
        //
        // The decorator pattern is used to add functionality to a class at run time
        //
        // These patterns are often used together
        //
        // Sample uses:
        //  Composite:
        //      Employee structure for a company where each employee has
        //      a name and some of them have subordinates
        //
        //  Decorator:
        //      Employee's can have responsibilities and job functions
        //
        //  The exmaple below shows using the composite and decorator patterns to show an employee
        //  listing and their job functions
        //
        //  The basic pattern for composite is such that an interface defines the common functions
        //    and another class implements the required functions:
        //      AddChild, RemoveChild, GetChild, DoOperation (Operations from the interface)
        //
        //  The basic pattern for decorator is to have an interface that is implmented by an abstract class
        //      The abstract class has a reference to each decorated wrapping
        //      Finding all the decorations is done by recursivly calling each decoration
        //
        //  
        //  Since the composite pattern is for heirachal data, we will use it to maintain the
        //      manager / employee heirachy
        //  To manage each employee's job responsibilities we will use the decorator pattern since employee's
        //      can have multiple job functions
        //


        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Begin decorator pattern section for the job responsibilties
        //
        //Decorator Pattern
        //

        //Base abstract class
        public abstract class JobFunction
        {
            public abstract string Job();
        }

        //Concrete Class
        public class EmployeeJobFunction : JobFunction
        {
            public override string Job()
            {
                return "CompanyWorker";
            }
        }


        //Decoration abstract class
        public abstract class WorkerJobFunction : EmployeeJobFunction
        {
            protected JobFunction JobFunction { get; set; }

            protected WorkerJobFunction(JobFunction w)
            {
                JobFunction = w;
            }
        }

        //Decoration class - actual implementation of types
        public class CEO_JobFunction : WorkerJobFunction
        {
            public CEO_JobFunction(JobFunction s): base(s)
            {
            }

            public override string Job()
            {
                return "CEO " + JobFunction.Job();
            }
        }

        public class Developer_JobFunction : WorkerJobFunction
        {
            public Developer_JobFunction(JobFunction s)
                : base(s)
            {
            }

            public override string Job()
            {
                return "Developer " + JobFunction.Job();
            }
        }

        public class Contractor_JobFunction : WorkerJobFunction
        {
            public Contractor_JobFunction(JobFunction s)
                : base(s)
            {
            }

            public override string Job()
            {
                return "Contractor " + JobFunction.Job();
            }
        }

        public class SeniorDeveloper_JobFunction : WorkerJobFunction
        {
            public SeniorDeveloper_JobFunction(JobFunction s)
                : base(s)
            {
            }

            public override string Job()
            {
                return "SeniorDeveloper " + JobFunction.Job();
            }
        }

        public class Manager_JobFunction : WorkerJobFunction
        {
            public Manager_JobFunction(JobFunction s)
                : base(s)
            {
            }

            public override string Job()
            {
                return "Manager " + JobFunction.Job();
            }
        }

        //End decoration pattern section
        ////////////////////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////////////////////////
        // Composite pattern start
        //
        // Composite interface - Define the 'DoOperations' here
        // We will include the reference to JobFunction which is decorated using the
        //      decorator classes above


        public interface IEmployed
        {
            int EmployeeID { get; set; }
            string Name { get; set; }

            JobFunction JobFunction { get; set; }

        }



        //
        //Composite Class
        //

        public class Employee : JobFunction, IEmployed, IEnumerable<IEmployed>
        {
            //DoOperations from the base interface
            public int EmployeeID { get; set; }

            public string Name { get; set; }

            public JobFunction JobFunction { get; set; }


            //
            //Requires operations: AddChild, RemoveChild, GetChild
            //
            public List<IEmployed> children = new List<IEmployed>();

            public void Add(IEmployed item)
            {
                children.Add(item);
            }

            public void Delete(IEmployed item)
            {
                children.Remove(item);
            }

            public IEmployed Get(int index)
            {
                return children[index];
            }


            //
            //Optional: To make the class easier to use, we implement Count so the caller
            //          does not have to exceed the list index
            public int Count()
            {
                return children.Count();
            }

            //
            //Optional: To make the class easier to use, we implement IEnumerator
            //
            public IEnumerator<IEmployed> GetEnumerator()
            {
                foreach (IEmployed item in children)
                {
                    yield return item;
                }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override string Job()
            {
                return string.Empty;
            }



        } //end Employee Class

       
        //
        //Leaf class   - This is a leaf only class for a composite pattern
        //
        public class Contractor : IEmployed
        {
            //DoOperations from the base interface
            public int EmployeeID { get; set; }
            public string Name { get; set; }

            public JobFunction JobFunction { get; set; }
        }

        // 
        //Run
        //

        public void Run()
        {
            //Create employee's
            Employee Rahul = new Employee { EmployeeID = 1, Name = "Rahul", JobFunction = new EmployeeJobFunction() };
            // Rahul is the CEO and also a Manager, so decorate him with those job functions
            Rahul.JobFunction = new CEO_JobFunction(Rahul.JobFunction);
            Rahul.JobFunction = new Manager_JobFunction(Rahul.JobFunction);

            Employee Amit = new Employee { EmployeeID = 2, Name = "Amit", JobFunction = new EmployeeJobFunction() };
            Employee Mohan = new Employee { EmployeeID = 3, Name = "Mohan", JobFunction = new EmployeeJobFunction() };

            //Amit and Mohan are managers, so decorate them with those job functions
            Amit.JobFunction = new Manager_JobFunction(Amit.JobFunction);
            Mohan.JobFunction = new Manager_JobFunction(Mohan.JobFunction);

            //Add Amit and Mohan as subordiantes to Rhaul
            Rahul.Add(Amit);
            Rahul.Add(Mohan);

            //Add more employees
            Employee Rita = new Employee { EmployeeID = 4, Name = "Rita", JobFunction = new EmployeeJobFunction() };
            Employee Hari = new Employee { EmployeeID = 5, Name = "Hari", JobFunction = new EmployeeJobFunction() };

            //Rita and Hari are developers, so decorate them with those responsibilities
            Rita.JobFunction = new Developer_JobFunction(Rita.JobFunction);
            Hari.JobFunction = new Developer_JobFunction(Hari.JobFunction);

            //Add subordinates to Amit
            Amit.Add(Rita);
            Amit.Add(Hari);

            //Create more employee's
            Employee Kamal = new Employee { EmployeeID = 6, Name = "Kamal", JobFunction = new EmployeeJobFunction() };
            Employee Raj = new Employee { EmployeeID = 7, Name = "Raj", JobFunction = new EmployeeJobFunction() };

            //Kamal and Raj are developers and senior developers, so decorate them with those responsitiblities
            Kamal.JobFunction = new Developer_JobFunction(Kamal.JobFunction);
            Raj.JobFunction = new Developer_JobFunction(Raj.JobFunction);
            Kamal.JobFunction = new SeniorDeveloper_JobFunction(Kamal.JobFunction);
            Raj.JobFunction = new SeniorDeveloper_JobFunction(Raj.JobFunction);


            //Create leaf only contractors
            Contractor Sam = new Contractor { EmployeeID = 8, Name = "Sam", JobFunction = new EmployeeJobFunction() };
            Contractor tim = new Contractor { EmployeeID = 9, Name = "Tim", JobFunction = new EmployeeJobFunction() };

            //Sam and Tim are contrators, so decorate them with those responsibilties
            Sam.JobFunction = new Contractor_JobFunction(Sam.JobFunction);
            tim.JobFunction = new Contractor_JobFunction(tim.JobFunction);

            //Add all under Mohan
            Mohan.Add(Kamal);
            Mohan.Add(Raj);
            Mohan.Add(Sam);
            Mohan.Add(tim);
                        


            Console.WriteLine("EmpID={0}, Name={1}, Job={2}", Rahul.EmployeeID, Rahul.Name, Rahul.JobFunction.Job());

            foreach (Employee manager in Rahul)
            {
                Console.WriteLine("\n EmpID={0}, Name={1}, Job={2}", manager.EmployeeID, manager.Name, manager.JobFunction.Job());

                foreach (var employee in manager)
                {
                    Console.WriteLine(" \t EmpID={0}, Name={1}, Job={2}", employee.EmployeeID, employee.Name, employee.JobFunction.Job());
                }
            }

        }//end Run()
    }
}
