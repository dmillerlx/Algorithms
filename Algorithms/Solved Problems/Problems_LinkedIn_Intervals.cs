using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_Intervals
    {
        public interface Intervals
        {

            /**
             * Adds an interval [from, to) into internal structure.
             */
            void addInterval(int from, int to);


            /**
             * Returns a total length covered by the added intervals.
             * If several intervals intersect, the intersection should be counted only once.
             * Example:
             *
             * addInterval(3, 6)
             * addInterval(8, 9)
             * addInterval(1, 5)
             *
             * getTotalCoveredLength() -> 6
             * I.e. [1,5) and [3,6) intersect and give a total covered interval [1,6).
             * [1,6) and [8,9) don't intersect, so the total covered length is a sum of both intervals, that is 5+1=6.
             *
             *           ______
             *                         _
             *     _________
             *
             * 0  1  2  3  4  5  6  7  8  9  10
             *
             */
            int getTotalCoveredLength();
        }

        public class MyIntervals : Intervals
        {

            public class Node : IComparable
            {
                public int Start;
                public int End;

                public Node(int start, int end)
                {
                    Start = start;
                    End = end;
                }


                public int CompareTo(object obj)
                {
                    Node n = (Node)obj;
                    return Start.CompareTo(n.Start);
                }

                public override string ToString()
                {
                    return Start.ToString() + ", " + End.ToString();
                }
            }

            List<Node> nodes = new List<Node>();

            public bool Contains(Node n, int start, int end)
            {
                if (start >= n.Start && end <= n.End)
                    return true;
                return false;
            }

            public int getTotalCoveredLength()
            {
                int count = 0;
                for (int x=0; x < segments.Count(); x++)
                {
                    count += Math.Abs(segments[x].End - segments[x].Start);
                }

                return count;
            }

            // -1 0 1 2 3 4 5 6 7 8 9 
            //      -----   -----
            //        --
            //        ---
            //      --
            //    ------
            //         --------
            //    ------------------

            List<Node> segments = new List<Node>();
            //This version starts by sorting the list
            public void addInterval(int from, int to)
            {
                segments.Add(new Node(from, to));
                segments.Sort();

                for (int x=1; x < segments.Count; )
                {
                    if (Contains(segments[x - 1], segments[x].Start, segments[x].End))
                    {
                        //Current segment contained by previous segment
                        segments.RemoveAt(x);
                    }
                    else if (Contains(segments[x - 1], segments[x].Start, segments[x].Start))
                    {
                        //Current segment starts inside previous segment but does not end in previous segment
                        //so set end of the previous segment to the end of the current segment and remove the current segment
                        segments[x - 1].End = segments[x].End;
                        segments.RemoveAt(x);
                    }                    
                    else x++;   //Else advance 
                }              

            }

            //This version inserts the new span while iterating and then starts merging segments
            //that are on/after the inserted string.  Don't need to check those before the inserted segment
            //since they should already be merged
            public void addInterval_InsertSort(int from, int to)
            {                
                if (segments.Count == 0)
                {
                    segments.Add(new Node(from, to));
                    return;
                }

                bool added = false;
                for (int x = 0; x < segments.Count; )
                {
                    if (!added)
                    {
                        if (from <= segments[x].Start)
                        {
                            segments.Insert(x, new Node(from, to));
                            added = true;
                            x++;
                        }
                        else if (x == segments.Count-1)
                        {
                            segments.Add(new Node(from, to));
                            added = true;
                            x++;
                        }
                        else x++;
                    }
                    else
                    {
                        if (Contains(segments[x - 1], segments[x].Start, segments[x].End))
                        {
                            //Current segment contained by previous segment
                            segments.RemoveAt(x);
                        }
                        else if (Contains(segments[x - 1], segments[x].Start, segments[x].Start))
                        {
                            //Current segment starts inside previous segment but does not end in previous segment
                            //so set end of the previous segment to the end of the current segment and remove the current segment
                            segments[x - 1].End = segments[x].End;
                            segments.RemoveAt(x);
                        }
                        else x++;   //Else advance 
                    }
                }

            }
        }


        public void Run()
        {

            MyIntervals m = new MyIntervals();

            m.addInterval(1, 5);
            m.addInterval(8, 12);
            m.addInterval(2, 3);
            m.addInterval(4, 10);
            m.addInterval(0, 3);
            m.addInterval(20, 23);

            Console.WriteLine(m.getTotalCoveredLength());

            MyIntervals m2 = new MyIntervals();

            m2.addInterval_InsertSort(1, 5);
            m2.addInterval_InsertSort(8, 12);
            m2.addInterval_InsertSort(2, 3);
            m2.addInterval_InsertSort(4, 10);
            m2.addInterval_InsertSort(0, 3);
            m2.addInterval_InsertSort(20, 23);

            Console.WriteLine(m2.getTotalCoveredLength());


        }

    }
}
