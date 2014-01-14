
using System.Collections.Generic;


namespace MSA
{
    /// <summary>
    /// Class that represents properties of the node
    /// </summary>
    class Node
    {
        private Node leftChild;
        private Node rightChild;
        private List<object> listSequences;

        private bool nodeFirst = false;
        private bool nodeSecond = false;

        private bool optimized = false;

        /// <summary>
        /// Constructor that initializes list of seqeunces and performs initialization of left and right child if
        /// a node had been inherited in the list of sequences.
        /// </summary>
        /// <param name="listSequences"></param>
        public Node(List<object> listSequences)
        {
            this.listSequences = listSequences;
            if (listSequences[0] is Node == true)
            {
                addLeftChild((Node)listSequences[0]);
                nodeFirst = true;

            }
            if (listSequences[1] is Node == true)
            {
                addRightChild((Node)listSequences[1]);
                nodeSecond = true;
            }

            if (listSequences.Contains(leftChild))
            {
                listSequences.Remove(leftChild);
            }
            if (listSequences.Contains(rightChild))
            {
                listSequences.Remove(rightChild);
            }



        }

        /// <summary>
        /// Method that adds a left child to the current node
        /// </summary>
        /// <param name="node"></param>
        public void addLeftChild(Node node)
        {
            this.leftChild = node;
        }

        /// <summary>
        /// Method that adds a right child to the current node
        /// </summary>
        /// <param name="node"></param>
        public void addRightChild(Node node)
        {
            this.rightChild = node;
        }

        /// <summary>
        /// Getter method for left child
        /// </summary>
        public Node LeftChild
        {
            get { return leftChild; }
        }

        /// <summary>
        /// Getter method for right child
        /// </summary>
        public Node RightChild
        {
            get { return rightChild; }
        }

        /// <summary>
        /// Getter method for list of sequences
        /// </summary>
        public List<object> ListSequences
        {
            get { return listSequences; }
            set { listSequences = value; }
        }

        /// <summary>
        /// Getter method for boolean value if the node has been optimized
        /// </summary>
        public bool Optimized
        {
            get { return optimized; }
            set { optimized = value; }
        }
    }
}
