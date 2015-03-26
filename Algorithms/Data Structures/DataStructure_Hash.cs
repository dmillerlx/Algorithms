using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    class DataStructure_Hash
    {
               
        class RabinCarp
        {
            //MIT open course documentation is located here: http://courses.csail.mit.edu/6.006/spring11/rec/rec06.pdf
           
            //This is a non-working example but it explains the basics of the rolling hash

            //The example shown below works only for integers between 0 and 9.  It does not work for
            //strings because the base (b) is too small.
            //The base value allows us to left shift the hash sum

            //Here's how it works
            // 1. Compute the hash of the pattern to match
            //      a. Each digit is left shifted and summed
            //          which means that when using the numbers 9, 0, 2, 1, 0
            //          you get the actual number of 90210 by
            //          doing 
            //              0 * 10^0 + 1 * 10^1 + 2 * 10^2 + 0 * 10^3 + 9 *10*4    <-- doing in reverse order to show the power number increasing
            //          =   0*1 + 1*10 + 2*100 + 0*1000 + 9*10000
            //          =   0 + 10 + 200 + 0 + 90000
            //          =   90210

            // 2. Compute a rolling hash for the string to search
            //      We start with the same process for the pattern shown above
            //      Consider this array to search: [4, 8, 9, 0, 2, 1, 0, 7]
            //      So the digits we are hashing are the digits the length
            //      of the search pattern, which is 5 digits (90210)
            //
            //      So, we do this operation for the first 5 digits: 4,8,9,0,2

            //              2 * 10^0 + 0 * 10^1 + 9 * 10^2 + 8 * 10^3 + 4 *10*4    
            //          =   2*1 + 0*10 + 9*100 + 8*1000 + 4*10000
            //          =   2 + 00 + 900 + 8000 + 40000
            //          =   48902

            // 3.   Now we compare 48902 to 90210 and find it does not match, so we start
            //      rolling the hash.  To roll the hash we remove the first digit (4) and
            //      append the next digit (1)
            //
            //      To remove the first digit we subtract from the hash (48902) the first
            //      digit to the 4th power
            //      48902 - 4*10^4 = 8902
            //
            //      To append the next digit (1) we left shift the hash and then add the new digit
            //      Left shift by multipling by the base (10)
            //      8902 * 10 = 89020
            //
            //      Now we add the new digit
            //      89020 + 1 = 89021

            // 4.   Now we compare the hash again. We find 89020 != 90210 so we do the rolling
            //      hash again
            //
            //      First subtract the first digit (8)
            //      89021 - 8*10^4 = 89021 - 80000 = 9021
            //
            //      Now we left shift and add the new digit (0)
            //      Left shift by multiplying by the base (10)
            //      9021 * 10 = 90210
            //
            //      Now we add the new digit (0)
            //      90210 + 0 = 90210
            //
            // 5.   Now we compare the hash again.  We find 90210 = 90210 so we have a hash match.
            //      But we are not done.  This hash match means we should now compare the actual digits
            //      In this case the digits match so we can return true.

            //
            //  Modulus
            //
            //  This is where it gets really fun and interesting
            //
            //  The example above is simple and uses a small base (10).  But if the string is longer and a larger
            //  character set is used, the base will be larger and the resulting hash number can get very large.
            //  
            //  Suppose we are searching across all ascii character strings so the base b = 256
            //  Now if we use the sample above the ascii characters translate to integer values as shown:
            //  0 = 48
            //  1 = 49
            //  2 = 50
            //  3 = 51
            //  4 = 52
            //  5 = 53
            //  6 = 54
            //  7 = 55
            //  8 = 56
            //  9 = 57
            
            //
            //  So when making a hash integer sum for 90210 we get
            //  0 * 256^0 + 1 *256^1 + 2 * 256^2 + 0 * 256^3 + 9 * 256^4
            //  = 0*1 + 1*256 + 2*65536 + 0*16777216 + 9*4294967296
            //  = 0 + 256 + 131072 + 0 + 38654705664
            //  = 38654836992
            // 
            //  This works, but it is apparent that the numers can quickly exceed the size of an integer for even
            //  a small search word.  This word was only 5 characters
            //
            //  This is where modular arthrithmic is helpful
            //  When using the modular function we normally use a prime number, like 101  (reason for the prime is not disussed here)
            //  
            //  The interesting part of modular math is that the mod function is transitive
            //  This means that for integers A and B with prime number Q
            //      a mod q * b mod q = (a*b) mod q
            //
            //  To prove this you can use the math.pow function
            //  Math.Pow(256, 4) = 256*256*256*256 = 4294967296
            //
            //  Now if you do this with q = 101
            //  (256 mod q * 256 mod q * 256 mod q * 256 mod q) mod q = (256*256*256*256) mod q
            //  256 mod q = 54
            //  (54 * 54 * 54 * 54) mod q =    68
            //
            //  So what this means is that we can use 'mod q' to reduce the size of the numbers but still
            //  maintain the hash.  The size of 'q' sets how many collissions we will have, but even a reasonable size of 101
            //  reduces the number of collisions to pretty small


            //
            //  So lets redo the example from above with mod's
            //
            //  Changes
            //  - Lets change the base from 10 to 256
            //  - Lets also change from using array of integers to a string array
            //      Search string is: 4890210
            //      Pattern to find is: 90210
            //

            //Here's how it works
            // 1. Compute the hash of the pattern to match
            //      a. Each digit is left shifted and summed
            //          So we are using the string 90210 and we will use the ascii values for each character
            //          90210 = ascii codes [57, 48, 50, 49, 48]
            //          Now lets get the hash sums and use mod
            //                      0 = 48               1 = 49                 2 = 50                0 = 48                 9 = 57                              
            //              ( (48 * 256^0) mod 101 + (49 * 256^1) mod 101 + (50 * 256^2) mod 101 + (48 * 256^3) mod 101 + (57 * 256^4) mod 101 ) mod 101
            //          =   ( (48) mod 101 +         (12544) mod 101 +      (3276800) mod 101 +    (805306368) mod 101 +  (244813135872) mod 101 ) mod 101
            //          =   ( 48 +                      20 +                        57 +                38 +                    38              )  mod 101
            //          =   ( 201 ) mod 101
            //          =   100                      <-- this is the hash we want to match


            // 2. Compute a rolling hash for the string to search
            //      We start with the same process for the pattern shown above
            //      Consider this array to search: [4, 8, 9, 0, 2, 1, 0, 7]
            //      So the digits we are hashing are the digits the length
            //      of the search pattern, which is 5 digits (90210)
            //
            //      So, we do this operation for the first 5 digits: 4,8,9,0,2

            //                      2 = 50               0 = 48                 9 = 57                8 = 56                 4 = 52                              
            //              ( (50 * 256^0) mod 101 + (48 * 256^1) mod 101 + (57 * 256^2) mod 101 + (56 * 256^3) mod 101 + (52 * 256^4) mod 101 ) mod 101
            //          =   ( (50 mod 101 +         (48) mod 101 +           (3735552) mod 101 +    (939524096) mod 101 +  (223338299392) mod 101 ) mod 101
            //          =   ( 50 +                      67 +                         67 +                    78 +             1)  mod 101
            //          =   ( 263 ) mod 101
            //          =   61                      <-- This is the first hash to check

            // 3.   Now we compare 61 to 100 and find it does not match, so we start
            //      rolling the hash.  To roll the hash we remove the first digit (4) and
            //      append the next digit (1)
            //
            //      To remove the first digit we subtract from the hash ('48902' = 61 ) the first
            //      digit ('4' = 52) to the 4th power
            //      
            //      ( (61 - (52 * 256 ^ 4) mod 101 ) mod 101
            //      = (61 - (223338299392) mod 101) mod 101
            //      = (61 - 1) mod 101
            //      = 60                           <-- Note, if you are using the normal % function it will be -11 which is wrong
            //
            //      To append the next digit ('1' = 49) we left shift the hash and then add the new digit
            //      Left shift by multipling by the base (256)
            //      (60 * 256) mod 101 = (15360) mod 101 = 8
            //
            //      Now we add the new digit ('1' = 49)
            //      (8 + 49) mod 101 = (57) mod 101 = 57           <-- This is the next hash to check


            // 4.   Now we compare the hash again. We find 57 != 100 so we do the rolling
            //      hash again
            //
            //      First subtract the first digit ('8' = 56)
            //      ( (57 - (56 * 256 ^ 4) mod 101) mod 101
            //      = (57 - (240518168576) mod 101) mod 101
            //      = (57 - 71) mod 101
            //      = 87                           <-- Note, if you are using the normal % function it will be -27 which is wrong
            //                                      The C# mod function operator % is really just a remainder operator.  It does not do actual mod
            //                                      An alternative way to do this is to add q (101) to the value if it is negative.
            //                                      so (57 - 71) = -14.  -14 + 101 = 87
            //
            //      Now we left shift and add the new digit ('0' = 48)
            //      Left shift by multiplying by the base (256)
            //      (87 * 256) mod 101 = (22272) mod 101 = 52
            //
            //      Now we add the new digit ('0' = 48)
            //      52 + 48 = 100 
            //
            // 5.   Now we compare the hash again.  We find 100 = 100 so we have a hash match.
            //      But we are not done.  This hash match means we should now compare the actual digits
            //              '90210' == '90210'
            //      In this case the digits match so we can return true.
            //
            

            // Can we make this simplier?
            // Yes, we still have large numbers with the power's.
            // We still use (x * 256^4) mod 101
            //
            // But due to mod arithmatic we can change this to
            //  (x * 256 ^ 4) mod 101 
            //  = x mod 101 * 256 mod 101 * 256 mod 101 * 256 mod 101 * 256 mod 101
            //  = x mod 101 * 54 * 54 * 54 * 54
            //  = x mod 101 * 8503056 mod 101
            //  = x mod 101 * 68
            //
            // So instead of using 256 ^0...256^4
            // we can use
            // 256^0 = 1
            // 256^1 = 54
            // 256^2 = 88
            // 256^3 = 5
            // 256^4 = 68

            //So, our original pattern hash can change to:
            //                      0 = 48               1 = 49                 2 = 50                0 = 48                 9 = 57                              
            //              ( (48 * 1) mod 101 + (49 * 54) mod 101 + (50 * 88) mod 101 + (48 * 5) mod 101 + (57 * 68) mod 101 ) mod 101
            //          =   ( (48) mod 101 +     (2646) mod 101 +    (4400) mod 101 +    (240) mod 101 +    (3876) mod 101 ) mod 101
            //          =   ( 48 +                      20 +                        57 +                38 +                    38              )  mod 101
            //          =   ( 201 ) mod 101
            //          =   100                      <-- this is the hash we want to match

            //Now we are using much smaller numbers that will not exceed the integer limits even if if we have a long string.

            //Further more, we can compute a long (256 ^ x) mod 101 value iterativly like this:

            // int d = 256;   <-- base
            // int h = 1;     <-- multiplier we are calculating for subtracting the first digit
            // int q = 101    <-- mod prime
            // for (int x=0; x < 10; x++)
            //      h = (h * 256) % q;
            //
            // So we do not need to compute a large number and then take the mod of it.  We can take the mod at each iteration.
            // This is necessary because Math.Pow(256, 10) is very big, and unnecessary
            //
            // We can then use this precalculated value when we left shift the hash during the rotating hash algorithm
            //
            //
            //      t = (d * (t - text[x] * h) + text[x + M]) % q;
                    //   ^^^^^^^^^^^^^^^^^^^^^   ^^^^^^^^^^^
                    //   This removes the left   this appends the next
                    //   most element from the   character to the hash
                    //   hash






            //  -------------------------Other explinaation based on MIT example----------------------------------------

            //h(k) = ( k[0]*b^(L-1) + k[1]*b^(L-2) + k[2]*b^(L-3)...k[L-1]*b^0 ) mod b
            //Here b = a base value
            //k = string to hash
            //m = prime number longer than string to match

            //Calculating the next hash value would be
            // h(S i+1) = b (h(S i) - b^(L-1)) + S[i+L] mod m
            // What this is doing is removing the first character from the hash and adding the last character
            // to the hash
            //
            // So consider
            // P = [9, 0, 2, 1, 0]   <--Pattern to find
            // S = [4, 8, 9, 0, 2, 1, 0, 7] <-- String to search
            //
            // S0 = [4, 8, 9, 0, 2]  <-- Substring 0 to match against
            // S1 = [8, 9, 0, 2, 1]  <-- Substring 1 to match against
            // S2 = [9, 0, 2, 1, 0]  <-- Substring 2 to match against
            //
            // L = length of P = 5
            //
            // If we use b = 10 then     (using b = 10 to make the numbers look simple for the example)
            //
            // P = [9, 0, 2, 1, 0] --> Hash(P) = 9*10^4 + 0 *10^3 + 2*10^2 + 1*10^1 + 0*10^0 = 90210 mod m
            //                                   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^   ^^^^^^^^^^^ 
            //                                   This basically converts the array to an int    This is the hash value we need to match
            //
            // S0 = [4, 8, 9, 0, 2] --> Hash(S0) = 4*10^4 + 8 *10^3 + 9*10^2 + 0*10^1 + 2*10^0 = 48902 mod m
            //                                     ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^   ^^^^^^^^^^^
            //                                      Convert array to int                         Get hash using mod
            //
            // S1 = [8, 9, 0, 2, 1] so
            // S1 = Remove first digit of S0 (4) and append digit for S1 (1)
            // S1 = Hash(S1) = ((48902 - 4*10^5) * 10 + 1) mod m = ((48902 - 40000) * 10 + 1) mod m = ((89021) * 10 + 1) mod m =    (89021) mod m
            //                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^      ^^^^^^^^^^^     
            //                 (48902 - 4*10^5) --> Take previous int and remove first digits by subtracting from FirstInt * 10^5        New hash but we only had to do 2 operations
            //                 * 10 --> Next left shift everything by multiplying by 10                                                  Remove head and append tail
            //                 + 1 --> Finally append the new digit from S1 by adding the int                                            We did not have to manipulate the entire string
            //  
            // S2 = [9, 0, 2, 1, 0] so
            // S2 = Remove first digit of S1 (8) and append digit for S2 (0)
            // S2 = Hash(S2) = ((89021 - 8*10^5) * 10 + 0) mod m = ((89021 - 80000) * 10 + 0) mod m = ((9021) * 10 + 0) mod m =     (90210) mod m
            //                 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^      ^^^^^^^^^^^     
            //                 (89021 - 8*10^5) --> Take previous int and remove first digits by subtracting from FirstInt * 10^5        New hash but we only had to do 2 operations
            //                 * 10 --> Next left shift everything by multiplying by 10                                                  Remove head and append tail
            //                 + 0 --> Finally append the new digit from S1 by adding the int                                            We did not have to manipulate the entire string
            //
            //  This hash ((90210) mod m) matches our pattern hash so we found a potential match




            
            public bool FindMatch(string text, string pattern, out int index)
            {
                //Based on GeekForGeeks algorithm
                //http://www.geeksforgeeks.org/searching-for-patterns-set-3-rabin-karp-algorithm/

                index = -1;

                int M = pattern.Length;

                int p = 0;      //hash of string pattern

                int q = 101;    //prime used for mod

                int d = 256;    //Base for character set and used with the multiplier used to left shift hash
                                //This number should be based on the maximum size of each character
                                //if using Unicode, it may need to be larger

                int h = 1;      //Multiplier for shifting the hash to the left

                int t = 0;      //rolling hash of text

                if (text.Length < pattern.Length)
                    return false;

                //This function is equlivilant to Math.Pow(256, M - 1) % q
                //This number will help us remove the left digits from the hash              

                //We use this iterative method instead of the Math.Pow function because
                //if M is large, this number gets big very quickly
                //Also, modulo arithimatic states that
                // (256*256*256*256) % q = (256 % q) * (256 % q) * (256 % q) * (256 % q)
                //So, this allows us to work with smaller numbers that will not exceed the variable size

                for (int x = 0; x < M -1; x++)
                    h = (h * 256) % q;

                //Calculate hash for pattern
                for (int x = 0; x < M; x++)
                {
                    p = (d * p + pattern[x]) % q;
                }

                //Calculate hash for start of string
                for (int x = 0; x < M; x++)
                {
                    t = (d * t + text[x]) % q;
                }


                for (int x = 0; x < text.Length - M; x++)
                {
                    //Check to see if the hashes match
                    if (t == p)
                    {
                        //If the hashes match, do a string compare to see if the strings are equlivant
                        if (text.Substring(x, M) == pattern)
                        {
                            index = x;
                            return true;
                        }                        
                    }

                    //Hashes do not match so rotate hash

                    t = (d * (t - text[x] * h) + text[x + M]) % q;
                    //   ^^^^^^^^^^^^^^^^^^^^^   ^^^^^^^^^^^
                    //   This removes the left   this appends the next
                    //   most element from the   character to the hash
                    //   hash

                    //If we get a negative value for t, then use the actual mod function
                    //For C# the % operator calculates the remainder, not the actual mod
                    //As such, it does not work properly on negative numbers
                    //Doing the correct mod operation will get the correct modulus value.
                    //An alternative is to just add 'q' to the value
                    if (t < 0)
                        t = mod(t, q);        //same as doing t = (t + q);
                                                //but leaving the nfmod function in here as a reminder that
                                                //in C# the % operator is not mod, it is remainder

                }

                //No match was found
                return false;

            }

            //Correct implmentation of mod in c# since % is the remainder, not the mod
            int mod(int a, int b)
            {
                return a - b * (int)Math.Floor((double)a / (double)b);
            }

        }

        
        public void Run()
        {
            
            RabinCarp a = new RabinCarp();
            int index = 0;

            if (a.FindMatch("48902107", "90210", out index))
            {
                Console.WriteLine("Match found at index: " + index);
            }
            else
            {
                Console.WriteLine("Match not found");
            }


            if (a.FindMatch("aaslkdfjweij.sk,m.,xzmvxcvijioszjdfljfksamfasjdfunvzxlckvm';lkjosijasoidfasdmflaksdmflkasjdfkjk", "fun", out index))
            {
                Console.WriteLine("Match found at index: " + index);
            }
            else
            {
                Console.WriteLine("Match not found");
            }

            if (a.FindMatch("aaslkdfjweij.sk,m.,xzmvxcvijioszjdfljfksamfasjdfunvzxlckvm';lkjosijasoidfasdmflaksdmflkasjdfkjk", "jdfunvzxlckvm';lkjosi", out index))
            {
                Console.WriteLine("Match found at index: " + index);
            }
            else
            {
                Console.WriteLine("Match not found");
            }
            
                      

        }
    }
}
