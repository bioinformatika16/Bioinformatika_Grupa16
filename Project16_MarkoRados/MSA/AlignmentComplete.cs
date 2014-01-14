using System;
using System.Collections.Generic;

namespace MSA
{
    /// <summary>
    /// Class that completes the alignement of nodes that have their child(ren) as optimized=true.
    /// Implements interface "IAlignment"
    /// </summary>
    class AlignmentComplete : IAlignment
    {
        private string firstSequence;
        private string secondSequence;



        /// <summary>
        /// Method that performs alignment.
        /// If node is not optimized(aligned) we check if its child(ren) are optimized. If they are we perform alignment
        /// on the current node, if its not we go deeper into recursive call of the current node's child(ren).
        /// </summary>
        /// <param name="node"></param>
        /// <returns>Node</returns>
        public Node alignNodes(Node node)
        {
            if (node.Optimized == false)
            {
                //if both childs are not null - checking if they are optimized already
                if (node.LeftChild != null && node.RightChild != null)
                {
                    if (node.LeftChild.Optimized == true && node.RightChild.Optimized == true)
                    {
                        doAlign(node);
                    }
                    if (node.LeftChild.Optimized == false)
                    {
                        alignNodes(node.LeftChild);
                    }
                    if (node.RightChild.Optimized == false)
                    {
                        alignNodes(node.RightChild);
                    }
                }
                //If one of the child is null - checking if other child is optimized.
                else
                {
                    if (node.LeftChild != null)
                    {
                        if (node.LeftChild.Optimized == true)
                        {
                            doAlign(node);
                        }
                        else
                        {
                            alignNodes(node.LeftChild);
                        }
                    }
                    else
                    {
                        if (node.RightChild.Optimized == true)
                        {
                            doAlign(node);
                        }
                        else
                        {
                            alignNodes(node.RightChild);
                        }
                    }
                }
            }

            return node;

        }


        /// <summary>
        /// Method that does alignment with choosing which node's list of sequence is going to be aligned.
        /// </summary>
        /// <param name="node"></param>
        private void doAlign(Node node)
        {
            List<object> list1;
            List<object> list2;

            //If both childs are optimized we are taking their list of sequences to align together into current node
            if (node.LeftChild != null && node.RightChild != null)
            {
                list1 = node.LeftChild.ListSequences;
                list2 = node.RightChild.ListSequences;
            }
            //One of the child is not null and it is optimized where other child is null and we take the node's list of sequences for that null child.
            else
            {

                if (node.LeftChild != null)
                {
                    list1 = node.LeftChild.ListSequences;
                    list2 = node.ListSequences;
                }
                else
                {
                    list1 = node.ListSequences;
                    list2 = node.RightChild.ListSequences;
                }
            }

            List<object> alignedList = createAlignmentList(list1, list2);
            node.ListSequences = alignedList;
            node.Optimized = true;


        }

        /// <summary>
        /// Adds together aligned combinations of two lists of sequences and returns combined list back to the node.
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        private List<object> createAlignmentList(List<object> list1, List<object> list2)
        {

            List<object> listOfAlignedObjects = new List<object>();
            if (list1[0].ToString().Length < list2[0].ToString().Length)
            {
                foreach (object seq in list2)
                {
                    listOfAlignedObjects.Add(seq);
                }
                double maxMatch = 0;
                string bestSequenceMatch = "";
                foreach (var seq1 in list1)
                {
                    foreach (var seq2 in list2)
                    {
                        generateTwoSequences(seq1, seq2);
                        double ratio = DistanceCalculation.getResults(this.firstSequence, this.secondSequence);
                        if (maxMatch < ratio || (ratio == 0 && maxMatch == 0))
                        {
                            maxMatch = ratio;
                            bestSequenceMatch = this.firstSequence;
                        }
                    }
                    listOfAlignedObjects.Add(bestSequenceMatch);
                }
                return listOfAlignedObjects;
            }
            else if (list1[0].ToString().Length > list2[0].ToString().Length)
            {
                foreach (object seq in list1)
                {
                    listOfAlignedObjects.Add(seq);
                }
                double maxMatch = 0;
                string bestSequenceMatch = "";
                foreach (var seq2 in list2)
                {
                    foreach (var seq1 in list1)
                    {
                        generateTwoSequences(seq1, seq2);
                        double ratio = 1 - DistanceCalculation.getResults(this.firstSequence, this.secondSequence);
                        if (maxMatch < ratio || (ratio == 0 && maxMatch == 0))
                        {
                            maxMatch = ratio;
                            bestSequenceMatch = this.secondSequence;
                        }
                    }
                    listOfAlignedObjects.Add(bestSequenceMatch);
                }
                return listOfAlignedObjects;
            }
            else
            {
                foreach (var seq1 in list1)
                {
                    listOfAlignedObjects.Add(seq1);
                }
                foreach (var seq2 in list2)
                {
                    listOfAlignedObjects.Add(seq2);
                }
                return listOfAlignedObjects;
            }



        }



        /// <summary>
        /// Method that generates two aligned sequences using dynamic programming
        /// </summary>
        /// <param name="sequence1"></param>
        /// <param name="sequence2"></param>
        private void generateTwoSequences(object sequence1, object sequence2)
        {
            string seq1 = (string)sequence1;
            string seq2 = (string)sequence2;

            List<char> SeqAlign1 = new List<char>();
            List<char> SeqAlign2 = new List<char>();

            Cell[,] matrix = DynamicProgramming.Intialization_Step(seq1, seq2, 2, -1, -2);
            DynamicProgramming.Traceback_Step(matrix, seq1, seq2, SeqAlign1, SeqAlign2);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int j = SeqAlign1.Count - 1; j >= 0; j--)
            {
                sb.Append(SeqAlign1[j].ToString());

            }
            this.firstSequence = Convert.ToString(sb);

            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();

            for (int f = SeqAlign2.Count - 1; f >= 0; f--)
            {
                sb1.Append(SeqAlign2[f].ToString());
            }
            this.secondSequence = Convert.ToString(sb1);
        }
    }
}
