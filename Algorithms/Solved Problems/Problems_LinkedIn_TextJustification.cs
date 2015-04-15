using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_TextJustification
    {
        //code a text justification routine (Given a line length insert white space so text is uniformly displayed within the given length).

        public string JustifyText(string text, int pageWidth)
        {
            if (text.Length == pageWidth)
                return text;

            if (text.Length > pageWidth)
                return null;

            string[] words = text.Split(new string[]{" "}, StringSplitOptions.RemoveEmptyEntries);

            int wordWidth = 0;
            for (int x = 0; x < words.Length; x++)
            {
                wordWidth += words[x].Length;
            }

            int widthDiff = pageWidth - wordWidth;
            //This is a test
            //    ^  ^ ^        3 places to add spaces
            //not adding spaces before first or after last

            //This is number of spaces after each word except last word
            int spacesPerWord = widthDiff / (words.Length - 1);
            
            //Left over spaces that need to be added if the spaces are not even
            int leftOverSpaces = pageWidth % (spacesPerWord * (words.Length - 1));

            StringBuilder spaces = new StringBuilder();
            for (int y = 0; y < spacesPerWord; y++)
                spaces.Append(" ");

            StringBuilder sb = new StringBuilder();
            
            for (int x=0; x < words.Length; x++)
            {
                if (x != 0)
                {
                    //add spaces per word, before word, except for the first word
                    //for (int y = 0; y < spacesPerWord; y++)
                    //    sb.Append(" ");
                    sb.Append(spaces);

                    if (leftOverSpaces > 0)
                    {
                        //Add 1 of left over space for each word to distribute across words
                        sb.Append(" ");
                        leftOverSpaces--;
                    }
                }
                //Add word
                sb.Append(words[x]);
            }

            return sb.ToString();
        }



        public void Run()
        {
            string text = "This is a test.";

            string ret = JustifyText(text, 25);

            Console.WriteLine("Text: " + text);
            Console.WriteLine("Justify ("+ret.Length+"): " + ret);

        }

    }
}
