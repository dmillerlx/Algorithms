using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_String_ReverseWords
    {
        //Given a sentence, reverse the order of the words in the sentence.

        public string ReverseWords (string val)
        {
            string[] tok = val.Split(new string[] { " " }, StringSplitOptions.None);

            StringBuilder sb = new StringBuilder();
            for (int x = tok.Length - 1; x >= 0; x--)
            {
                if (sb.Length > 0)
                    sb.Append(" ");
                sb.Append(tok[x]);
            }

            return sb.ToString();

        }

        public void Run()
        {
            Console.WriteLine(ReverseWords("This is a test"));

        }

    }
}
