using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Solved_Problems
{
    class Problems_LinkedIn_DeepIterator
    {
        //Write a deep iterator

        // http://blog.baozitraining.org/2014/08/linkedin-twitter-and-hulu-onsite-how-to.html

        //Write a deep iterator to iterate through a list of objects or integer which could be another list or integer. 
        //This is frequently asked by LinkedIn, Twitter and Hulu. 


        // For example, this collection contains Integer or another collection. L means it is a collection that contains either integer or another L. 

        //                          ----> 5
        //                          |
        //          ---- 3 -> 4  -> L  -> 6
        //          |
        //1 -> 2 -> L -> 7-> 8

        //We would expect an iterator to loop through it will print out 1, 2, 3, 4, 5, 6, 7, 8 

        
        public class IteratorNode
        {
            public MyList<IteratorNode> nodes = null;
            public int Data = default(int);

            public IteratorNode(MyList<IteratorNode> newNodes, int newData)
            {
                nodes = newNodes;
                Data = newData;
            }

            public IteratorNode(MyList<IteratorNode> newNodes)
            {
                nodes = newNodes;
            }

            public IteratorNode(int newData)
            {
                nodes = null;
                Data = newData;
            }            
        }

        public class MyList<T>
        {
            public List<T> list = null;
            public int currentItem = -1;

            public MyList()
            {
                list = new List<T>();
            }

            public MyList(List<T> newList)
            {                
                list = newList;
                if (newList.Count > 0)
                    currentItem = 0;
                else currentItem = -1;
            }

            public T this[int index]
            {
                get { return list[index]; }
            }

            public void Inset(int index, T item)
            {
                list.Insert(index, item);
            }

            public void RemoveAt(int index)
            {
                list.RemoveAt(index);
            }

            public int Count { get { return list.Count; } }

        }

        //public void DeepIterator_Method(List<Node> headNodeList)
        //{
            
        //    for (int x=0; x < headNodeList.Count; x++)
        //    {
        //        if (headNodeList[x].nodes == null)
        //        {
        //            if (!first)
        //                Console.Write(",");
        //            first = false;
        //            Console.Write(headNodeList[x].Data);
        //        }
        //        else
        //        {
        //            DeepIterator_Method(headNodeList[x].nodes);
        //        }
        //    }          
        //}

        //public int currentElement = 0;
        //public MyList<MyList<IteratorNode>> listList = new MyList<MyList<IteratorNode>>();
          
        



        //public bool hasNext()
        //{
        //    if (listList.Count == 0)
        //        return false;

        //    MyList<IteratorNode> currentList = listList[0];
        //    //                   L
        //    //               L-> L
        //    //               L -> 4 -> 5
        //    //               L -> 6 -> 7
        //    //1 -> 2 -> 3 -> L -> 8 -> 9

        //    if (currentList.currentItem < 0)
        //        return false;

        //    if (currentList.currentItem >= currentList.list.Count)
        //        return false;

        //    if (currentList.list[currentList.currentItem].nodes == null)  //node == null means we are on a data element, no an 'L'
        //        return true;

        //    Stack<MyList<IteratorNode>> stack = new Stack<MyList<IteratorNode>>();
        //    while (currentList.list[currentList.currentItem].nodes != null)
        //    {
        //        stack.Push(currentList);
        //        currentList = currentList.list[currentList.currentItem].nodes;
        //    }

        //    while (stack.Count > 0)
        //    {
        //        currentList = stack.Pop();
        //        if (currentList.list.Count > 0)
        //    }



        //    return false;
        //}



        //public int next()
        //{
        //    if (listList.Count == 0)
        //        return int.MinValue;

        //    MyList<IteratorNode> currentList = listList[0];

        //    while (currentList != null && (currentList.currentItem < 0 || currentList.currentItem > currentList.list.Count))      
        //    {
        //        listList.RemoveAt(0);
        //        currentList = listList[0];
        //    }

        //    if (currentList == null)
        //        return int.MinValue;

        //    //while (currentList != null && (currentList.list[currentList.currentItem].nodes != null))
        //    //{
        //    //    //listList.Insert(0,


        //    //}

        //}

        public class Solution1
        {
            bool first = true;

            public class MyNode
            {
                public int Data;
                public List<MyNode> SubList = null;

                public MyNode() { }

                public MyNode(List<MyNode> newSubList)
                {
                    SubList = newSubList;
                }

                public MyNode(int newData)
                {
                    Data = newData;
                }

                public bool hasSubList { get { return SubList != null; } }

            }

            public int CurrentIteration = -1;
            public Stack<int> IterationStack = new Stack<int>();

            public List<MyNode> MainList = null;
            public Stack<List<MyNode>> ListStack = new Stack<List<MyNode>>();

            public int next()
            {
                if (ListStack == null || ListStack.Count == 0)
                    return int.MinValue;

                if (IterationStack == null || IterationStack.Count == 0)
                    return int.MinValue;

                List<MyNode> list = ListStack.Pop();
                int iterator = IterationStack.Pop();

                iterator++;

                if (iterator >= list.Count)
                {
                    list = null;
                    bool foundItems = false;
                    while (ListStack.Count > 0 && !foundItems)
                    {
                        list = ListStack.Pop();
                        iterator = IterationStack.Pop();

                        iterator++;

                        if (list.Count > 0 && iterator < list.Count)
                            foundItems = true;
                    }

                    if (!foundItems)
                        return int.MinValue;
                }


                //               L
                //               L
                //               L -> 4 -> 5
                //               L -> 6 -> 7
                //1 -> 2 -> 3 -> L -> 8 -> 9
                do
                {

                    while (iterator < list.Count && list[iterator].hasSubList)
                    {
                        ListStack.Push(list);
                        IterationStack.Push(iterator);

                        list = list[iterator].SubList;
                        iterator = 0;
                    }

                    ListStack.Push(list);
                    IterationStack.Push(iterator - 1);

                    bool done = false;
                    while (!done)
                    {
                        list = ListStack.Pop();
                        iterator = IterationStack.Pop();

                        iterator++;

                        if (iterator < list.Count && list[iterator].hasSubList == false)
                        {
                            ListStack.Push(list);
                            IterationStack.Push(iterator);
                            return list[iterator].Data;
                        }
                        else if (iterator < list.Count && list[iterator].hasSubList)
                        {
                            done = true;
                        }
                    }

                }
                while (list[iterator].hasSubList);

                return int.MinValue;

            }

            public bool hasNext()
            {
                //Copy the stacks
                Stack<List<MyNode>> tmp = new Stack<List<MyNode>>();
                Stack<List<MyNode>> tmp2 = new Stack<List<MyNode>>();

                Stack<int> tmp3 = new Stack<int>();
                Stack<int> tmp4 = new Stack<int>();

                while (ListStack.Count > 0)
                {
                    tmp.Push(ListStack.Peek());
                    tmp2.Push(ListStack.Pop());
                }

                while (IterationStack.Count > 0)
                {
                    tmp3.Push(IterationStack.Peek());
                    tmp4.Push(IterationStack.Pop());
                }

                while (tmp.Count > 0)
                {
                    ListStack.Push(tmp.Pop());
                }

                while (tmp3.Count > 0)
                {
                    IterationStack.Push(tmp3.Pop());
                }


                bool ret = false;
                if (next() != int.MinValue)
                    ret = true;

                ListStack.Clear();
                IterationStack.Clear();
                while (tmp2.Count > 0)
                {
                    ListStack.Push(tmp2.Pop());
                }

                while (tmp4.Count > 0)
                {
                    IterationStack.Push(tmp4.Pop());
                }

                return ret;
            }


            public void Run()
            {
                //First list    0 - 4
                List<MyNode> list = new List<MyNode>();
                for (int x = 0; x < 5; x++)
                {
                    list.Add(new MyNode(x));
                }

                //Second list   5-7
                List<MyNode> list2 = new List<MyNode>();
                for (int x = 5; x < 8; x++)
                {
                    list2.Add(new MyNode(x));
                }

                //3rd List      8-9
                List<MyNode> list3 = new List<MyNode>();
                for (int x = 8; x < 10; x++)
                {
                    list3.Add(new MyNode(x));
                }

                //                              5-7                                    
                //Add list2 to list1        0-4 L
                list.Add(new MyNode(list2));


                //Add more elements to list1    10-11
                for (int x = 10; x < 12; x++)
                {
                    list.Add(new MyNode(x));
                }

                //                                  8-9
                //                              5-7 L
                //Add list3 to list2        0-4 L       10-11
                list2.Add(new MyNode(list3));
                //Add more elements to list2


                List<MyNode> list6 = new List<MyNode>();
                List<MyNode> list5 = new List<MyNode>();
                List<MyNode> list4 = new List<MyNode>();
                //list4.Add(new MyNode(12));

                list4.Add(new MyNode(list5));   //add 5 to 4
                list5.Add(new MyNode(list6));   //add 6 to 5

                list5.Add(new MyNode(12));

                list.Add(new MyNode(list4));
                list.Add(new MyNode(13));
                list.Add(new MyNode(14));

                Console.WriteLine();
                first = true;
                //DeepIterator_Method(list);

                PrintAll(list);
                Console.WriteLine();
            }

            public void PrintAll(List<MyNode> headList)
            {
                ListStack.Push(headList);
                IterationStack.Push(-1);

                int val = 0;
                first = true;
                while (hasNext())
                {
                    val = next();
                    if (val != int.MinValue)
                    {
                        if (!first)
                            Console.Write(", ");
                        first = false;
                        Console.Write(val);
                    }

                }
            }
        }


        public class Solution2
        {

            public Stack<List<object>> ListStack = new Stack<List<object>>();
            public Stack<int> IterationStack = new Stack<int>();

            public object top = null;

            public bool hasNext()
            {
                if (top != null) return true;

                if (ListStack == null || ListStack.Count == 0)
                    return false;

                if (IterationStack == null || IterationStack.Count == 0)
                    return false;

                //Get top item from stack
                List<object> list = ListStack.Pop();
                int iterator = IterationStack.Pop();

                //Increment the iterator
                iterator++;

                //Iterator exceeds list, so look for other items in stack
                if (iterator >= list.Count)
                {
                    list = null;
                    bool foundItems = false;
                    while (ListStack.Count > 0 && !foundItems)
                    {
                        //Pop list and iterator from stack
                        list = ListStack.Pop();
                        iterator = IterationStack.Pop();

                        //Increment the iterator
                        iterator++;

                        //Check to see if any items remain in the list
                        if (list.Count > 0 && iterator < list.Count)
                            foundItems = true;
                    }

                    //Nothing found, return false
                    if (!foundItems)
                        return false;
                }


                //               L
                //               L
                //               L -> 4 -> 5
                //               L -> 6 -> 7
                //1 -> 2 -> 3 -> L -> 8 -> 9
                do
                {
                    //list and iterator postion is on another list item
                    while (iterator < list.Count && list[iterator] is List<object>)
                    {
                        //push current list and iterator position onto the stack
                        ListStack.Push(list);
                        IterationStack.Push(iterator);

                        list = list[iterator] as List<object>;
                        iterator = 0;

                        //This is in a loop in case the first item of the sublist is another list
                    }

                    //Push the list and iterator onto the stack.  Using iterator-1 since we will be incrementing the iterator when popping
                    ListStack.Push(list);
                    IterationStack.Push(iterator - 1);


                    //Now that we have a list that potentially has items, iterate and look for the next item                    
                    bool done = false;
                    while (!done)
                    {
                        //get item off the stack
                        list = ListStack.Pop();
                        iterator = IterationStack.Pop();

                        //Increment the iterator
                        iterator++;

                        //If item is NOT a list, push list and iterator onto stack, set top, and return true
                        if (iterator < list.Count && (list[iterator] is List<object>) == false)
                        {
                            ListStack.Push(list);
                            IterationStack.Push(iterator);
                            top = list[iterator];
                            return true;
                        }
                        else if (iterator < list.Count && (list[iterator] is List<object>))
                        {
                            //Found a list that may have an item
                            done = true;
                        }
                    }

                }
                while (list[iterator] is List<object>);

                return false;

            }
            
            public int next()
            {
                if (top != null)
                {
                    int ret = (int)top;
                    top = null;
                    return ret;
                }
                else
                {
                    return int.MinValue;
                }

            }




            public void Run()
            {
                //First list    0 - 4
                List<object> list = new List<object>();
                for (int x = 0; x < 5; x++)
                {
                    list.Add(x);
                }

                //Second list   5-7
                List<object> list2 = new List<object>();
                for (int x = 5; x < 8; x++)
                {
                    list2.Add(x);
                }

                //3rd List      8-9
                List<object> list3 = new List<object>();
                for (int x = 8; x < 10; x++)
                {
                    list3.Add(x);
                }

                //                              5-7                                    
                //Add list2 to list1        0-4 L
                list.Add(list2);


                //Add more elements to list1    10-11
                for (int x = 10; x < 12; x++)
                {
                    list.Add(x);
                }

                //                                  8-9
                //                              5-7 L
                //Add list3 to list2        0-4 L       10-11
                list2.Add(list3);
                //Add more elements to list2


                List<object> list6 = new List<object>();
                List<object> list5 = new List<object>();
                List<object> list4 = new List<object>();
                //list4.Add(12);

                list4.Add(list5);   //add 5 to 4
                list5.Add(list6);   //add 6 to 5

                list5.Add(12);

                list.Add(list4);
                list.Add(13);
                list.Add(14);

                Console.WriteLine();
                first = true;
                //DeepIterator_Method(list);

                PrintAll(list);
                Console.WriteLine();
            }

            bool first = true;
            public void PrintAll(List<object> headList)
            {
                ListStack.Push(headList);
                IterationStack.Push(-1);

                int val = 0;
                first = true;
                while (hasNext())
                {
                    val = next();
                    if (!first)
                        Console.Write(", ");
                    first = false;
                    Console.Write(val);

                }
            }

        }

        public void Run()
        {
            Solution2 s = new Solution2();
            s.Run();

        }

    }
}
