using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Algorithms.Data_Structures;

namespace UnitTest
{
    public class TestBase
    {
        #region Helper Functions
        public string ToString(char[] arr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in arr)
            {
                sb.Append(c);
            }

            return sb.ToString();

        }
        

        public void MatrixHelper_Print(int[,] arr)
        {
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Debug.Write(string.Format("{0} ", arr[i, j]));
                }
                Debug.Write(Environment.NewLine + Environment.NewLine);
            }
        }

        public bool MatrixHelper_Compare(int[,] one, int[,] two)
        {
            int rowLength = one.GetLength(0);
            int colLength = one.GetLength(1);

            if (one.GetLength(0) != two.GetLength(0))
                return false;
            if (one.GetLength(1) != two.GetLength(1))
                return false;
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (one[i, j] != two[i, j])
                        return false;
                }
            }

            return true;

        }

        public bool LinkedList_Compare(DataStructure_LinkedList<int> one, DataStructure_LinkedList<int>two)
        {
            if (one == null && two != null)
                return false;

            if (two == null && one != null)
                return false;

            if (one == null && two == null)
                return true;

            DataStructure_LinkedList<int>.Node oneNode = one.head;
            DataStructure_LinkedList<int>.Node twoNode = two.head;

            while (oneNode != null && twoNode != null)
            {
                if (oneNode.Data != twoNode.Data)
                    return false;
                oneNode = oneNode.Next;
                twoNode = twoNode.Next;
            }

            if (oneNode != null || twoNode != null)
                return false;

            return true;
        }        
        #endregion
    }
}
