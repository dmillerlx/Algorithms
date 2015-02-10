using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DesignPattern_Prototype
    {
        //A comparison between the flyweight pattern and the prototype pattern shows some similarities in the UML, 
        //in that both use a manager to store and retrieve the objects in the collection. 
        //But there is a clear difference between the two. 
        //
        //The prototype pattern is used to create new objects that are similar in nature (hence it's a creational pattern), 
        //while the flyweight pattern is used to allow the application to point to the same instance 
        //of the object to save memory (hence it's a structural pattern).


        //Simulate creating a large object that takes a long time to populate
        public class LargeObject
        {
            public string Data{get;set;}
            //public string DataID { get; set; }

            public LargeObject()
            {

            }

            public LargeObject(int size)
            {
                Console.WriteLine("Creating object of size: " + size);
                System.Random rnd = new Random((int)DateTime.Now.Ticks);

                for (int x = 0; x < size; x++)
                {
                    char val = (char)('a' + rnd.Next(35));
                    Data += val;
                }

                //DataID = System.Guid.NewGuid().ToString();

                System.Threading.Thread.Sleep(3000);
            }
        }

        //Hash table to keep track of objects
        System.Collections.Hashtable ht = new System.Collections.Hashtable();

        //Constructor
        public DesignPattern_Prototype()
        {

        }

        public LargeObject GetObject(string dataid)
        {
            //Look for prototype in collection
            LargeObject refObject = null;
            if (ht.Contains(dataid))
            {
                //Found prototype
                refObject = (LargeObject)ht[dataid];
            }
            else
            {
                //Failed to find prototype, so lets create it
                System.Random rnd = new Random((int)System.DateTime.Now.Ticks);
                refObject = new LargeObject(rnd.Next(100) * rnd.Next(100) * rnd.Next(100));

                ht.Add(dataid, refObject);
            }

            //Now make a copy of the prototype and return it
            LargeObject ret = new LargeObject();
            ret.Data = refObject.Data;

            return ret;
        }


        public void Run()
        {
            DateTime start, end;
            TimeSpan lapse;

            Console.WriteLine("Prototype pattern");
            Console.WriteLine("-------------------------------------");
            //Create prototypes
            for (int x = 0; x < 3; x++)
            {
                start = DateTime.Now;
                LargeObject lrg = GetObject(x.ToString());
                end = DateTime.Now;
                lapse = end-start;
                Console.WriteLine("Creation time for (" + x + ") size ("+lrg.Data.Length+") is (" + lapse.TotalSeconds + ")");
            }

            Console.Write("Now get copies of data");
            Console.WriteLine("-------------------------------------");
            for (int x = 0; x < 3; x++)
            {
                start = DateTime.Now;
                LargeObject lrg = GetObject(x.ToString());
                end = DateTime.Now;
                lapse = end - start;
                Console.WriteLine("Creation time for (" + x + ") size (" + lrg.Data.Length + ") is (" + lapse.TotalSeconds + ")");
            }



        }


    }
}
