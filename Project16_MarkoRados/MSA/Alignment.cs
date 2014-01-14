using System;
using System.Collections.Generic;

namespace MSA
{
    /// <summary>
    /// Class that represents the alignment of sequences in nodes of the tree.
    /// </summary>
    class Alignment
    {
        private Node root;

        /// <summary>
        /// Inital constructor that initiates root node.
        /// </summary>
        /// <param name="root"></param>
        public Alignment(Node root)
        {
            this.root = root;
        }

        /// <summary>
        /// Method that performs alignment of sequences of nodes.
        /// </summary>
        public void alignSequences()
        {
            //Here we align the leaves
            AlignmentInitial alignmentInitial = new AlignmentInitial();
            this.root = alignmentInitial.alignNodes(this.root);

            //Here we align all the nodes that have their child(ren) as optimized=true
            AlignmentComplete alignmentComplete = new AlignmentComplete();
            while (this.root.Optimized == false)
            {
                this.root = alignmentComplete.alignNodes(this.root);
            }

            //Printing out onto the console the alignment of all sequences
            foreach (var seq in this.root.ListSequences)
            {
                Console.WriteLine(seq.ToString());
            }

        }

        /// <summary>
        /// Getter method for list of aligned sequences
        /// </summary>
        /// <returns></returns>
        public List<object> GetListOfAlignedObjects()
        {
            return this.root.ListSequences;
        } 
    }
}
