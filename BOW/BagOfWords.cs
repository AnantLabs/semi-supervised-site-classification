using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Project_Phase_B___Engine_BagofWords
{
    public class BagofWords
    {
        char[] wordSeparators = new char[] { ' ', '\n', '\r', ',', ';', '.', '!', '?', '-', ' ', '"', '\'' };
        
        Dictionary<string, int> dictionary;
        double[,] sitesFreqVector;
        int sitesFreqVector_rows=0;
        int sitesFreqVector_cols=0;
        int num_of_sites;
        int num_of_words_in_dictionary;
        string[] sitesFileNames;

        public BagofWords(string[] sitesFileNames)
        {
            dictionary = new Dictionary<string, int>();
            num_of_sites = sitesFileNames.Count();
            this.sitesFileNames = sitesFileNames;

            foreach (string filename in sitesFileNames)
            {
                StreamReader streamReader = new StreamReader(filename);
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                string[] words = text.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < words.Length; i++)
                {
                    string word = words[i].ToLowerInvariant().Trim();
                    int value;
                    if (!dictionary.TryGetValue(word, out value))
                    {
                        dictionary.Add(word, 0);
                    }

                } // end for (int i = 0; i < words.Length; i++)

            } // end foreach (string filename in sitesFileNames)

            num_of_words_in_dictionary = dictionary.Count;

            this.sitesFreqVector = new double[num_of_sites, num_of_words_in_dictionary];

            for (int i = 0; i < num_of_sites; i++)
            {
                for (int j = 0; j < num_of_words_in_dictionary; j++)
                {
                    sitesFreqVector[i,j] = 0;
                }
            }
            

        } // end constructor : public BagofWords(string[] sitesFileNames)

        // need to make a method that will build the 2D array of sites and their frequencies

        public void setSitesFreqVector()
        {
            foreach (string filename in this.sitesFileNames)
            {
                StreamReader streamReader = new StreamReader(filename);
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                string[] words = text.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < words.Length; i++)
                {
                    string word = words[i].ToLowerInvariant().Trim();
                    int value;
                    if (dictionary.TryGetValue(word, out value))
                    {
                        dictionary[word] = value + 1;
                    }
                    
                } // end for (int i = 0; i < words.Length; i++)

                
                foreach (KeyValuePair<string, int> pair in dictionary)
                {
                    sitesFreqVector[sitesFreqVector_rows, sitesFreqVector_cols] = pair.Value;
                    sitesFreqVector_cols++;
                }

                
                // need to zero the freq of the words in dictionary
                foreach (var key in dictionary.Keys.ToList())
                {
                    dictionary[key] = 0;
                } 



                sitesFreqVector_rows++;
                sitesFreqVector_cols = 0;

            } // end foreach (string filename in sitesFileNames)
                       

        } // end Method : public void setSitesFreqVector()

                     

        public double[,] getSitesFreqVector()
        {
            return sitesFreqVector;
        }

        public int getNumOfSites()
        {
            return num_of_sites;
        }

        public int getNumOfWordsInDictionary()
        {
            return num_of_words_in_dictionary;
        }
    }











}