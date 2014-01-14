import java.io.FileWriter;
import java.io.IOException;


public class SpremnikDNA {
	public static void ispisLanaca(String[] lanci){
		for (int i = 0;i < lanci.length; i++)
			System.out.println(lanci[i]);
	}
	
	public static void zapisiUDatoteku(String datoteka, String[] lanci){
		try {
			FileWriter fw = new FileWriter(datoteka);
			for (int i = 0; i < lanci.length; i++){
				fw.write(lanci[i]);
				fw.write('\n');
				fw.flush();
			}
		
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		
		
	}

	//argumenti: DNA.txt out.txt
	public static void main(String[] args) {

		
		if (args.length < 1 || args.length > 2){
			System.out.println("argumenti: ulaznaDatoteka <izlaznaDatoteka>");
		}
		
			
		//Čitanje datoteke
		CitacGenoma cg = new CitacGenoma();
		
		String[] DNAlanci = cg.getDNA(args[0]);
		String[] rjesenja;
		Genomi genomi = new Genomi(DNAlanci);
		
		long start = System.currentTimeMillis();
		//poravnanje
		
		rjesenja = genomi.dohvatiPoravnateGenome();
			
		
		long stop = System.currentTimeMillis();
		System.out. println("Vrijeme: " + ((stop-start)/1000.0) + "s");
		
		//zapis
		if (args.length == 2)
			zapisiUDatoteku(args[1], rjesenja);

	}
}

