import java.io.FileNotFoundException;
import java.io.FileReader;
import java.util.NoSuchElementException;
import java.util.Scanner;


public class CitacGenoma {
	
	public String[] getDNA(String file){
		
		Scanner sc = null;
		FileReader fr;
		String DNAlanci[] = null;
		
		//broj DNA lanaca
		int rows = 0;
		
		try{
			
			//brojanje redaka i provjera ulaza
			try{
				fr= new FileReader(file);
				sc = new Scanner(fr);
				String ulaz;
				while (true){
					ulaz = sc.nextLine();
					//provjera znakova u ulaznoj datoteci
					for (int i = 0; i < ulaz.length(); i++){
						char temp = ulaz.charAt(i);
						if (temp != 'A' && temp != 'C' && temp != 'G' && 
								temp != 'T'){
							System.out.println("Datoteka sadrži nedozvoljeni znak :\"" + temp + "\"");
							System.exit(-1);
						}	
					}
					//brojanje redaka
					++rows;
				}
			}
			catch(NoSuchElementException e){}

			//spremnik za DNA lance
			DNAlanci = new String[rows];
			
			//iznova krenuti u fileu
			fr= new FileReader(file);
			sc = new Scanner(fr);
				
			for (int i = 0; i < rows; i++){
				DNAlanci[i] = sc.nextLine();
			}


		}catch (FileNotFoundException e){
			System.out.println("Datoteka >>" + file + "<< nije pronađena");
			System.exit(-1);
		}

		return DNAlanci;
	}
}
