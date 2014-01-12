package file;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class Reader {
	
    private String fileName;

    public Reader(String fileName) {
    	this.fileName = fileName;
    }

    public List<String> readFromFile() throws IOException {
		BufferedReader bufferedReader = new BufferedReader(new FileReader(fileName));
		try {
		    List<String> listString = new ArrayList<String>();
		    String line = bufferedReader.readLine();
		    
		    while (line != null) {
			listString.add(line);
			line = bufferedReader.readLine();
		    }
		    
		    return listString;
		    
		} finally {
		    bufferedReader.close();
		}
    }
}
