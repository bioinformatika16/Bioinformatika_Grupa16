using System;
using System.Collections.Generic;

namespace MSA
{
    /// <summary>
    /// Class that guides the nodes through the tree in the correct order given on the distance between the sequences.
    /// </summary>
    class GuideTreeAlgorithm
    {
        private double[,] matrixMatch;
        private List<Object> listCurrentNodes;
        private int iIndex;
        private int jIndex;
        private Node nodeRoot;

        /// <summary>
        /// Constructor that initializes the matrix and list
        /// </summary>
        /// <param name="matrixMatch"></param>
        /// <param name="listNodes"></param>
        public GuideTreeAlgorithm(double[,] matrixMatch, List<Object> listNodes)
        {
            this.matrixMatch = matrixMatch;
            this.listCurrentNodes = listNodes;
        }

        /// <summary>
        /// Getter method for returning root node of the tree
        /// </summary>
        public Node NodeRoot
        {
            get { return nodeRoot; }
        }


        /// <summary>
        /// Method that generates the tree setup with beginning distribution of sequences.
        /// </summary>
        public void generateTree()
        {

            findShortestDistance(this.matrixMatch);

            while (listCurrentNodes.Count > 2)
            {
                double[] firstRow = newNodeRow(this.matrixMatch, this.iIndex, this.jIndex);
                double[,] newMatrix = fillNewMatrix(firstRow, this.matrixMatch, createSubMatrix(this.matrixMatch));
                deleteSequenceFromTheList(this.iIndex, this.jIndex);
                findShortestDistance(newMatrix);
                this.matrixMatch = newMatrix;
                //double[] firstRow1 = redakNovogCvora(this.matrixMatch, this.iIndex, this.jIndex);
                //double[,] newMatrix1 = popuniNovuMatricu(firstRow, this.matrixMatch, kreirajPodMatricu(this.matrixMatch));
            }

            if (listCurrentNodes.Count == 2)
            {
                deleteSequenceFromTheList(0, 1);
            }
        }


        /// <summary>
        /// Method that finds the shortest distance and select its coordinates in the distance matrix.
        /// </summary>
        /// <param name="matrix"></param>
        private void findShortestDistance(double[,] matrix)
        {
            // Get upper bounds for the array.
            int bound0 = matrix.GetUpperBound(0);
            int bound1 = matrix.GetUpperBound(1);
            double minValue = 1;
            // Use for-loops to iterate over the array elements.
            for (int i = 0; i <= bound0; i++)
            {
                for (int j = 0; j <= bound1; j++)
                {
                    if (j > i)
                    {
                        double value = matrix[i, j];
                        if (value < minValue)
                        {
                            minValue = value;
                            this.iIndex = i;
                            this.jIndex = j;
                            //Console.WriteLine("Najmanja vrijednost: " + value + " " + i + " " + j);
                        }


                    }
                }
                //Console.WriteLine();
            }
        }

        /// <summary>
        /// Method that creates a new node's row
        /// </summary>
        /// <param name="matrixMatch"></param>
        /// <param name="iIndex"></param>
        /// <param name="jIndex"></param>
        /// <returns></returns>
        private double[] newNodeRow(double[,] matrixMatch, int iIndex, int jIndex)
        {
            double[] newRow = new double[matrixMatch.GetUpperBound(0)];
            newRow[0] = 0;
            int index = 1;
            double firstValue = 0;
            double secondValue = 0;

            int bound = matrixMatch.GetUpperBound(0);

            for (int i = 0; i <= bound; i++)
            {
                if (i != iIndex && i != jIndex)
                {
                    if (i > iIndex)
                    {
                        firstValue = matrixMatch[iIndex, i];
                    }
                    else
                    {
                        firstValue = matrixMatch[i, iIndex];
                    }

                    if (i > jIndex)
                    {
                        secondValue = matrixMatch[jIndex, i];
                    }
                    else
                    {
                        secondValue = matrixMatch[i, jIndex];
                    }

                    newRow[index] = (firstValue + secondValue) / 2;
                    index++;
                }
            }
            return newRow;
        }

        /// <summary>
        /// Method that creates the subMatrix when the shortest distance vortices have been selected and removed
        /// from the matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        private double[,] createSubMatrix(double[,] matrix)
        {
            double[,] newMatrix = new double[matrix.GetUpperBound(0) - 1, matrix.GetUpperBound(1) - 1];
            int bound0 = matrix.GetUpperBound(0);
            int bound1 = matrix.GetUpperBound(1);

            int newI = 0;
            int newJ = 0;

            for (int i = 0; i <= bound0; i++)
            {
                for (int j = 0; j <= bound1; j++)
                {
                    if (i != this.iIndex && i != this.jIndex && j != this.iIndex && j != this.jIndex)
                    {
                        if (newJ == bound1 - 1)
                        {
                            newI++;
                            newJ = 0;
                        }
                        newMatrix[newI, newJ] = matrix[i, j];
                        newJ++;
                    }
                }

            }

            return newMatrix;
        }

        /// <summary>
        /// Method that fills in the new matrix on given new sub matrix and first row array.
        /// </summary>
        /// <param name="firstRow"></param>
        /// <param name="matrix"></param>
        /// <param name="subMatrix"></param>
        /// <returns></returns>
        private double[,] fillNewMatrix(double[] firstRow, double[,] matrix, double[,] subMatrix)
        {
            double[,] newMatrix = new double[matrix.GetUpperBound(0), matrix.GetUpperBound(1)];
            int bound0 = matrix.GetUpperBound(0);
            int bound1 = matrix.GetUpperBound(1);

            for (int i = 0; i < bound0; i++)
            {
                for (int j = 0; j < bound1; j++)
                {
                    if (i == 0)
                    {
                        newMatrix[i, j] = firstRow[j];
                    }
                    if (i > 0 && j > 0)
                    {
                        newMatrix[i, j] = subMatrix[i - 1, j - 1];
                    }

                }

            }

            return newMatrix;
        }

        /// <summary>
        /// Method that deletes the sequences from the list on given indexes.
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="jIndex"></param>
        private void deleteSequenceFromTheList(int iIndex, int jIndex)
        {
            List<object> cvorSekvence = new List<object>();
            cvorSekvence.Add(listCurrentNodes[iIndex]);
            cvorSekvence.Add(listCurrentNodes[jIndex]);

            Node node = new Node(cvorSekvence);
            this.nodeRoot = node;

            if (iIndex < jIndex)
            {
                listCurrentNodes.RemoveAt(iIndex);
                listCurrentNodes.RemoveAt(jIndex - 1);
            }
            else
            {
                listCurrentNodes.RemoveAt(jIndex);
                listCurrentNodes.RemoveAt(iIndex - 1);
            }

            listCurrentNodes.Insert(0, node);
        }
    }
}
