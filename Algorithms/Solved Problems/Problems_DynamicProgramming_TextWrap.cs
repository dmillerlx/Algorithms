using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_DynamicProgramming_TextWrap
    {
        public class Solution1
        {

            public Dictionary<int, int> DP;
            public int[] solution;

            public int badness(int[] words, int i, int j, int lineWidth)
            {
                int actualLen = 0;
                for (int x = i; x <= j; x++)
                    actualLen += words[x];

                if (actualLen > lineWidth) return int.MaxValue;
                return (lineWidth - actualLen) * (lineWidth - actualLen);
            }

            public void solveWordWrap(int[] words, int n, int lineWidth, int index)
            {

                if (index > n - 1) return;

                if (index == n - 1) //return the badness of the word as in if that word takes up the entire line
                {
                    DP[index] = (lineWidth - words[n - 1]) * (lineWidth - words[n - 1]);
                    return;
                }

                int min = Int32.MaxValue;
                int breakWhere = 0;

                for (int i = index; i < n; i++)
                {
                    if (!DP.ContainsKey(i + 1)) solveWordWrap(words, n, lineWidth, i + 1);
                    int temp = badness(words, index, i, lineWidth);
                    temp = (temp == int.MaxValue) ? int.MaxValue : temp + DP[i + 1];
                    if (temp < min)
                    {
                        breakWhere = i;
                        min = temp;
                    }
                }
                DP[index] = min;
                solution[breakWhere] = 1;
            }

            public void Main()//string[] args)
            {
                int[] l = { 3, 2, 2, 5 };
                solution = new int[4];
                for (int i = 0; i < 4; i++)
                    solution[i] = 0;
                int M = 6;
                DP = new Dictionary<int, int>();
                solveWordWrap(l, 4, M, 0);
                Console.WriteLine(DP[0]);
                for (int i = 0; i < 4; i++)
                    Console.WriteLine(solution[i]);
                Console.Read();
            }
        }

        public class Solution2
        {
            public int PageWidth { get; set; }

            public string[] words = null;

            public double Badness(int i, int j)
            {
                StringBuilder sb = new StringBuilder();
                int totalWidth = 0;
                for (int x = i; x < j && totalWidth < PageWidth; x++)
                {
                    totalWidth += words[x].Length;
                    if (x != i)
                    {
                        totalWidth += 1;    //Add space if not first word
                    }
                    sb.Append(words[x] + " ");
                }

                double badnessRet = double.MaxValue;

                if (totalWidth < PageWidth)
                    badnessRet = Math.Pow((PageWidth - totalWidth), 3);

               // Console.WriteLine("String:" + sb.ToString() + " Len:" + sb.Length + " Badness:" + badnessRet);

                return badnessRet;
            }

            //List<int> lineStartIndex = new List<int>();
            //List<BadnessItem> lineWraps = new List<BadnessItem>();

            public class BadnessItem : IComparable
            {
                public int i;
                public int j;
                public double badness;
                public BadnessItem(int newI, int newJ, double newBadness)
                {
                    i = newI;
                    j = newJ;
                    badness = newBadness;
                }

                public int CompareTo(object obj)
                {
                    BadnessItem o = (BadnessItem)(obj);
                    return badness.CompareTo(o.badness);
                }
            }

            public List<BadnessItem> findLineStarts()
            {
                List<BadnessItem> lineWraps = new List<BadnessItem>();

                int start = 0;

                do
                {
                    bool doneWithLine = false;
                    BadnessItem minBadnessItem = null;
                    int i = start;
                    for (int j = i + 1; j < words.Count() && !doneWithLine; j++)
                    {
                        double badness = Badness(i, j);
                        if (badness < double.MaxValue)
                        {
                            if (minBadnessItem == null)
                                minBadnessItem = new BadnessItem(i, j, badness);
                            else
                                minBadnessItem = (minBadnessItem.badness < badness) ? minBadnessItem : new BadnessItem(i, j, badness);
                        }
                        else
                        {
                            doneWithLine = true;
                        }
                    }
                
                    if (minBadnessItem == null)
                        return null;

                    //Console.WriteLine("Min badness: i=" + minBadnessItem.i + " j=" + minBadnessItem.j + " Badness=" + minBadnessItem.badness);

                    //PrintWords(minBadnessItem.i, minBadnessItem.j);

                    lineWraps.Add(minBadnessItem);
                    start = minBadnessItem.j;

                } while (start < (words.Count() - 1));

                return lineWraps;
            }

            public List<BadnessItem> findLineStartsV2()
            {
                List<BadnessItem> lineWraps = new List<BadnessItem>();

                int start = 0;

                do
                {
                    for (int i = 0; i < words.Count(); i++)
                    {
                        for (int j = i; j < words.Count(); j++)
                        {
                            double badness = Badness(i, j);

                        }
                    }


                    //bool doneWithLine = false;
                    //BadnessItem minBadnessItem = null;
                    //int i = start;
                    //for (int j = i + 1; j < words.Count() && !doneWithLine; j++)
                    //{
                    //    double badness = Badness(i, j);
                    //    if (badness < double.MaxValue)
                    //    {
                    //        if (minBadnessItem == null)
                    //            minBadnessItem = new BadnessItem(i, j, badness);
                    //        else
                    //            minBadnessItem = (minBadnessItem.badness < badness) ? minBadnessItem : new BadnessItem(i, j, badness);
                    //    }
                    //    else
                    //    {
                    //        doneWithLine = true;
                    //    }
                    //}

                    //if (minBadnessItem == null)
                    //    return null;

                    ////Console.WriteLine("Min badness: i=" + minBadnessItem.i + " j=" + minBadnessItem.j + " Badness=" + minBadnessItem.badness);

                    ////PrintWords(minBadnessItem.i, minBadnessItem.j);

                    //lineWraps.Add(minBadnessItem);
                    //start = minBadnessItem.j;

                } while (start < (words.Count() - 1));

                return lineWraps;
            }


            public void PrintWords(int i, int j)
            {
                for (int x = i; x < j && x < words.Count(); x++)
                {
                    if (x != i)
                        Console.Write(" ");
                    Console.Write(words[x]);
                }
            }

            public void Run(string words)
            {
                this.words = words.Split(new string[] { " " }, StringSplitOptions.None);

                List<BadnessItem> lineWraps = findLineStarts();

                for (int x = 0; x < lineWraps.Count; x++)
                {
                    PrintWords(lineWraps[x].i, lineWraps[x].j);
                    Console.WriteLine();
                }


            }


        }


        public class Solution3
        {
            public class Visited
            {
                public Node Node { get; set; }
                public double Distance { get; set; }
                public Node PrevNode { get; set; }
            }

            public class Node
            {
                static int nodeid = 0;
                public Node(BadnessItem data, string val)
                {
                    Data = data;
                    Edges = new List<Edge>();
                    NodeID = nodeid++;
                    Val = val;
                }

                public BadnessItem Data { get; set; }
                public List<Edge> Edges { get; set; }
                public int NodeID { get; set; }

                public string Val { get; set; }

                public override string ToString()
                {
                    return Val;
                }
            }

            public class Edge
            {
                public Edge(Node newNode, double newWeight)
                {
                    Node = newNode;
                    Weight = newWeight;
                }
                public Node Node { get; set; }
                public double Weight { get; set; }

                public override string ToString()
                {
                    return Node.ToString();
                }
            }


            public void Dijkstra(Node start, Node end)
            {
                Visited[] visited = new Visited[100000];

                Queue<Node> queue = new Queue<Node>();

                queue.Enqueue(start);

                while (queue.Count > 0)
                {

                    Node currentNode = queue.Dequeue();

                    //Set distance for self if not in list already
                    if (visited[currentNode.NodeID] == null)
                    {
                        visited[currentNode.NodeID] = new Visited() { Node = currentNode, Distance = 0, PrevNode = null };
                    }

                    //Now check each edge to see if we have a short distance with this path
                    foreach (Edge e in currentNode.Edges)
                    {
                        if (e.Node == end)
                        {
                            end = end;
                        }

                        //Not in visited, so add it
                        if (visited[e.Node.NodeID] == null)
                        {
                            double distance = visited[currentNode.NodeID].Distance + e.Weight;
                            visited[e.Node.NodeID] = new Visited() { Node = e.Node, Distance = distance /*e.Weight*/, PrevNode = currentNode };
                            //Follow this path since it is new
                            queue.Enqueue(e.Node);
                        }
                        else
                        {                            
                            double currentWeight = visited[currentNode.NodeID].Distance;
                            double newWeight = currentWeight + e.Weight;
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
                    Console.WriteLine();
                    Console.WriteLine("-----------------");
                    Console.WriteLine("Shortest Path from (" + start.Data + ") to (" + end.Data + ") distance (" + visited[end.NodeID].Distance + "): ");
                    Node n = end;

                    Stack<string> output = new Stack<string>();

                    while (n != null)
                    {
                        //if (n.NodeID != end.NodeID)
                        //    Console.Write(",");
                        if (n.Data.i >= 0)
                        {
                            //PrintWords(n.Data.i, n.Data.j);
                            //Console.WriteLine();
                            output.Push(n.ToString());
                        }
                        else if (n == end)
                            Console.WriteLine("END");
                        else if (n == start)
                            Console.WriteLine("START");
                        //Console.Write(n.Data);
                        n = visited[n.NodeID].PrevNode;
                    }

                    while (output.Count() > 0)
                    {
                        Console.WriteLine(output.Pop());
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
                            double currentWeight = visited[currentNode.NodeID].Distance;
                            double newWeight = currentWeight + e.Weight;
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
                            double currentWeight = visited[currentNode.NodeID].Distance;
                            double newWeight = currentWeight + e.Weight;
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

            public int PageWidth { get; set; }

            public string[] words = null;

            public double Badness(int i, int j)
            {
                StringBuilder sb = new StringBuilder();
                int totalWidth = 0;
                for (int x = i; x <= j && totalWidth <= PageWidth; x++)
                {
                    totalWidth += words[x].Length;
                    if (x != i)
                    {
                        totalWidth += 1;    //Add space if not first word
                        sb.Append(" ");
                    }
                    sb.Append(words[x]);
                    if (sb.ToString() == "verification or scrutiny will protect")
                        sb = sb;

                }

                double badnessRet = double.MaxValue;

                if (totalWidth <= PageWidth)
                    badnessRet = Math.Pow((PageWidth - totalWidth), 3);

                // Console.WriteLine("String:" + sb.ToString() + " Len:" + sb.Length + " Badness:" + badnessRet);

                return badnessRet;
            }

            //List<int> lineStartIndex = new List<int>();
            //List<BadnessItem> lineWraps = new List<BadnessItem>();

            public class BadnessItem : IComparable
            {
                public int i;
                public int j;
                public double badness;
                public BadnessItem(int newI, int newJ, double newBadness)
                {
                    i = newI;
                    j = newJ;
                    badness = newBadness;
                }

                public int CompareTo(object obj)
                {
                    BadnessItem o = (BadnessItem)(obj);
                    return badness.CompareTo(o.badness);
                }
            }

            public List<BadnessItem> findLineStarts()
            {
                List<BadnessItem> lineWraps = new List<BadnessItem>();

                int start = 0;

                do
                {
                    bool doneWithLine = false;
                    BadnessItem minBadnessItem = null;
                    int i = start;
                    for (int j = i + 1; j < words.Count() && !doneWithLine; j++)
                    {
                        double badness = Badness(i, j);
                        if (badness < double.MaxValue)
                        {
                            if (minBadnessItem == null)
                                minBadnessItem = new BadnessItem(i, j, badness);
                            else
                                minBadnessItem = (minBadnessItem.badness < badness) ? minBadnessItem : new BadnessItem(i, j, badness);
                        }
                        else
                        {
                            doneWithLine = true;
                        }
                    }

                    if (minBadnessItem == null)
                        return null;

                    //Console.WriteLine("Min badness: i=" + minBadnessItem.i + " j=" + minBadnessItem.j + " Badness=" + minBadnessItem.badness);

                    //PrintWords(minBadnessItem.i, minBadnessItem.j);

                    lineWraps.Add(minBadnessItem);
                    start = minBadnessItem.j;

                } while (start < (words.Count() - 1));

                return lineWraps;
            }

            Node head = null;

            public string GetWords(int i, int j)
            {
                StringBuilder sb = new StringBuilder();
                for (int x = i; x <= j && x < words.Count(); x++)
                {
                    if (x != i)
                        sb.Append(" ");
                    sb.Append(words[x]);
                }
                return sb.ToString();
            }

            public void CreateSuffixs()
            {
                
                double badness = 0;
                badness = Badness(0,0);

                //Start of DAG
                head = new Node(new BadnessItem(0, 0, badness), "<HEAD>");

                Node current = head;
                Node end = new Node(new BadnessItem(-1, -1, 0), "<END>");

                for (int i = 1; i < words.Count(); i++)
                {
                    Node n = null;
                    for (int j = i; j < words.Count(); j++)
                    {
                        badness = Badness(i, j);
                        n = new Node(new BadnessItem(i, j, badness), GetWords(i, j));
                        current.Edges.Add(new Edge(n, badness));
                    }

                    n.Edges.Add(new Edge(end, 0));  //add edge to end
                }

            }


            public void CreateSuffixs2(Node end, int inputJ)
            {
                if (inputJ < 0)
                    return;

                int j = inputJ;

                for (int i = inputJ; i >= 0; i--)
                {
                    Node n = null;
                    //for (int j = i; j < words.Count(); j++)
                    //{

                        double badness = Badness(i, j);
                        //badness = (-1) * badness;
                        n = new Node(new BadnessItem(i, j, badness), GetWords(i, j)); //create new node for i, j

                        if (badness < double.MaxValue && badness < 10000)
                        {
                            n.Edges.Add(new Edge(end, end.Data.badness));// link new node into next node with next.node badness weight

                            if (i == 0)
                            {
                                head.Edges.Add(new Edge(n, n.Data.badness));    //If this is a suffix starting at i=0, add link from head
                            }


                            CreateSuffixs2(n, i - 1);  //create suffix going into this node starting at i-1
                        }
                    //}

                }

            }

            public void Create()
            {
                head = new Node(new BadnessItem(-1, -1, 0), "<HEAD>");
                Node end = new Node(new BadnessItem(-1, -1, 0), "<END>");

                CreateSuffixs2(end, words.Count() - 1);

                //List <Node> path = Dijkstra_LongestPath(head);

                Dijkstra(head, end);

                //for (int x=0; x < path.Count; x++)
                //{
                //    if (path[x].Data.i >= 0)
                //    {
                //        PrintWords(path[x].Data.i, path[x].Data.j);
                //        Console.WriteLine();
                //    }
                //}

            }


            public void PrintWords(int i, int j)
            {
                for (int x = i; x <= j && x < words.Count(); x++)
                {
                    if (x != i)
                        Console.Write(" ");
                    Console.Write(words[x]);
                }
            }

            public void Run(string words)
            {
                this.words = words.Split(new string[] { " " }, StringSplitOptions.None);

                //List<BadnessItem> lineWraps = findLineStarts();

                //for (int x = 0; x < lineWraps.Count; x++)
                //{
                //    PrintWords(lineWraps[x].i, lineWraps[x].j);
                //    Console.WriteLine();
                //}

                Create();



            }


        }



        public void Run()
        {
            Solution3 s = new Solution3();
            s.PageWidth = 6;

            s.Run("aaa bb cc ddddd");

            s.PageWidth = 37;
            s.Run("You cant trust code that you did not create yourself. (Especially code from companies that employ people like me.) No amount of source-level verification or scrutiny will protect you from untrusted code. ");
           
            
            //s.Run("The time has come for all good Americans to come to the aid of their country for which it stands one nation under God, indivisiable, and justice for all.  This is the end.");


            //s.Run("This is pretty common, but for some reason, changing the version to 2.0.0.0 made things work again. I don't know if it's a Windows 7 specific thing (I've only been using it for 3-4 weeks), or if it's random, or what, but it fixed it for me. I'm guessing that VS was keeping a handle on each file it generated, so it would know how to increment things? I'm really not sure and have never seen this happen before. But if someone else out there is also pulling their hair out, give it a try. This is pretty common, but for some reason, changing the version to 2.0.0.0 made things work again. I don't know if it's a Windows 7 specific thing (I've only been using it for 3-4 weeks), or if it's random, or what, but it fixed it for me. I'm guessing that VS was keeping a handle on each file it generated, so it would know how to increment things? I'm really not sure and have never seen this happen before. But if someone else out there is also pulling their hair out, give it a try. This is pretty common, but for some reason, changing the version to 2.0.0.0 made things work again. I don't know if it's a Windows 7 specific thing (I've only been using it for 3-4 weeks), or if it's random, or what, but it fixed it for me. I'm guessing that VS was keeping a handle on each file it generated, so it would know how to increment things? I'm really not sure and have never seen this happen before. But if someone else out there is also pulling their hair out, give it a try. This is pretty common, but for some reason, changing the version to 2.0.0.0 made things work again. I don't know if it's a Windows 7 specific thing (I've only been using it for 3-4 weeks), or if it's random, or what, but it fixed it for me. I'm guessing that VS was keeping a handle on each file it generated, so it would know how to increment things? I'm really not sure and have never seen this happen before. But if someone else out there is also pulling their hair out, give it a try.");

        }


    }
}
