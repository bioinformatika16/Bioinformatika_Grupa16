using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioInf
{
    //this class implements the "field" for dynamic programming
    //matrix in list of elements won't have any element that isn't calculated
    //list toInitialize and toCalculate have locations of elements that needs to be calculated
    class Matrix
    {
        private List<MatrixElement> _elements = new List<MatrixElement>();
        private int _similarityScore = 1;
        private int _nonSimilityScore = -1;
        private int _gapScore = -2;
        //private int _gapScore = -1;
        private List<Sequence> _sequences = new List<Sequence>();
        private List<List<int>> _toInitialize = new List<List<int>>(); 
        private List<List<int>> _toCalculate = new List<List<int>>();
        //it will be reversed!!
        private List<Sequence> _sequencesOutput = new List<Sequence>(); 

        public List<MatrixElement> Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        public int SimilarityScore
        {
            get { return _similarityScore; }
            set { _similarityScore = value; }
        }

        public int NonSimilarityScore
        {
            get { return _nonSimilityScore; }
            set { _nonSimilityScore = value; }
        }

        public int GapScore
        {
            get { return _gapScore; }
            set { _gapScore = value; }
        }

        public List<Sequence> Sequences
        {
            get { return _sequences; }
            set { _sequences = value; }
        }

        public List<List<int>> ToInitialize
        {
            get { return _toInitialize; }
            set { _toInitialize = value; }
        }

        public List<List<int>> ToCalculate
        {
            get { return _toCalculate; }
            set { _toCalculate = value; }
        }

        public List<Sequence> SequencesOutput
        {
            get { return _sequencesOutput; }
            set { _sequencesOutput = value; }
        }

        public Matrix(List<Sequence> sequencesInput, List<int> sizes)
        {
            Sequences = new List<Sequence>(sequencesInput);
            for (int i = 0; i < Sequences.Count; i++)
            {
                SequencesOutput.Add(new Sequence());
            }
            int length = sizes.Count;
            //location of first matrixElement
            List<int> tmp = new List<int>();
            for (int i = 0; i < length; i++)
            {
                tmp.Add(0);
            }
            MatrixElement firstElement = new MatrixElement(tmp, 0);
            Elements.Add(firstElement);
            //need to make division to lists -> outer and inner cube
            CalculateLists(tmp, sizes);
            CalculateToInitialize(sizes);
            Console.WriteLine("Initialization finished!");
            CalculateToCalculate(sizes);
            Console.WriteLine("Matrix calculations finished!");
            CalculateAlignment(sizes);
            Console.WriteLine("I'm done!");

        }
        //check if list toInitialize already has that element
        public bool InitializeListContains(List<int> testLocation)
        {
            bool contains = false;
            foreach (List<int> tmp in ToInitialize)
            {
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (tmp[i] != testLocation[i])
                    {
                        contains = false;
                        break;
                    }
                    else
                    {
                        contains = true;
                    }
                }
                if (contains == true)
                {
                    return contains;
                }
            }
            return contains;
        }
        //check if list toCalculate already has that element
        public bool CalculateListContains(List<int> testLocation )
        {
            bool contains = false;
            foreach (List<int> tmp in ToCalculate)
            {
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (tmp[i] != testLocation[i])
                    {
                        contains = false;
                        break;
                    }
                    else
                    {
                        contains = true;
                    }
                }
                if (contains == true)
                {
                    return contains;
                }
            }

            return contains;
        }

        //check if location is for initialization list
        public int CountZeros(List<int> x)
        {
            int number = 0;
            foreach (int element in x)
            {
                if (element == 0)
                {
                    number++;
                }
            }

            return number;
        }

        //calculates all neighbours (side operation -> helps calculate neighbours -> operations for indexes!)
        public int[] GetIntBinary(int length, int number)
        {
            int[] exit = new int[length];
            int position = length- 1;
            int i = 0;
            while (i < length)
            {
                if ((number & (1 << i)) != 0)
                {
                    exit[position] = 1;
                }
                else
                {
                    exit[position] = 0;
                }
                position--;
                i++;
            }

            return exit;
        }

        //tests if matrix contains element with input location
        public bool ContainsElement(List<int> elementLocation)
        {
            bool contains = false;
            foreach (MatrixElement mE in Elements)
            {
                for (int i = 0; i < mE.Location.Count; i++)
                {
                    if (mE.Location[i] != elementLocation[i])
                    {
                        contains = false;
                        break;
                    }
                    else
                    {
                        contains = true;
                    }
                }
                if (contains == true)
                {
                    return contains;
                }
            }
            return contains;
        }
        //calculates locations for inner and outer hypercube
        public void CalculateLists(List<int> start, List<int> sizes)
        {
            int length = sizes.Count;
            int number = Convert.ToInt32(Math.Pow(2, length) - 1);
            List<int[]> bin = new List<int[]>();
            for (int i = 1; i <= number; i++)
            {

                int[] z = GetIntBinary(length, i);
                bin.Add(z);
            }
            int counter = 0;
            //check if you spreaded toInitialize list
            int flag2 = 0;
            //calculating locations for initialization list (neighbours)
            while (true)
            {
                foreach (int[] x in bin)
                {
                    int flag = 0;
                    List<int> tmpLocation = new List<int>();
                    for (int i = 0; i < length; i++)
                    {
                        int help = x[i] + start[i];
                        if (help > sizes[i])
                        {
                            flag = 1;
                            break;
                        }
                        tmpLocation.Add(help);
                    }
                    if (flag == 0)
                    {
                        //check if toInitialize list
                        if (CountZeros(tmpLocation) > 0)
                        {
                            if (InitializeListContains(tmpLocation))
                            {
                                continue;
                            }
                            else
                            {
                                ToInitialize.Add(tmpLocation);
                            }
                        }
                        else
                        {
                            if (CalculateListContains(tmpLocation))
                            {
                                continue;
                            }
                            else
                            {
                                ToCalculate.Add(tmpLocation);
                            }
                        }
                    }
                }
                if (flag2 != 0)
                {
                    counter++;
                }
                else
                {
                    flag2 = 1;
                }
                if (counter == ToInitialize.Count)
                {
                    break;
                }
                else
                {
                    start = ToInitialize[counter];
                }
            }
            counter = 0;
            start = ToCalculate[0];
            while (true)
            {
                foreach (int[] x in bin)
                {
                    int flag = 0;
                    List<int> tmpLocation = new List<int>();
                    for (int i = 0; i < length; i++)
                    {
                        int help = x[i] + start[i];
                        if (help > sizes[i])
                        {
                            flag = 1;
                            break;
                        }
                        tmpLocation.Add(help);
                    }
                    if (flag == 0)
                    {
                        if (CountZeros(tmpLocation) == 0)
                        {
                            if (CalculateListContains(tmpLocation))
                            {
                                continue;
                            }
                            else
                            {
                                ToCalculate.Add(tmpLocation);
                            }
                        }
                    }
                }
                counter++;
                if (counter == ToCalculate.Count)
                {
                    break;
                }
                else
                {
                    start = ToCalculate[counter];
                }
            }
        }

        //gets value of element on selected location
        //this method should always be called after ContainsElement!
        public int GetValueByLocation(List<int> elementLocation)
        {
            int value = 0;
            int counter = 0;
            foreach (MatrixElement mE in Elements)
            {
                for (int i = 0; i < elementLocation.Count; i++)
                {
                    if (mE.Location[i] == elementLocation[i])
                    {
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                        break;
                    }
                }
                if (counter == elementLocation.Count)
                {
                    return mE.Value;
                }
            }
            //this should never happen!!
            return value;
        }

        //I expect that elementLocation that calls this method exists in matrix
        public List<int> GetPathByLocation(List<int> elementLocation)
        {
            List<int> path = new List<int>();
            int counter = 0;
            foreach (MatrixElement mE in Elements)
            {
                for (int i = 0; i < elementLocation.Count; i++)
                {
                    if (mE.Location[i] == elementLocation[i])
                    {
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                        break;
                    }
                }
                if (counter == elementLocation.Count)
                {
                    
                    //check for best solution!
                    int countDifferences = 0;
                    int bestDifferences = 0;
                    
                    foreach (List<int> p in mE.Paths)
                    {
                        countDifferences = 0;
                        for (int i = 0; i < p.Count; i++)
                        {
                            if (p[i] != elementLocation[i])
                            {
                                countDifferences++;
                            }
                        }
                        if (bestDifferences < countDifferences)
                        {
                            bestDifferences = countDifferences;
                            path = new List<int>(p);
                        }
                        
                    }
                    break;
                }
            }
            return path;
        }

        //gets neighbours for elements of outer hypercube
        public List<List<int>> GetAllNeighboursToInitialize(List<int> elementLocation, List<int> sizes)
        {
            List<List<int>> exit = new List<List<int>>();
            int length = sizes.Count;
            int number = Convert.ToInt32(Math.Pow(2, length) - 1);
            List<int[]> bin = new List<int[]>();
            for (int i = 1; i <= number; i++)
            {

                int[] z = GetIntBinary(length, i);
                bin.Add(z);
            }
            foreach (int[] x in bin)
            {
                int flag = 0;
                List<int> tmpLocation = new List<int>();
                for (int i = 0; i < length; i++)
                {
                    int help =elementLocation[i] - x[i];
                    if (help < 0)
                    {
                        flag = 1;
                        break;
                    }
                    tmpLocation.Add(help);
                }
                if (flag == 0)
                {
                    exit.Add(tmpLocation);
                }

            }
            return exit;
        }
    
        //calculates outer hypercube
        //1. check if matrix has elements that is needed to calculate
        //2. if it has, calculate, delete it from list and move on
        //3. if it doesn't have, go to another element of toInitialize (you will come back for what you leave behind)
        public void CalculateToInitialize(List<int> sizes)
        {
            int counter = 0;    //I'm on the beginning of toInitialize list

            while (true)
            {
                //pick up element with counter, check his neighbours (if you have them calculated)
                List<List<int>> neighbours = new List<List<int>>(GetAllNeighboursToInitialize(ToInitialize[counter], sizes));
                int countNeighbours = 0;
                foreach (List<int> neighbourLocation in neighbours)
                {
                    if (ContainsElement(neighbourLocation) == true)
                    {
                        countNeighbours++;
                    }
                }
                if (countNeighbours == neighbours.Count)
                {
                    List<List<int>> tmpPath= new List<List<int>>();
                    int max = 0;
                    int tmpCalc = 0;
                    int first = 1;
                    //calculate
                    //if they exist, get their values and calculateSumOfPairs
                    foreach (List<int> neighbourLocation in neighbours)
                    {
                        if (first == 1)
                        {
                            //it is first calculation, max =value
                            tmpCalc = GetValueByLocation(neighbourLocation) +
                                      CalculateSumOfPairs(neighbourLocation, ToInitialize[counter]);
                            tmpPath.Add(neighbourLocation);
                            max = tmpCalc;
                            first = 0;
                        }
                        else
                        {
                            //it isn't first calculation, compare max with value!!
                            tmpCalc = GetValueByLocation(neighbourLocation) +
                                      CalculateSumOfPairs(neighbourLocation, ToInitialize[counter]);
                            if (max < tmpCalc)
                            {
                                max = tmpCalc;
                                tmpPath.Clear();
                                tmpPath.Add(neighbourLocation);
                            }
                            else if (max == tmpCalc)
                            {
                                tmpPath.Add(neighbourLocation);
                            }
                        }
                    }
                    
                    //calculation finished
                    //add that element :)
                    MatrixElement tmpMatrixElement = new MatrixElement(ToInitialize[counter], max, tmpPath);
                    Elements.Add(tmpMatrixElement);
                    ToInitialize.RemoveAt(counter);
                    //don't enlarge counter, just check if it is in the limits of list
                    if (counter == ToInitialize.Count)
                    {
                        if (ToInitialize.Count == 0)
                        {
                            //initialization is finished
                            break;
                        }
                        else
                        {
                            counter = 0;
                        }
                    }
                }
                else
                {
                    //go to the next one
                    if (counter == ToInitialize.Count)
                    {
                        counter = 0;
                    }
                    else
                    {
                        counter++;
                    }
                }
            }
        }

        public void CalculateToCalculate(List<int> sizes)
        {
            int counter = 0;    //I'm on the beginning of toCalculate list
            while (true)
            {
                //pick up element with counter, check his neighbours (if you have them calculated)
                List<List<int>> neighbours = new List<List<int>>(GetAllNeighboursToInitialize(ToCalculate[counter], sizes));
                int countNeighbours = 0;
                foreach (List<int> neighbourLocation in neighbours)
                {
                    if (ContainsElement(neighbourLocation) == true)
                    {
                        countNeighbours++;
                    }
                }
                if (countNeighbours == neighbours.Count)
                {
                    List<List<int>> tmpPath = new List<List<int>>();
                    int max = 0;
                    int tmpCalc = 0;
                    int first = 1;
                    //calculate
                    //if they exist, get their values and calculateSumOfPairs
                    foreach (List<int> neighbourLocation in neighbours)
                    {
                        if (first == 1)
                        {
                            //it is first calculation, max =value
                            tmpCalc = GetValueByLocation(neighbourLocation) +
                                      CalculateSumOfPairs(neighbourLocation, ToCalculate[counter]);
                            tmpPath.Add(neighbourLocation);
                            max = tmpCalc;
                            first = 0;
                        }
                        else
                        {
                            //it isn't first calculation, compare max with value!!
                            tmpCalc = GetValueByLocation(neighbourLocation) +
                                      CalculateSumOfPairs(neighbourLocation, ToCalculate[counter]);
                            if (max < tmpCalc)
                            {
                                max = tmpCalc;
                                tmpPath.Clear();
                                tmpPath.Add(neighbourLocation);
                            }
                            else if (max == tmpCalc)
                            {
                                tmpPath.Add(neighbourLocation);
                            }
                        }
                    }
                    //calculation finished
                    //add that element :)
                    MatrixElement tmpMatrixElement = new MatrixElement(ToCalculate[counter], max, tmpPath);
                    Elements.Add(tmpMatrixElement);
                    ToCalculate.RemoveAt(counter);
                    //don't enlarge counter, just check if it is in the limits of list
                    if (counter == ToCalculate.Count)
                    {
                        if (ToCalculate.Count == 0)
                        {
                            //initialization is finished
                            break;
                        }
                        else
                        {
                            counter = 0;
                        }
                    }

                }
                else
                {
                    //go to the next one
                    if (counter == ToCalculate.Count)
                    {
                        counter = 0;
                    }
                    else
                    {
                        counter++;
                    }
                    
                }
            }

        }

        public int CalculateSumOfPairs(List<int> neighbour, List<int> elementLocation)
        {
            int sum = 0;
            List<char> help = new List<char>();
            for (int i = 0; i < neighbour.Count; i++)
            {
                if (elementLocation[i] == neighbour[i] + 1)
                {
                    //you take the element
                    //LOOK OUT!!!!!!!***********************
                    help.Add(Sequences[i].GetElementById(elementLocation[i]-1));    //because elementLocation will always in this case be bigger than 0
                }
                else
                {
                    //you take the gap
                    help.Add('-');
                }
            }
            for (int i = 0; i < help.Count - 1; i++)
            {
                for (int j = i + 1; j < help.Count; j++)
                {
                    /*if (help[i] == help[j])
                    {
                        sum = sum + SimilarityScore;
                    }
                    else if (help[i] == '-' || help[j] == '-')
                    {
                        sum = sum + GapScore;
                    }
                    else
                    {
                        sum = sum + NonSimilarityScore;
                    }*/
                    if (help[i] == '-' || help[j] == '-')
                    {
                        sum = sum + GapScore;
                    }
                    else if (help[i] == help[j])
                    {
                        sum = sum + SimilarityScore;
                    }
                    else
                    {
                        sum = sum + NonSimilarityScore;
                    }
                }
            }
            return sum;
        }

        public void CalculateAlignment(List<int> sizes)
        {
            int length = sizes.Count;
            //first get to the max field in matrix!
            List<int> maxField = new List<int>();
            
            foreach (int x in sizes)
            {
                maxField.Add(x);
            }
            
            //now I have maxField and I need to follow its path to field with all 0 locations!
            List<int> currentLocation = new List<int>(maxField);
            while (true)
            {
                int zeroCounter = 0;
                //when you get path -> count its zeros and see if it's first element of matrix!
                //get path!
                List<int> nextLocation = new List<int>(GetPathByLocation(currentLocation));
                //fill list for output!
                for (int i = 0; i < currentLocation.Count; i++)
                {
                    if (currentLocation[i] == nextLocation[i])
                    {
                        SequencesOutput[i].Elements.Add('-');
                    }
                    else
                    {
                        SequencesOutput[i].Elements.Add(Sequences[i].GetElementById(currentLocation[i]-1));
                    }
                }
                
                //refresh zeroCounter
                for (int i = 0; i < nextLocation.Count; i++)
                {
                    if (nextLocation[i] == 0)
                    {
                        zeroCounter++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (zeroCounter == length)
                {
                    //you are at the beginning -> the end :)
                    break;
                }
                //refresh currentLocation
                currentLocation = new List<int>(nextLocation);
            }
        }
    }
}
