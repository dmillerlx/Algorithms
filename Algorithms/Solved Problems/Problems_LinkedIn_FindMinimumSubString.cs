using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_FindMinimumSubString
    {
        //Given an input string and a target string, find the minimum substring of the input string that 
        //contains all of the characters in the target string 


        //This solution requires the characters to be in order
        public class SubString
        {
            public int Start { get; set; }
            public int End { get; set; }

            public int TargetIndex { get; set; }
            public string TargetString { get; set; }

            public int TargetLength { get; set; }

            public bool Complete { get { return TargetIndex == TargetLength; } }

            public int TotalLength { get { return End - Start + 1; } }

            public SubString(int start, string targetString)
            {
                Start = start;
                End = -1;
                TargetString = targetString;
                TargetLength = TargetString.Length;
                TargetIndex = 1;
            }

            public void ProcessChar(char c, int index)
            {
                if (Complete)
                    return;

                if (c == TargetString[TargetIndex])
                    TargetIndex++;

                if (Complete)
                    End = index;

            }

            
        }

        public string FindMinimumSubString(string input, string target)
        {
            List<SubString> list = new List<SubString>();
            for (int x = 0; x < input.Length; x++)
            {
                SubString newItem = null;
                if (input[x] == target[0])
                {
                    //Create new starting substring item
                    newItem = new SubString(x, target);

                    list.Add(newItem);
                }

                for (int y = 0; y < list.Count(); y++)
                {
                    if (list[y] != newItem && !list[y].Complete)
                    {
                        list[y].ProcessChar(input[x], x);
                    }
                }
            }

            int smallestIndex = -1;

            for (int x=0; x < list.Count(); x++)
            {
                if (list[x].Complete)
                if (smallestIndex == -1 || list[x].TotalLength < list[smallestIndex].TotalLength)
                {
                    smallestIndex = x;
                }
            }

            if (smallestIndex < int.MaxValue)
            {
                string ret = input.Substring(list[smallestIndex].Start, list[smallestIndex].TotalLength);
                return ret;
            }

            return "No Solution";
        }


        //This solution finds the minimum substring in 'input' that contains all of the characters in 'target'
        //The substring must contain all the characters, but the characters can be in any order
        //
        // n = length of input
        // m = length of target
        // Time: O(n+m) 
        // Space: O(2m) 
        public string FindMinimumSubString_AnyOrder(string input, string target)
        {
            //Approach
            //1.    Create count of each target character in a hash table
            //2.    Find first substring by counting the number of characters in 'input' that have
            //      the characters in 'target'
            //      This is done by advancing the 'end' index until the counts match
            //3.    Ensure the substring is the minium for that substring by advancing the 'start' index until
            //      the substring no longer has the correct count
            //      This produces the minimum first substring and the maximum window we need to look at
            //4.    Now advance the window across the input string, adding the characters that enter the window (in the end index)
            //      and removing the characters that exit the window (in the start index)
            //5.    If we encounter another substring in the window that has the character counts, advance the 'start' index
            //      until the counts no longer match.  This gives us the next minimum window size and minimum substring
            //6.    Repeat steps 4 and 5 until end of the string

            //1. Create hash of target characters
            Dictionary<char, int> targetHash = new Dictionary<char, int>();
            for (int x=0; x < target.Count(); x++)
            {
                if (targetHash.ContainsKey(target[x]))
                    targetHash[target[x]] = targetHash[target[x]] + 1;
                else
                    targetHash[target[x]] = 1;
            }

            //2. Create hash of input up to where all target characters are found
            //This will be the first 'window' for the solution
            Dictionary<char, int> inputHash = new Dictionary<char, int>();
            bool found = false;
            int minStart = -1;
            int minEnd = -1;

            for (int x=0; x < input.Count() && !found; x++)
            {
                if (targetHash.ContainsKey(input[x]))
                {
                    if (minStart < 0)
                        minStart = x;      //Mark the start of where the first character starts.
                    if (inputHash.ContainsKey(input[x]))
                        inputHash[input[x]]++;
                    else inputHash[input[x]] = 1;
                }
                
                bool foundSubString = true;
                foreach (char k in targetHash.Keys)
                {
                    if (inputHash.ContainsKey(k) == false)
                    {
                        foundSubString = false;
                        break;
                    }
                    else if (inputHash[k] < targetHash[k])
                    {
                        foundSubString = false;
                        break;
                    }
                }

                if (foundSubString)
                {
                    found = true;
                    minEnd = x;            //Mark the end where the final character was found
                }
            }

            if (!found) return "Failed to find substring";


            //3. Advance start to ensure we are at the first part of the substring
            do
            {
                if (targetHash.ContainsKey(input[minStart]))
                    inputHash[input[minStart]]--;
                minStart++;
            } while (HasSubString(inputHash, targetHash));

            minStart--;
            inputHash[input[minStart]]++;
            //minStart is now at the start of the minimum first substring



            //4. Move window across the string
            //
            //Start and End now point to the first substring that contains all the characters.
            //Now advance Start and End until another substring is found with all the characters.
            //When another substring is found, tighten the window by advancing Start until the substring does not contain
            //all of the characters.  This finds the next smallest window.
            //Repeat this process until the end of the string

            int start = minStart;
            int end = minEnd;

            do
            {
                //Advance start and end
                if (inputHash.ContainsKey(input[start]))
                    inputHash[input[start]]--;  //Remove character from hash at 'start'
                start++;                    //increment start

                //Append new character to the hash
                end++;
                if (targetHash.ContainsKey(input[end]))
                {
                    if (inputHash.ContainsKey(input[end]) == false)
                    {
                        inputHash[input[end]] = 1;
                    }
                    else inputHash[input[end]]++;
                }
                

                //5. Check to see if inputHash has all the characters in the target string.  
                if (HasSubString(inputHash, targetHash))
                {
                    //Found another substring that has all the characters in the target string
                    //So this substring is less than or equal in size to the previous substring
                    //Shrink the substring by advancing start only until the it is no longer a valid substring
                    //at which point we have the outer bounds for this substring and can compare against
                    //the current shortest substring (minStart, minEnd)

                    do
                    {
                        if (targetHash.ContainsKey(input[start]))
                            inputHash[input[start]]--;
                        start++;
                    } while (HasSubString(inputHash, targetHash));

                    start--;
                    inputHash[input[start]]++;
                    //Start is now on new minimum, check to see if new minimum is smaller than current minimum
                    if ((end - start) < (minEnd - minStart))
                    {
                        minStart = start;
                        minEnd = end;
                    }
                }

            //6. - Repeat until the end of the list
            } while (end < input.Length - 1);

            Console.WriteLine("MinStart: " + minStart);
            Console.WriteLine("MinEnd: " + minEnd);

            return input.Substring(minStart, minEnd - minStart + 1);


        }


        bool HasSubString(Dictionary<char, int> inputHash, Dictionary<char, int> targetHash)
        {
            bool foundSubString = true;
            //for (int y=0; y < targetHash.Keys.Count(); y++)                
            foreach (char k in targetHash.Keys)
            {
                if (inputHash.ContainsKey(k) == false)
                {
                    foundSubString = false;
                    break;
                }
                else if (inputHash[k] < targetHash[k])
                {
                    foundSubString = false;
                    break;
                }
            }

            return foundSubString;
        }


        public void Process(string val, string target)
        {
            Console.WriteLine("--------Min Sub String-----------");
            Console.WriteLine();

            string minSub = FindMinimumSubString_AnyOrder(val, target);
            Console.WriteLine("Val: " + val);
            Console.WriteLine("Target: " + target);
            Console.WriteLine("Solution: " + minSub);
        }


        public void Run()
        {
            Process("12345aj67890abcdefghijklmnop12345678a90aefghijklmnop1234567890aghijklmnop", "ajm");

            Process("12345aj67890abcdefghijklmnop12345678a9m0aeflghijklmnop1234567890aghijklmnop", "ajm");

            Process("1234m5aj67890abcdefghaijklmnop12345678a90aefghijklmnop1234567890aghijklmnop", "ajm");
        }


    }
}
