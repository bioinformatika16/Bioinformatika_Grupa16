using System;
using System.Collections.Generic;

namespace MSA
{
    class Program
    {
        static void Main(string[] args)
        {
            IOOperations io = new IOOperations();
            List<object> listOfData = io.getBioData(args);
            if (listOfData == null || listOfData.Count == 1)
            {
                Console.WriteLine("Incorrect bio data - there has to be 2 or more sequences to perform MSA!\nPlease try again");
                Console.ReadKey();
                System.Environment.Exit(-1);
            }

            List<string> tempList = new List<string>();
            foreach (var obj in listOfData)
            {
                tempList.Add(obj.ToString());
            }
            DistanceMatrix distance = new DistanceMatrix(tempList);
            distance.generateDistanceMatrix();

            double[,] matrix = distance.DistanceMatrix1;

            GuideTreeAlgorithm guide = new GuideTreeAlgorithm(matrix, listOfData);
            guide.generateTree();
            Node root = guide.NodeRoot;

            Alignment alignment = new Alignment(root);
            alignment.alignSequences();


            List<object> resultList = alignment.GetListOfAlignedObjects();
            io.saveTxtFile(resultList);

            Console.ReadKey();


        }
    }
}
