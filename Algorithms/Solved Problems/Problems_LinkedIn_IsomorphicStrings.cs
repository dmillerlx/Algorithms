using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_IsomorphicStrings
    {
        //Given two (dictionary) words as Strings, determine if they are isomorphic. Two words are called isomorphic if the letters 
        //in one word can be remapped to get the second word. Remapping a letter means replacing all occurrences of it with another 
        //letter while the ordering of the letters remains unchanged. No two letters may map to the same letter, but a letter may map to itself.
        public bool IsIsomorphic(string val1, string val2)
        {
            char[] arr = new char[26];
            char[] arr2 = new char[26];

            if (val1.Length != val2.Length) return false;

            for (int x=0; x < val1.Length; x++)
            {
                char v1 = val1[x];
                char v2 = val2[x];

                int index = v1 - 'a';
                int index2 = v2 - 'a';

                if (arr[index] == '\0' && arr2[index2] == '\0')
                {
                    arr[index] = v2;
                    arr2[index2] = v1;
                }
                else
                {
                    if (arr[index] != v2 || arr2[index2] != v1)
                        return false;
                }
            }

            return true;

        }




        public void Run()
        {

            Console.WriteLine("foo to app: " + IsIsomorphic("foo", "app"));

            Console.WriteLine("bar to foo: " + IsIsomorphic("bar", "foo"));

            Console.WriteLine("turtle to tletur: " + IsIsomorphic("turtle", "tletur"));

            Console.WriteLine("ab to ca: " + IsIsomorphic("ab", "ca"));


        }
    }
}
