using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace BioInf
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Process proc = Process.GetCurrentProcess();
            Console.WriteLine("(1) - FASTA; (2) -TXT");
            sw.Stop();
            string inputMenu = Console.ReadLine();
            sw.Start();
            if (inputMenu != "1" && inputMenu != "2")
            {
                Console.WriteLine("Wrong input!!");
                Environment.Exit(0);
            }
            List<Sequence> inputSequences = new List<Sequence>();
            if (inputMenu == "2")
            {
                Console.WriteLine(
                    "Name of the .txt file with sequences (without extension, file must be in debug folder!):");
                sw.Stop();
                string fileName = Console.ReadLine();
                sw.Start();
                string line;
                try
                {
                    StreamReader fileReader = new StreamReader(fileName + ".txt");
                    //reading txt file with sequences line by line
                    while ((line = fileReader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        //check for empty input
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                        Sequence tmp = new Sequence(line);
                        inputSequences.Add(tmp);
                    }
                }
                catch
                {
                    Console.WriteLine("Error occured while reading .txt file!");
                    Environment.Exit(0);
                }
            }
            else if (inputMenu == "1")
            {
                Console.WriteLine("Name of the .fasta file with sequences (without extension, file must be in debug folder!):");
                sw.Stop();
                string fileName = Console.ReadLine();
                sw.Start();
                string line;
                try
                {
                    StreamReader fileReader = new StreamReader(fileName + ".fasta");
                    //reading fasta file with sequences line by line
                    string tmpRead = "";
                    while ((line = fileReader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                        //because fasta can be in multiple lines
                        if (line[0] == '>')
                        {
                            if (!string.IsNullOrEmpty(tmpRead))
                            {
                                Sequence tmp = new Sequence(tmpRead);
                                inputSequences.Add(tmp);
                                tmpRead = "";
                            }
                        }
                        else
                        {
                            tmpRead += line;
                        }
                    }
                    //because at the end of file, some protein data can be stored in tmpRead!
                    if (!string.IsNullOrEmpty(tmpRead))
                    {
                        Sequence tmp2 = new Sequence(tmpRead);
                        inputSequences.Add(tmp2);
                    }
                }
                catch 
                {
                    Console.WriteLine("Error occured while reading .fasta file!");
                    Environment.Exit(0);
                }
            }
            //this part is the same for every type of input file
            Console.WriteLine("Name of the .txt file for output (without extension, file will be in debug folder!):");
            sw.Stop();
            string outputFileName = Console.ReadLine();
            sw.Start();
            //list of input sequences is made!
            //check if only one sequence is inserted!
            if (inputSequences.Count == 1)
            {
                Console.WriteLine("I can't allign only one sequence!");
                Environment.Exit(0);
            }
            Console.WriteLine("Input OK");

            int numberOfSequences = inputSequences.Count;
            List<int> listNumbersSequences = new List<int>();
            for (int i = 0; i < numberOfSequences; i++)
            {
                listNumbersSequences.Add(inputSequences[i].Length());
            }
            
            Matrix matrixDyn = new Matrix(inputSequences, listNumbersSequences);
            try
            {
                using (StreamWriter fileWriter = new StreamWriter(outputFileName + ".txt", false))
                {
                    foreach (Sequence seq in matrixDyn.SequencesOutput)
                    {
                        for (int i = seq.Length() - 1; i >= 0; i--)
                        {
                            fileWriter.Write(seq.Elements[i]);
                            
                        }
                        fileWriter.WriteLine();
                    }
                }
            }
            catch 
            {
                Console.WriteLine("Error occured while writeing .txt file!");
                Environment.Exit(0);
            }
            Console.WriteLine("Finished!");
            Console.WriteLine("PROC");
            Console.WriteLine(proc.PrivateMemorySize64/1024);
            sw.Stop();
            Console.WriteLine("TIME: "+sw.ElapsedMilliseconds);
            
            
        }
    }
}
