using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Algorithms
{
    abstract class a
    {
        abstract public void test();
        abstract public void test2();

        public void test3()
        {

        }
    }

    class b : a
    {
        public override void test()
        {
            
        }

        public override void test2()
        {
            throw new NotImplementedException();
        }
    }

    public sealed class SingletonV2
    {
        static int instanceNumber = 0;
        public int myInstanceNumber { get; set; }

        public static readonly SingletonV2 instance = new SingletonV2();
        private SingletonV2(){
            myInstanceNumber = instanceNumber++;
        }
    }


    public class DesignPattern_Singleton
    {
        static DesignPattern_Singleton instance = null;
        static object lockObject = new object();

        static int instanceNumber = 0;

        public int myInstanceNumber { get; set; }

        public DesignPattern_Singleton()
        {
            myInstanceNumber = instanceNumber++;
        }

        public DesignPattern_Singleton GetInstance()
        {
            Random r = new Random((int)System.DateTime.Now.Ticks);

            System.Threading.Thread.Sleep(r.Next(100));

            if (instance == null)
            {
                System.Threading.Thread.Sleep(r.Next(100));
                lock (lockObject)
                {
                    System.Threading.Thread.Sleep(r.Next(100));
                    if (instance == null)
                    {
                        Debug.WriteLine("*************Singleton - First One In!!!!");
                        instance = new DesignPattern_Singleton();
                    }
                }
            }

            return instance;
        }

    }

    public class TestSingleton
    {
        
    }
}
