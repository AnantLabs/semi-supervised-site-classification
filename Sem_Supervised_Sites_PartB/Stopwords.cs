using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.IO;



namespace Project_Phase_B___Engine_Stopwords
{
    public class Stopwords
    {
       
        private string[] stopWordsArrary;
        public string[] sitesFileNames;

        public Stopwords(string filename_stopwords)
        {
           
	        // 1
	        // Declare new List.
	        List<string> lines = new List<string>();

        	// 2
	        // Use using StreamReader for disposing.
	        using (StreamReader r = new StreamReader(filename_stopwords))
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
                                            .Replace("~", string.Empty)
                                            .Replace("@", string.Empty)
                                            .Replace("#", string.Empty)
                                            .Replace("$", string.Empty)
                                            .Replace("%", string.Empty)
                                            .Replace("&", string.Empty)
                                            .Replace("<", string.Empty)
                                            .Replace(">", string.Empty)
                                            .Replace("-", string.Empty)
                                            .Replace("_", string.Empty)
                                            .Replace("=", string.Empty)
                                            .Replace(":", string.Empty)
                                            .Replace(";", string.Empty)
                                            .Replace("0", string.Empty)
                                            .Replace("1", string.Empty)
                                            .Replace("2", string.Empty)
                                            .Replace("3", string.Empty)
                                            .Replace("4", string.Empty)
                                            .Replace("5", string.Empty)
                                            .Replace("6", string.Empty)
                                            .Replace("7", string.Empty)
                                            .Replace("8", string.Empty)
                                            .Replace("9", string.Empty)
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
                
                if (!myStopWordsCol.Contains(word))
                    sb.Append(word + " ");
            }

            return sb.ToString();
        }



   

    } // end Class Stopwords
    
    
      

   
}



