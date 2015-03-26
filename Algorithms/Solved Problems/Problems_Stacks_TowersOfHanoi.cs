using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_Stacks_TowersOfHanoi
    {

        public class Towers
        {
            Stack<int> A = new Stack<int>();
            Stack<int> B = new Stack<int>();
            Stack<int> C = new Stack<int>();
            /*
             
            -
            --
            ---

                            -
                    --
              
                    -
                    --         
                                -
                               --
                              ---
            ------  -----   -----
            */


            public void Run()
            {
                int rod1 = 1;
                int rod2 = 2;
                int rod3 = 3;
                                        //////Execution for each Step
                                        //        
                A.Push(rod3);           //       -
                A.Push(rod2);           //      --
                A.Push(rod1);           //     ---

                C.Push(A.Pop());        //      --              
                                        //     ---              -

                B.Push(A.Pop());        //
                                        //      ---     --      -

                                        //              -
                B.Push(C.Pop());        //      ---     --


                C.Push(A.Pop());        //             -    
                                        //             --       ---

                A.Push(B.Pop());        //      -       --      ---

                C.Push(B.Pop());        //                       --
                                        //      -               ---

                C.Push(A.Pop());        //                      -
                                        //                      --
                                        //                      ---


            }



        }


        public class Towers2
        {

            public class Tower
            {
                private Stack<int> disks;
                private int index;

                public Tower(int i)
                {
                    disks = new Stack<int>();
                    index = i;
                }

                public int Index()
                {
                    return index;
                }

                public void add(int d)
                {
                    if (!(disks.Count() == 0) && disks.Peek() <= d)
                    {
                        Console.WriteLine("Error placing disk " + d);
                    }
                    else
                    {
                        disks.Push(d);
                    }
                }

                public void moveTopTo(Tower t)
                {
                    int top = disks.Pop();

                    t.add(top);

                    //Console.WriteLine("Move disk " + top + " from " + Index() + " to " + t.Index());
                }

                public void moveDisks(int n, Tower destination, Tower buffer)
                {
                    if (n > 0)
                    {
                        moveDisks(n - 1, buffer, destination);
                        moveTopTo(destination);
                        buffer.moveDisks(n - 1, destination, this);
                    }

                }
            }


            public void Run()
            {
                int n = 5;
                Tower[] towers = new Tower[n];

                for (int i = 0; i < 3; i++)
                {
                    towers[i] = new Tower(i);
                }

                for (int i=n - 1; i>= 0; i--)
                {
                    towers[0].add(i);
                }

                towers[0].moveDisks(n, towers[2], towers[1]);

            }

        }



        public class Towers3
        {

            public class Tower
            {
                Stack <int> peg = new Stack<int>();
                public Tower(string name)
                {
                    Name = name;
                }

                public int Get()
                {
                    if (peg.Count == 0)
                    {
                        Console.WriteLine("Get Failed on ("+Name+") - Tower is empty!");
                        throw new Exception("Failed");
                    }

                    return peg.Pop();
                }

                public void Put(int val)
                {
                    if (peg.Count > 0)
                    {
                        if (peg.Peek() < val)
                        {
                            Console.WriteLine("Put failed on (" + Name + ") - Top is (" + peg.Peek() + ") and put val is (" + val + ")");
                        }
                    }
                    peg.Push(val);
                }

                public string Name { get; set; }

                public override string ToString()
                {
                    Stack<int> tempStack = new Stack<int>();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("Peg (" + Name + "): ");
                    if (peg.Count > 0)
                    {
                        bool first = true;
                        while (peg.Count > 0)
                        {
                            if (!first)
                                sb.Append(",");
                            first = false;
                            sb.Append(peg.Peek());

                            tempStack.Push(peg.Pop());
                        }

                        while (tempStack.Count > 0)
                        {
                            peg.Push(tempStack.Pop());
                        }
                    }
                    else
                    {
                        sb.Append("Empty");
                    }


                    return sb.ToString();
                }

            }

            Tower A = new Tower("A");
            Tower B = new Tower("B");
            Tower C = new Tower("C");


            void MoveTopDisk(Tower Source, Tower Destination)
            {
                int item = Source.Get();

                Destination.Put(item);
                moves++;
            }

            void MoveDisks(int n, Tower Source, Tower Buffer, Tower Destination)
            {
                if (n <= 0)
                    return;

                MoveDisks(n - 1, Source, Destination, Buffer);
                MoveTopDisk(Source, Destination);
                MoveDisks(n - 1, Buffer, Source, Destination);
            }

            int moves = 0;

            public void Run(int n)
            {
                Console.WriteLine("Towers of Hanoi - using (" + n + ") disks:");
                //Setup Tower A with n disks

                for (int x=n; x > 0; x--)
                {
                    A.Put(x);
                }

                Console.WriteLine("Before:");
                Console.WriteLine(A);
                Console.WriteLine(B);
                Console.WriteLine(C);

                MoveDisks(n, A, B, C);

                Console.WriteLine("Compelted in (" + moves + ") moves: ");
                Console.WriteLine(A);
                Console.WriteLine(B);
                Console.WriteLine(C);

            }

        }


        public void Run()
        {
            Towers3 t = new Towers3();
            t.Run(8);
            

        }

    }
}
