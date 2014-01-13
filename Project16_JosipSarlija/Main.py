from MSA import MSA
from InputOutput import InputData, OutputData
from Hypercube import Hypercube
import time
import resource
import sys

def memory_usage():
    return resource.getrusage(resource.RUSAGE_SELF).ru_maxrss /float(1000)

def main():
    if len(sys.argv) != 2:
        print ("Usage: python Main.py <input_file_location>")
        return

    print ("INFO: Starting Multiple Sequence Alignment.")
    performance = dict()
    start_time = time.time()
    #input_data = InputData("./input.txt")
    input_data = InputData(sys.argv[1])
    end_time = time.time()
    performance["input"] = (end_time - start_time, memory_usage())

    start_time = time.time()
    hypercube = Hypercube(input_data.sequences)
    end_time = time.time()
    performance["hcube"] = (end_time - start_time, memory_usage())


    msa = MSA(hypercube)
    start_time = time.time()
    msa.align()
    end_time = time.time()
    performance["MSA"] = (end_time - start_time, memory_usage())

    for output in msa.output:
        print (output)

    output_data = OutputData("./output.txt", msa.output)

    print ("INFO: Performance: (Execution time [s], Memory usage [MB])")
    for p in performance:
        print ("\t{0}: \t{1} s, \t{2} MB".format(p, performance[p][0], performance[p][1]))
    print ("INFO: Done.")
    return
    
main()
