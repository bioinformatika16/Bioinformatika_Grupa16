using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInf
{
    //this class implements points in table for dynamic programming
    class MatrixElement
    {
        private List<int> _location = new List<int>();
        private int _value;
        private List<List<int>> _paths = new List<List<int>>();

        public List<int> Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public List<List<int>> Paths
        {
            get { return _paths; }
            set { _paths = value; }
        }

        public MatrixElement(List<int> locationInput, int valueInput, List<List<int>> pathsInput)
        {
            Location = new List<int>(locationInput);
            Value = valueInput;
            Paths = new List<List<int>>(pathsInput);
        }

        public MatrixElement(List<int> locationInput, int valueInput)
        {
            Location = new List<int>(locationInput);
            Value = valueInput;
        }

    }
}
