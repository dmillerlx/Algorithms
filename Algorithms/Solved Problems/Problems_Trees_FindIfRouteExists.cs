using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Trees_FindIfRouteExists
    {
        //Given a directed graph, find out if a rotue exists beween two nodes

        //Assumption - directed graph, but not ascylic graph

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
            public override bool Equals(object obj)
            {
                Node other = (Node)obj;
                return this.NodeID == other.NodeID;
            }
        }

        public class Edge
        {
            public Node Node { get; set; }
            public int Weight { get; set; }
        }


        public bool PathExists(Node start, Node end)
        {
            //BFS transversal

            Queue<Node> queue = new Queue<Node>();
            Dictionary<Node, bool> visited = new Dictionary<Node, bool>();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Node currentNode = queue.Dequeue();

                if (currentNode == end)     //Found it!
                    return true;

                //Mark current node as visited
                visited.Add(currentNode, true);

                foreach (Edge e in currentNode.Edges)
                {
                    //Enqueue all nodes not visited
                    if (visited.ContainsKey(e.Node) == false)
                    {
                        queue.Enqueue(e.Node);
                    }
                }
            }

            return false;

        }

        public void Run()
        {
            //Setup directed graph

            Node A = new Node(1);
            Node B = new Node(2);
            Node C = new Node(3);
            Node D = new Node(4);
            Node E = new Node(5);
            Node F = new Node(6);
            


            
            int weight = 1;
            // A -> B -> C -> D -> E -> F
            //A.Edges.Add(new Edge() { Node = B });
            //B.Edges.Add(new Edge() { Node = C });
            //C.Edges.Add(new Edge() { Node = D });
            //D.Edges.Add(new Edge() { Node = E });
            //E.Edges.Add(new Edge() { Node = F });
            //F.Edges.Add(new Edge());

            //A -> B -> C     D -> E -> F
            //A.Edges.Add(new Edge() { Node = B });
            //B.Edges.Add(new Edge() { Node = C });
            
            //D.Edges.Add(new Edge() { Node = E });
            //E.Edges.Add(new Edge() { Node = F });
            //F.Edges.Add(new Edge());


            //A -> B -> C -> D -> E -> F
            //      -> E -> F
            //A.Edges.Add(new Edge() { Node = B });
            //B.Edges.Add(new Edge() { Node = C });
            //C.Edges.Add(new Edge() { Node = D });
            //D.Edges.Add(new Edge() { Node = E });
            //E.Edges.Add(new Edge() { Node = F });
            //F.Edges.Add(new Edge());
            ////second row
            //B.Edges.Add(new Edge() { Node = E });
            //E.Edges.Add(new Edge() { Node = F });

            //A <-> B <-> C   <->   E <-> D
            //             <-> F <->
            
            //left to right 
            A.Edges.Add(new Edge() { Node = B });
            B.Edges.Add(new Edge() { Node = C });
            C.Edges.Add(new Edge() { Node = E });            
            E.Edges.Add(new Edge() { Node = D });
            //right to left
            B.Edges.Add(new Edge() { Node = A });
            C.Edges.Add(new Edge() { Node = B });
            E.Edges.Add(new Edge() { Node = C });
            D.Edges.Add(new Edge() { Node = E });

            //second row
            C.Edges.Add(new Edge() { Node = F });
            F.Edges.Add(new Edge() { Node = C });
            F.Edges.Add(new Edge() { Node = E });
            E.Edges.Add(new Edge() { Node = F });


            Console.WriteLine("Path exists: " + PathExists(A, D));




        }


    }
}
