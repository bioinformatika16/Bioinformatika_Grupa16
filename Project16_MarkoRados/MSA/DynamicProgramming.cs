using System.Collections.Generic;


namespace MSA
{
    /// <summary>
    /// Class that represenets dynamic programming
    /// </summary>
    class DynamicProgramming
    {

        /// <summary>
        /// Method that initiaites dynamic programming matrix and calculates the values in the matrix.
        /// 1. Initializing first row and column with gap penalty points.
        /// 2. Iterating through rest of the matrix and storing values for each field on given max value from 3 field diagonal
        /// left and up from the current field.
        /// 3. Return the newly made optimized matrix
        /// </summary>
        /// <param name="seq1">first sequence</param>
        /// <param name="seq2">second sequence</param>
        /// <param name="match">Match point</param>
        /// <param name="mismatch">MisMatch point</param>
        /// <param name="gap">Gap penalty</param>
        /// <returns>Optimized matrix</returns>
        public static Cell[,] Intialization_Step(string seq1, string seq2, int match, int mismatch, int gap)
        {
            int size1 = seq1.Length + 1;
            int size2 = seq2.Length + 1;

            Cell[,] Matrix = new Cell[size2, size1];


            //Performing initialization of the first row.
            for (int i = 0; i < Matrix.GetLength(1); i++)
            {
                Matrix[0, i] = new Cell(0, i, i * gap);

            }

            //Performing initialization of the first column.
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                Matrix[i, 0] = new Cell(i, 0, i * gap);

            }
            //Performing valuation of the rest of the matrix with getting maximum value between fields:
            // (j-1, i-1), (j-1, i) and (j, i-1). Maximum value out of the given 3 is stored in the curren field of the matrix
            for (int j = 1; j < Matrix.GetLength(0); j++)
            {
                for (int i = 1; i < Matrix.GetLength(1); i++)
                {
                    Matrix[j, i] = getMaximumValue(i, j, seq1, seq2, Matrix, match, mismatch, gap);
                }

            }

            return Matrix;
        }


        /// <summary>
        /// Method that returns the maximum value for the given field(i,j coordinates).
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="seq1"></param>
        /// <param name="seq2"></param>
        /// <param name="matrix"></param>
        /// <param name="match"></param>
        /// <param name="misMatch"></param>
        /// <param name="gap"></param>
        /// <returns></returns>
        public static Cell getMaximumValue(int i, int j, string seq1, string seq2, Cell[,] matrix, int match, int misMatch, int gap)
        {
            Cell temp = new Cell();
            int Sim;
            int gapPenalty = gap;
            if (seq1[i - 1] == seq2[j - 1])
            {
                Sim = match;
            }
            else
            {
                Sim = misMatch;
            }
            int value1 = matrix[j - 1, i - 1].CellScore + Sim;
            int value2 = matrix[j, i - 1].CellScore + gapPenalty;
            int value3 = matrix[j - 1, i].CellScore + gapPenalty;

            
            int max = value1 >= value2 ? value1 : value2;
            int Mmax = value3 >= max ? value3 : max;
            if (Mmax == value1)
            { temp = new Cell(j, i, value1, matrix[j - 1, i - 1], Cell.PreviousCellDirection.Diagonal); }
            else
            {
                if (Mmax == value2)
                { temp = new Cell(j, i, value2, matrix[j, i - 1], Cell.PreviousCellDirection.Left); }
                else
                {
                    if (Mmax == value3)
                    { temp = new Cell(j, i, value3, matrix[j - 1, i], Cell.PreviousCellDirection.Above); }
                }
            }

            return temp;
        }



        /// <summary>
        /// Method that tracebacks through the matrix and stores information on which direction did the cell come from.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="seq1"></param>
        /// <param name="seq2"></param>
        /// <param name="listSeq1"></param>
        /// <param name="listSeq2"></param>
        public static void Traceback_Step(Cell[,] matrix, string seq1, string seq2, List<char> listSeq1, List<char> listSeq2)
        {

            Cell currentCell = matrix[seq2.Length, seq1.Length];


            while (currentCell.CellPointer != null)
            {
                if (currentCell.Type == Cell.PreviousCellDirection.Diagonal)
                {

                    listSeq1.Add(seq1[currentCell.CellColumn - 1]);
                    listSeq2.Add(seq2[currentCell.CellRow - 1]);

                }
                if (currentCell.Type == Cell.PreviousCellDirection.Left)
                {
                    listSeq1.Add(seq1[currentCell.CellColumn - 1]);
                    listSeq2.Add('-');

                }
                if (currentCell.Type == Cell.PreviousCellDirection.Above)
                {
                    listSeq1.Add('-');
                    listSeq2.Add(seq2[currentCell.CellRow - 1]);

                }

                currentCell = currentCell.CellPointer;

            }




        }
    }
}
