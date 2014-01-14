using System;
using System.Collections.Generic;

namespace MSA
{
    /// <summary>
    /// Class that align initial nodes - leaves.
    /// Implements inteface "Ialignment"
    /// </summary>
    class AlignmentInitial : IAlignment
    {
        /// <summary>
        /// Method that perfors alignment of leaf nodes.
        /// If both children are null we reached the leaf node and perform alignment on its pair
        /// of sequences.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>Node</returns>
        public Node alignNodes(Node node)
        {

            if (node.LeftChild != null || node.RightChild != null)
            {
                if (node.LeftChild != null)
                {
                    alignNodes(node.LeftChild);
                }
                if (node.RightChild != null)
                {
                    alignNodes(node.RightChild);
                }
            }
            else
            {
                List<object> pair = node.ListSequences;
                if (pair.Count == 2)
                {
                    string seq1 = (string)pair[0];
                    string seq2 = (string)pair[1];

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

                    node.ListSequences[0] = firstSequence;
                    node.ListSequences[1] = secondSequence;
                    node.Optimized = true;


                }

            }
            return node;

        }
    }
}
