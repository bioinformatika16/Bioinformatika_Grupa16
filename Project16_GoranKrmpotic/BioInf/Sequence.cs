using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInf
{
    //class that contains sequence element stored in list of char
    class Sequence
    {
        private List<char> _elements = new List<char>();

        public List<char> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        public Sequence(string input)
        {
            input = input.ToUpper();
            foreach (char c in input)
            {
                if (c != 'A' && c != 'T' && c != 'C' && c != 'G' && c != '-')
                {
                    Console.WriteLine("Invalid input!");
                    Environment.Exit(0);
                }
                Elements.Add(c);
            }
        }

        public Sequence()
        {
            
        }

        public int Length()
        {
            return Elements.Count;
        }

        public char GetElementById(int id)
        {
            return Elements[id];
        }
    }
}
