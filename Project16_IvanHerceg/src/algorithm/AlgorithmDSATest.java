package algorithm;

import static org.junit.Assert.*;

import org.junit.Test;

public class AlgorithmDSATest {
	
	AlgorithmDSA dsa;
	
	String dna1;
	String dna2;

	public AlgorithmDSATest() {
		dna1 = "AAAA";
		dna2 = "AATAA";
	}

	@Test
	public void testAlgorithmDSA() {
		dsa = new AlgorithmDSA(dna1, dna2);
		
		assertNotNull("Construcotr", dsa);
	}

	@Test
	public void testStartAlgorithm() {
		dsa = new AlgorithmDSA(dna1, dna2);
		
		dsa.startAlgorithm();
		
		assertNotNull("Start algorithm", dsa);
	}

	@Test
	public void testGetBacktrack() {
		dsa = new AlgorithmDSA(dna1, dna2);
		
		dsa.startAlgorithm();
		assertEquals("Test startAlgorithm", "AA-AA", dsa.getBacktrack(false));
	}

}
