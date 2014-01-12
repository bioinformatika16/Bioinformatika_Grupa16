package algorithm;

/* Dynamiy alignment algorithm */
public class AlgorithmDSA {

    private String dnaA = new String();
    private String dnaB = new String();

    private MatrixCell[][] matrix;

    public AlgorithmDSA(String dnaA, String dnaB) {
    	if (dnaA.length() >= dnaB.length()) {
    		this.dnaA = dnaA;
    		this.dnaB = dnaB;
    	} else {
    		this.dnaA = dnaB;
    		this.dnaB = dnaA;
    	}

		matrix = new MatrixCell[this.dnaA.length() + 1][this.dnaB.length() + 1];
    }

    public void startAlgorithm() {
		
		initMatrix();
		
		fillMatrix();
	
    }

    /* Get backtrace from matrix */
    public String getBacktrack(boolean result) {
    	int y = dnaA.length();
    	int x = dnaB.length();
    		
    	String string = new String();
    		
    	while ((y > 0) || (x > 0)) {
    		/* Get diagonal backtrace */
    	    if (matrix[y][x].getDiagonal()) {
	    		if (dnaA.charAt(y-1) == dnaB.charAt(x-1))
	    		    string = dnaB.charAt(x-1) + string;
	    		/* Second step */
	    		else if (result)
	    			string = dnaB.charAt(x-1) + string;
	    		else
	    		    string = '-' + string;
	    		x--;
	    		y--;
    	    }
    	    /* Get up backtrace */
    	    else if (matrix[y][x].getUp()) {
    	    	/* Second step */
    	    	if (result)
    	    		string = dnaA.charAt(y-1) + string;
    	    	else
    	    		string = '-' + string;
	    		y--;
	    	/* Get left backtrace */
    	    } else {
    	    	/* Second step */
    	    	if (result)
    	    		string = dnaA.charAt(x-1) + string;
    	    	else
    	    		string = '-' + string;
	    		x--;
    	    }
    	}
    	return string;
	}
    
    /* Init matrix with backtrace and values */
    private void initMatrix() {
    	for(int i = 0; i <= dnaA.length(); i++) {
    	    for(int j = 0; j <= dnaB.length(); j++) {
	    		if (i == 0) {
	    		    matrix[i][j] = new MatrixCell(j, false, true);
	    		}
	    		else if (j == 0) {
	    		    matrix[i][j] = new MatrixCell(i, true, false);
	    		}
	    		else {
	    		    matrix[i][j] = new MatrixCell(0, false, false);
	    		}
    	    }
    	}
    }

    /* Fill matrix with backtrace and value */
    private void fillMatrix() {
    	int diagonal;
    	int up;
    	int left;
    	for(int i = 1; i < dnaA.length() + 1; i++) {
    	    for(int j = 1; j < dnaB.length() + 1; j++) {
	    		char nucleotideA = dnaA.charAt(i-1);
	    		char nucleotideB = dnaB.charAt(j-1);
	    				
	    		diagonal = matrix[i-1][j-1].getValue();
	    		if (nucleotideA != nucleotideB) {
	    		    diagonal++;
	    		}
	    		up = matrix[i-1][j].getValue() + 1;
	    		left = matrix[i][j-1].getValue() + 1;
	    				
	    		setBacktrack(diagonal, up, left, i, j);
    	    }
    	}
    }

    /* Set backtrack */
    private void setBacktrack(int diagonale, int up, int left, int i, int j) {
    	int min = Math.min(diagonale, Math.min(up, left));
		
    	matrix[i][j].setValue(min);
    		
    	if (diagonale == min) {
    	    matrix[i][j].setDiagonal();
    	}
    	if (up == min) {
    	    matrix[i][j].setUp();
    	}
    	if (left == min) {
    	    matrix[i][j].setLeft();
    	}
    }
}
