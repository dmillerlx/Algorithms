using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Algorithms
{
    class DataStructure_Tries
    {
        public class Node
        {
            public Node[] edges = new Node[26];
            public int words = 0;
            public int prefix = 0;
        }


        public class Tries
        {
            Node root = new Node();

            int charToIndex(char k)
            {
                return (int)k - (int)'a';
            }

            public void AddWord(string word)
            {
                AddWord(word, root, 0);
            }

            void AddWord(string word, Node vertex, int words)
            {
                if (string.IsNullOrEmpty(word))
                {
                    //vertex.words++;
                    vertex.words = words + 1;
                }
                else
                {
                    vertex.prefix++;
                    char k = word.ToLower()[0];
                    if (vertex.edges[charToIndex(k)] == null)
                    {
                        vertex.edges[charToIndex(k)] = new Node();
                    }
                    word = word.Substring(1);

                    int w = Math.Max(words, vertex.words);
                    AddWord(word, vertex.edges[charToIndex(k)], w);
                }
            }

            public int CountWords(string word)
            {
                return CountWords(root, word);
            }

            int CountWords(Node vertex, string word)
            {
    //            countWords(vertex, word)
    //k=firstCharacter(word)
    //if isEmpty(word)
    //    return vertex.words
    //else if notExists(edges[k])
    //    return 0
    //else
    //    cutLeftmostCharacter(word)
    //    return countWords(edges[k], word);
                if (string.IsNullOrEmpty(word))
                    return vertex.words;
                
                char k = word.ToLower()[0];
                if (vertex.edges[charToIndex(k)] == null)
                    return 0;
                else
                {
                    word = word.Substring(1);
                    return CountWords(vertex.edges[charToIndex(k)], word);
                }
            }

            public int CountPrefixs(Node vertex, string prefix)
            {
                //        countPrefixes(vertex, prefix)
                //k=firstCharacter(prefix)
                //if isEmpty(word)
                //    return vertex.prefixes
                //else if notExists(edges[k])
                //    return 0
                //else
                //    cutLeftmostCharacter(prefix)
                //    return countWords(edges[k], prefix)
                if (string.IsNullOrEmpty(prefix))
                    return vertex.prefix;

                char k = prefix.ToLower()[0];
                if (vertex.edges[charToIndex(k)] == null)
                    return 0;
                else
                {
                    prefix = prefix.Substring(1);
                    return CountWords(vertex.edges[charToIndex(k)], prefix);
                }
            }  


        }

        public void Run()
        {
            Tries t = new Tries();
            Console.WriteLine("*******************************");
            Console.WriteLine("Data Structure - Tries");
            Console.WriteLine("*******************************");
            Console.WriteLine("Adding words...");
            t.AddWord("one");
            t.AddWord("two");
            t.AddWord("three");
            t.AddWord("four");
            t.AddWord("five");
            t.AddWord("six");
            t.AddWord("seven");

            string[] lookup = { "five", "ten", "fiv" };
            Console.WriteLine("Looking up words:");

            foreach (string s in lookup)
            {
                int count = t.CountWords(s);
                Console.WriteLine("Word: " + s + " " + (count > 0 ? "found" : "not found"));
            }          

        }

        public void Run_Wordlist()
        {
            FileStream fs = null;
            StreamReader sr = null;

            Tries t = new Tries();
            Console.WriteLine("*******************************");
            Console.WriteLine("Data Structure - Tries - Using Scrabble Word List");
            Console.WriteLine("*******************************");
            Console.WriteLine("Adding words...");

            int wordCount = 0;

            try
            {
                fs = new FileStream("EnglishScrabbleWordList.txt", FileMode.Open);
                sr = new StreamReader(fs);

                do
                {
                    string word = sr.ReadLine();
                    if (!string.IsNullOrEmpty(word))
                    {
                        wordCount++;
                        t.AddWord(word);

                        if ((wordCount % 1000) == 0)
                        {
                            Console.WriteLine("Added " + wordCount + " words...");
                        }

                    }
                }
                while (!sr.EndOfStream);

                Console.WriteLine("Word list loaded.  Word count: " + wordCount);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                if (sr != null)
                    sr.Close();

                if (fs != null)
                    fs.Close();
            }

            
            string[] lookup = { "five", "ten", "fiv", "absolute", "vodka", "knowledge", "essential", "depth", "hash", "code", "you", "the", "dude" };
            Console.WriteLine("Looking up words:");

            foreach (string s in lookup)
            {
                int count = t.CountWords(s);
                Console.WriteLine("Word: " + s + " " + (count > 0 ? "found" : "not found"));
            } 
        }

        public void Run_Suffix()
        {
            Tries t = new Tries();
            Console.WriteLine("*******************************");
            Console.WriteLine("Data Structure - Tries - Suffix");
            Console.WriteLine("*******************************");

            string word = "aaabbb$";

            //Add each suffix to a trie
            Console.WriteLine("Adding suffix...");

            for (int x = 0; x < word.Length; x++)
            {
                string suffix = word.Substring(word.Length - x - 1);
                Console.WriteLine(suffix);
                t.AddWord(suffix);
            }

            string[] lookup = { "ab", "aab", "aba" };
            Console.WriteLine("Looking up words:");

            foreach (string s in lookup)
            {
                int count = t.CountWords(s);
                Console.WriteLine("Word: " + s + " " + (count > 0 ? "found" : "not found"));
            } 
            
            

        }


       


    }
}
