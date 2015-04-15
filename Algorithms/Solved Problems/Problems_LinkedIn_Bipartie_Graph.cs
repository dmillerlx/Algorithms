using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_Bipartie_Graph
    {
        public class Solution1
        {

            public class Node 
            {
                public List<Node> edges = null;

                public int Value { get; set; }

                public Node (int Value)
                {
                    this.Value = Value;
                    edges = new List<Node>();
                }

            }

            public bool IsBipartite(Node root)
            {
                Dictionary<Node, int> visited = new Dictionary<Node, int>();

                Queue<Node> queue = new Queue<Node>();

                queue.Enqueue(root);

                while (queue.Count > 0)
                {
                    Node current = queue.Dequeue();

                    int currentColor = 0;
                    if (visited.ContainsKey(current) == false)
                    {
                        visited.Add(current, currentColor);
                    }
                    else
                    {
                        currentColor = visited[current];
                    }

                    foreach (Node e in current.edges)
                    {
                        if (visited.ContainsKey(e))
                        {
                            int childColor = visited[e];
                            if (childColor == currentColor)
                                return false;   //Found a child that is the same color
                        }
                        else
                        {
                            visited.Add(e, currentColor == 0 ? 1 : 0);
                            queue.Enqueue(e);
                        }
                    }
                }

                //No children with the same color found, so graph is bipartite
                return true;
            }


            public void Run()
            {
                Node r1 = new Node(0);
                Node r2 = new Node(1);

                Node s1 = new Node(2);
                Node s2 = new Node(3);
                Node s3 = new Node(4);
                Node s4 = new Node(5);
                Node s5 = new Node(6);

                r1.edges.Add(s1);
                r1.edges.Add(s2);
                r1.edges.Add(s3);
                r2.edges.Add(s3);
                r2.edges.Add(s4);
                r2.edges.Add(s5);

                s1.edges.Add(r1);
                s2.edges.Add(r1);
                s3.edges.Add(r1);
                s3.edges.Add(r2);
                s4.edges.Add(r2);
                s5.edges.Add(r2);

                Console.WriteLine("IsBipartite: " + IsBipartite(r1));

                r1.edges.Add(r2);
                Console.WriteLine("IsBipartite: " + IsBipartite(r1));
            }




        }


        public void Run()
        {
            Solution1 s1 = new Solution1();
            s1.Run();
            
        
        
        }

    }
}
