package algorithm;

import static org.junit.Assert.*;

import java.util.ArrayList;
import java.util.List;

import org.junit.Test;

public class AlgorithmMSATest {
	
	AlgorithmMSA msa;
	
	List<String> listDNA = new ArrayList<String>();

	public AlgorithmMSATest() {
		String dna1 = "AAAA";
		String dna2 = "AATAA";
		
		listDNA.add(dna1);
		listDNA.add(dna2);
	}

	@Test
	public void testAlgorithmMSA() {
		msa = new AlgorithmMSA(listDNA);
		
		assertNotNull("Construcotr", msa);
	}

	@Test
	public void testCheckNucleotides() {
		msa = new AlgorithmMSA(listDNA);
		
		assertEquals("Check nucleotides", true, msa.checkNucleotides());
	}

	@Test
	public void testStartAlgorithm() {
		msa = new AlgorithmMSA(listDNA);
		
		List<String> listDNA_TEMP = msa.startAlgorithm();
		assertEquals("Start algorithm", "AA-AA", listDNA_TEMP.get(0));
		assertEquals("Start algorithm", "AATAA", listDNA_TEMP.get(1));
	}

}
