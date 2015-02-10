using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Algorithms
{
    class DesignPattern_Composite
    {
        //
        // Composite Design Pattern
        // 
        // This design pattern is used to organize items in a hericheral structure
        // where by the leaf and nodes both implement simular functions
        //
        // Sample uses:
        //  File system has folders and file, both support operations like
        //      Move, Delete, Rename, Copy...
        //
        //  Employee structure for a company where each employee has
        //      a name and some of them have subordinates
        //
        //  The exmaple below shows using the composite pattern to show an employee
        //  listing
        //
        //  The basic pattern is such that an interface defines the common functions
        //    and another class implements the required functions:
        //      AddChild, RemoveChild, GetChild, DoOperation (Operations from the interface)

        public interface IEmployed
        {
            int EmployeeID { get; set; }
            string Name { get; set; }
        }

        //
        //Composite Class
        //

        public class Employee : IEmployed, IEnumerable<IEmployed>
        {
            //DoOperations from the base interface
            public int EmployeeID {get;set;}
            
            public string Name {get;set;}

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
        } //end Employee Class


        //
        //Leaf class
        //
        public class Contractor : IEmployed
        {
            //DoOperations from the base interface
            public int EmployeeID { get; set; }
            public string Name { get; set; }
        }

        // 
        //Run
        //
           
        public void Run()
        {
            //Create employee's
            Employee Rahul = new Employee { EmployeeID = 1, Name = "Rahul" };

            Employee Amit = new Employee { EmployeeID = 2, Name = "Amit" };
            Employee Mohan = new Employee { EmployeeID = 3, Name = "Mohan" };

            //Add Amit and Mohan as subordiantes to Rhaul
            Rahul.Add(Amit);
            Rahul.Add(Mohan);

            //Add more employees
            Employee Rita = new Employee { EmployeeID = 4, Name = "Rita" };
            Employee Hari = new Employee { EmployeeID = 5, Name = "Hari" };

            //Add subordinates to Amit
            Amit.Add(Rita);
            Amit.Add(Hari);

            //Create more employee's
            Employee Kamal = new Employee { EmployeeID = 6, Name = "Kamal" };
            Employee Raj = new Employee { EmployeeID = 7, Name = "Raj" };

            //Create leaf only contractors
            Contractor Sam = new Contractor { EmployeeID = 8, Name = "Sam" };
            Contractor tim = new Contractor { EmployeeID = 9, Name = "Tim" };
            
            //Add all under Mohan
            Mohan.Add(Kamal);
            Mohan.Add(Raj);
            Mohan.Add(Sam);
            Mohan.Add(tim);

            Console.WriteLine("EmpID={0}, Name={1}", Rahul.EmployeeID, Rahul.Name);

            foreach (Employee manager in Rahul)
            {
                Console.WriteLine("\n EmpID={0}, Name={1}", manager.EmployeeID, manager.Name);

                foreach (var employee in manager)
                {
                    Console.WriteLine(" \t EmpID={0}, Name={1}", employee.EmployeeID, employee.Name);
                }
            }

        }//end Run()

    } 
}
