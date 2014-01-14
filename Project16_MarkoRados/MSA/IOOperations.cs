using System;
using System.Collections.Generic;
using System.IO;

namespace MSA
{
    /// <summary>
    /// Class that represents input/output operations.
    /// </summary>
    class IOOperations
    {
        List<object> listBioData = new List<object>(); 
        List<string> listSeqNames = new List<string>(); 

        /// <summary>
        /// Method that gets the biological data from the argument/file passed into the program.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>list of bio data</returns>
        public List<object> getBioData(string[] args)
        {
            if (args.Length == 1)
            {
                string path = args[0];
                //if file is txt file
                if (IsTxtFile(path))
                {
                    readTextFile(path);
                }
                else if (IsFastaFile(path))
                {
                    readFastaFile(path);
                }
                else
                {
                    Console.WriteLine("Given argument has an invalid format. Acceptable formats are: .txt and .fasta. Please try again!");
                }
            }
            else
            {
                Console.WriteLine("Invalid number of arguments. Only one argument can be passed. Please try again!");
            }
            return listBioData;
        }

        

        

        /// <summary>
        /// Method that checks the .txt extension.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>bool value</returns>
        private bool IsTxtFile(string path)
        {
            return path != null &&
                path.EndsWith(".txt", StringComparison.Ordinal);
        }

        /// <summary>
        /// Method that reads the .txt file and stores bio data into a list. 
        /// </summary>
        /// <param name="path"></param>
        private void readTextFile(string path)
        {
            var reader = new StreamReader(path);
            string sequence = "";
            while (true)
            {
                var line = reader.ReadLine();
                if (checkForChars(line))
                {
                    if (string.IsNullOrEmpty(line))
                        break;
                    else
                    {
                        listBioData.Add(line);
                    }
                }
                else
                {
                    Console.WriteLine("Unsupported characters in FASTA file! Only 'A', 'T', 'C', '-' allowed.");
                    break;
                }
            }
            
        }


        /// <summary>
        /// Method that checks the .fasta extension.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>bool value</returns>
        private bool IsFastaFile(string path)
        {
            return path != null &&
                path.EndsWith(".fasta", StringComparison.Ordinal);
        }

        /// <summary>
        /// Method that reads the .fasta file and stores bio data into a list. 
        /// </summary>
        /// <param name="path"></param>
        private void readFastaFile(string path)
        {
            var reader = new StreamReader(path);
            string sequence = ""; 
            while(true)
            {
                var line = reader.ReadLine();
                if (checkForChars(line))
                {

                    if (string.IsNullOrEmpty(line))
                    {
                        if (sequence != "")
                        {
                            listBioData.Add(sequence);
                            sequence = "";
                        }
                        break;
                    }

                    if (line.StartsWith(">"))
                    {
                        if (sequence != "")
                        {
                            listBioData.Add(sequence);
                            sequence = "";
                        }
                        listSeqNames.Add(line);
                    }
                    else
                        sequence = sequence + line;
                }
                else
                {
                    Console.WriteLine("Unsupported characters in FASTA file! Only 'A', 'T', 'C', '-' allowed.");
                    break;
                }
            }
        }

        /// <summary>
        /// Method that performs checks on the given string line whether there are any characters which are not supported.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool checkForChars(string line)
        {
            if(line!=null)
            {
                foreach (char c in line)
                {
                    if (c != 'A' && c != 'T' && c != 'G' && c != 'C' && c != '-' && c!='\n')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Method that saves the list of aligned sequences into a text file - "results.txt".
        /// </summary>
        /// <param name="listOfAlignedObjects"></param>
        public void saveTxtFile(List<object> listOfAlignedObjects)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("results.txt"))
            {
                foreach (string sequence in listOfAlignedObjects)
                {
                    file.WriteLine(sequence);
                }
            }
        }





    }
}
