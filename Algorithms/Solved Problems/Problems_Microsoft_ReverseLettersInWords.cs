using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Microsoft_ReverseLettersInWords
    {
        
        public string ReverseWord(string val)
        {
            StringBuilder sb = new StringBuilder();
            for (int x=val.Length-1; x >= 0; x--)
            {
                sb.Append(val[x]);
            }
            return sb.ToString();
        }

        public string ReverseLettersInSentence(string val)
        {
            //Tokenize string
            string[] tok = val.Split(new string[] { " " }, StringSplitOptions.None);

            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < tok.Length; x++)
            {
                if (sb.Length > 0)
                    sb.Append(" ");
                sb.Append(ReverseWord(tok[x]));
            }

            return sb.ToString();
            

        }


        public string ReverseLettersInSentence2(string val)
        {

            int startIndex = -1;
            int endIndex = -1;
            StringBuilder sb = new StringBuilder();
            for (int x=0; x < val.Length; x++)
            {
                if (startIndex < 0)
                {
                    if (isLetter(val[x]))
                    {
                        startIndex = x;
                    }
                    else
                    {
                        sb.Append(val[x]);
                    }
                }
                else
                {
                    if (!isLetter(val[x]))
                    {
                        endIndex = x - 1;

                        //Reverse startIndex to endIndex
                        for (int y=endIndex; y >= startIndex; y--)
                        {
                            sb.Append(val[y]);
                        }

                        sb.Append(val[x]);

                        startIndex = -1;
                        endIndex = -1;
                    }
                }

                //Handle corner case at end of string
                if (x == val.Length -1)
                {
                    if (startIndex >= 0)
                    {
                        endIndex = x;

                        //Reverse startIndex to endIndex
                        for (int y = endIndex; y >= startIndex; y--)
                        {
                            sb.Append(val[y]);
                        }
                    }
                }

            }

            return sb.ToString();

        }

        private bool isLetter(char p)
        {
            return char.IsLetterOrDigit(p);

        }



        public void Run()
        {
            Console.WriteLine("Reverse: " + ReverseLettersInSentence2("This is a test"));

            Console.WriteLine("Reverse: " + ReverseLettersInSentence2("This is a test."));

            Console.WriteLine("Reverse: " + ReverseLettersInSentence2("   This is a test."));

            Console.WriteLine("Reverse: " + ReverseLettersInSentence2("   This is a test.   "));
        }

    }
}
