package main;

import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.List;

import algorithm.AlgorithmMSA;
import file.Reader;

public class Main {
	final static String ERROR_LENGTH = "args.length error";
	final static String ERROR_READER = "reader error";
	final static String ERROR_NUCLEOTIDE = "nucleotide error";
	final static String ERROR_WRITER = "writer error";
	final static String ERROR_EMPTY = "empty error";
	
	public static void main(String[] args) {
		
		/* Check arguments */
		if (args.length != 1) {
			System.out.println(ERROR_LENGTH);
			System.exit(0);
		}
		
		/* Get strings from file */
		List<String> listDNA;
		listDNA = getStringsFromFile(args[0]);
		
		/* Check empty file */
		if (listDNA.size() == 0) {
			System.out.println(ERROR_EMPTY);
			System.exit(0);
		}
		
		/* Init algorithm */
		AlgorithmMSA msa = new AlgorithmMSA(listDNA);
		
		
		/* Check nucleotides */
		if (!msa.checkNucleotides()) {
			System.out.println(ERROR_NUCLEOTIDE);
			System.exit(0);
		}
		
		/* Start time */
		long startTime = System.currentTimeMillis();
		
		/* Start algorithm */
		listDNA = msa.startAlgorithm();
		
		/* End time */
		long endTime = System.currentTimeMillis();
		
		/* Memory */
		Runtime runtime = Runtime.getRuntime();
		long allocatedMemory = runtime.totalMemory();
		long freeMemory = runtime.freeMemory();
		
		/* Console */
		for(String string : listDNA) {
			for(char nucleotide : string.toCharArray())
				System.out.print(nucleotide);
			System.out.println();
		}
		long resultTime = endTime - startTime;
		System.out.format("Time:\t%,d ms\n", resultTime);
		System.out.format("Memory:\t%,d kB", (allocatedMemory - freeMemory) / 1024);
		
		/* TXT */
		writeInTXT(listDNA);

	}
	
	private static List<String> getStringsFromFile(String fileName) {
		Reader reader = new Reader(fileName);
			
		List<String> listString;
		try {
		    listString = reader.readFromFile();
		    return listString;
		} catch (IOException e) {
		    System.out.println(ERROR_READER);
		    System.exit(0);
		}
			
		return null;
	}
	
	private static void writeInTXT(List<String> listDNA) {
		PrintWriter out = null;
		try {
			out = new PrintWriter("out.txt");
		} catch (FileNotFoundException e) {
		    System.out.println(ERROR_WRITER);
		    System.exit(0);
		}
		for(String string : listDNA)
			out.println(string);
		out.close();
	}

}
