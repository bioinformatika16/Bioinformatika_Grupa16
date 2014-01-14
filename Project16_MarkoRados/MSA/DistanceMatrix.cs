using System;
using System.Collections.Generic;

namespace MSA
{
    /// <summary>
    /// Class that represents distance matrix.
    /// </summary>
    class DistanceMatrix
    {
        private List<string> listOfEntries;
        private double[,] distanceMatrix;
        private int size;

        /// <summary>
        /// Constructor for initializing matrix size and list of entries.
        /// </summary>
        /// <param name="listOfEntries"></param>
        public DistanceMatrix(List<string> listOfEntries)
        {
            this.listOfEntries = listOfEntries;
            this.size = listOfEntries.Count;
        }

        /// <summary>
        /// Main method where the distance matrix is generated.
        /// </summary>
        public void generateDistanceMatrix()
        {
            this.distanceMatrix = new double[listOfEntries.Count, listOfEntries.Count];

            for (int i = 0; i < size; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    this.distanceMatrix[i, j] = calculateDistance(listOfEntries[i], listOfEntries[j]);
                }
            }
        }

        /// <summary>
        /// Getter method for distance matrix.
        /// </summary>
        public double[,] DistanceMatrix1
        {
            get { return distanceMatrix; }
        }


        /// <summary>
        /// Method that calculates the difference between matched characters and non-matched characters.
        /// </summary>
        /// <param name="seq1"></param>
        /// <param name="seq2"></param>
        /// <returns></returns>
        private double calculateDistance(string seq1, string seq2)
        {


            List<char> SeqAlign1 = new List<char>();
            List<char> SeqAlign2 = new List<char>();

            string firstSequence;
            string secondSequence;

            Cell[,] matrix = DynamicProgramming.Intialization_Step(seq1, seq2, 2, -1, -2);
            DynamicProgramming.Traceback_Step(matrix, seq1, seq2, SeqAlign1, SeqAlign2);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int j = SeqAlign1.Count - 1; j >= 0; j--)
            {
                sb.Append(SeqAlign1[j].ToString());

            }
            firstSequence = Convert.ToString(sb);

            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            for (int f = SeqAlign2.Count - 1; f >= 0; f--)
            {
                sb1.Append(SeqAlign2[f].ToString());
            }
            secondSequence = Convert.ToString(sb1);

            return DistanceCalculation.getResults(firstSequence, secondSequence);
        }





    }

    public static class DistanceCalculation
    {
        /// <summary>
        /// Method that calculates the ratio between matched and total characters between two strings.
        /// </summary>
        /// <param name="seq1"></param>
        /// <param name="seq2"></param>
        /// <returns></returns>
        public static double getResults(string seq1, string seq2)
        {
            int totalChars = 0;
            int totalMatches = 0;
            for (int i = 0; i < seq1.Length; i++)
            {
                if (seq1[i] != '-' && seq2[i] != '-')
                {
                    if (seq1[i] == seq2[i])
                    {
                        totalMatches++;
                        totalChars++;
                    }
                    else
                    {
                        totalChars++;
                    }

                }
            }
            double result = 1 - ((double)totalMatches / totalChars);
            return result;
        }
    }
}
