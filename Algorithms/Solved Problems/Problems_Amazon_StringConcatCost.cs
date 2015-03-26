using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Amazon_StringConcatCost
    {
        /*
        <p>You are to concatenate n strings (concatenate in any order) and a function:
<br>int strCat(str1, str2); // returns the concatenated str length
<br>Concatenate all strings in any order so that total cost is minimum.
<br>Example: Strings A="abc", B="wxyz", C="a"
<br>Cost of strCat(A,B) = (3+4) = 7
<br>Cost of strCat(AB,C) = 7+1 = 8 
<br>Total cost = 7+8 =15
<br>Other way: 
<br>Cost of strCat (A,C) = 3+1 = 4,
<br>Cost of strCat (AC,B) = 4+4 = 8
<br>Total Cost = 4+8 = 12
<br>In this case, min(12,15) = 12 so Ans=12.</p>
        */

        public int strCat(string a, string b)
        {
            return a.Length + b.Length;
        }

        public class Item : IComparable
        {
            public string Value { get; set; }
            public int Length { get; set; }
            
            public Item(string v, int len)
            {
                this.Value = v;
                this.Length = len;
            }

            public int CompareTo(object obj)
            {
                Item i = (Item)obj;
                return Length.CompareTo(i.Length);
            }
        }

        public void ProcessString(string str)
        {
            string []strings = str.Split(new string[] { " " }, StringSplitOptions.None);

            Heap<Item> Items = new Heap<Item>(Heap<Item>.heapTypeEnum.min);

            for (int x=0; x < strings.Length; x++)
            {
                Items.Enqueue(new Item(strings[x], strings[x].Length));
            }

            int cost = 0;

            while (Items.size > 1)
            {
                Item item1 = Items.Dequeue();
                Item item2 = Items.Dequeue();

                string newString = item1.Value + item2.Value;
                         
                int concatCost = strCat(item1.Value, item2.Value);
                cost += concatCost;
                Console.WriteLine("Concat: " + item1.Value + " + " + item2.Value +" = "+concatCost);

                Items.Enqueue(new Item(newString, newString.Length));
            }

            Item result = Items.Dequeue();
            Console.WriteLine("Final String: " + result.Value);
            Console.WriteLine("Cost: " + cost);


        }

        public void Run()
        {
            ProcessString("abc wxyz a");

        }


    }
}
