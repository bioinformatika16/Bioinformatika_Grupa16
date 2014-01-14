
public class Genomi {
	private String prvi, drugi, treci, prviPoravnati, drugiPoravnati, treciPoravnati;

	private Celija[][][] matrica;
	private int matricaRow, matricaCol, matricaDep;
	
	public static int CIJENA_PRAZNINE = 1;
	public static int CIJENA_ZAMIJENE = 1;
	
	
	public static final int Ixx = 0;
	public static final int xJx = 1;
	public static final int xxK = 2;
	public static final int IJx = 3;
	public static final int IxK = 4;
	public static final int xJK = 5;
	public static final int IJK = 6;
	
	public Genomi(String prvi, String drugi, String treci){
		this.prvi = prvi;
		this.drugi = drugi;
		this.treci = treci;
	}
	
	public Genomi(String[] genomi){
		this(genomi[0],genomi[1],genomi[2]);
	}
	
	public String[] dohvatiPoravnateGenome(){
		inicijalizirajMatricu();
		popuniTablicu();
		poravnajLance();
		String[] temp = {prviPoravnati, drugiPoravnati, treciPoravnati};
		return temp;
	}
	
	private int usporediTriZnaka(char a, char b, char c){
		return match(a,b)+match(a,c)+match(b,c);
	}
	
	private void inicijalizirajMatricu(){
		matricaRow = prvi.length()+1;
		matricaCol = drugi.length()+1;
		matricaDep = treci.length()+1;
		matrica = new Celija[matricaRow][matricaCol][matricaDep];
		
		
		
		//inicijalizacija matrice
		for (int i = 0; i <= prvi.length(); i++)
			for (int j = 0; j <= drugi.length(); j++)
				for (int k = 0; k <= treci.length(); k++)
					matrica[i][j][k] = new Celija();

		matrica[0][0][0].opt = -1;
		
		//i + j ravnina
		for (int i = 1; i <= prvi.length(); i++){
			for (int j = 0; j <= drugi.length(); j++){
				if (j>i)
					matrica[i][j][0].cijena = j*CIJENA_PRAZNINE;
				else
					matrica[i][j][0].cijena = i*CIJENA_PRAZNINE;
				if (j == 0)
					matrica[i][j][0].opt = Ixx;
				else
					matrica[i][j][0].opt = IJx;
				
			}
		}
		
		//j + k ravnina
		for (int j = 1; j <= drugi.length(); j++){
			for (int k = 0; k <= treci.length(); k++){
				if (k>j)
					matrica[0][j][k].cijena = k*CIJENA_PRAZNINE;
				else
					matrica[0][j][k].cijena = j*CIJENA_PRAZNINE;
				if (k == 0)
					matrica[0][j][k].opt = xJx;
				else
					matrica[0][j][k].opt = xJK;
				
			}
		}
		
		//Dubina
		for (int k = 1; k <= treci.length(); k++){
			for (int i = 0; i <= prvi.length(); i++){
				if (i>k)
					matrica[i][0][k].cijena = i*CIJENA_PRAZNINE;
				else
					matrica[i][0][k].cijena = k*CIJENA_PRAZNINE;
				if (i == 0)
					matrica[i][0][k].opt = xxK;
				else
					matrica[i][0][k].opt = IxK;
				
			}
		}
		
	}
	
	private void popuniTablicu(){
		int[] opt = new int[7];
		
		//popuni tablicu
		for (int i = 1; i < matricaRow; i++){
			for (int j = 1; j < matricaCol; j++){
				for (int k = 1; k < matricaDep; k++){
					opt[Ixx] = matrica[i-1][j][k].cijena + usporediTriZnaka(prvi.charAt(i-1), '-', '-');
					opt[xJx] = matrica[i][j-1][k].cijena + usporediTriZnaka('-', drugi.charAt(j-1), '-');
					opt[xxK] = matrica[i][j][k-1].cijena + usporediTriZnaka('-', '-', treci.charAt(k-1));
					opt[IJx] = matrica[i-1][j-1][k].cijena + usporediTriZnaka(prvi.charAt(i-1), drugi.charAt(j-1), '-');
					opt[IxK] = matrica[i-1][j][k-1].cijena + usporediTriZnaka(prvi.charAt(i-1), '-', treci.charAt(k-1));
					opt[xJK] = matrica[i][j-1][k-1].cijena + usporediTriZnaka('-', drugi.charAt(j-1), treci.charAt(k-1));
					opt[IJK] = matrica[i-1][j-1][k-1].cijena + usporediTriZnaka(prvi.charAt(i-1), drugi.charAt(j-1), treci.charAt(k-1));

					int min = opt[6];
					int minS = 6;

					//dobivanje najmanje cijene i koeficijenta
					for (int s = 0; s < 6; s++){
						if (min > opt[s]){
							min = opt[s];
							minS = s;
						}
					}
					matrica[i][j][k].cijena = min;
					matrica[i][j][k].opt = minS;
				}
			}
		}
		//System.out.println(matrica[matricaRow-1][matricaCol-1].cijena);
		
		
	}

	private void poravnajLance(){
		StringBuilder sb1, sb2, sb3;
		sb1 = new StringBuilder();
		sb2 = new StringBuilder();
		sb3 = new StringBuilder();
		
		int i,j,k;
		i = matricaRow - 1;
		j = matricaCol - 1;
		k = matricaDep - 1;
		
		//zadnji element matrice
		Celija tempCelija = matrica[i][j][k];
		
		while(tempCelija.opt != -1){
			switch (tempCelija.opt) {
			case Ixx:
				sb1.insert(0, prvi.charAt(i-1));
				sb2.insert(0, '-');
				sb3.insert(0, '-');
				--i;
				
				break;
			case xJx:
				sb1.insert(0, '-');
				sb2.insert(0, drugi.charAt(j-1));
				sb3.insert(0, '-');
				--j;
			
				break;
			case xxK:
				sb1.insert(0, '-');
				sb2.insert(0, '-');
				sb3.insert(0, treci.charAt(k-1));
				--k;
				
				break;
			case IJx:
				sb1.insert(0, prvi.charAt(i-1));
				sb2.insert(0, drugi.charAt(j-1));
				sb3.insert(0, '-');
				--i;
				--j;
				
				break;
			case IxK:
				sb1.insert(0, prvi.charAt(i-1));
				sb2.insert(0, '-');
				sb3.insert(0, treci.charAt(k-1));
				--i;
				--k;
				
				break;
			case xJK:
				sb1.insert(0, '-');
				sb2.insert(0, drugi.charAt(j-1));
				sb3.insert(0, treci.charAt(k-1));
				--j;
				--k;
				
				break;
			case IJK:
				sb1.insert(0, prvi.charAt(i-1));
				sb2.insert(0, drugi.charAt(j-1));
				sb3.insert(0, treci.charAt(k-1));
				--i;
				--j;
				--k;
				
				break;
			default:
				System.out.println("tempCelija.opt daje cudnu vrijednost->Genomi.java->~130 red");
				break;
			}
			tempCelija = matrica[i][j][k];
		}
		
		prviPoravnati = sb1.toString();
		drugiPoravnati = sb2.toString();
		treciPoravnati = sb3.toString();
	}
	
	
	private int match(char a, char b){
		
		if (a == '-' && b == '-')
			return 2*CIJENA_PRAZNINE;
		else if (a == '-' || b == '-')
			return CIJENA_PRAZNINE;
		else if (a == b)
			return 0;
		
		else return CIJENA_ZAMIJENE;
	}
	
	
}
