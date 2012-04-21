﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;
using Project_Phase_B___Engine_BagofWords;
using PCA_Logic;

namespace Project_Phase_B___Engine_Stopwords
{
    public class SearchHelper
    {
       /* private static string[] stopWordsArrary = new string[] { "a", "about", "actually", "after", "also", "am", "an", "and", "any", "are", "as", "at", "be", "because", "but", "by", 
                                                "could", "do", "each", "either", "en", "for", "from", "has", "have", "how", 
                                                "i", "if", "in", "is", "it", "its", "just", "of", "or", "so", "some", "such", "that", 
                                                "the", "their", "these", "thing", "this", "to", "too", "very", "was", "we", "well", "what", "when", "where",
                                                "who", "will", "with", "you", "your" 
                                            }; */
        private string[] stopWordsArrary;
        public string[] sitesFileNames;

        public SearchHelper()
        {
           // const string f = "stopwords.txt";

	        // 1
	        // Declare new List.
	        List<string> lines = new List<string>();

        	// 2
	        // Use using StreamReader for disposing.
	     //   using (StreamReader r = new StreamReader(@"d:\stopwords.txt"))
            using (StreamReader r = new StreamReader("stopwords.txt"))
	        {
	            // 3
	            // Use while != null pattern for loop
	            string line;
	            while ((line = r.ReadLine()) != null)
	            {
		            // 4
		            // Insert logic here.
		            // ...
		            // "line" is a line in the file. Add it to our List.
		            lines.Add(line);
	            }
	        }

	        // 5
	        // Print out all the lines.
            stopWordsArrary=new string[lines.Count];
            int i = 0;
            foreach (string s in lines)
	        {
	            //Console.WriteLine(s);
                stopWordsArrary[i] = s.ToLowerInvariant().Trim();
                i++;
	        }
        }


        
        /// 
        /// Removes stop words from the specified search string.
        /// 
        public string CleanSearchedWords(string searchedWords)
        {

            searchedWords = searchedWords
                                            .Replace("\\", string.Empty)
                                            .Replace("|", string.Empty)
                                            .Replace("(", string.Empty)
                                            .Replace(")", string.Empty)
                                            .Replace("[", string.Empty)
                                            .Replace("]", string.Empty)
                                            .Replace("*", string.Empty)
                                            .Replace("?", string.Empty)
                                            .Replace("}", string.Empty)
                                            .Replace("{", string.Empty)
                                            .Replace("^", string.Empty)
                                            .Replace("+", string.Empty);

            // transform search string into array of words
            char[] wordSeparators = new char[] { ' ', '\n', '\r', ',', ';', '.', '!', '?', '-', ' ', '"', '\'' };
            string[] words = searchedWords.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries);

            // Create and initializes a new StringCollection.
            StringCollection myStopWordsCol = new StringCollection();
            // Add a range of elements from an array to the end of the StringCollection.
            myStopWordsCol.AddRange(stopWordsArrary);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i].ToLowerInvariant().Trim();
                //if (word.Length > 1 && !myStopWordsCol.Contains(word))
                if (!myStopWordsCol.Contains(word))
                    sb.Append(word + " ");
            }

            return sb.ToString();
        }
    }
    
    
    class Stopwords
    {
        

        static void Main(string[] args)
        {
            
            //const string f = "Site1.txt";
            SearchHelper sh1= new SearchHelper();
            string[] sitesFileNames = { "Site1_new.txt", "Site2_new.txt", "Site3_new.txt" };
            BagofWords bag;
            double[,] sitesFreq;

            StreamReader streamReader = new StreamReader("Site1.txt");
            string text = streamReader.ReadToEnd();
            streamReader.Close();


            using (StreamWriter outfile = new StreamWriter("Site1_new.txt"))
            {
                outfile.Write(sh1.CleanSearchedWords(text));
            }

           streamReader = new StreamReader("Site2.txt");
            text = streamReader.ReadToEnd();
            streamReader.Close();


            using (StreamWriter outfile = new StreamWriter("Site2_new.txt"))
            {
                outfile.Write(sh1.CleanSearchedWords(text));
            }

            streamReader = new StreamReader("Site3.txt");
            text = streamReader.ReadToEnd();
            streamReader.Close();


            using (StreamWriter outfile = new StreamWriter("Site3_new.txt"))
            {
                outfile.Write(sh1.CleanSearchedWords(text));
            }
            
            bag = new BagofWords(sitesFileNames);
            bag.setSitesFreqVector();
            sitesFreq = bag.getSitesFreqVector();
            int NumOfSites=bag.getNumOfSites(); 
            int NumOfWordsDictionary = bag.getNumOfWordsInDictionary();

            for (int i = 0; i < NumOfSites; i++)
            {
                for (int j = 0; j < NumOfWordsDictionary; j++)
                {

                    Console.Write("{0} ", sitesFreq[i, j]);

                }
                Console.WriteLine("");
            }
            Console.ReadKey();
            PCA pc = new PCA(sitesFreq, 3);
            pc.compute();
            Console.ReadKey();
        }
    }
}



