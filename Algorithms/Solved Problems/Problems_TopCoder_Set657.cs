using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    /*
     * ProblemSetEasy
Problem Statement
Cat Snuke came up with some problems. He wants to construct as many problem sets as possible using those problems. Each problem set must contain exactly three problems: one for the Easy slot, one for the Medium slot, and one for the Hard slot. Each problem can only be assigned to a single slot in a single problem set. He came up with E + EM + M + MH + H problems in total. The distribution of the problems is as follows:
E problems can only be used in the Easy slot.
EM problems can be used either in the Easy slot or the Medium slot.
M problems can only be used in the Medium slot.
MH problems can be used either in the Medium slot or the Hard slot.
H problems can only be used in the Hard slot.
Return the maximal number of problem sets he can construct.
 
Definition
    	
Class:	ProblemSetsEasy
Method:	maxSets
Parameters:	int, int, int, int, int
Returns:	int
Method signature:	int maxSets(int E, int EM, int M, int MH, int H)
(be sure your method is public)
    
 
Constraints
-	E, EM, M, MH, H must be between 0 and 100000, inclusive.
 
Examples
0)	
    	
2
2
1
2
2
Returns: 3
One of EM problems should be used for the Easy slot, and the other should be used for the Medium slot. One of MH problems should be used for the Medium slot, and the other should be used for the Hard slot.
1)	
    	
100
100
100
0
0
Returns: 0
Unfortunately, no problem can be used for the Hard slot.
2)	
    	
657
657
657
657
657
Returns: 1095
3)	
    	
1
2
3
4
5
Returns: 3
4)	
    	
100000
100000
100000
100000
100000
Returns: 166666




    */

    class Problems_TopCoder_Set657
    {



        public int maxSets(int E, int EM, int M, int MH, int H)
        {
            int sets = 0;

            while (E+EM > 0 && EM+M+MH > 0 && MH+H > 0)
            {

                if (M > 0)
                    M--;
                else
                {
                    if (EM + E >= MH + H)
                        EM--;
                    else MH--;
                }

                if (E + EM == 0)
                    return sets;

                if (H + MH == 0)
                    return sets;

                if (E > 0)
                    E--;
                else EM--;

                if (H > 0)
                    H--;
                else MH--;
                sets++;
            }

            return sets;
        }

        public void Run()
        {

            Console.WriteLine(maxSets(2, 2, 1, 2, 2));

            Console.WriteLine(maxSets(100, 100, 100, 0, 0));

            Console.WriteLine(maxSets(1000, 1000, 1000, 1000, 1000));

        }


    }
}
