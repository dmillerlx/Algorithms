using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_DynamicProgramming_EditDistance
    {
        public class Solution1
        {

            //Visited class is used with Dijkstra to find the shortest path
            public class Visited
            {
                public Node Node { get; set; }

                public Node PrevNode { get; set; }

                public int Distance { get; set; }

                public string Action { get; set; }
            }

            //Each Node represents a suffix of the string
            //and has a list of edges that connect to other Nodes
            public class Node
            {
                static int nodeid = 0;

                public int I { get; set; }
                public int J { get; set; }

                public Node(int newI, int newJ)
                {
                    I = newI;
                    J = newJ;
                    NodeID = nodeid++;
                }

                public Node(int newI, int newJ, string val)
                {
                    I = newI;
                    J = newJ;
                    NodeID = nodeid++;
                    this.Val = val;
                }


                public int NodeID { get; set; }             // NodeID is a unique ID for each Node

                public string Val { get; set; }

                public override string ToString()
                {
                    return I.ToString() + ", " + J.ToString();
                }
            }

            //Edge is a connection between Nodes.  
            //Each edge has a weight associated with it
            public class Edge
            {
                public Edge(Node newNode, string Action)
                {
                    Node = newNode;
                    this.Action = Action;
                }
                public Node Node { get; set; }
                public string Action { get; set; }

                public override string ToString()
                {
                    return Action + Node.ToString();
                }
            }

            //Dijkstra's algorithm finds the shortest path from the start node to the
            //end node.  It only works on a DAG and uses a Breadith First transversal of the graph
            //Visited nodes are stored in the 'visited' dictionary/hash and have a previous pointer
            //and distance traveled from the start.  If a shorter distance is found to a node
            //the node visited node is updated with the shortest path and smallest distance
            //
            //After all nodes are mapped, we check to see if the 'end' node was visited
            //If it was not visited, then there is no path
            //If it was visited, then we follow the 'PrevNode' pointers back to the start
            //Following these pointers is the shortest path through the graph
            public Dictionary<int, Visited> Dijkstra(Node start, Node end, int M, int N)
            {
                Dictionary<int, Visited> visited = new Dictionary<int, Visited>();

                Queue<Node> queue = new Queue<Node>();

                queue.Enqueue(start);

                while (queue.Count > 0)
                {
                    Node currentNode = queue.Dequeue();

                    //Set distance for self if not in list already
                    if (visited.ContainsKey(currentNode.NodeID) == false)
                    {
                        visited[currentNode.NodeID] = new Visited() { Node = currentNode, Distance = 0, PrevNode = null };
                    }


                    //3 edges in this graph - up (i-1), left (j-1), diagonal (i-1, j-1)
                    List<Edge> edges = new List<Edge>();
                    int i = currentNode.I;
                    int j = currentNode.J;

                    if (i < M)
                        edges.Add(new Edge(newNode(i + 1, j), "insert"));   //up
                    if (j < N)
                        edges.Add(new Edge(newNode(i, j + 1), "delete"));   //left
                    if (i < M && j < N)
                        edges.Add(new Edge(newNode(i + 1, j + 1), "replace"));   //diagonal

                    foreach (Edge e in edges)
                    {
                        Node n = e.Node;
                        //Not in visited, so add it
                        if (visited.ContainsKey(n.NodeID) == false)
                        {
                            int distance = visited[currentNode.NodeID].Distance + matrix[n.I, n.J];
                            visited[n.NodeID] = new Visited() { Node = n, Distance = distance, PrevNode = currentNode, Action=e.Action };
                            //Follow this path since it is new
                            queue.Enqueue(n);
                        }
                        else
                        {
                            int currentWeight = visited[currentNode.NodeID].Distance;
                            int newWeight = currentWeight + matrix[n.I, n.J];
                            if (newWeight < visited[n.NodeID].Distance)
                            {
                                visited[n.NodeID].Distance = newWeight;
                                visited[n.NodeID].PrevNode = currentNode;

                                //Follow this path since it is shorter
                                //queue.Enqueue(n);
                            }
                        }
                    }

                }

                //Now check to see if the 'end' node was visited
                if (visited.ContainsKey(end.NodeID) == false)
                {
                    return null;
                    //Console.WriteLine("No path found from (" + start.ToString() + ") to (" + end.ToString() + ")");
                }
                else
                {
                    return visited;
                }

            }

            void PrintPath(Dictionary<int, Visited> visited, Node start, Node end)
            {
                if (visited == null)
                {
                    Console.WriteLine("No Path");
                }

                //Shortest path found
                //Walk backwards through the 'PrevNode' pointers from 'end' to 'start'
                //in the 'visited' dictionary/hashtable.
                //Place the output from each step into a stack so we can reverse the results to print
                //from start to end

                Console.WriteLine();
                Console.WriteLine("-----------------");
                Console.WriteLine("Shortest Path from (" + start + ") to (" + end + ") distance (" + visited[end.NodeID].Distance + "): ");
                Node n = end;

                Stack<string> output = new Stack<string>();

                Visited v = null;

                while (n != null)
                {
                    v = visited[n.NodeID];

                    //output.Push(matrix[n.I, n.J].ToString() + " - " + v.Action);
                    //Console.WriteLine(n.I + ", " + n.J + " " + matrix[n.I, n.J] + " >" + v.Distance + " " + v.Action);
                    output.Push(n.I + ", " + n.J + " " + matrix[n.I, n.J] + " >" + v.Distance + " " + v.Action);

                    n = visited[n.NodeID].PrevNode;
                    
                }

                //Finally print the results to the console
                while (output.Count() > 0)
                {
                    Console.WriteLine(output.Pop());
                }

            }


            //Head and End of DAG
            //This DAG has multiple start and end points, so a dummy node is added to the start
            //and end of the DAG with a 0 edge weight to each start and end node
            //This allows us to find the shortest path between two nodes
            Node head = null;
            Node end = null;
            
            //This function creates a new node for i and j
            //Nodes are memoized in the nodeHash dictionary
            //so they are only created once for each i,j pair
            Dictionary<string, Node> nodeHash = null;
            Node newNode(int i, int j)
            {
                string key = i.ToString() + "_" + j.ToString();
                if (nodeHash.ContainsKey(key))
                    return nodeHash[key];

                Node n = new Node(i, j);
                nodeHash[key] = n;
                return n;
            }


            int[,] matrix = null;

            public void Run(string a, string b)
            {
                nodeHash = new Dictionary<string, Node>();

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("----------START--------------");

                Console.WriteLine("Edit distance for:");
                Console.WriteLine(a + " --> " + b);
                Console.WriteLine();

                int M = a.Length;
                int N = b.Length;
                matrix = new int[M + 1, N + 1];

                //Initalize the matrix
                for (int i = 0; i <= M; i++)
                    matrix[i, 0] = i;
                for (int j = 0; j <= N; j++)
                    matrix[0, j] = j;


                for (int i = 1; i <= M; i++)
                {
                    for (int j = 1; j <= N; j++)                    
                    {
                        //define edit distance cost here:
                        int insert = matrix[i - 1, j] + 1;
                        int delete = matrix[i, j - 1] + 1;

                        bool equals = a[i - 1] == b[j - 1]; //using -1 since strings are offset of 0

                        int replace = matrix[i - 1, j - 1] + (equals ? 0 : 2);

                        int min = Math.Min(insert, delete);
                        min = Math.Min(min, replace);

                        matrix[i, j] = min;
                    }
                }


                //Edit distance solution is in position (M,N)
                Console.WriteLine("Edit Distance: " + matrix[M, N]);

                //Priting matrix for debugging
                Console.WriteLine("----------------------------------");
                for (int i = 0; i <= M; i++)
                {
                    Console.WriteLine();
                    for (int j=0; j <= N; j++)
                    {
                        if (j != 0)
                        {
                            Console.Write(" ");
                            if (matrix[i, j] < 10)
                                Console.Write(" ");
                        }
                        Console.Write(matrix[i, j]);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("----------------------------------");

                //To review the edit changes, use Dijkstra to find shortest path through the array
                //Back pointers and direction show the type of operation
                Node start = newNode(0, 0);
                Node end = newNode(M, N);
                Dictionary<int, Visited> path = Dijkstra(start, end, M, N);

                PrintPath(path, start, end);

                Console.WriteLine("----------END--------------");
            }

        }

        public void Run()
        {
            Solution1 s1 = new Solution1();

            s1.Run("INTENTION", "EXECUTION");

            s1.Run("MOP", "AMOP");

            s1.Run("EXECUTION", "INTENTION");

            s1.Run("INTENTION", "INTENTIOP");

            s1.Run("Saturday", "Sunday");


        }


    }
}
