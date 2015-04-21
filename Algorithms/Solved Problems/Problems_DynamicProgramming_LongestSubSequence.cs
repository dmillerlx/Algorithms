using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_DynamicProgramming_LongestSubSequence
    {
        public class Visited
        {
            public Node Node { get; set; }
            public int Distance { get; set; }
            public Node PrevNode { get; set; }
        }
        
        public class Node
        {
            static int nodeid = 0;
            public Node(int data)
            { 
                Data = data;
                Edges = new List<Edge>();
                NodeID = nodeid++;
            }

            public int Data { get; set; }
            public List<Edge> Edges { get; set; }
            public int NodeID { get; set; }
        }

        public class Edge
        {            
            public Node Node { get; set; }
            public int Weight { get; set; }            
        }


        public void Dijkstra(Node start, Node end)
        {
            Visited []visited = new Visited[10];

            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {

                Node currentNode = queue.Dequeue();

                //Set distance for self if not in list already
                if (visited[currentNode.NodeID] == null)
                {
                    visited[currentNode.NodeID] = new Visited(){Node = currentNode, Distance=0, PrevNode=null};
                }

                //Now check each edge to see if we have a short distance with this path
                foreach (Edge e in currentNode.Edges)
                {
                    //Not in visited, so add it
                    if (visited[e.Node.NodeID] == null)
                    {
                        visited[e.Node.NodeID] = new Visited() { Node = e.Node, Distance = e.Weight, PrevNode = currentNode };
                        //Follow this path since it is new
                        queue.Enqueue(e.Node);
                    }
                    else
                    {
                        int currentWeight = visited[currentNode.NodeID].Distance;
                        int newWeight = currentWeight + e.Weight;
                        //Console.WriteLine("New weight: " + newWeight);
                        if (newWeight < visited[e.Node.NodeID].Distance)
                        {
                            visited[e.Node.NodeID].Distance = newWeight;
                            visited[e.Node.NodeID].PrevNode = currentNode;

                            //Follow this path since it is shorter
                            queue.Enqueue(e.Node);
                        }
                    }

                }
                
                //Console.WriteLine(currentNode.Data);
            }

            if (visited[end.NodeID] == null)
            {
                Console.WriteLine("No path found from (" + start.Data + ") to (" + end.Data + ")");
            }
            else
            {
                Console.WriteLine("Shortest Path from (" + start.Data + ") to (" + end.Data + ") distance ("+visited[end.NodeID].Distance+"): ");
                Node n = end;
                while (n != null)
                {
                    if (n.NodeID != end.NodeID)
                        Console.Write(",");
                    Console.Write(n.Data);
                    n = visited[n.NodeID].PrevNode;
                }

            }
        }

        Dictionary<int, List<Node>> memoization = new Dictionary<int, List<Node>>(); 


        public List<Node> Dijkstra_LongestPath(Node start)
        {
            Dictionary<int, Visited> visited = new Dictionary<int, Visited>();

            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {

                Node currentNode = queue.Dequeue();

                //Set distance for self if not in list already
                if (visited.Keys.Contains(currentNode.NodeID) == false)// == null)
                {
                    visited[currentNode.NodeID] = new Visited() { Node = currentNode, Distance = 0, PrevNode = null };
                }

                //Now check each edge to see if we have a short distance with this path
                foreach (Edge e in currentNode.Edges)
                {
                    //Not in visited, so add it
                    if (visited.Keys.Contains(e.Node.NodeID) == false)// == null)
                    {
                        visited[e.Node.NodeID] = new Visited() { Node = e.Node, Distance = e.Weight, PrevNode = currentNode };
                        //Follow this path since it is new
                        queue.Enqueue(e.Node);
                    }
                    else
                    {
                        int currentWeight = visited[currentNode.NodeID].Distance;
                        int newWeight = currentWeight + e.Weight;
                        //Console.WriteLine("New weight: " + newWeight);
                        if (newWeight < visited[e.Node.NodeID].Distance)
                        {
                            visited[e.Node.NodeID].Distance = newWeight;
                            visited[e.Node.NodeID].PrevNode = currentNode;

                            //Follow this path since it is shorter
                            queue.Enqueue(e.Node);
                        }
                    }

                }

                //Console.WriteLine(currentNode.Data);
            }


            //Find the shortest path overall
            Visited longestPath = null;
            foreach (int key in visited.Keys)//Visited v in visited.Keys)
            {
                Visited v = visited[key];
                if (v != null)
                {
                    if (longestPath == null)
                    {
                        longestPath = v;
                    }
                    else if (v.Distance < longestPath.Distance)
                    {
                        longestPath = v;
                    }

                }
            }

            //Console.WriteLine("Longest Path overall: ");
            Node n = longestPath.Node;
            List<Node> retList = new List<Node>();
            while (n != null)
            {
                retList.Insert(0, n);
                //if (n.NodeID != longestPath.Node.NodeID)
                //    Console.Write(",");
                //Console.Write(n.Data);
                n = visited[n.NodeID].PrevNode;
            }

            return retList;
        }

        public List<Node> Dijkstra_memo(Node start)
        {
            List<Node> ret = new List<Node>();

            if (memoization.ContainsKey(start.NodeID))
                return new List<Node>(memoization[start.NodeID]);

            ret.Add(start);

            if (start.Edges.Count == 0)
                return ret;

            List<Node> longest = null;
            foreach (Edge e in start.Edges)
            {
                if (longest == null)
                {
                    longest = new List<Node>(Dijkstra_memo(e.Node)); //.Insert(0, start);
                    //longest.Insert(0, start);
                }
                else
                {
                    List<Node> temp = Dijkstra_memo(e.Node);
                    //temp.Insert(0, start);
                    if (temp.Count > longest.Count)
                        longest = temp;
                }
            }

            longest.Insert(0, start);

            memoization[start.NodeID] = new List<Node>(longest);

            return longest;
        }


        public List<Node> Dijkstra_LongestPath_Memoized(Node start)
        {
            Dictionary<int, Visited> visited = new Dictionary<int, Visited>();

            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {

                Node currentNode = queue.Dequeue();

                //Set distance for self if not in list already
                if (visited.Keys.Contains(currentNode.NodeID) == false)// == null)
                {
                    visited[currentNode.NodeID] = new Visited() { Node = currentNode, Distance = 0, PrevNode = null };
                }

                //Now check each edge to see if we have a short distance with this path
                foreach (Edge e in currentNode.Edges)
                {
                    //Not in visited, so add it
                    if (visited.Keys.Contains(e.Node.NodeID) == false)// == null)
                    {
                        visited[e.Node.NodeID] = new Visited() { Node = e.Node, Distance = e.Weight, PrevNode = currentNode };
                        //Follow this path since it is new
                        queue.Enqueue(e.Node);
                    }
                    else
                    {
                        int currentWeight = visited[currentNode.NodeID].Distance;
                        int newWeight = currentWeight + e.Weight;
                        //Console.WriteLine("New weight: " + newWeight);
                        if (newWeight < visited[e.Node.NodeID].Distance)
                        {
                            visited[e.Node.NodeID].Distance = newWeight;
                            visited[e.Node.NodeID].PrevNode = currentNode;

                            //Follow this path since it is shorter
                            queue.Enqueue(e.Node);
                        }
                    }

                }

                //Console.WriteLine(currentNode.Data);
            }


            //Find the shortest path overall
            Visited longestPath = null;
            foreach (int key in visited.Keys)//Visited v in visited.Keys)
            {
                Visited v = visited[key];
                if (v != null)
                {
                    if (longestPath == null)
                    {
                        longestPath = v;
                    }
                    else if (v.Distance < longestPath.Distance)
                    {
                        longestPath = v;
                    }

                }
            }

            //Console.WriteLine("Longest Path overall: ");
            Node n = longestPath.Node;
            List<Node> retList = new List<Node>();
            while (n != null)
            {
                retList.Insert(0, n);
                //if (n.NodeID != longestPath.Node.NodeID)
                //    Console.Write(",");
                //Console.Write(n.Data);
                n = visited[n.NodeID].PrevNode;
            }

            return retList;
        }


        public class NodeList : IComparable
        {
            public NodeList(Node n)
            {
                Node = n;
                LongestPath = new List<Node>();
            }

            public Node Node { get; set; }
            public List<Node> LongestPath { get; set; }

            public int CompareTo(object obj)
            {
                NodeList other = (NodeList)obj;

                //return this.LongestPath.Count.CompareTo(other.LongestPath.Count);
                return other.LongestPath.Count.CompareTo(this.LongestPath.Count);
            }
        }

        //public void Run()
        //{
        //    // 5, 2, 8, 6, 3, 6b, 9, 7

        //    Node _5 = new Node("5");
        //    Node _2 = new Node("2");
        //    Node _8 = new Node("8");
        //    Node _6 = new Node("6");
        //    Node _3 = new Node("3");
        //    Node _6b = new Node("6b");
        //    Node _9 = new Node("9");
        //    Node _7 = new Node("7");

        //    List<NodeList> nodeList = new List<NodeList>();
        //    nodeList.Add(new NodeList(_5));
        //    nodeList.Add(new NodeList(_2));
        //    nodeList.Add(new NodeList(_8));
        //    nodeList.Add(new NodeList(_6));
        //    nodeList.Add(new NodeList(_3));
        //    nodeList.Add(new NodeList(_6b));
        //    nodeList.Add(new NodeList(_9));
        //    nodeList.Add(new NodeList(_7));

            
        //    int weight = -1;

        //    _5.Edges.Add(new Edge() { Node = _8, Weight = weight });
        //    _5.Edges.Add(new Edge() { Node = _6, Weight = weight });
        //    _5.Edges.Add(new Edge() { Node = _6b, Weight =weight });
        //    _5.Edges.Add(new Edge() { Node = _9, Weight = weight });
        //    _5.Edges.Add(new Edge() { Node = _7, Weight = weight });
            
        //    _2.Edges.Add(new Edge() { Node = _8, Weight = weight });
        //    _2.Edges.Add(new Edge() { Node = _6, Weight = weight });
        //    _2.Edges.Add(new Edge() { Node = _3, Weight = weight });
        //    _2.Edges.Add(new Edge() { Node = _6b, Weight =weight });
        //    _2.Edges.Add(new Edge() { Node = _9, Weight = weight });
        //    _2.Edges.Add(new Edge() { Node = _7, Weight = weight });
                                                          
        //    _8.Edges.Add(new Edge() { Node = _9, Weight = weight });
                                                          
        //    _6.Edges.Add(new Edge() { Node = _9, Weight = weight });
                                                          
        //    _3.Edges.Add(new Edge() { Node = _6b, Weight =weight });
        //    _3.Edges.Add(new Edge() { Node = _9, Weight = weight });
        //    _3.Edges.Add(new Edge() { Node = _7, Weight = weight });
                                                          
        //    _6b.Edges.Add(new Edge() { Node = _9, Weight =weight });
        //    _6b.Edges.Add(new Edge() { Node = _7, Weight =weight });


        //    foreach (NodeList n in nodeList)
        //    {
        //        n.LongestPath = Dijkstra_LongestPath(n.Node);
        //    }

        //    nodeList.Sort();

        //    Console.WriteLine("Longest paths:");
            
        //    foreach (NodeList n in nodeList)
        //    {
        //        Console.Write("Dist (" + n.LongestPath.Count + ") ");
        //        bool first = true;
        //        foreach (Node node in n.LongestPath)
        //        {
        //            if (!first)
        //                Console.Write(",");
        //            first = false;
        //            Console.Write(node.Data);
        //        }
        //        Console.WriteLine();
        //    }

        //}

        public void Run()
        {
            int weight = -1;

            // 5, 2, 8, 6, 3, 6b, 9, 7

            Random rnd = new Random((int)DateTime.Now.Ticks);
            List<NodeList> nodeList = new List<NodeList>();

            for (int x = 0; x < 500; x++)
            {
                Node n = new Node(rnd.Next(1000));
                nodeList.Add(new NodeList(n));
            }


            //Node _5 = new Node(5);
            //Node _2 = new Node(2);
            //Node _8 = new Node(8);
            //Node _6 = new Node(6);
            //Node _3 = new Node(3);
            //Node _6b = new Node(6);
            //Node _9 = new Node(9);
            //Node _7 = new Node(7);

            //List<NodeList> nodeList = new List<NodeList>();
            //nodeList.Add(new NodeList(_5));
            //nodeList.Add(new NodeList(_2));
            //nodeList.Add(new NodeList(_8));
            //nodeList.Add(new NodeList(_6));
            //nodeList.Add(new NodeList(_3));
            //nodeList.Add(new NodeList(_6b));
            //nodeList.Add(new NodeList(_9));
            //nodeList.Add(new NodeList(_7));


            //nodeList.Add(new NodeList(new Node(5)));
            //nodeList.Add(new NodeList(new Node(2)));
            //nodeList.Add(new NodeList(new Node(8)));
            //nodeList.Add(new NodeList(new Node(6)));
            //nodeList.Add(new NodeList(new Node(3)));
            //nodeList.Add(new NodeList(new Node(6)));
            //nodeList.Add(new NodeList(new Node(9)));
            //nodeList.Add(new NodeList(new Node(7)));

            for (int x = 0; x < nodeList.Count; x++)
            {
                for (int y = x + 1; y < nodeList.Count; y++)
                {
                    if (nodeList[y].Node.Data > nodeList[x].Node.Data)
                    {
                        nodeList[x].Node.Edges.Add(new Edge() { Node = nodeList[y].Node, Weight = weight });
                    }
                }
            }

            

            //_5.Edges.Add(new Edge() { Node = _8, Weight = weight });
            //_5.Edges.Add(new Edge() { Node = _6, Weight = weight });
            //_5.Edges.Add(new Edge() { Node = _6b, Weight = weight });
            //_5.Edges.Add(new Edge() { Node = _9, Weight = weight });
            //_5.Edges.Add(new Edge() { Node = _7, Weight = weight });

            //_2.Edges.Add(new Edge() { Node = _8, Weight = weight });
            //_2.Edges.Add(new Edge() { Node = _6, Weight = weight });
            //_2.Edges.Add(new Edge() { Node = _3, Weight = weight });
            //_2.Edges.Add(new Edge() { Node = _6b, Weight = weight });
            //_2.Edges.Add(new Edge() { Node = _9, Weight = weight });
            //_2.Edges.Add(new Edge() { Node = _7, Weight = weight });

            //_8.Edges.Add(new Edge() { Node = _9, Weight = weight });

            //_6.Edges.Add(new Edge() { Node = _9, Weight = weight });

            //_3.Edges.Add(new Edge() { Node = _6b, Weight = weight });
            //_3.Edges.Add(new Edge() { Node = _9, Weight = weight });
            //_3.Edges.Add(new Edge() { Node = _7, Weight = weight });

            //_6b.Edges.Add(new Edge() { Node = _9, Weight = weight });
            //_6b.Edges.Add(new Edge() { Node = _7, Weight = weight });


            DateTime start;
            DateTime end;

            start = DateTime.Now;

            foreach (NodeList n in nodeList)
            {
                n.LongestPath = Dijkstra_LongestPath(n.Node);
            }

            end = DateTime.Now;

            Console.WriteLine("Dijkstra longest path time: " + (end - start).TotalSeconds);

            start = DateTime.Now;

            foreach (NodeList n in nodeList)
            {
                n.LongestPath = Dijkstra_memo(n.Node);
            }

            end = DateTime.Now;
            Console.WriteLine("Memoized longest path time: " + (end - start).TotalSeconds);


            nodeList.Sort();

            Console.WriteLine("Longest path(s):");
            int len = nodeList[0].LongestPath.Count;
            foreach (NodeList n in nodeList)
            {
                if (n.LongestPath.Count < len)
                    break;
                Console.Write("Dist (" + n.LongestPath.Count + ") ");
                bool first = true;
                foreach (Node node in n.LongestPath)
                {
                    if (!first)
                        Console.Write(",");
                    first = false;
                    Console.Write(node.Data);
                }
                Console.WriteLine();
            }

        }


        //Longest Increasing Subsequence
        //Updated algorithm to not use a DAG

        //Good video: https://www.youtube.com/watch?v=CE2b_-XfVDk&list=PLrmLmBdmIlpsHaNTPP_jHHDx_os9ItYXr&index=17
        //
        // This algorithm is O(N^2)
        //
        // O(n log(n)) solution exists but not a DP solution
        //      http://en.wikipedia.org/wiki/Longest_increasing_subsequence#Efficient_algorithms
        public int LongestIncreasingSubSequence(int []arr)
        {
            int length = arr.Length;

            int[] T = new int[length];
            List<int> ret = new List<int>();

            int[] solution = new int[length];


            int i = 0;
            int j = 0;

            int max = -1;
            int maxStart = -1;
            // 1, 4, -1, 8, 5, 6

            for (i=0; i < length; i++)
            {
                T[i] = 1;   //Initialize with 1 since all elements of size 1 are subsequence
                solution[i] = -1;
            }

            //Iterate over array with i from 1 to length
            //Iterate over array with j from 0 to j
            //When we find that arr[j] is less than arr[i], then we check to see if
            //T[j] +1 > T[i].  If it is, then set T[i] to T[j]+1 and track j in the solution array for back tracking
            

            for (i=1; i < length; i++)
            {
                for (j=0; j < i; j++)
                {
                    if (arr[j] < arr[i])    //Find where arr[j] is less than arr[i]
                    {
                        if (T[j] + 1 > T[i])
                        {
                            T[i] = T[j] + 1;    // <---  Only need to set this and the max below to find just the size of the solution
                            solution[i] = j;    //Set solution for i to backpoint to j

                            //If T[i] is greater than max, then it is the new longest sequence
                            //max will be the length of the longest sequence
                            //maxStart is the starting element in the solution array
                            if (T[i] > max)
                            {
                                max = T[i];     // <-- This and the T[i] = T[j]+1 above is all that is needed if only need the size of the solution
                                maxStart = i;
                            }
                            
                        }
                    }

                    
                }
            }

            //Write actual solution in reverse.  Can put in order using stack or just
            //insert to head of a list
            Console.WriteLine("Reverse Solution: ");
            int start = maxStart;
            for (int x = 0; x < max; x++)
            {
                Console.WriteLine(arr[start]);
                start = solution[start];
            }

            Console.Write("----");
            //Returning length of longest sequence
            return max;

        }


        public void Run2()
        {
            Console.WriteLine(LongestIncreasingSubSequence(new int[] { 3, 4, -1, 0, 6, 2, 3 }));

            int size = 20000;
            int[] arr = new int[size];
            Random rnd = new Random((int)DateTime.Now.Ticks);
            for (int x = 0; x < size; x++)
            {
                arr[x] = rnd.Next(size * 2);
            }

            Console.WriteLine(LongestIncreasingSubSequence(arr));


        }


    }
}
