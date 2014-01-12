package algorithm;

import java.util.ArrayList;
import java.util.List;

/* Main algortihm - MSA */
public class AlgorithmMSA {

    private List<String> _listDNA = new ArrayList<String>();

    private boolean nucleotideCheck = true;
    
    AlgorithmDSA dsa;
    
    public AlgorithmMSA(List<String> listString) {
		for(String dna : listString) {
			String dnaUpperCase = dna.toUpperCase();
		    for(char nucleotide : dnaUpperCase.toCharArray())
		    	if (!checkNucleotide(nucleotide))
		    		nucleotideCheck = false;
		    _listDNA.add(dna);
		}
    }
    
    /* Check if nucleotides are OK */
    public boolean checkNucleotides() {
    	return nucleotideCheck;
    }
    
    /* Main algortihm */
     public List<String> startAlgorithm() {
    	 /* Step 1 */
       	 String dna = _listDNA.get(0);
       	 for(int i = 1; i < _listDNA.size(); i++) {
       		 dsa = new AlgorithmDSA(dna, _listDNA.get(i));
       		 dsa.startAlgorithm();
       		 dna = dsa.getBacktrack(false);
       	 }

       	 /* Step 2 */
       	 List<String> listDNA = new ArrayList<String>();
       	 for(int i = 0; i < _listDNA.size(); i++) {
       		dsa = new AlgorithmDSA(dna, _listDNA.get(i));
       		dsa.startAlgorithm();
       		listDNA.add(dsa.getBacktrack(true));
       	 }
       	 
       	 return listDNA;
     }
    
    private boolean checkNucleotide(char nucleotide) {
    	if ((nucleotide == 'A') || (nucleotide == 'G') || (nucleotide == 'C') || (nucleotide == 'T'))
    		return true;
    	else
    		return false;
    }
    
    

}
