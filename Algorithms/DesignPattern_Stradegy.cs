using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Algorithms
{
    class DesignPattern_Stradegy
    {

        public interface IWeapon
        {
            void Use();
        }

        public class Axe : IWeapon
        {
            public void Use()
            {
                Debug.WriteLine("Axe hit");
            }
        }

        public class Sword : IWeapon
        {
            void IWeapon.Use()
            {
                Debug.WriteLine("Sword cut");
            }
        }

        public class NullWeapon : IWeapon
        {
            public void Use()
            {
                Debug.WriteLine("NULL");
            }
        }


        public class Player
        {
            IWeapon weapon;

            public Player(IWeapon w)
            {
                weapon = w;
            }

            public void Use()
            {
                weapon.Use();
            }


        }
        public DesignPattern_Stradegy() { }

        public void Run()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Design Pattern - Stradegy");
            Console.WriteLine("This example will call the 'Use()' function to attack with a weapon\n");
            
            Player a = new Player(new Axe());
            Player b = new Player(new Sword());

            a.Use();
            b.Use();

            Console.Out.Flush();
        }
    }

//////////////////////////////////////////////////////////////////////////////////////////////////
//Alternative example
    class DesignPattern_Stradegy_V2
    {

        public abstract class SortStradegy
        {
            abstract public void Sort(List<string> val);
        }

        public class DefaultSort : SortStradegy
        {
            public override void Sort(List<string> val)
            {
                val.Sort();
                Console.WriteLine("Using default sort");
            }
        }

        public class HeapSort : SortStradegy
        {
            public override void Sort(List<string> val)
            {
                Console.WriteLine("Using Heap Sort");
                Heap<string> heapSort = new Heap<string>(Heap<string>.heapTypeEnum.max);

                heapSort.BuildHeap(val.ToArray());

                int finalSize = 0;
                string[] sortedList = heapSort.Sort(out finalSize);

                val.Clear();
                foreach (string s in sortedList)
                {
                    val.Add(s);
                }

            }
        }

        public class SortedList
        {
            private List<string> list = new List<string>();

            public SortStradegy _sortStradegy;

            public void SetSortStrategy(SortStradegy newSortStradegy)
            {
                _sortStradegy = newSortStradegy;
            }

            public void Add(string s)
            {
                list.Add(s);
            }

            public void Sort()
            {
                _sortStradegy.Sort(list);

                Console.WriteLine("Sorted List:");
                foreach (string s in list)
                {
                    //Check for null only for heap sort since first element is null
                    if (!string.IsNullOrEmpty(s))
                        Console.WriteLine(s);
                }
            }
        }

        public void Run()
        {
            Console.WriteLine("\n\n-----------------------------------------------");
            Console.WriteLine("Design Pattern - Stradegy - V2");

            SortedList list = new SortedList();
            Console.WriteLine("Adding items to list");
            list.Add("Zebra");
            list.Add("Cat");
            list.Add("Dog");
            list.Add("Tiger");
            list.Add("Polar Bear");
            list.Add("Chicken");
            Console.WriteLine("Setting sort stradegy");
            list.SetSortStrategy(new HeapSort());
            list.Sort();

            Console.WriteLine("\nAdding new item to list");
            list.Add("Panther");

            Console.WriteLine("\nSetting sort stradegy");
            list.SetSortStrategy(new DefaultSort());
            list.Sort();

        }


    }




}
