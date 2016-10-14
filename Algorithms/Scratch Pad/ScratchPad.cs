using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Scratch_Pad
{
    public class ScratchPad
    {
        //Assumptions
        //only lower case characters a-z
        public static bool AllUniqueChars(char []arr)
        {
            if (arr.Length <= 1)
                return true;

            if (arr.Length > 26)
                return false;

            int[] charCount = new int[26];

            for (int index =0; index < arr.Length; index++)
            {
                //Get character
                char c = arr[index];
                //Convert character to integer with offset starting with 'a'
                int c_value = c - 'a';

                //Return false if we have already processed this character
                if (charCount[c_value] > 0)
                    return false;

                //Increment to indicate we have processed this character
                charCount[c_value]++;
            }

            return true;
        }

        public static char []Reverse(char []arr)
        {
            if (arr.Length <= 1)
                return arr;

            int s = 0;
            int e = arr.Length - 1;

            char tmp;
            while (s < e)
            {
                tmp = arr[s];
                arr[s] = arr[e];
                arr[e] = tmp;
                s++;
                e--;
            }
            //abcde
            //ebcda
            //edcba

            //abcdef
            //fbcdea
            //fecdba
            //fedcba

            return arr;


        }

        //Assumptions
        //  character set is lower case letters a-z
        public static bool IsPermutation(char []arr1, char []arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;

            int[] letterCount = new int[26];

            for (int index=0; index < arr1.Length; index++)
            {
                char c = arr1[index];
                int offset = c - 'a';
                letterCount[offset]++;
            }

            for (int index = 0; index < arr2.Length; index++)
            {
                char c = arr2[index];
                int offset = c - 'a';
                if (letterCount[offset] <= 0)
                    return false;
                letterCount[offset]--;
            }

            return true;
        }

    }
}
