package algorithm;

/* Cell in matrix for Dynamic alignment */
public class MatrixCell {
	
	public MatrixCell(int value, boolean up, boolean left) {
		this.value = value;
		this.up = up;
		this.left = left;
	}
	
	/* Value */
	private int value = 0;
	
	public int getValue() {
		return value;
	}
	
	public void setValue(int value) {
		this.value = value;
	}

	/* Diagonal */
    private boolean diagonal = false;
    
    public boolean getDiagonal() {
    	return diagonal;
    }
    
    public void setDiagonal() {
    	this.diagonal = true;
    }
    
    /* Up */
    private boolean up = false;
    
    public boolean getUp() {
    	return up;
    }
    
    public void setUp() {
    	this.up = true;
    }
    
    /* Left */
    private boolean left = false;

    public boolean getLeft() {
    	return left;
    }

    public void setLeft() {
    	this.left = true;
    }
}
